using CLDV6211_PART1_BOOKING_APP.Models;

namespace CLDV6211PART_1_App.Models
{
    public class EventType
    {
        public int EventTypeID { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Event>? Events { get; set; }
    }
}