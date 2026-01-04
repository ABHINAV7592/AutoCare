using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class MechanicHomeController : Controller
    {
        private readonly BookingDB _db;

        public MechanicHomeController(BookingDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookings = _db.GetAllBookingsForMechanic();
            return View(bookings);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int booking_id, string booking_status)
        {
            _db.UpdateBookingStatus(booking_id, booking_status);
            return RedirectToAction("Index");
        }
    }
}
