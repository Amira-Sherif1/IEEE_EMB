using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages
{
    
    public class SeminarsModel : PageModel
    {
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
            seminarTable = db.GetSeminar();
            participantCountTable = db.GetParticipantCount();
            Seminars = new List<Activity>();
            Participants = new List<ParticipantCounter>();

           
            foreach (DataRow row in seminarTable.Rows)
            {
                Seminars.Add(new Activity
                {
                    Id = (int)row["ID"],
                    Title = row["TITLE"].ToString(),
                    Capacity = (int)row["Capacity"],
                    startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                    Description = Convert.ToString(row["DESCRIPTION"]) == "" ? "No Description Yet!" : row["DESCRIPTION"].ToString(),
                    mentorName = row["NAME"].ToString()
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