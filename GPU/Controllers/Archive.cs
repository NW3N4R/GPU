using GPU.Helpers;
using GPU.Models;
using GPU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace GPU.Controllers
{
    [Authorize(Policy = "RequireArchList")]
    public class Archive : Controller
    {
        public IActionResult Index()
        {
            return View((Helper_StudentTable.GetArchiveStudentTable(), new StaticalTableModel(),
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths));
        }

        [HttpGet]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StaticalTableModel tbl, bool DoPrint, bool doSearch = true)
        {
            if (doSearch)
            {
                var viewModel = ((Helper_StudentTable.ArSearchHelper(tbl), tbl,
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths));
                if (DoPrint)
                {
                    var fileContent = await ToExcelPrint.DoPrint((Helper_StudentTable.ArSearchHelper(tbl)), 1);
                    string fileName = $"لیستی خوێندکاران {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return File(fileContent, contentType, fileName);
                }
                return View("Index", viewModel);
            }
            else
            {
                return View("Index", (Helper_StudentTable.GetArchiveStudentTable(), new StaticalTableModel(),
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths));

            }

        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = ArchiveService.ar_Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = ArchiveService.ar_Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = ArchiveService.ar_Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = ArchiveService.ar_department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = ArchiveService.ar_Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = ArchiveService.ar_Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;


            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: support));
        }

        public IActionResult Edit(int? id)
        {
            var personalStudent = ArchiveService.ar_Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = ArchiveService.ar_Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = ArchiveService.ar_Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = ArchiveService.ar_Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = ArchiveService.ar_department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = ArchiveService.ar_Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;
            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, StudentSupport: studentSupport,
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(Prefix = "PersonalStudent")] PersonalStudent personalStudent,
            [Bind(Prefix = "StudentContactInfo")] StudentContactInfo studentContactInfo,
            [Bind(Prefix = "StudentParentInfo")] StudentParentInfo studentParentInfo,
            [Bind(Prefix = "Student12Grade")] Student12Grade student12Grade,
            [Bind(Prefix = "StudentDepartmentInfo")] StudentDepartmentInfo studentDepartmentInfo,
            [Bind(Prefix = "studentSupport")] StudentSupport studentSupport)
        {

            await Helper_PersonalStudent.Ar_Update(personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, studentSupport);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            await Helper_PersonalStudent.DeleteRecord(id, "DeleteArchiveStudents");
            return RedirectToAction("Index");
        }
    }
}
