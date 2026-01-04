using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class TimeSlot
    {
        public int slot_id { get; set; }
        [Required]
        public DateTime slot_date { get; set; }

        [Required]
        public TimeSpan start_time { get; set; }

        [Required]
        public TimeSpan end_time { get; set; }

        [Required]
        public int max_bookings { get; set; }
    }
}
