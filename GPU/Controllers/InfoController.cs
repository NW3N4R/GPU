using Microsoft.AspNetCore.Mvc;
using GPU.Helpers;
using GPU.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
namespace GPU.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
