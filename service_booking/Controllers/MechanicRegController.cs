using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class MechanicRegController : Controller
    {
        private readonly MechanicDB _db;

        public MechanicRegController(MechanicDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("mechanic_register");
        }

        [HttpPost]
        public IActionResult Register(Mechanic clsobj)
        {
            if (!ModelState.IsValid)
            {
                return View("mechanic_register", clsobj);
            }

            int emailCount = _db.CheckEmailExists(clsobj.email);

            if (emailCount > 0)
            {
                ViewBag.Message = "Email already exists";
                return View("mechanic_register", clsobj);
            }

            int mechanicId = _db.InsertMechanic(
                clsobj.name,
                clsobj.phone,
                clsobj.expertise
            );

            _db.InsertLogin(mechanicId, clsobj.email, clsobj.password);

            ViewBag.Message = "Mechanic registered successfully";
            ModelState.Clear();

            return View("mechanic_register");
        }
    }
}
