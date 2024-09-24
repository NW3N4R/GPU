using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;

namespace GPU.Controllers
{
    public class Invoices : Controller
    {
        IEnumerable<InvoiceInfo> _Invo = Helper_Invoice._Invoices;
        IEnumerable<InvoiceInfo> ar_Invo = Helper_Invoice.ar_Invoices;
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.SID == id);

                foreach (var item in StudentInvoices)
                {
                    item.isFirst = false;
                }
                StudentInvoices.First().isFirst = true;
                var student = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo._department.First(x => x.SID == id) as StudentDepartmentInfo;
                InvoiceInfo Invoices = new InvoiceInfo();
                Invoices.Name = student.Name;
                Invoices.SID = student.Id;
                return View((studentDepartmentInfo, StudentInvoices, Invoices));
            }
            catch
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
        }
        public IActionResult Search([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel, int? id)
        {
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.InvoiceDate == InvoiceModel.InvoiceDate && x.SID == id);
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo._department.First(x => x.SID == id) as StudentDepartmentInfo;
                if (StudentInvoices.Any())
                {
                    var student = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
                    InvoiceModel.Name = student.Name;
                    InvoiceModel.SID = student.Id;
                    StudentInvoices.First().isFirst = true;
                    var viewModel = ((studentDepartmentInfo, StudentInvoices, InvoiceModel));
                    return View("Index", viewModel);
                }
                else
                {
                    return RedirectToAction("Index", new { id = id });
                }
            }
            catch
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.NewInvoice(InvoiceModel);
            }
            return RedirectToAction("Index", new { id = InvoiceModel.SID });

        }

        [HttpPost]
        public async Task<IActionResult> EditInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.UpdateInvoice(InvoiceModel);
            }
            return RedirectToAction("Index", new { id = InvoiceModel.SID });
        }

        // ------------------------- archive invoice --------------------------
        public IActionResult ArchiveInvoice(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = ar_Invo.Where(x => x.SID == id);

                foreach (var item in StudentInvoices)
                {
                    item.isFirst = false;
                }
                StudentInvoices.First().isFirst = true;
                var student = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id);
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.First(x => x.SID == id) as StudentDepartmentInfo;
                InvoiceInfo Invoices = new InvoiceInfo();
                Invoices.Name = student.Name;
                Invoices.SID = student.Id;
                return View((studentDepartmentInfo, StudentInvoices, Invoices));
            }
            catch
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
        }
        public IActionResult ARSearch([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel, int? id)
        {
            try
            {
                IEnumerable<InvoiceInfo> StudentInvoices = ar_Invo.Where(x => x.InvoiceDate == InvoiceModel.InvoiceDate && x.SID == id);
                StudentDepartmentInfo studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.First(x => x.SID == id) as StudentDepartmentInfo;
                if (StudentInvoices.Any())
                {
                    var student = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id);
                    InvoiceModel.Name = student.Name;
                    InvoiceModel.SID = student.Id;
                    StudentInvoices.First().isFirst = true;
                    var viewModel = ((studentDepartmentInfo, StudentInvoices, InvoiceModel));
                    return View("ArchiveInvoice", viewModel);
                }
                else
                {
                    return RedirectToAction("ArchiveInvoice", new { id = id });
                }
            }
            catch
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArNewInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.arNewInvoice(InvoiceModel);
            }
            return RedirectToAction("ArchiveInvoice", new { id = InvoiceModel.SID });

        }
        [HttpPost]
        public async Task<IActionResult> arEditInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.arUpdateInvoice(InvoiceModel);
            }
            return RedirectToAction("ArchiveInvoice", new { id = InvoiceModel.SID });
        }


    }
}
