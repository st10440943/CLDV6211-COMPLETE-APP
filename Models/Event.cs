using CLDV6211_PART1_BOOKING_APP.Models;
using CLDV6211PART_1_App.Models;
using System;

namespace CLDV6211_PART1_BOOKING_APP.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int VenueID { get; set; }
        public Venue Venue { get; set; }  // Navigation Property

        // Add EventType foreign key and navigation property
        public int EventTypeID { get; set; }
        public EventType? EventType { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

