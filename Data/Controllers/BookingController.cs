using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLDV6211_PART1_BOOKING_APP.Models;
using CLDV6211_PART1_BOOKING_APP.Models.Views;

namespace CLDV6211PART_1_App.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, int? eventTypeId, DateTime? startDate, DateTime? endDate, bool? isAvailable)
        {
            var bookings = _context.Booking
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                bookings = bookings.Where(b =>
                    b.BookingID.ToString().Contains(search) ||
                    b.Event.EventName.ToLower().Contains(search));
            }

            if (eventTypeId.HasValue)
            {
                bookings = bookings.Where(b => b.Event.EventTypeID == eventTypeId);
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate >= startDate);
            }

            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate <= endDate);
            }

            if (isAvailable.HasValue)
            {
                bookings = bookings.Where(b => b.Venue.IsAvailable == isAvailable.Value);
            }

            ViewData["EventTypes"] = new SelectList(await _context.EventType.ToListAsync(), "EventTypeID", "Name");

            return View(await bookings.ToListAsync());
        }

        public async Task<IActionResult> BookingDetails(string searchTerm)
        {
            var bookingsQuery = _context.Booking
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                .Select(b => new BookingDetailsViewModel
                {
                    BookingID = b.BookingID,
                    EventName = b.Event!.EventName,
                    EventDate = b.Event.EventDate,
                    VenueName = b.Venue!.VenueName,
                    Location = b.Venue.Location,
                    BookingDate = b.BookingDate,
                    EventTypeName = b.Event.EventType.Name
                });

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                bookingsQuery = bookingsQuery.Where(b =>
                    b.EventName.ToLower().Contains(searchTerm) ||
                    b.BookingID.ToString().Contains(searchTerm));
            }

            var result = await bookingsQuery.ToListAsync();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            return booking == null ? NotFound() : View(booking);
        }

        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventName");
            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "VenueName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (booking.BookingDate.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("BookingDate", "Booking date cannot be in the past.");
            }

            if (!_context.Event.Any(e => e.EventID == booking.EventID))
                ModelState.AddModelError("EventID", "Selected event does not exist.");

            if (!_context.Venue.Any(v => v.VenueID == booking.VenueID))
                ModelState.AddModelError("VenueID", "Selected venue does not exist.");

            bool isBooked = await _context.Booking
                .AnyAsync(b => b.VenueID == booking.VenueID &&
                               b.BookingDate.Date == booking.BookingDate.Date);

            if (isBooked)
            {
                ViewData["Error"] = "This venue is already booked on the selected date.";
            }

            if (ModelState.IsValid)
            {
                booking.Status = "Pending";
                try
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please check your data and try again.");
                }
            }

            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingID) return NotFound();

            if (!_context.Event.Any(e => e.EventID == booking.EventID))
                ModelState.AddModelError("EventID", "Selected event does not exist.");

            if (!_context.Venue.Any(v => v.VenueID == booking.VenueID))
                ModelState.AddModelError("VenueID", "Selected venue does not exist.");

            bool isBooked = await _context.Booking
                .AnyAsync(b => b.VenueID == booking.VenueID &&
                               b.BookingDate.Date == booking.BookingDate.Date &&
                               b.BookingID != booking.BookingID);

            if (isBooked)
                ModelState.AddModelError("", "This venue is already booked on the selected date.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Booking.Any(e => e.BookingID == booking.BookingID))
                        return NotFound();
                    else
                    {
                        ModelState.AddModelError("", "The booking was modified by another user. Please reload the data and try again.");
                        return View(booking);
                    }
                }
            }

            ViewData["EventID"] = new SelectList(_context.Event, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venue, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
