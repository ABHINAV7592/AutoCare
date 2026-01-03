using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class MechanicReg : Controller
    {
        private readonly MechanicDB _db;

        public MechanicReg(MechanicDB db)
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

           
            int emailCount = _db.GetEmailCount(clsobj.email,clsobj.password);

            if (emailCount > 0)
            {
                ViewBag.Message = "Email Already Exists";
                return View("mechanic_register", clsobj);
            }

            int mid = _db.GetMaxRegId();
            int regid = (mid == 0) ? 1 : mid + 1;

            _db.InsertMechanic(clsobj.name, clsobj.phone, clsobj.expertise);

            _db.InsertLogin(regid, clsobj.email, clsobj.password);

            ViewBag.Message = "Mechanic Registered Successfully";
            ModelState.Clear();

            return View("mechanic_register");
        }
    }
}
