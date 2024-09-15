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

namespace GPU.Controllers
{
    public class PersonalStudentsController : Controller
    {
        private readonly GPUContext _context;

        public PersonalStudentsController(GPUContext context)
        {
            _context = context;
        }

        // GET: PersonalStudents
        public async Task<IActionResult> Index()
        {
            return View(await Helper_PersonalStudent.GetStudents());
        }

        // GET: PersonalStudents/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: PersonalStudents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Sex,MartialStatus,BloodGroup,Religion,IdentityNo,Nationality,RationingFormNo")] PersonalStudent personalStudent)
        {
            if (ModelState.IsValid)
            {
                var student = new PersonalStudent
                {
                    Id = personalStudent.Id,
                    Name = personalStudent.Name,
                    Age = personalStudent.Age,
                    Sex = personalStudent.Sex,
                    MartialStatus = personalStudent.MartialStatus,
                    BloodGroup = personalStudent.BloodGroup,
                    Religion = personalStudent.Religion,
                    IdentityNo = personalStudent.IdentityNo,
                    Nationality = personalStudent.Nationality,
                    RationingFormNo = personalStudent.RationingFormNo
                };
                await Helper_PersonalStudent.Create(student);
           
                return RedirectToAction(nameof(Index));
            }
            return View(personalStudent);
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

        // POST: PersonalStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
