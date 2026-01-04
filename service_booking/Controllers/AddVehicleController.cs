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
        public IActionResult Add(VehicleRegister model)
        {
            
            if (string.IsNullOrEmpty(model.vehicle_number))
            {
                return Content("Vehicle Number not received");
            }

            model.user_id = 1;

            _db.InsertVehicle(model);

            return RedirectToAction("List");
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
