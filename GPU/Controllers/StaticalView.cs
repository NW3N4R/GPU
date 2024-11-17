using Microsoft.AspNetCore.Mvc;

using GPU.Helpers;
using GPU.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using GPU.Services;
using System.Data;
namespace GPU.Controllers
{
    [Authorize]
    public class StaticalView : Controller
    {
        public IActionResult Index()
        {
            return View((Helper_StudentTable.GetStatical(), new StaticalTableModel(),
                props: WebPropsServices.MergedProps, access: ManagerServices._auths));
        }

        [HttpGet]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StaticalTableModel tbl, bool DoPrint, bool doSearch = true)
        {
            Debug.WriteLine(tbl.Department);
            if (doSearch)
            {
                var viewModel = ((Helper_StudentTable.StaticalSearch(tbl), tbl, props: WebPropsServices.MergedProps, access: ManagerServices._auths));
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
                return View("Index", (Helper_StudentTable.GetStatical(), new StaticalTableModel(), props: WebPropsServices.MergedProps, access: ManagerServices._auths));

            }
        }
    }
}
