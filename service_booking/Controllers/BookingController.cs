using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class BookingController : Controller
    {
        private readonly VehicleRegDB _vehicleDB;
        private readonly ServiceDB _serviceDB;
        private readonly SlotDB _slotDB;
        private readonly BookingDB _bookingDB;

        public BookingController(
            VehicleRegDB vehicleDB,
            ServiceDB serviceDB,
            SlotDB slotDB,
            BookingDB bookingDB)
        {
            _vehicleDB = vehicleDB;
            _serviceDB = serviceDB;
            _slotDB = slotDB;
            _bookingDB = bookingDB;
        }

        [HttpGet]
        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Login");

            var vm = new BookServiceView
            {
                Vehicles = _vehicleDB.GetVehicles(userId.Value),

                Services = _serviceDB.GetServices(),
                Slots = _slotDB.GetAvailableSlots()
            };

            return View(vm);
        }
        //
         
        [HttpPost]
        public IActionResult Create(BookServiceView model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Login");

            // 🔴 DEBUG CHECK
            if (model.vehicle_id == 0 || model.service_type_id == 0 || model.slot_id == 0)
            {
                throw new Exception(
                    $"Binding failed: V={model.vehicle_id}, S={model.service_type_id}, Slot={model.slot_id}"
                );
            }

            _bookingDB.InsertBooking(userId.Value, model);
            return RedirectToAction("History");
        }


        [HttpGet]
        public IActionResult History()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Login");

            var history = _bookingDB.GetBookingHistory(userId.Value);

            return View(history);
        }
    }
}
