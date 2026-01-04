namespace service_booking.Models
{
    public class AdminBookingView
    {
        public int booking_id { get; set; }
        public string? user_name { get; set; }
        public string? vehicle_number { get; set; }
        public string? service_name { get; set; }
        public string? slot_time { get; set; }
        public string? booking_status { get; set; }
        public string? mechanic_name { get; set; }
    }
}
