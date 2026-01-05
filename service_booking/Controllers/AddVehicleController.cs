using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class AddVehicleController : Controller
    {
        private readonly VehicleRegDB _db;

        public AddVehicleController(VehicleRegDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(VehicleRegister mod)
        {
            if (!ModelState.IsValid)
                return View(mod);

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Login");

            mod.user_id = userId.Value;  

            _db.InsertVehicle(mod);

            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult List()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Login");

            var vehicles = _db.GetVehicles(userId.Value);
            return View(vehicles);
        }

    }
}
