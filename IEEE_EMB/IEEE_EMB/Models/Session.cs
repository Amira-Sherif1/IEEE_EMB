using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public class Session
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Task cannot exceed 1000 characters")]
        public string? Task { get; set; }

        [StringLength(2000, ErrorMessage = "Task answer cannot exceed 2000 characters")]
        public string? TaskAnswer { get; set; }

        [DataType(DataType.Url)]
        [StringLength(500, ErrorMessage = "Video URL cannot exceed 500 characters")]
        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
            ErrorMessage = "Please enter a valid URL")]
        public string? Video { get; set; }

        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "pdf,doc,docx", ErrorMessage = "Please upload a valid document file (pdf, doc, or docx)")]
        public string? Document { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; }

        public int ActivityId { get; set; }
    }
}
