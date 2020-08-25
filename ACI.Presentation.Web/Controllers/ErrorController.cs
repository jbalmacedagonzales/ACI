using Microsoft.AspNetCore.Mvc;

namespace ACI.Presentation.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }

    }
}
