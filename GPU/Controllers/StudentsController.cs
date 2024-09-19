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

namespace GPU.Controllers
{
    public class StudentsController : Controller
    {
        IEnumerable<StudentTableModel> students = Helper_StudentTable._stu;
        public async Task<IActionResult> Index()
        {
            await Helper_StudentTable.GetStudent("getstudents");
            return View((students, new StudentTableModel()));

        }
        [HttpPost]
        public IActionResult Search([Bind(Prefix = "table")] StudentTableModel model)
        {
            int z= 0, y=0;  
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
                students = Helper_StudentTable._stu.Where(x => !string.IsNullOrEmpty(x.Name) ? x.Name.Contains(model.Name) : z==y);
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
            var tbl = new StudentTableModel();
            tbl.Name = model.Name;
            tbl.department = model.department;
            tbl.StartingYear = model.StartingYear;
            var viewModel = (StudentTableModel: students, tbl);
            return View("Index", viewModel);

        }
        public async Task<IActionResult> Details(int? id)
        {
            await DbConnectionHelper.LoadAll("");
            if (id == null)
            {
                return View("~/Views/Students/NotFound.cshtml");
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._departmen.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = Helper_Invoice._Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var support = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            if (personalStudent == null)
            {
                return View("~/Views/Students/NotFound.cshtml");
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
            return View((personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, invoice, studentSupport));
        }



        public IActionResult Edit(int? id)
        {
            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.FirstOrDefault(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.FirstOrDefault(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.FirstOrDefault(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._departmen.FirstOrDefault(x => x.SID == id) as StudentDepartmentInfo;
            var invoice = Helper_Invoice._Invoices.FirstOrDefault(x => x.SID == id) as InvoiceInfo;
            var studentSupport = Helper_StudentSupport._Supports.FirstOrDefault(x => x.sid == id) as StudentSupport;

            return View((PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: studentSupport));

            //return View("~/Views/Students/NotFound.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Age,Sex,MartialStatus,BloodGroup,Religion,IdentityNo,Nationality,RationingFormNo")] PersonalStudent personalStudent)
        {
            return View("~/Views/Students/NotFound.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotFound404()
        {
            return View();
        }
    }
}
