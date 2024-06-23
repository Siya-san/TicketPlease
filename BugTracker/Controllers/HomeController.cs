using BugTracker.Models;
using BugTracker.Services;
using BugTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserServices userServices = new UserServices();
        TicketServices ticketServices = new TicketServices();
        Util util = new Util();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new UserTicketViewModel {
                bugsTickets = ticketServices.Read(TicketViewModel.TicketType.Bug), 
                requestTickets = ticketServices.Read(TicketViewModel.TicketType.Request),
                testTickets = ticketServices.Read(TicketViewModel.TicketType.Test),
                allUsers = userServices.Read() };

            return View(viewModel);
          
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [Authorization("QA")]
        public IActionResult CreateBug()
        {
            return View();
        }
       
       
        [HttpGet]
        [Authorization("Administrator")]
        public IActionResult ViewAllUsers()
        {
            List<UserViewModel> users = userServices.Read();
            return View(users);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
