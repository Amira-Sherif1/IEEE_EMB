using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    
    public class ActivitiesModel : PageModel
    {
        public DB db { get; set; }
        public ActivitiesModel(DB dB) { db = dB; }
        public List<Activity> activities { get; set; }
        public DataTable activitiesTable { get; set; }
        public void OnGet()
        {
            if(HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {

            activitiesTable = db.GetActvities();
            activities = new List<Activity>();
            foreach (DataRow row in activitiesTable.Rows)
            {
                    activities.Add(new Activity
                    {
                        Id = (int)row["ID"],
                        Title = row["TITLE"].ToString(),
                        startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                        Enddate = DateOnly.FromDateTime(Convert.ToDateTime(row["END_DATE"])),
                        Capacity = (int)row["Capacity"],
                        Type = row["TYPE"].ToString(),
                        status = row["STATUS"].ToString(),
                        mentorName = row["NAME"].ToString()
                    });
            }
            }
            else
            {
                var MentorId = HttpContext.Session.GetString("SSN");
                activitiesTable = db.GetActivitiesForMentor(MentorId) ?? new DataTable();
                activities = new List<Activity>();
                foreach (DataRow row in activitiesTable.Rows)
                {
                    activities.Add(new Activity
                    {
                        Id = (int)row["ID"],
                        Title = row["TITLE"].ToString(),
                        startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                        Enddate = DateOnly.FromDateTime(Convert.ToDateTime(row["END_DATE"])),
                        Capacity = (int)row["Capacity"],
                        Type = row["TYPE"].ToString(),
                        status = row["STATUS"].ToString(),
                        mentorName = row["NAME"].ToString()
                    });
                }

            }
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/Admin/Activities");
        }
        public IActionResult OnPostDelete(int itemId)
        {
            db.DeleteActivity(itemId);
            return RedirectToPage();
        }
    }
}
