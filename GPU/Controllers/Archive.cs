using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace GPU.Controllers
{
    public class Archive : Controller
    {

        IEnumerable<StudentTableModel> students = Helper_StudentTable.ar_stu;
        public async Task<IActionResult> Index()
        {
            await Helper_StudentTable.ar_GetStudent();
            return View((students, new StudentTableModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StudentTableModel model, bool DoPrint)
        {
            int z = 0, y = 0;

            if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                // search by all
                students = Helper_StudentTable.ar_stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                //search by name and dep
                students = Helper_StudentTable.ar_stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.department == model.department);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                //search by name and starting year
                students = Helper_StudentTable.ar_stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear == "0")
            {
                // search by name
                students = Helper_StudentTable.ar_stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                //search by dep and starting year
                students = Helper_StudentTable.ar_stu.Where(x => x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                // search by dep
                students = Helper_StudentTable.ar_stu.Where(x => x.department == model.department);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                // search by year
                students = Helper_StudentTable.ar_stu.Where(x => x.StartingYear == model.StartingYear);
            }
            else
            {
                // no result return all
                students = Helper_StudentTable.ar_stu;
            }
            var tbl = new StudentTableModel();
            tbl.Name = model.Name;
            tbl.department = model.department;
            tbl.StartingYear = model.StartingYear;
            var viewModel = (StudentTableModel: students, tbl);
            if (!DoPrint)
            {
                return View("Index", viewModel);
            }
            else
            {

                var fileContent = await ToExcelPrint.DoPrint(students.ToList(),2);
                string fileName = $"لیستی ئەرشیف {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(fileContent, contentType, fileName);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {

            //await DbConnectionHelper.LoadArchive();
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo.ar_Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = Helper_Invoice.ar_Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = Helper_StudentSupport.ar_Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;


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
            var personalStudent = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo.ar_Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = Helper_StudentSupport.ar_Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, StudentSupport: studentSupport));

        }
        public async Task<IActionResult> GetEdit(int? id)
        {
            //await DbConnectionHelper.LoadArchive();
            var personalStudent = Helper_PersonalStudent.ar_Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo.ar_Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo.ar_Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade.ar_Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo.ar_department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = Helper_StudentSupport.ar_Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;
            var viewModel = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, StudentSupport: studentSupport);
            return View("Edit",viewModel);

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
    }
}
