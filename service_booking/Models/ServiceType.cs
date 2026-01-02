using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class ServiceType
    {
        [Required]
        public string? service_name { get; set; }

        [Required]
        public string? description { get; set; }

        [Required]
        public decimal cost { get; set; }
    }
}
