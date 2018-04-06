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
    public class SacramentController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public SacramentController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Sacrament
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new SacramentViewModel();
            viewModel.Sacraments = await _context.Sacrament.Include(s => s.ClosingHymnNavigation)
                .Include(s => s.ClosingPrayerNavigation)
                .Include(s => s.ConductingBishopricNavigation)
                .Include(s => s.IntermediateHymnNavigation)
                .Include(s => s.OpeningHymnNavigation)
                .Include(s => s.OpeningPrayerNavigation)
                .Include(s => s.SacramentHymnNavigation)
                .ToListAsync();


            if (id != null)
            {

                ViewData["MeetingID"] = id.Value;

                var speakers = from s in _context.Speaker select s;
                speakers = speakers.Where(s => s.SacramentId.Equals(id));
                viewModel.Speakers = speakers;


                var selectedMeeting = viewModel.Sacraments.Where(x => x.SacramentId == id).Single();
                await _context.Entry(selectedMeeting).Collection(x => x.Speaker).LoadAsync();
                foreach(Speaker speaker in selectedMeeting.Speaker)
                {
                    await _context.Entry(speaker).Reference(x => x.People).LoadAsync();
                    await _context.Entry(speaker).Reference(x => x.Topic).LoadAsync();

                }

                viewModel.Speakers = selectedMeeting.Speaker;


            }

            return View(viewModel);

        }




        // GET: Sacrament/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .Include(s => s.ClosingHymnNavigation)
                .Include(s => s.ClosingPrayerNavigation)
                .Include(s => s.ConductingBishopricNavigation)
                .Include(s => s.IntermediateHymnNavigation)
                .Include(s => s.OpeningHymnNavigation)
                .Include(s => s.OpeningPrayerNavigation)
                .Include(s => s.SacramentHymnNavigation)
                .SingleOrDefaultAsync(m => m.SacramentId == id);
            
                SacramentDetailViewModel viewModel = new SacramentDetailViewModel();
                viewModel.Sacrament = sacrament;

                ViewData["MeetingID"] = id.Value;

                var speakers = from s in _context.Speaker select s;
                speakers = speakers.Where(s => s.SacramentId.Equals(id));
                viewModel.Speakers = speakers;

            foreach (Speaker speaker in speakers)
            {
                await _context.Entry(speaker).Reference(x => x.People).LoadAsync();
                await _context.Entry(speaker).Reference(x => x.Topic).LoadAsync();

            }

            viewModel.Speakers = speakers;
           


            if (sacrament == null)
            {
                return NotFound();
            }


            return View(viewModel);
        }

        // GET: Sacrament/Create
        public IActionResult Create()
        {
            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn");
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["ConductingBishopric"] = new SelectList(_context.Bishopric, "BishopricId", "BishopricTitle");
            ViewData["IntermediateHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn");
            ViewData["OpeningHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn");
            ViewData["OpeningPrayer"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["SacramentHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn");
            return View();
        }

        // POST: Sacrament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SacramentId,SacramentDate,ConductingBishopric,OpeningPrayer,OpeningHymn,SacramentHymn,IntermediateHymn,ClosingHymn,ClosingPrayer")] Sacrament sacrament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sacrament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.ClosingHymn);
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.ClosingPrayer);
            ViewData["ConductingBishopric"] = new SelectList(_context.Bishopric, "BishopricId", "BishopricTitle", sacrament.ConductingBishopric);
            ViewData["IntermediateHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.IntermediateHymn);
            ViewData["OpeningHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.OpeningHymn);
            ViewData["OpeningPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.OpeningPrayer);
            ViewData["SacramentHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.SacramentHymn);
            return View(sacrament);
        }

        // GET: Sacrament/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament.SingleOrDefaultAsync(m => m.SacramentId == id);
            if (sacrament == null)
            {
                return NotFound();
            }
            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.ClosingHymn);
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.ClosingPrayer);
            ViewData["ConductingBishopric"] = new SelectList(_context.Bishopric, "BishopricId", "BishopricTitle", sacrament.ConductingBishopric);
            ViewData["IntermediateHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.IntermediateHymn);
            ViewData["OpeningHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.OpeningHymn);
            ViewData["OpeningPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.OpeningPrayer);
            ViewData["SacramentHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.SacramentHymn);
            return View(sacrament);
        }

        // POST: Sacrament/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SacramentId,SacramentDate,ConductingBishopric,OpeningPrayer,OpeningHymn,SacramentHymn,IntermediateHymn,ClosingHymn,ClosingPrayer")] Sacrament sacrament)
        {
            if (id != sacrament.SacramentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sacrament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentExists(sacrament.SacramentId))
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
            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.ClosingHymn);
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullHymn", sacrament.ClosingPrayer);
            ViewData["ConductingBishopric"] = new SelectList(_context.Bishopric, "BishopricId", "BishopricTitle", sacrament.ConductingBishopric);
            ViewData["IntermediateHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.IntermediateHymn);
            ViewData["OpeningHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.OpeningHymn);
            ViewData["OpeningPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.OpeningPrayer);
            ViewData["SacramentHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.SacramentHymn);
            return View(sacrament);
        }

        // GET: Sacrament/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .Include(s => s.ClosingHymnNavigation)
                .Include(s => s.ClosingPrayerNavigation)
                .Include(s => s.ConductingBishopricNavigation)
                .Include(s => s.IntermediateHymnNavigation)
                .Include(s => s.OpeningHymnNavigation)
                .Include(s => s.OpeningPrayerNavigation)
                .Include(s => s.SacramentHymnNavigation)
                .SingleOrDefaultAsync(m => m.SacramentId == id);
            if (sacrament == null)
            {
                return NotFound();
            }

            return View(sacrament);
        }

        // POST: Sacrament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacrament = await _context.Sacrament.SingleOrDefaultAsync(m => m.SacramentId == id);
            _context.Sacrament.Remove(sacrament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SacramentExists(int id)
        {
            return _context.Sacrament.Any(e => e.SacramentId == id);
        }
    }
}
