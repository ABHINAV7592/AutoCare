using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleDB _db;

        public VehicleController(VehicleDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Add(Vehicle model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    model.user_id = 1;


        //    _db.InsertVehicle(model);

        //    return RedirectToAction("List");
        //}
        [HttpPost]
        public IActionResult Add(Vehicle model)
        {
            if (model.manufacturing_year == 0)
                throw new Exception("Model binding failed: manufacturing_year = 0");

            model.user_id = 1;
            _db.InsertVehicle(model);

            return RedirectToAction("List");
        }



        [HttpGet]
        public IActionResult List()
        {
            int userId = 1; 

            var vehicles = _db.GetVehiclesByUser(userId);

            return View(vehicles);
        }
    }

}


