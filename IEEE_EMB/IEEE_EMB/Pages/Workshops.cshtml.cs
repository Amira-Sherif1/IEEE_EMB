using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace IEEE_EMB.Pages
{

    public class WorkshopsModel : PageModel
    {
        public List<Activity> Workshops { get; set; }

        public void OnGet()
        {
            Workshops = new List<Activity>
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



