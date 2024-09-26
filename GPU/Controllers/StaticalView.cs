using Microsoft.AspNetCore.Mvc;

using GPU.Helpers;
using GPU.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
using Microsoft.Identity.Client;
namespace GPU.Controllers
{
    public class StaticalView : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View((Helper_StudentTable.GetStatical(), new StaticalTableModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StaticalTableModel tbl, bool DoPrint, bool doSearch = true)
        {
            if (doSearch)
            {
                var viewModel = ((Helper_StudentTable.StaticalSearch(tbl), tbl));
                if (DoPrint)
                {
                    var fileContent = await ToExcelPrint.DoPrint((Helper_StudentTable.StaticalSearch(tbl)), 2);
                    string fileName = $"لیستی خوێندکاران {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return File(fileContent, contentType, fileName);
                }
                return View("Index", viewModel);
            }
            else
            {
                return View("Index", (Helper_StudentTable.GetStatical(), new StaticalTableModel()));

            }
        }
    }
}
