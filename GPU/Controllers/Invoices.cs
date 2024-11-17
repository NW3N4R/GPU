using GPU.Helpers;
using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;

namespace GPU.Controllers
{

    [Authorize]
    public class Invoices : Controller
    {
        IEnumerable<InvoiceInfo> _Invo = StudentServices._Invoices;
        IEnumerable<InvoiceInfo> ar_Invo = ArchiveService.ar_Invoices;


        [Authorize(Policy = "RequireStuList")]
        public IActionResult Index(int? id)
        {

            IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.SID == id);

            foreach (var item in StudentInvoices)
            {
                item.isFirst = false;
            }

            var student = StudentServices._Student.FirstOrDefault(x => x.Id == id);
            StudentDepartmentInfo studentDepartmentInfo = StudentServices._department.First(x => x.SID == id) as StudentDepartmentInfo;
            if (studentDepartmentInfo.AcceptanceType.Contains("زانکۆ"))
            {
                return BadRequest();
            }
            InvoiceInfo Invoices = new InvoiceInfo();
            Invoices.Name = student.Name;
            Invoices.SID = student.Id;
            return View((studentDepartmentInfo, StudentInvoices, Invoices));

        }

        [Authorize(Policy = "RequireStuList")]
        public IActionResult Search([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel, int? id)
        {

            IEnumerable<InvoiceInfo> StudentInvoices = _Invo.Where(x => x.InvoiceDate == InvoiceModel.InvoiceDate && x.SID == id);
            StudentDepartmentInfo studentDepartmentInfo = StudentServices._department.First(x => x.SID == id) as StudentDepartmentInfo;
            if (StudentInvoices.Any())
            {
                var student = StudentServices._Student.FirstOrDefault(x => x.Id == id);
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

        [Authorize(Policy = "RequireStuList")]
        [HttpPost]
        public async Task<IActionResult> NewInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.NewInvoice(InvoiceModel);
            }
            return RedirectToAction("Index", new { id = InvoiceModel.SID });

        }

        [Authorize(Policy = "RequireStuList")]
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

        [Authorize(Policy = "RequireArchList")]
        public IActionResult ArchiveInvoice(int? id)
        {


            IEnumerable<InvoiceInfo> StudentInvoices = ar_Invo.Where(x => x.SID == id);

          
        
            var student = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id);
            StudentDepartmentInfo studentDepartmentInfo = ArchiveService.ar_department.First(x => x.SID == id) as StudentDepartmentInfo;
            InvoiceInfo Invoices = new InvoiceInfo();
            if (studentDepartmentInfo.AcceptanceType.Contains("زانکۆ"))
            {
                return BadRequest();
            }
            Invoices.Name = student.Name;
            Invoices.SID = student.Id;
            return View((studentDepartmentInfo, StudentInvoices, Invoices));

        }

        [Authorize(Policy = "RequireArchList")]
        public IActionResult ARSearch([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel, int? id)
        {
           
                IEnumerable<InvoiceInfo> StudentInvoices = ar_Invo.Where(x => x.InvoiceDate == InvoiceModel.InvoiceDate && x.SID == id);
                StudentDepartmentInfo studentDepartmentInfo = ArchiveService.ar_department.First(x => x.SID == id) as StudentDepartmentInfo;
                if (StudentInvoices.Any())
                {
                    var student = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id);
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

        [Authorize(Policy = "RequireArchList")]
        [HttpPost]
        public async Task<IActionResult> ArNewInvoice([Bind(Prefix = "InvoiceModel")] InvoiceInfo InvoiceModel)
        {
            if (!string.IsNullOrEmpty(InvoiceModel.InvoiceId) && !string.IsNullOrEmpty(InvoiceModel.Amount))
            {
                await Helper_Invoice.arNewInvoice(InvoiceModel);
            }
            return RedirectToAction("ArchiveInvoice", new { id = InvoiceModel.SID });

        }
        [Authorize(Policy = "RequireArchList")]
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
