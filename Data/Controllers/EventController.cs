using Microsoft.AspNetCore.Mvc;
using CLDV6211_PART1_BOOKING_APP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CLDV6211_PART1_BOOKING_APP.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var events = await _context.Event.Include(e => e.Venue).ToListAsync();
            return View(events);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            PopulateVenueDropdown();
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model)
        {
            if (ModelState.IsValid)
            {
                _context.Event.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateVenueDropdown(model.VenueID);
            return View(model);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Event.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            PopulateVenueDropdown(eventItem.VenueID);
            return View(eventItem);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,EventDate,Description,VenueID")] Event eventItem)
        {
            if (id != eventItem.EventID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventItem.EventID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateVenueDropdown(eventItem.VenueID);
            return View(eventItem);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var eventItem = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (eventItem == null)
                return NotFound();

            return View(eventItem);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Event
                .Include(e => e.Bookings) // Navigation property assumed
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (eventItem == null)
                return NotFound();

            if (eventItem.Bookings != null && eventItem.Bookings.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete this event because it has existing bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Event.Remove(eventItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int eventID)
        {
            return _context.Event.Any(e => e.EventID == eventID);
        }

        private void PopulateVenueDropdown(int? selectedVenueId = null)
        {
            var venues = _context.Venue.ToList();
            ViewData["VenueID"] = new SelectList(venues, "VenueID", "VenueName", selectedVenueId);
        }
    }
}
