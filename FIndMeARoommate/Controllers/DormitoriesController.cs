using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FIndMeARoommate;
using FIndMeARoommate.Entities;

namespace FIndMeARoommate.Controllers
{
    public class DormitoriesController : Controller
    {
        private readonly RoommateDbContext _context;

        public DormitoriesController(RoommateDbContext context)
        {
            _context = context;
        }

        // GET: Dormitories
        public async Task<IActionResult> Index()
        {
              return _context.Dormitories != null ? 
                          View(await _context.Dormitories.ToListAsync()) :
                          Problem("Entity set 'RoommateDbContext.Dormitories'  is null.");
        }

        // GET: Dormitories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dormitories == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormitories == null)
            {
                return NotFound();
            }

            return View(dormitories);
        }

        // GET: Dormitories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dormitories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,MaxCapacity")] Dormitories dormitories)
        {
            dormitories.StudentsList = _context.Students.Where(x=>x.DormitoriesId == dormitories.Id).ToList();
            if (ModelState.IsValid)
            {
                _context.Add(dormitories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dormitories);
        }

        // GET: Dormitories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dormitories == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories.FindAsync(id);
            if (dormitories == null)
            {
                return NotFound();
            }
            return View(dormitories);
        }

        // POST: Dormitories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,MaxCapacity")] Dormitories dormitories)
        {
            if (id != dormitories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dormitories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormitoriesExists(dormitories.Id))
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
            return View(dormitories);
        }

        // GET: Dormitories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dormitories == null)
            {
                return NotFound();
            }

            var dormitories = await _context.Dormitories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormitories == null)
            {
                return NotFound();
            }

            return View(dormitories);
        }

        // POST: Dormitories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dormitories == null)
            {
                return Problem("Entity set 'RoommateDbContext.Dormitories'  is null.");
            }
            var dormitories = await _context.Dormitories.FindAsync(id);
            if (dormitories != null)
            {
                _context.Dormitories.Remove(dormitories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormitoriesExists(int id)
        {
          return (_context.Dormitories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
