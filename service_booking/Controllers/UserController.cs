using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDB _db;

        public UserController(UserDB db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Register(User clsobj)
        {
            // 1️⃣ Get max reg_id
            int mid = _db.GetMaxRegId();

            int regid = (mid == 0) ? 1 : mid + 1;

            // 2️⃣ Check email existence
            int emailCount = _db.GetEmailCount(clsobj.email, clsobj.password);

            if (emailCount == 0)
            {
                // 3️⃣ Insert into Users table
                _db.InsertUser(clsobj.name, clsobj.phone);

                // 4️⃣ Insert into Login table
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

