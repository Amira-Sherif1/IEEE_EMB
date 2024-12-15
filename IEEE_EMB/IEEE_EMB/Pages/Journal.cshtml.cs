using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages
{
    public class JournalModel : PageModel
    {
        public List<Activity> Journal { get; set; }

        public void OnGet()
        {
            Journal = new List<Activity>
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


            };

        }
    }
}
