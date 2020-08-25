using ACI.Application.Identity.DTOs;
using ACI.Application.Identity.Services;
using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using ACI.Infrastructure.CrossCutting.Identity.Services;
using ACI.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ACI.Presentation.Web
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "ACIcookie";
                    options.LoginPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.Zero;
                });
            services.AddControllersWithViews();
            services.AddIdentityCore<UserDTO>().AddDefaultTokenProviders();
            services.AddScoped<IUserIdentity, UserIdentity>();
            services.AddScoped<IUserStore<UserDTO>, UserIdentityAppService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromDays(1));
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/NotFoundPage");
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
                (
                    name: "initial", 
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
