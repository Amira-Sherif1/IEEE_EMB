using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Globalization;

namespace IEEE_EMB.Pages
{
    
    public class SeminarsModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string searchTerm { get; set; }
        public DB db { get; set; }
        public SeminarsModel(DB db)
        {
            this.db = db;
        }
        public List<Activity> Seminars { get; set; }
        public List<ParticipantCounter> Participants { get; set; }
      
        public DataTable seminarTable { get; set; }
        public DataTable participantCountTable { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                seminarTable = db.SearchForSeminar(searchTerm) ?? new DataTable();
            }
            else
            {
                seminarTable = db.GetSeminar() ?? new DataTable();
            }
            //seminarTable = db.GetSeminar();
            participantCountTable = db.GetParticipantCount();
            Seminars = new List<Activity>();
            Participants = new List<ParticipantCounter>();

           
            foreach (DataRow row in seminarTable.Rows)
            {
                Seminars.Add(new Activity
                {
                    Id = (int)row["ID"],
                    Title = row["TITLE"]?.ToString() ?? "No Title",
                    Capacity = row["Capacity"] != DBNull.Value ? (int)row["Capacity"] : 0,
                    startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                    Description = Convert.ToString(row["DESCRIPTION"]) == "" ? "No Description Yet!" : row["DESCRIPTION"].ToString(),
                    status = row["STATUS"].ToString(),
                    mentorName = row["NAME"]?.ToString() ?? "Unknown Instructor",
                });
            }

           
            foreach (DataRow row in participantCountTable.Rows)
            {
                Participants.Add(new ParticipantCounter
                {
                    activityId = (int)row["ACTIVITY_ID"],
                    counter = (int)row["ParticipantsCount"]
                });
            }

           
            var participantCountDict = Participants.ToDictionary(p => p.activityId, p => p.counter);

           
            foreach (var seminar in Seminars)
            {
                if (participantCountDict.TryGetValue(seminar.Id, out int count))
                {
                    seminar.participantsCounter = count;
                }
                else
                {
                    seminar.participantsCounter = 0; // Default value if no participants found
                }
            }
        }

        public IActionResult OnPostSearch(String search)
        {
            return RedirectToPage("/Seminars" ,new { searchTerm = search });
        }
        public double GetPercentage(int current, int max) // Get Participance Percentage per seminar
        {
            return ((double)current / max) * 100;
        }

        public int GetRemainingSlots(int registered, int capacity)
        {
            return (capacity - registered);
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