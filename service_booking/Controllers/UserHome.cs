using Microsoft.AspNetCore.Mvc;

namespace service_booking.Controllers
{
    public class UserHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
