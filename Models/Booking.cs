using System.ComponentModel.DataAnnotations;
using CLDV6211PART_1_App.Models;

namespace CLDV6211_PART1_BOOKING_APP.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Event is required")]
        [Display(Name = "Event")]
        public int EventID { get; set; }

        [Required(ErrorMessage = "Venue is required")]
        [Display(Name = "Venue")]
        public int VenueID { get; set; }

        [Required(ErrorMessage = "Booking Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        // Navigation properties
        public Event? Event { get; set; }
        public Venue? Venue { get; set; }
    }
}
