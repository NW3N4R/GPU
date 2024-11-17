using GPU.Helpers;
using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPU.Controllers
{
    [Authorize]
    public class Home : Controller
    {
        public IActionResult Index()
        {
            #region MyRegion
            List<HomeModel> list = new List<HomeModel>();
            foreach (var item in Helper_StudentTable.GetStatical().Select(x => x.Department).Distinct().ToList())
            {
                var deps = new HomeModel
                {
                    Title = string.IsNullOrWhiteSpace(item) ? "-1" : item,
                    Filter = "Dep"
                };
                list.Add(deps);
            }
            foreach (var item in list.Where(x => x.Filter == "Dep").ToList())
            {
                var depsCount = Helper_StudentTable.combinedStudents.Count(x => x.Department == item.Title);
                item.No = depsCount;
            }
            foreach (var item in Helper_StudentTable.GetStatical().Select(x => x.Province).Distinct().ToList())
            {
                var deps = new HomeModel
                {
                    Title = string.IsNullOrWhiteSpace(item) ? "-1" : item,
                    Filter = "Province"
                };
                list.Add(deps);
            }
            foreach (var item in list.Where(x => x.Filter == "Province").ToList())
            {
                var depsCount = Helper_StudentTable.combinedStudents.Count(x => x.Province == item.Title);
                item.No = depsCount;
            }
            foreach (var item in Helper_StudentTable.GetStatical().Select(x => x.AcceptanceType).Distinct().ToList())
            {
                var deps = new HomeModel
                {
                    Title = string.IsNullOrWhiteSpace(item) ? "-1" : item,
                    Filter = "Acceptance"
                };
                list.Add(deps);
            }
            foreach (var item in list.Where(x => x.Filter == "Acceptance").ToList())
            {
                var depsCount = Helper_StudentTable.combinedStudents.Count(x => x.AcceptanceType == item.Title);
                item.No = depsCount;
            }
            var Male = new HomeModel
            {
                Title = "خوێندکاری نێر",
                No = Helper_StudentTable.combinedStudents.Count(x => x.Sex.Contains("نێ"))
            };
            list.Add(Male);
            var Female = new HomeModel
            {
                Title = "خوێندکاری مێ",
                No = Helper_StudentTable.combinedStudents.Count(x => x.Sex.Contains("مێ"))
            };
            list.Add(Female);
            var Stu = new HomeModel
            {
                Title = "خوێندکار لە خوێندندایە",
                No = StudentServices._Student.Count()
            };
            list.Add(Stu);
            var Arch = new HomeModel
            {
                Title = "خوێندکارانی ساڵانی پێشوو",
                No = ArchiveService.ar_Student.Count()
            };
            list.Add(Arch);

            #endregion
            decimal RowCount = Arch.No + Stu.No;
            return View((list, RowCount));
        }
    }
}
