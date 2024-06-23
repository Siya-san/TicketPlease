namespace BugTracker.Models
{
    public class UserTicketViewModel
    {
        public List<TicketViewModel> bugsTickets { get; set; }
        public List<TicketViewModel> testTickets { get; set; }
        public List<TicketViewModel> requestTickets { get; set; }
        public List<UserViewModel> allUsers { get; set; }
        public UserTicketViewModel()
        {
            bugsTickets = new List<TicketViewModel>();
            testTickets = new List<TicketViewModel>();
            requestTickets = new List<TicketViewModel>();
            allUsers = new List<UserViewModel>();
     
        }
    }
}
