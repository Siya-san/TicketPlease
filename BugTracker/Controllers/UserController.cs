using BugTracker.Models;
using BugTracker.Services;
using BugTracker.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class UserController : Controller

    {
        UserServices userServices = new UserServices();
        Util util = new Util();
        


        public IActionResult Login()
        {
            if (util.IsUserTableEmpty())
            {
                userServices.createAdmin();
            }
            return View("Login");
        }
      
        public IActionResult ProcessLogin(UserViewModel user)
        {
            var loggedInUser = userServices.Login(user);
            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("Username", loggedInUser.username);
                HttpContext.Session.SetString("UserRole", loggedInUser.type.ToString());
                HttpContext.Session.SetString("dpw", loggedInUser.defaultPw.ToString());
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
        [HttpGet]
       public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessChangePassword(UserViewModel user,int id)
        {
            if (ModelState.IsValid)
            {
                userServices.UpdatePassword(user, id);
                return RedirectToAction("Index", "Home");
            }
            return View("ChangePassword",user);
        }
        [HttpGet]
        [Authorization("Administrator")]
        public IActionResult ViewAllUsers()
        {
            List<UserViewModel> users = userServices.Read();
            return View(users);
        }
    }
}
