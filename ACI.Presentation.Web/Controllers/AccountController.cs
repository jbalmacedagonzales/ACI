using ACI.Application.Identity.DTOs;
using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using ACI.Infrastructure.CrossCutting.Security;
using ACI.Presentation.Web.Helpers;
using ACI.Presentation.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACI.Presentation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserDTO> _userManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<UserDTO> userManager, IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._emailSender = emailSender;
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserModel model, string returnUrl = null)
        {

            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, Security.Decrypt(model.PasswordHash)))
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    await HttpContext.SignInAsync
                        (CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberPassword,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                        });
                    return RedirectToLocal(returnUrl);
                }
                else
                    Handler.Error("The account email is not confirmed", this);
            }
            else
                Handler.Error("The email or password is not valid", this);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("NotFoundPage", "Error");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new UserDTO { Id = Guid.NewGuid(), FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, BirthDate = model.BirthDate };

                var activationLink = Url.Action("ConfirmEmail", "Account", new
                {
                    token = Security.Encrypt(await _userManager.GenerateEmailConfirmationTokenAsync(user)),
                    email = Security.Encrypt(model.Email)
                }, Request.Scheme);

                var create = await _userManager.CreateAsync(user, Security.Decrypt(model.PasswordHash));
                if (create.Succeeded && await _emailSender.SendAsync(model.Email, activationLink))
                    ViewBag.Succeeded = 200;
                else
                {
                    foreach (var error in create.Errors)
                    {
                        Handler.Error(error.Description, this);
                    }
                }
            }
            else
                Handler.Error("This email already exists", this);

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(Security.Decrypt(email));
            if (user != null && (await _userManager.ConfirmEmailAsync(user, Security.Decrypt(token))).Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("NotFoundPage", "Error");
        }
    }
}
