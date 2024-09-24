using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Diagnostics;

namespace GPU.Controllers
{
    public class Print : Controller
    {
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == id);
            var invoices = Helper_Invoice._Invoices.Where(x => x.SID == id).AsEnumerable();
            var support = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id);
            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            var studentViewModel = new AllViewModel
            {
                PersonalStudent = personalStudent ?? new PersonalStudent(),
                StudentContactInfo = studentContactInfo ?? new StudentContactInfo(),
                StudentParentInfo = studentParentInfo ?? new StudentParentInfo(),
                Student12Grade = student12Grade ?? new Student12Grade(),
                StudentDepartmentInfo = studentDepartmentInfo ?? new StudentDepartmentInfo(),
                MyInvoice = invoices,
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo() // If you need an empty InvoiceInfo object
            };
            return View(studentViewModel);
        }

        public async Task<IActionResult> DoLoadPrint(int? id )
        {
           
                //await DbConnectionHelper.LoadStudent();
           
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id);
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == id);
            var invoices = Helper_Invoice._Invoices.Where(x => x.SID == id).AsEnumerable();
            var support = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id);
            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            var studentViewModel = new AllViewModel
            {
                PersonalStudent = personalStudent ?? new PersonalStudent(),
                StudentContactInfo = studentContactInfo ?? new StudentContactInfo(),
                StudentParentInfo = studentParentInfo ?? new StudentParentInfo(),
                Student12Grade = student12Grade ?? new Student12Grade(),
                StudentDepartmentInfo = studentDepartmentInfo ?? new StudentDepartmentInfo(),
                MyInvoice = invoices,
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo() // If you need an empty InvoiceInfo object
            };
            return View("Index",studentViewModel);
        }
        public async Task<IActionResult> IndexArchive(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id);
            var studentContactInfo = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = Helper_StudentParentInfo.ar_Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == id);
            var invoices = Helper_Invoice.ar_Invoices.Where(x => x.SID == id).AsEnumerable();
            var support = Helper_StudentSupport.ar_Supports.FirstOrDefault(x => x.sid == id);
            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            var studentViewModel = new AllViewModel
            {
                PersonalStudent = personalStudent ?? new PersonalStudent(),
                StudentContactInfo = studentContactInfo ?? new StudentContactInfo(),
                StudentParentInfo = studentParentInfo ?? new StudentParentInfo(),
                Student12Grade = student12Grade ?? new Student12Grade(),
                StudentDepartmentInfo = studentDepartmentInfo ?? new StudentDepartmentInfo(),
                MyInvoice = invoices,
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo() // If you need an empty InvoiceInfo object
            };
            return View("Index",studentViewModel);
        }

        public async Task<IActionResult> DoArchiveLoadPrint(int? id)
        {
          
                //await DbConnectionHelper.LoadArchive();

            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id);
            var studentContactInfo = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = Helper_StudentParentInfo.ar_Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == id);
            var invoices = Helper_Invoice.ar_Invoices.Where(x => x.SID == id).AsEnumerable();
            var support = Helper_StudentSupport.ar_Supports.FirstOrDefault(x => x.sid == id);
            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            var studentViewModel = new AllViewModel
            {
                PersonalStudent = personalStudent ?? new PersonalStudent(),
                StudentContactInfo = studentContactInfo ?? new StudentContactInfo(),
                StudentParentInfo = studentParentInfo ?? new StudentParentInfo(),
                Student12Grade = student12Grade ?? new Student12Grade(),
                StudentDepartmentInfo = studentDepartmentInfo ?? new StudentDepartmentInfo(),
                MyInvoice = invoices,
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo() // If you need an empty InvoiceInfo object
            };
            return View("Index", studentViewModel);
        }

    }
}
