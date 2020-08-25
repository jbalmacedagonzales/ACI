using Microsoft.AspNetCore.Mvc;

namespace ACI.Presentation.Web.Helpers
{
    public static class Handler
    {
        public static void Error(string errorMessage, ControllerBase @base)
        {
            @base.ModelState.AddModelError(string.Empty, errorMessage);
        }
    }
}
