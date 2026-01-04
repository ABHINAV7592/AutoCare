using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class AdminServiceController : Controller
    {
        private readonly ServiceDB _db;

        public AdminServiceController(ServiceDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var services = _db.GetServices();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _db.InsertService(model);
            return RedirectToAction("Index");
        }
    }
}
