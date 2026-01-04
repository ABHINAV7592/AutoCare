namespace service_booking.Models
{
    public class VehicleRegister
    {
        public int vehicle_id { get; set; }
        public int user_id { get; set; }

        public string? vehicle_number { get; set; }
        public string? brand { get; set; }
        public string? model { get; set; }
        public int manufacturing_year { get; set; }
    }
}
