using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

public class AdminBookingController : Controller
{
    private readonly AdminBookingDB _db;

    public AdminBookingController(AdminBookingDB db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var bookings = _db.GetAllBookings(); 
        return View(bookings);
    }

    [HttpPost]
    public IActionResult UpdateStatus(int booking_id, string booking_status)
    {
        _db.UpdateBookingStatus(booking_id, booking_status);

        return RedirectToAction(nameof(Index));
    }
}
