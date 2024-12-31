using IEEE_EMB.Models.Utilities;
using System.ComponentModel.DataAnnotations;
namespace IEEE_EMB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class Member
    {
        public const string AuthenticationString = "Member";

        [Required(ErrorMessage = "SSN is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be exactly 14 characters")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "SSN must contain exactly 14 digits")]
        public string SSN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        
        public DateOnly Currentyear { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number")]

        
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Previous experience is required")]
        [StringLength(1000, ErrorMessage = "Previous experience cannot exceed 1000 characters")]
        public string PreviousExperience { get; set; }


        public string status { get; set; } = Status.Waiting;

        [Required(ErrorMessage = "Major is required")]
        [StringLength(50, ErrorMessage = "Major cannot exceed 50 characters")]
        public string Major { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? PersonalPhoto { get; set; }

        [Required(ErrorMessage = "Brief is required")]
        [StringLength(500, ErrorMessage = "Brief cannot exceed 500 characters")]
        public string Brief { get; set; }

        [Required(ErrorMessage = "University is required")]
        [StringLength(100, ErrorMessage = "University name cannot exceed 100 characters")]
        public string University { get; set; }
    }
}