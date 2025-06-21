using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLDV6211_PART1_BOOKING_APP.Models;

namespace CLDV6211PART_1_App.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venue/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venue.ToListAsync());
        }

        // GET: Venue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Please fill in all required fields.");
            return View(venue);
        }

        // GET: Venue/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // POST: Venue/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (id != venue.VenueID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                        return NotFound();
                    throw;
                }
            }

            return View(venue);
        }

        // GET: Venue/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // POST: Venue/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venue
                .Include(v => v.Events)
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(v => v.VenueID == id);

            if (venue == null)
                return NotFound();

            if ((venue.Events != null && venue.Events.Any()) || 
                (venue.Bookings != null && venue.Bookings.Any()))
            {
                TempData["ErrorMessage"] = "Cannot delete this venue as it has associated events or bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueID == id);
        }
    }
}
