namespace service_booking.Models
{
    public class BookServiceView
    {
        public int vehicle_id { get; set; }
        public int service_type_id { get; set; }
        public int slot_id { get; set; }

        
        public List<VehicleRegister> Vehicles { get; set; }
        public List<ServiceType> Services { get; set; }
        public List<TimeSlot> Slots { get; set; }
    }
}
