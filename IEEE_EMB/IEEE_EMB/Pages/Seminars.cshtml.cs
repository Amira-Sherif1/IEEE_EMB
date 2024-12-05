using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages
{
    public class SeminarsModel : PageModel
    {
        public List<Activity> Seminars { get; set; }
       
        
        // public string Hamada { get; set; } = "Hamada";
        public void OnGet()
        {
            // Mock data - In a real application, this would come from a database
            Seminars = new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    Title = "Advanced Web Development with ASP.NET Core",
                    Capacity = 100,
                    Type = "Seminar",
                },
                new Activity
                {
                    Id = 2,
                    Title = "Machine Learning Fundamentals",
                    Capacity = 100,
                    Type = "Seminar",
                }
                // Add more seminars as needed
            };
        }
    }
}
