using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class Certificates : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
