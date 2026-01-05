using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class UserRegController : Controller
    {
        private readonly UserDB _db;

        public UserRegController(UserDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegistration clsobj)
        {
            if (!ModelState.IsValid)
            {
                return View(clsobj);
            }

            int emailCount = _db.GetEmailCount(clsobj.email, clsobj.password);
            if (emailCount > 0)
            {
                ViewBag.Message = "Email Already Exists";
                return View(clsobj);
            }

            int userId = _db.InsertUser(clsobj.name, clsobj.phone);

            _db.InsertLogin(
                userId,                 
                clsobj.email,
                clsobj.password,
                "User"
            );

            ViewBag.Message = "Successfully Registered";
            ModelState.Clear();

            return View();
        }
    }
}
