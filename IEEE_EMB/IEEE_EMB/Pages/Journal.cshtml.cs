using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages
{
    public class JournalModel : PageModel
    {
        DB db {  get; set; }
        public JournalModel(DB dB) { db = dB; }
        public DataTable JournalClubTable { get; set; }
        public DataTable ParticipantCounterTable { get; set; }
        public List<ParticipantCounter> participants { get; set; }
        public List<Activity> Journals { get; set; }

        public void OnGet()
        {
            JournalClubTable = db.GetJournalClubs();
            ParticipantCounterTable = db.GetParticipantCount();
            Journals = new List<Activity>();
            participants = new List<ParticipantCounter>();
            foreach(DataRow row in JournalClubTable.Rows)
            {
                Journals.Add(new Activity
                {
                    Id = (int)row["ID"],
                    Title = row["TITLE"].ToString(),
                    Capacity = (int)row["Capacity"],
                    startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                    Description = Convert.ToString(row["DESCRIPTION"]) == "" ? "No Description Yet!" : row["DESCRIPTION"].ToString(),
                    status = row["STATUS"].ToString(),
                    mentorName = row["NAME"].ToString()
                });
            }

            foreach(DataRow row in ParticipantCounterTable.Rows)
            {
                participants.Add(new ParticipantCounter
                {
                    activityId = (int)row["ACTIVITY_ID"],
                    counter = (int)row["ParticipantsCount"]
                });
            }

            var participantsCountDict = participants.ToDictionary(p => p.activityId, p => p.counter);
            foreach(var activity in Journals)
            {
                if(participantsCountDict.TryGetValue(activity.Id, out int count))
                {
                    activity.participantsCounter = count;
                }
                else
                {
                    activity.participantsCounter = 0;
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
