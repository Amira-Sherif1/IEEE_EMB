using IEEE_EMB.Models.Utilities;
using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public struct ParticipantCounter
    {
        public int activityId { get; set; }
        public int counter { get; set; }
    }

    public class Activity
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateOnly startdate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateOnly Enddate { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^(Pending|Active|Completed|Cancelled)$", ErrorMessage = "Invalid status")]
        public string status { get; set; } = ActivityStatus.Pending;

        [Required(ErrorMessage = "Mentor name is required")]
        [StringLength(100, ErrorMessage = "Mentor name cannot exceed 100 characters")]
        public string mentorName { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Participants counter cannot be negative")]
        public int participantsCounter { get; set; }

        // Custom validation for date range
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Enddate < startdate)
            {
                yield return new ValidationResult(
                    "End date must be after start date",
                    new[] { nameof(Enddate) });
            }

            if (participantsCounter > Capacity)
            {
                yield return new ValidationResult(
                    "Participants count cannot exceed capacity",
                    new[] { nameof(participantsCounter) });
            }
        }
    }


}
