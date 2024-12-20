using IEEE_EMB.Models.Utilities;

namespace IEEE_EMB.Models
{
    public class Admin
    {
        public const string AuthenticationString = "Admin";
        public string SSN { get; set; }
        public string Name { get; set; }
        
        public string Phone { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
       
        public string CV { get; set; }
        public string Role { get; set; }
        public string PersonalPhoto { get; set; }

        public string Brief { get; set; }
        public string University { get; set; }
    }
}
