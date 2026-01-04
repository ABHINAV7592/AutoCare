using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class ServiceType
    {
        public int service_type_id { get; set; }
        [Required]
        public string? service_name { get; set; }

        [Required]
        public string? description { get; set; }

        [Required]
        public decimal cost { get; set; }
    }
}
