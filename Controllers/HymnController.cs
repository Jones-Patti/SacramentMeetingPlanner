using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Controllers
{
    public class HymnController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public HymnController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Hymn
        public async Task<IActionResult> Index(string searchString)
        {

            var hymns = from s in _context.Hymn select s;

            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {

                hymns = hymns.Where(s => s.HymnTitle.Contains(searchString));
                            

            }

            return View(await _context.Hymn.ToListAsync());
        }

        // GET: Hymn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn
                .SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }

            return View(hymn);
        }

        // GET: Hymn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hymn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HymnId,HymnTitle")] Hymn hymn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hymn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hymn);
        }

        // GET: Hymn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn.SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }
            return View(hymn);
        }

        // POST: Hymn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HymnId,HymnTitle")] Hymn hymn)
        {
            if (id != hymn.HymnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hymn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HymnExists(hymn.HymnId))
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
            return View(hymn);
        }

        // GET: Hymn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn
                .SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }

            return View(hymn);
        }

        // POST: Hymn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hymn = await _context.Hymn.SingleOrDefaultAsync(m => m.HymnId == id);
            _context.Hymn.Remove(hymn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HymnExists(int id)
        {
            return _context.Hymn.Any(e => e.HymnId == id);
        }
    }
}
