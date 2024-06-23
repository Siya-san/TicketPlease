using BugTracker.Models;
using BugTracker.Services;
using BugTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using React.TinyIoC;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        TicketServices ticketServices = new TicketServices();
        [HttpGet]
        [Authorization("Administrator", "QA", "PM")]
        public IActionResult ViewTicket(int Id)
        {

            return View(ticketServices.GetTicketById(Id));
        }
        [HttpGet]
        [Authorization("Administrator", "QA")]
         public IActionResult CreateBugTicket()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewBugTicket(TicketViewModel ticket)
        {
          
                ticketServices.CreateBugTicket(ticket);
                return RedirectToAction("Index", "Home");
         
        }
        
    

        public IActionResult CreateTestTicket()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Administrator", "QA")]
        public IActionResult CreateNewTestTicket(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                ticketServices.CreateTestTicket(ticket);
                return RedirectToAction("Index", "Home");
            }
            return View(ticket);
        }


        [Authorization("Administrator", "PM")]
        public IActionResult CreateRequestTicket()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Administrator", "PM")]
        public IActionResult CreateNewRequestTicket(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                ticketServices.CreateRequestTicket(ticket);
                return RedirectToAction("Index", "Home");
            }
            return View(ticket);
        }

        [HttpGet]
        [Authorization("Administrator", "PM")]
        public IActionResult DeleteTicket(int id)
        {
    
            return View(ticketServices.GetTicketById(id));
        }
        [HttpPost]
        public IActionResult ProcessTicketDelete(int id )
        {
       
                ticketServices.DeleteTicket(id);
                return RedirectToAction("Index", "Home");
            
     
        }
        [HttpGet]
        [Authorization("Administrator", "PM")]
        public IActionResult ResolveTicket(int id)
        {

            return View(ticketServices.GetTicketById(id));
        }
        [HttpPost]
        public IActionResult ProcessTicketResolve(int id)
        {

            ticketServices.ResolveTicket(id);
            return RedirectToAction("Index", "Home");


        }
        [HttpPost]
        public IActionResult ProcessTicketEdit(TicketViewModel ticket, int Id)
        {

            ticketServices.UpdateTicket(ticket,Id);
            return RedirectToAction("Index", "Home");


        }
        [HttpGet]
        [Authorization("Administrator", "PM")]
        public IActionResult EditTicket(int id)
        {

            return View(ticketServices.GetTicketById(id));
        }
    }
}
