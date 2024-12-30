using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public class Participants
    {
        [Required(ErrorMessage = "SSN is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "SSN must be exactly 14 characters")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "SSN must contain exactly 14 digits")]
        public int SSN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public int Attendence_Count { get; set; }

        [Required(ErrorMessage = "University is required")]
        [StringLength(200, ErrorMessage = "University name cannot exceed 200 characters")]
        public string University { get; set; }


        public int MentorId { get; set; }


        public int TeamId { get; set; }
    }
}
