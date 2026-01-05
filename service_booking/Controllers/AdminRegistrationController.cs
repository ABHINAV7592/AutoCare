using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class AdminRegController : Controller
    {
        private readonly UserDB _db;

        public AdminRegController(UserDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int count = _db.GetEmailCount(model.email, model.password);
            if (count > 0)
            {
                ViewBag.Message = "Admin already exists";
                return View(model);
            }

            _db.InsertLogin(
                0,
                model.email,
                model.password,
                "Admin"
            );

            return RedirectToAction("Index", "Login");
        }
    }
}
