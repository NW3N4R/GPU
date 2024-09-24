using Microsoft.AspNetCore.Mvc;

using GPU.Helpers;
using GPU.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
namespace GPU.Controllers
{
    public class StaticalView : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = StaticalHelper._Statical;
            return View((model,new StaticalTableModel()));
        }
    }
}
