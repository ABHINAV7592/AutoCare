using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace service_booking.Models
{
    public class Login
    {
        [Required]
        public int reg_id { get; set; }

        [Required]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }

        [Required]
        public string? login_type { get; set; }
    }
}
