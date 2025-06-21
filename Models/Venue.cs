using System.Collections.Generic;

namespace CLDV6211_PART1_BOOKING_APP.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // New property to track availability
        public bool IsAvailable { get; set; } = true;

        public ICollection<Event>? Events { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
