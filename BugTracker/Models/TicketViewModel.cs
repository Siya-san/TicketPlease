

namespace BugTracker.Models
{
    public class TicketViewModel
    {
        public enum TicketStatus
        {
            Underway,
            Resolved

        }
        public enum TicketSeverity {
            critical,
            major,
            moderate,
            minor,
            low
        }
        public enum TicketPriority
        {
            High,
            Medium,
            low,
            Non

        }
        public enum TicketType
        {
            Request,
            Test,
            Bug
        }
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public string TicketSummary { get; set; }
        public string TicketDescription { get; set; }
        public TicketSeverity Severity { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketType Type { get; set; }




    }
}
