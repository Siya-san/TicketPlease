using BugTracker.Models;
using BugTracker.Services;
using BugTracker.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class UserController : Controller

    {
        UserServices userServices = new UserServices();
     
        public IActionResult Index(UserViewModel user)
        {

            return View(user);
        }


        public IActionResult Login(UserViewModel user)
        {
            return View("Login");
        }
        public IActionResult SignUp()
        {
            return View();

        }
        public IActionResult ProcessLogin(UserViewModel user)
        {
            var loggedInUser = userServices.Login(user);
            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("Username", loggedInUser.username);
                HttpContext.Session.SetString("UserRole", loggedInUser.type.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", user);
            }
        }

        [HttpGet]
        [Authorization("Administrator")]
        public IActionResult CreateUsers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewUsers(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                userServices.CreateNewUsers(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
