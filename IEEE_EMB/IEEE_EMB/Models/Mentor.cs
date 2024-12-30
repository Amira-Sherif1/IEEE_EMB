using IEEE_EMB.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public class Mentor
    {
        public const string AuthenticationString = "Mentor";

        [Required(ErrorMessage = "SSN is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be exactly 14 characters")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "SSN must contain exactly 14 digits")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Education is required")]
        [StringLength(500, ErrorMessage = "Education details cannot exceed 500 characters")]
        public string Education { get; set; }

        [Required(ErrorMessage = "CV is required")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "pdf,doc,docx", ErrorMessage = "Please upload a valid CV file (pdf, doc, or docx)")]
        public string CV { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "BIO is required")]
        [StringLength(1000, ErrorMessage = "BIO cannot exceed 1000 characters")]
        public string BIO { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Personal photo is required")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Please upload a valid image file (jpg, jpeg, or png)")]
        public string PersonalPhoto { get; set; }
        public string University { get; set; }
        public string Brief { get; set; }
        public string status { get; set; } = Status.Waiting;
    }
}
