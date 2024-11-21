using Microsoft.AspNetCore.Mvc;
using GPU.Models;
using GPU.Helpers;
using Microsoft.AspNetCore.Authorization;
using GPU.Services;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.InteropServices;

namespace GPU.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {

        [Authorize(Policy = "RequireStuList")]
        public IActionResult Index()
        {
            return View((Helper_StudentTable.GetStudentTable(), new StaticalTableModel(),
                             props: WebPropsServices.MergedProps, access: ManagerServices._auths));
        }

        [Authorize(Policy = "RequireStuList")]
        [HttpGet]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StaticalTableModel tbl, bool DoPrint, bool doSearch = true)
        {
            if (doSearch)
            {
                var viewModel = ((Helper_StudentTable.SearchHelper(tbl), tbl,
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths));
                if (DoPrint)
                {
                    var fileContent = await ToExcelPrint.DoPrint((Helper_StudentTable.SearchHelper(tbl)), 0);
                    string fileName = $"لیستی خوێندکاران {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    return File(fileContent, contentType, fileName);
                }
                return View("Index", viewModel);
            }
            else
            {
                return View("Index", (Helper_StudentTable.GetStudentTable(), new StaticalTableModel(), props: WebPropsServices.MergedProps, access: ManagerServices._auths));

            }

        }
        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = StudentServices._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = StudentServices._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = StudentServices._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = StudentServices._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = StudentServices._department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = StudentServices._Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = StudentServices._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: support));
        }

        public IActionResult Create()
        {
            var personalStudent = new PersonalStudent();
            var studentContactInfo = new StudentContactInfo();
            var studentParentInfo = new StudentParentInfo();
            var student12Grade = new Student12Grade();
            var studentDepartmentInfo = new StudentDepartmentInfo();
            var invoice = new InvoiceInfo();
            var studentSupport = new StudentSupport();
            var viewMode = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: studentSupport,
                             props: WebPropsServices.MergedProps,
                             access: ManagerServices._auths
                           );
            return View(viewMode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(Prefix = "PersonalStudent")] PersonalStudent personalStudent,
            [Bind(Prefix = "StudentContactInfo")] StudentContactInfo studentContactInfo,
            [Bind(Prefix = "StudentParentInfo")] StudentParentInfo studentParentInfo,
            [Bind(Prefix = "Student12Grade")] Student12Grade student12Grade,
            [Bind(Prefix = "StudentDepartmentInfo")] StudentDepartmentInfo studentDepartmentInfo,
            [Bind(Prefix = "InvoiceInfo")] InvoiceInfo invoice,
            [Bind(Prefix = "studentSupport")] StudentSupport studentSupport)
        {

            await Helper_PersonalStudent.Create(personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, invoice, studentSupport);
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireStuList")]
        public IActionResult Edit(int? id)
        {
            var personalStudent = StudentServices._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = StudentServices._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = StudentServices._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = StudentServices._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = StudentServices._department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = StudentServices._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

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

            await Helper_PersonalStudent.Update(personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, studentSupport);
            return RedirectToAction("Index");

        }



        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> Gallery(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var NewImg = new StudentDepartmentInfo();
            NewImg.Id = id;

            return View((NewImg, id));
        }


        [HttpPost]
        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> NewImage([Bind(Prefix = "NewImage")] StudentDepartmentInfo personalStudent)
        {
            Debug.WriteLine(personalStudent.Id);
            string path = "C:\\Users\\Aurora\\Desktop\\Student List Images\\Pdf Files";
#if !DEBUG
            path = @$"C:\Users\nwenar\Desktop\Student List Images\\Pdf Files";
#endif

            await Helper_PersonalStudent.SaveOtherFiles(personalStudent.OtherFiles, personalStudent.Id.ToString(), path);

            return RedirectToAction("Gallery", new { id = personalStudent.Id });
        }

        [Authorize(Policy = "RequireStuList")]
        public IActionResult RemoveImage(int? id, string? fileName)
        {
            //if (id == 0 || fileName != null)
            //{
            //    return BadRequest();
            //}
            Debug.WriteLine($"fileName was {fileName}");
            string path = $"C:\\Users\\Aurora\\Desktop\\Student List Images\\Pdf Files\\{fileName}";
#if !DEBUG
            path = @$"C:\Users\nwenar\Desktop\Student List Images\\Pdf Files\\{fileName}";
#endif
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("Gallery", new { id = id });
        }

        [Authorize(Policy = "RequireStuList")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            await Helper_PersonalStudent.DeleteRecord(id, "DeleteStudents");
            return RedirectToAction("Index");
        }
    }
}
