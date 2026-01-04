using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class Bookings
    {
        public int booking_id { get; set; }
        public int user_id { get; set; }
        public int vehicle_id { get; set; }
        public int service_type_id { get; set; }
        public int slot_id { get; set; }
        public string? booking_status { get; set; }
        public DateTime date { get; set; }
    }
}
