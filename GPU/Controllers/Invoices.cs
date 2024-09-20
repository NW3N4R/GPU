using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GPU.Controllers
{
    public class Invoices : Controller
    {
        IEnumerable<InvoiceInfo> _Invo = Helper_Invoice._Invoices;
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.SID == id);

                StudentInvoices.First().isFirst = true;
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo._department.First(x => x.SID == id) as StudentDepartmentInfo;
                var student = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
                InvoiceInfo Invoices = new InvoiceInfo();
                Invoices.Name = student.Name;
                return View((studentDepartmentInfo, StudentInvoices, Invoices));
            }
            catch
            {

                return View("~/Views/Students/NotFound404.cshtml");
            }
        }
        public IActionResult Search([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel,int? id)
        {
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.InvoiceDate == InvoiceModel.InvoiceDate && x.SID == id);
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo._department.First(x => x.SID == id) as StudentDepartmentInfo;
                StudentInvoices.First().isFirst = true;
                var student = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
                InvoiceModel.Name = student.Name;
                var viewModel = ((studentDepartmentInfo, StudentInvoices, InvoiceModel));
                return View("Index", viewModel);
            }
            catch
            {
                return View("~/Views/Students/NotFound404.cshtml");

            }
        }
    }
}
