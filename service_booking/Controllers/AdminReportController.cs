using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

public class AdminReportController : Controller
{
    private readonly AdminReportDB _db;
    public AdminReportController(AdminReportDB db) => _db = db;

    public IActionResult Index()
    {
        return View(_db.GetServiceReport());
    }
}
