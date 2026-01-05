using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserDB _db;
        private readonly MechanicDB _mechanicDB;

        public LoginController(UserDB db, MechanicDB mechanicDB)
        {
            _db = db;
            _mechanicDB = mechanicDB;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _db.Login(model.email, model.password);

            if (result == null)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", result.user_id);
            HttpContext.Session.SetString("Role", result.login_type);

            if (result.login_type == "User")
                return RedirectToAction("Index", "UserHome");

            if (result.login_type == "Mechanic")
            {
                string expertise = _mechanicDB.GetExpertiseByMechanicId(result.user_id);
                HttpContext.Session.SetString("Expertise", expertise);

                return RedirectToAction("Index", "MechanicHome");
            }

            if (result.login_type == "Admin")
                return RedirectToAction("Index", "AdminHome");

            ViewBag.Message = "Invalid role";
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
