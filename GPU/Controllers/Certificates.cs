using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    [Authorize]
    public class Certificates : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
