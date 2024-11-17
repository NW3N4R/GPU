using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
                case 400:
                    return View("400");
                case 403:
                    return View("403");
                default:
                    return View("Error");
            }
        }
    }
}
