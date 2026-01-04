using Microsoft.AspNetCore.Mvc;

namespace service_booking.Controllers
{
    public class AdminHomeController : Controller
    {
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
