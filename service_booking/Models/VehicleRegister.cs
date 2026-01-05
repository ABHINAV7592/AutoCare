using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class VehicleRegister
    {
        public int vehicle_id { get; set; }
        public int user_id { get; set; }

        [Required(ErrorMessage = "Vehicle number is required")]
        public string? vehicle_number { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public string? brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string? model { get; set; }

        [Required(ErrorMessage = "Manufacturing year is required")]
        public int manufacturing_year { get; set; }
    }
}
