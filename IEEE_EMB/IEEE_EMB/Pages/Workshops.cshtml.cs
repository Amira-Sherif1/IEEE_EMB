using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;


namespace IEEE_EMB.Pages
{

    public class WorkshopsModel : PageModel
    {
        public DB db { get; set; }
        public WorkshopsModel(DB dB)
        {
            db = dB;
        }

        DataTable WorkshopsTable { get; set; }
        DataTable ParticipantsCounterTable { get; set; }
        public List<ParticipantCounter> ParticipantsCounter { get; set; }

        public List<Activity> Workshops { get; set; }

        public void OnGet()
        {
            WorkshopsTable = db.GetWorkshops();
            ParticipantsCounterTable = db.GetParticipantCount();
            Workshops = new List<Activity>();
            ParticipantsCounter = new List<ParticipantCounter>();
            foreach (DataRow row in WorkshopsTable.Rows)
            {
                Workshops.Add(new Activity
                {
                    Id = (int)row["ID"],
                    Title = row["TITLE"].ToString(),
                    Capacity = (int)row["Capacity"],
                    startdate = DateOnly.FromDateTime(Convert.ToDateTime(row["START_DATE"])),
                    Description = Convert.ToString(row["DESCRIPTION"]) == "" ? "No Description Yet!" : row["DESCRIPTION"].ToString()!,
                    status = row["STATUS"].ToString(),
                    mentorName = row["NAME"].ToString()
                });
            }

            foreach (DataRow row in ParticipantsCounterTable.Rows)
            {
                ParticipantsCounter.Add(new ParticipantCounter
                {
                    activityId = (int)row["ACTIVITY_ID"],
                    counter = (int)row["ParticipantsCount"]
                });
            }

            var participantCountDict = ParticipantsCounter.ToDictionary(p => p.activityId, p => p.counter);
            foreach (var workshop in Workshops)
            {
                if(participantCountDict.TryGetValue(workshop.Id, out int count))
                {
                    workshop.participantsCounter = count;
                }
                else
                {
                    workshop.participantsCounter = 0;
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




    }
}



