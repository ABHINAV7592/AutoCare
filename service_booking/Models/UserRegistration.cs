using System.ComponentModel.DataAnnotations;

namespace service_booking.Models
{
    public class UserRegistration
    {
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
