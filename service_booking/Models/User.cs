using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class User
    {
        [Required]
        public string? name { get; set; }

        [Required]
        public string? phone { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
