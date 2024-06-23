

namespace BugTracker.Models
{
    public class UserViewModel
    {
       public enum UserType
        {
            QA,
            RD,
            PM,
            Administrator
        }
     public int Id { get; set; }
     public UserType type { get; set; }
     public string username { get; set; }
     public string password { get; set; }
        
        public bool defaultPw { get; set; }

    }   
}
