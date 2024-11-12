using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeFanWeb.Data;
using AnimeFanWeb.Models;

namespace AnimeFanWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            List<EventIndexViewModel> eventData = await (from m in _context.Events
                                                         join moderator in _context.Moderators
                                                            on m.Moderator.Id equals moderator.Id
                                                         orderby m.Title
                                                         select new EventIndexViewModel
                                                         {
                                                             EventId = m.Id,
                                                             EventTitle = m.Title,
                                                             ModeratorName = moderator.FullName,
                                                             EventDate = m.EventDate
                                                         }).ToListAsync();

            return View(eventData);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            EventCreateViewModel viewModel = new();
            viewModel.AllAvailableModerators = _context.Moderators.OrderBy(m => m.FullName).ToList();
            return View(viewModel);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel @event)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new()
                {
                    Description = @event.Description,
                    Title = @event.Title,
                    Moderator = new Moderator()
                    {
                        Id = @event.ChosenModerator
                    },
                    EventDate = @event.EventDate ?? DateTime.Now
                };

                // Tell EF that we have not modified the existing instructor 
                _context.Entry(newEvent.Moderator).State = EntityState.Unchanged;

                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            @event.AllAvailableModerators = _context.Moderators.OrderBy(m => m.FullName).ToList();
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Moderator) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            var viewModel = new EventEditViewModel
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                EventDate = @event.EventDate,
                ModeratorId = @event.ModeratorId, 
                AllModerators = _context.Moderators
                    .OrderBy(m => m.FullName)
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.FullName
                    }).ToList() // Populate the list of moderators
            };

            return View(viewModel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,EventDate,ModeratorId")] EventEditViewModel @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var errors = ModelState[modelStateKey].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
                return View(@event); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Find the existing event
                    var existingEvent = await _context.Events
                        .Include(e => e.Moderator)  
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (existingEvent == null)
                    {
                        return NotFound();
                    }

                    // Map the values 
                    existingEvent.Title = @event.Title;
                    existingEvent.Description = @event.Description;
                    existingEvent.EventDate = @event.EventDate;

                    // If ModeratorId is selected, update the Moderator
                    if (@event.ModeratorId.HasValue)
                    {
                        existingEvent.ModeratorId = @event.ModeratorId.Value;
                    }

                    // Update the event in the database
                    _context.Update(existingEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}