using GPU.Helpers;
using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Diagnostics;

namespace GPU.Controllers
{
    [Authorize]
    public class Print : Controller
    {
        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = StudentServices._Student.FirstOrDefault(x => x.Id == id);
            var studentContactInfo = StudentServices._Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = StudentServices._Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = StudentServices._Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = StudentServices._department.FirstOrDefault(x => x.SID == id);
            var invoices = StudentServices._Invoices.Where(x => x.SID == id);
            var stages = StudentServices._Stages.Where(x=>x.Sid == id);
            var support = StudentServices._Supports.FirstOrDefault(x => x.sid == id);
    
       

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
                MyInvoice = invoices.ToList(),
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo(),
                Stages = stages.ToList(),
                status = new GPU.Models.StudentStages()

            };
            return View(studentViewModel);
        }

        [Authorize(Policy = "RequireArchList")]
        public async Task<IActionResult> IndexArchive(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id);
            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }
            var studentContactInfo = ArchiveService.ar_Contacts.FirstOrDefault(x => x.SID == id);
            var studentParentInfo = ArchiveService.ar_Parent.FirstOrDefault(x => x.SID == id);
            var student12Grade = ArchiveService.ar_Grade.FirstOrDefault(x => x.SID == id);
            var studentDepartmentInfo = ArchiveService.ar_department.FirstOrDefault(x => x.SID == id);
            var invoices = ArchiveService.ar_Invoices.Where(x => x.SID == id);
            var stages = ArchiveService.ar_Stages.Where(x => x.Sid == id);
            var support = ArchiveService.ar_Supports.FirstOrDefault(x => x.sid == id);
       
 
            
            var studentViewModel = new AllViewModel
            {
                PersonalStudent = personalStudent ?? new PersonalStudent(),
                StudentContactInfo = studentContactInfo ?? new StudentContactInfo(),
                StudentParentInfo = studentParentInfo ?? new StudentParentInfo(),
                Student12Grade = student12Grade ?? new Student12Grade(),
                StudentDepartmentInfo = studentDepartmentInfo ?? new StudentDepartmentInfo(),
                MyInvoice = invoices.ToList(),
                StudentSupport = support ?? new StudentSupport(),
                Invoice = new InvoiceInfo(),
                Stages = stages.ToList(),
                status = new GPU.Models.StudentStages()
            };
            return View("Index", studentViewModel);
        }

    }
}
