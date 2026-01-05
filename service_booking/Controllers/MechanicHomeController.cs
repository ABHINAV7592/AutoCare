using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

public class MechanicHomeController : Controller
{
    private readonly BookingDB _bookingDB;

    public MechanicHomeController(BookingDB bookingDB)
    {
        _bookingDB = bookingDB;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Role") != "Mechanic")
            return RedirectToAction("Index", "Login");

        string expertise = HttpContext.Session.GetString("Expertise");

        var bookings = _bookingDB.GetBookingsForMechanic(expertise);

        return View(bookings);
    }

    [HttpPost]
    public IActionResult UpdateStatus(int booking_id, string booking_status)
    {
        _bookingDB.UpdateBookingStatus(booking_id, booking_status);
        return RedirectToAction("Index");
    }
}
