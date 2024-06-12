

namespace BugTracker.Models
{
    public class BugViewModel
    {
        enum BugStatus
        {
            Underway,
            Resolved

        }
        enum BugSeverity{
            critical,
            major,
            moderate,
            minor,
            low
        }
        enum BugPriority
        {
            High,
            Medium,
            low,
            Non

        }
        int id;
        BugStatus Status;
        string BugSummary;
        string BugDescription;
        string author;
        BugSeverity Severity;
        BugPriority Priority;

        
        
         
    }
}
