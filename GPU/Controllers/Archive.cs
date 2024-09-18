using GPU.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GPU.Controllers
{
    public class Archive : Controller
    {
      

        public async Task<IActionResult>  Index()
        {
            return View(await Helper_StudentTable.GetStudent("Ar_GetStudents"));
        }
    }
}
