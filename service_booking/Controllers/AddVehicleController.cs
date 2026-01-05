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
            {
                return View(mod);
            }

            mod.user_id = 1;

            _db.InsertVehicle(mod);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            int userId = 1;
            var vehicles = _db.GetVehicles(userId);
            return View(vehicles);
        }
    }
}
