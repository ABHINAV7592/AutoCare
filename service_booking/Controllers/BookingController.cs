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

        public BookingController(VehicleRegDB v, ServiceDB s, SlotDB sl, BookingDB b)
        {
            _vehicleDB = v;
            _serviceDB = s;
            _slotDB = sl;
            _bookingDB = b;
        }

        [HttpGet]
        public IActionResult Create()
        {
            int userId = 1;

            var vm = new BookServiceView
            {
                Vehicles = _vehicleDB.GetVehicles(userId),
                Services = _serviceDB.GetServices(),
                Slots = _slotDB.GetAvailableSlots()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(BookServiceView model)
        {
            int userId = 1;
            _bookingDB.InsertBooking(userId, model);
            return RedirectToAction("History");
        }

        public IActionResult History()
        {
            int userId = 1;
            var history = _bookingDB.GetBookingHistory(userId);
            return View(history);
        }
    }
}
