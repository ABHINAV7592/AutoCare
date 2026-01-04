using Microsoft.AspNetCore.Mvc;
using service_booking.Models;

namespace service_booking.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserDB _db;

        public LoginController(UserDB db)
        {
            _db = db;
        }

      
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Index(Models.Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _db.Login(model.email, model.password);

            if (result == null)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View(model);
            }

            
            if (result.login_type == "User")
                return RedirectToAction("Index", "UserHome");

            if (result.login_type == "Mechanic")
                return RedirectToAction("Index", "MechanicHome");

            ViewBag.Message = "Invalid login type";
            return View(model);
        }
    }
}
