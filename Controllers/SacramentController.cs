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
                .Include(s => s.ConductingBishopricNavigation.People)
                .ToListAsync();


            if (id != null)
            {

                ViewData["MeetingID"] = id.Value;

                var speakers = await _context.Speaker
                                         .Include(s => s.Topic)
                                         .Include(s => s.People)
                                         .Where(s => s.SacramentId.Equals(id))
                                         .OrderBy(s => s.SpeakerOrder)
                                         .ToListAsync();


                viewModel.Speakers = speakers;


                ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FullName");
                ViewData["SacramentId"] = id;
                ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");

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



            var bishop = await _context.Bishopric
                                       .Include(s => s.People)
                                       .SingleOrDefaultAsync(m => m.BishopricId == sacrament.ConductingBishopric);

            viewModel.Bishopric = bishop;

            var speakers = await _context.Speaker
                                         .Include(s => s.Topic)
                                         .Include(s => s.People)
                                         .Where(s => s.SacramentId.Equals(id))
                                         .OrderBy(s => s.SpeakerOrder)
                                         .ToListAsync();

            int count = _context.Speaker.Count(t => t.SacramentId == id);
            int roundpeopletop = count / 2;
            int squarepeoplebottom = count - roundpeopletop;

            ViewBag.roundpeopletop = roundpeopletop;
            ViewBag.squarepeoplebottom = squarepeoplebottom;


            viewModel.Speakers = speakers;

            if (sacrament == null)
            {
                return NotFound();
            }

            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["SacramentId"] = id;
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");

            return View(viewModel);
        }

        // GET: Sacrament/Create
        public IActionResult Create()
        {
            var bishoprics = _context.Bishopric
                                    .Include(s => s.People).Where(s => s.Active.Equals(true)).ToList();

            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn");
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["ConductingBishopric"] = new SelectList(bishoprics, "BishopricId", "TitleAndName");
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

            var  bishoprics = await _context.Bishopric
                                     .Include(s => s.People)
                                     .ToListAsync();

                
            ViewData["ClosingHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.ClosingHymn);
            ViewData["ClosingPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.ClosingPrayer);
            ViewData["ConductingBishopric"] = new SelectList(bishoprics, "BishopricId", "TitleAndName", sacrament.ConductingBishopric);
            ViewData["IntermediateHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.IntermediateHymn);
            ViewData["OpeningHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.OpeningHymn);
            ViewData["OpeningPrayer"] = new SelectList(_context.People, "PeopleId", "FullName", sacrament.OpeningPrayer);
            ViewData["SacramentHymn"] = new SelectList(_context.Hymn, "HymnId", "FullHymn", sacrament.SacramentHymn);

            SacramentDetailViewModel viewModel = new SacramentDetailViewModel();
            viewModel.Sacrament = sacrament;

           

            //viewModel.Bishopric = bishop;

            ViewData["MeetingID"] = id.Value;

            var speakers = await _context.Speaker
                                         .Include(s => s.Topic)
                                         .Include(s => s.People)
                                         .Where(s => s.SacramentId.Equals(id))
                                         .OrderBy(s => s.SpeakerOrder)
                                         .ToListAsync();

            viewModel.Speakers = speakers;

            if (sacrament == null)
            {
                return NotFound();
            }

            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["SacramentId"] = id;
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");


            //return View(sacrament);
            return View(viewModel);

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

        // GET:  Sacarment/Delete/5
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

            SacramentDetailViewModel viewModel = new SacramentDetailViewModel();
            viewModel.Sacrament = sacrament;

            ViewData["MeetingID"] = id.Value;

            var speakers = await _context.Speaker
                                         .Include(s => s.Topic)
                                         .Include(s => s.People)
                                         .Where(s => s.SacramentId.Equals(id))
                                         .OrderBy(s => s.SpeakerOrder)
                                         .ToListAsync();

            int count = _context.Speaker.Count(t => t.SacramentId == id);
            int roundpeopletop = count / 2;
            int squarepeoplebottom = count - roundpeopletop;

            ViewBag.roundpeopletop = roundpeopletop;
            ViewBag.squarepeoplebottom = squarepeoplebottom;


            viewModel.Speakers = speakers;

            if (sacrament == null)
            {
                return NotFound();
            }

            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["SacramentId"] = id;
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");

            return View(viewModel);
        }

        // POST: Sacrament/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var speakers = from s in _context.Speaker select s;
            speakers = speakers.Where(s => s.SacramentId.Equals(id));

            foreach (Speaker speaker in speakers)
            {
                _context.Speaker.Remove(speaker);
            }
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
