using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class AdminSlotController : Controller
    {
        private readonly SlotDB _db;

        public AdminSlotController(SlotDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var slots = _db.GetAllSlots();
            return View(slots);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TimeSlot model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _db.InsertSlot(model);
            return RedirectToAction("Index");
        }
    }
}
