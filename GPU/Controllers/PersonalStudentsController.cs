using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GPU.Data;
using GPU.Models;
using GPU.Helpers;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace GPU.Controllers
{
    public class PersonalStudentsController : Controller
    {
        private readonly GPUContext _context;
        StudentTableModel stu;

        public PersonalStudentsController(GPUContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string txt = "MyCode")
        {
            //await Helper_StudentTable.GetStudent("getstudents");

            //if (txt != "MyCode")
            //{
            //    if (Helper_StudentTable._stu.Where(x => x.Name.Contains(txt)) as StudentTableModel != null)
            //    {
            //        stu = (StudentTableModel)Helper_StudentTable._stu.Where(x => x.id != -1);
            //    }
            //}
            //else
            //{
            //    stu = Helper_StudentTable._stu.Where(x => x.id != -1) as StudentTableModel;
            //}

            return View(await Helper_StudentTable.GetStudent("getstudents"));
        }


        public async Task<IActionResult> Details(int? id)
        {

            await Helper_PersonalStudent.GetStudents();
            if (id == null)
            {
                return NotFound();
            }

            var personalStudent = Helper_PersonalStudent._Student.FirstOrDefault(x => x.Id == id) as PersonalStudent;
            var studentContactInfo = Helper_StudentContactInfo._Contacts.Where(x => x.SID == id) as StudentContactInfo;
            var studentParentInfo = Helper_StudentParentInfo._Parent.Where(x => x.SID == id) as StudentParentInfo;
            var student12Grade = Helper_Student12Grade._Grade.Where(x => x.SID == id) as Student12Grade;
            var studentDepartmentInfo = Helper_StudentDepartmentInfo._departmen.Where(x => x.SID == id) as StudentDepartmentInfo;
            var support = Helper_Invoice._Invoices.Where(x => x.SID == id) as StudentSupport;
            var invoice = Helper_StudentSupport._Supports.Where(x => x.sid == id) as InvoiceInfo;

            var viewModel = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: support);


            if (personalStudent == null)
            {
                return NotFound("404");
            }

            return View(viewModel);
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

            var viewModel = (PersonalStudent: personalStudent, StudentContactInfo: studentContactInfo,
                             StudentParentInfo: studentParentInfo, Student12Grade: student12Grade,
                             StudentDepartmentInfo: studentDepartmentInfo, InvoiceInfo: invoice,
                             StudentSupport: studentSupport);

            return View(viewModel);
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
            _context.Add(personalStudent);



            await Helper_PersonalStudent.Create(personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, invoice, studentSupport);


            return View((personalStudent, studentContactInfo, studentParentInfo, student12Grade, studentDepartmentInfo, invoice, studentSupport));
        }







        // GET: PersonalStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalStudent = await _context.PersonalStudent.FindAsync(id);
            if (personalStudent == null)
            {
                return NotFound();
            }
            return View(personalStudent);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Sex,MartialStatus,BloodGroup,Religion,IdentityNo,Nationality,RationingFormNo")] PersonalStudent personalStudent)
        {
            if (id != personalStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalStudentExists(personalStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personalStudent);
        }

        // GET: PersonalStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalStudent = await _context.PersonalStudent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalStudent == null)
            {
                return NotFound();
            }

            return View(personalStudent);
        }

        // POST: PersonalStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalStudent = await _context.PersonalStudent.FindAsync(id);
            if (personalStudent != null)
            {
                _context.PersonalStudent.Remove(personalStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalStudentExists(int id)
        {
            return _context.PersonalStudent.Any(e => e.Id == id);
        }
    }
}
