using Microsoft.AspNetCore.Mvc;

namespace service_booking.Controllers
{
    public class MechanicHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
