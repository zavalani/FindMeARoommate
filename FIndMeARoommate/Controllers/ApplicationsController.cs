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
    public class ApplicationsController : Controller
    {
        private readonly RoommateDbContext _context;

        public ApplicationsController(RoommateDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var roommateDbContext = _context.Applications.Include(a => a.Announcements).Include(a => a.Students);
            return View(await roommateDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id");
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentsId,AnnouncementsId,IsActive")] Applications applications)
        {
            applications.Students = _context.Students.Where(i => i.Id == applications.StudentsId).FirstOrDefault();
            applications.Announcements = _context.Announcements.Where(i => i.Id == applications.AnnouncementsId).FirstOrDefault();
            bool CheckAnnouncement = _context.Applications.Any(x=>x.StudentsId == applications.StudentsId);
            if (CheckAnnouncement)
                return BadRequest("Kjo dormitory ka nje announcement");

            if (applications.Students != null || applications.Announcements != null)
            {
                _context.Add(applications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications.FindAsync(id);
            if (applications == null)
            {
                return NotFound();
            }
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentsId,AnnouncementsId,IsActive")] Applications applications)
        {
            if (id != applications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationsExists(applications.Id))
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
            ViewData["AnnouncementsId"] = new SelectList(_context.Announcements, "Id", "Id", applications.AnnouncementsId);
            ViewData["StudentsId"] = new SelectList(_context.Students, "Id", "Id", applications.StudentsId);
            return View(applications);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Announcements)
                .Include(a => a.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applications == null)
            {
                return NotFound();
            }

            return View(applications);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'RoommateDbContext.Applications'  is null.");
            }
            var applications = await _context.Applications.FindAsync(id);
            if (applications != null)
            {
                _context.Applications.Remove(applications);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationsExists(int id)
        {
          return (_context.Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
