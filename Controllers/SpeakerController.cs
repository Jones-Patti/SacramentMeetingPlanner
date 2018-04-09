using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Data;
using SacramentMeetingPlanner.Models;
using Newtonsoft.Json;

namespace SacramentMeetingPlanner.Controllers
{
    public class SpeakerController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public SpeakerController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Speaker
        public async Task<IActionResult> Index()
        {
            var sacramentMeetingPlannerContext = _context.Speaker.Include(s => s.People).Include(s => s.Sacrament).Include(s => s.Topic);
            return View(await sacramentMeetingPlannerContext.ToListAsync());
        }

        // GET: Speaker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.People)
                .Include(s => s.Sacrament)
                .Include(s => s.Topic)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speaker/Create
        public IActionResult Create(int id)
        {
            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FullName");
            ViewData["SacramentId"] = id;
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");
            return View();
        }

        // POST: Speaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,PeopleId,SacramentId,TopicId")] Speaker speaker)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(speaker);
               
                var count = _context.Speaker.Count(t => t.SacramentId == speaker.SacramentId);
                speaker.SpeakerOrder = (count + 1);
                _context.Add(speaker);
                await _context.SaveChangesAsync();

                return RedirectToAction("Edit", "Sacrament", new {id =  speaker.SacramentId });
            }
            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FirstName", speaker.PeopleId);
            ViewData["SacramentId"] = new SelectList(_context.Sacrament, "SacramentId", "SacramentId", speaker.SacramentId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.TopicId);
            //return View(speaker);

            return RedirectToAction("Edit", "Sacrament", new { id = speaker.SacramentId });
        }

        // GET: Speaker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }
            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FirstName", speaker.PeopleId);
            ViewData["SacramentId"] = new SelectList(_context.Sacrament, "SacramentId", "SacramentId", speaker.SacramentId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.TopicId);
            return View(speaker);
        }

        // POST: Speaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,PeopleId,SacramentId,SpeakerOrder,TopicId")] Speaker speaker)
        {
            if (id != speaker.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //switch the orders around
                    String speakerID = speaker.SpeakerId.ToString();
                    String speakerOrder = speaker.SpeakerOrder.ToString();
                    string sql = "call changeTalkOrder( " + speakerID + ","  + speakerOrder + ")";
                    await _context.Database.ExecuteSqlCommandAsync(sql);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.SpeakerId))
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
            ViewData["PeopleId"] = new SelectList(_context.People, "PeopleId", "FirstName", speaker.PeopleId);
            ViewData["SacramentId"] = new SelectList(_context.Sacrament, "SacramentId", "SacramentId", speaker.SacramentId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.TopicId);

            return View(speaker);
        }

        // GET: Speaker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.People)
                .Include(s => s.Sacrament)
                .Include(s => s.Topic)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            _context.Speaker.Remove(speaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speaker.Any(e => e.SpeakerId == id);
        }

        [HttpPost]
        public async Task<string> SwitchSpeakers([Bind("talk1,talk2")]SwitchSpeakers switchSpeakers)
        {

            if (ModelState.IsValid)
            {

                string sql = "call switch_speakers_by_id(" + switchSpeakers.talk1.ToString() + "," + switchSpeakers.talk2.ToString() + ")";
                await _context.Database.ExecuteSqlCommandAsync(sql);
               
                var sacramentId = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == switchSpeakers.talk1);
                var speakers = await _context.Speaker
                                             .Include(s => s.Topic)
                                             .Include(s => s.People)
                                             .Where(s => s.SacramentId.Equals(sacramentId.SacramentId))
                                             .OrderBy(s => s.SpeakerOrder)
                                             .ToListAsync();

                SpeakerViewModel viewModel = new SpeakerViewModel();
                viewModel.Speakers = speakers;

                string json = JsonConvert.SerializeObject(viewModel, Formatting.Indented,
            new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

                return json;
            }
            else {
                return "0";
            }
        }

        [HttpPost]
        public async Task<string> CreateSpeaker([Bind("PeopleId,SacramentId,TopicId")] Speaker speaker)
        {

            if (ModelState.IsValid)
            {
                _context.Add(speaker);

                var count = _context.Speaker.Count(t => t.SacramentId == speaker.SacramentId);
                speaker.SpeakerOrder = (count + 1);
                _context.Add(speaker);
                await _context.SaveChangesAsync();

                //return RedirectToAction("Edit", "Sacrament", new { id = speaker.SacramentId });
            


                var speakers = await _context.Speaker
                                             .Include(s => s.Topic)
                                             .Include(s => s.People)
                                             .Where(s => s.SacramentId.Equals(speaker.SacramentId))
                                             .OrderBy(s => s.SpeakerOrder)
                                             .ToListAsync();

                SpeakerViewModel viewModel = new SpeakerViewModel();
                viewModel.Speakers = speakers;

                string json = JsonConvert.SerializeObject(viewModel, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return json;
            }
            else {
                return "0";
            }
           
        }


    }




}
