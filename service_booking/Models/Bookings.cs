using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class Bookings
    {
        [Required]
        public int user_id { get; set; }

        [Required]
        public int vehicle_id { get; set; }

        [Required]
        public int service_type_id { get; set; }

        [Required]
        public int slot_id { get; set; }

        [Required]
        public string? booking_status { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
