using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace service_booking.Models
{
    public class Vehicle
    {
        public int vehicle_id { get; set; }

        [Required]
        public string? vehicle_number { get; set; }

        [Required]
        public string? model { get; set; }

        [Required]
        public string? brand { get; set; }

        public int user_id { get; set; }
    }
}
