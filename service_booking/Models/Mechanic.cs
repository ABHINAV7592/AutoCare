using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class Mechanic
    {
        [Required]
        public string? name { get; set; }

        [Required]
        public string? phone { get; set; }

        [Required]
        public string? expertise { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
