using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GPU.Models;
using GPU.Helpers;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static NuGet.Packaging.PackagingConstants;
using OfficeOpenXml;

namespace GPU.Controllers
{
    public class StudentsController : Controller
    {
        IEnumerable<StudentTableModel> students = Helper_StudentTable._stu;
        public async Task<IActionResult> Index()
        {
            await Helper_StudentTable.GetStudent();
            return View((students, new StudentTableModel()));
        }


        [HttpPost]
        public async Task<IActionResult> Search([Bind(Prefix = "table")] StudentTableModel model, bool DoPrint)
        {
            int z = 0, y = 0;
            if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                // search by all
                students = Helper_StudentTable._stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                //search by name and dep
                students = Helper_StudentTable._stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.department == model.department);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                //search by name and starting year
                students = Helper_StudentTable._stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y && x.StartingYear == model.StartingYear);
            }
            else if (!string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear == "0")
            {
                // search by name
                students = Helper_StudentTable._stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z == y);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear != "0")
            {
                //search by dep and starting year
                students = Helper_StudentTable._stu.Where(x => x.department == model.department && x.StartingYear == model.StartingYear);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department != "0" && model.StartingYear == "0")
            {
                // search by dep
                students = Helper_StudentTable._stu.Where(x => x.department == model.department);
            }
            else if (string.IsNullOrEmpty(model.Name) && model.department == "0" && model.StartingYear != "0")
            {
                // search by year
                students = Helper_StudentTable._stu.Where(x => x.StartingYear == model.StartingYear);
            }
            else
            {
                // no result return all
                students = Helper_StudentTable._stu;
            }

            foreach (var item in students)
            {
                Debug.WriteLine(item.Name);
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

                var fileContent = await ToExcelPrint.DoPrint(students.ToList());
                string fileName = $"لیستی خوێندکاران {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(fileContent, contentType, fileName);
            }

        }
        public async Task<IActionResult> Details(int? id)
        {
            //await DbConnectionHelper.LoadStudent();
            if (id == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = Helper_Invoice._Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound404.cshtml");
            }

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: support));
        }
        #region CRUD

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
                             StudentSupport: studentSupport);
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



        public IActionResult Edit(int? id)
        {
            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, StudentSupport: studentSupport));

        }

        public async Task<IActionResult> GetEdit(int? id)
        {
            //await DbConnectionHelper.LoadStudent();
            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._department.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var studentSupport = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            var viewModel = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, StudentSupport: studentSupport);
            return View("Edit", viewModel);

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

            Debug.WriteLine($"the edit commited for the id of {personalStudent.Id}");
            await Helper_PersonalStudent.Update(personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, studentSupport);
            return RedirectToAction("Index");

        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]


        #endregion


    }
}
