using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    
    public class AddActivityModel : PageModel
    {
        DB db;
        private readonly ILogger<AddActivityModel> _logger;
        
        public AddActivityModel(DB dB) {  db = dB; }
        [BindProperty]
        public Activity activity { get; set; }
        public Assign assign { get; set; } // to be completed later.
        public List<Mentor> mentors { get; set; }
        public Mentor AddedMentor { get; set; }
        public DataTable mentorsTable { get; set; }
        public void OnGet()
        {
            
            mentorsTable = db.GetMentorsNames();
            
            mentors = new List<Mentor>();
            activity = new Activity();

            foreach (DataRow row in mentorsTable.Rows) // I made this foreach to allow the admin to choose which mentor he will assign for a certain activity
            {

                mentors.Add(new Mentor 
                {
                    Name = row["NAME"].ToString() 
                });

            }
        }
        public IActionResult OnPost()
        {
           
            assign = new Assign();
           
            

            db.AddActivity(activity);
            assign.AddminSSN = HttpContext.Session.GetString("SSN");
            assign.MentorSSN = db.GetMentor(activity.mentorName);
            int ActivityID = db.GetActivityID(activity.Title);
            if(ActivityID != -1) { assign.ActivityId = ActivityID; }
            db.AddAssignment(assign);
            return RedirectToPage("/Admin/Activities");
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("AuthenticationString");
            HttpContext.Session.Remove("SSN");
            HttpContext.Session.Remove("Email");

            return RedirectToPage("/Login");
        }
    }
}
