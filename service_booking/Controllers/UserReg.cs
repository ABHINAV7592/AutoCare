using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class UserReg : Controller
    {
        private readonly UserDB _db;

        public UserReg(UserDB db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("usrinsertpage_load");
        }


        [HttpPost]
        public IActionResult Register(Models.UserRegistration clsobj)
        {
            if (!ModelState.IsValid)
            {
                return View("usrinsertpage_load", clsobj);
            }

            int mid = _db.GetMaxRegId();
            int regid = (mid == 0) ? 1 : mid + 1;

            int emailCount = _db.GetEmailCount(clsobj.email, clsobj.password);

            if (emailCount == 0)
            {
                _db.InsertUser(clsobj.name, clsobj.phone);
                _db.InsertLogin(regid, clsobj.email, clsobj.password, "User");

                ViewBag.Message = "Successfully Inserted";
            }
            else
            {
                ViewBag.Message = "Email Already Exists";
            }

            return View("usrinsertpage_load", clsobj);
        }

    }
}

