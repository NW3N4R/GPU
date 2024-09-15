using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GPU.Data;
using GPU.Models;

namespace GPU.Controllers
{
    public class StudentContactInfoController : Controller
    {
        private readonly GPUContext _context;

        public IActionResult StudentContactInfoPartial()
        {
            // Return the partial view to be loaded into the pivot item
            return PartialView("StudentContactInfoPartial");
        }
        public StudentContactInfoController(GPUContext context)
        {
            _context = context;
        }

        // GET: StudentContactInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentContactInfo.ToListAsync());
        }

        // GET: StudentContactInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentContactInfo = await _context.StudentContactInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentContactInfo == null)
            {
                return NotFound();
            }

            return View(studentContactInfo);
        }

        // GET: StudentContactInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentContactInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentPhone,StudentEmail,Province,City,District,Village,SID")] StudentContactInfo studentContactInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentContactInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentContactInfo);
        }

        // GET: StudentContactInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentContactInfo = await _context.StudentContactInfo.FindAsync(id);
            if (studentContactInfo == null)
            {
                return NotFound();
            }
            return View(studentContactInfo);
        }

        // POST: StudentContactInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentPhone,StudentEmail,Province,City,District,Village,SID")] StudentContactInfo studentContactInfo)
        {
            if (id != studentContactInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentContactInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentContactInfoExists(studentContactInfo.Id))
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
            return View(studentContactInfo);
        }

        // GET: StudentContactInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentContactInfo = await _context.StudentContactInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentContactInfo == null)
            {
                return NotFound();
            }

            return View(studentContactInfo);
        }

        // POST: StudentContactInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentContactInfo = await _context.StudentContactInfo.FindAsync(id);
            if (studentContactInfo != null)
            {
                _context.StudentContactInfo.Remove(studentContactInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentContactInfoExists(int id)
        {
            return _context.StudentContactInfo.Any(e => e.Id == id);
        }
    }
}
