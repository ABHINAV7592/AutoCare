using Microsoft.AspNetCore.Mvc;

namespace service_booking.Controllers
{
    public class UserHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
