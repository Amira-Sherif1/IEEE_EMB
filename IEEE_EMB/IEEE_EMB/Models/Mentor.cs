using IEEE_EMB.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public class Mentor
    {
        public const string AuthenticationString = "Mentor";
        [Required(ErrorMessage = "SSN is required")]
        public string SSN { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Education { get; set; }
        //[Required(ErrorMessage = "CV is required")]
        public string CV { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string BIO { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string PersonalPhoto { get; set; }
        [Required(ErrorMessage = "University is required")]
        public string University { get; set; }
        public string Brief { get; set; }
        public string status { get; set; } = Status.Waiting;

    }
}
