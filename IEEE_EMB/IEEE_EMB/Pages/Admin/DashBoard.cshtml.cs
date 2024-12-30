using ChartExample.Models.Chart;
using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class DashBoardModel : PageModel
    {
        public DB db { get; set; }
        public int NA { get; set; }
        public int NMEN { get; set; }
        public int NMEM { get; set; }
        public int NP { get; set; }
        public string[] ActivitiesLabel { get; set; } = Array.Empty<string>();
        public string[] ActivitiesDistribution { get; set; } = Array.Empty<string>();
        public DataTable part_per_activity { get; set; }
        public string[] months { get; set; } = Array.Empty<string>();

        public int[] MonthlyWorkshops { get; set; } = new int[12];
        public int[] MonthlySeminars { get; set; } = new int[12];
        public int[] MonthlyJournalClubs { get; set; } = new int[12];
        // public DataTable TopParticipants { get; set; }
        // public DataTable TopActivities { get; set; }
       
        public string[] TopParticipantLabels { get; set; }
        public string[] TopParticipantCounts { get; set; }
        public string[] TopActivityLabels { get; set; }
        public string[] TopActivityCounts { get; set; }
        public DashBoardModel(DB db)
        {
            this.db = db;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {

                // Retrieve counts
                NA = db.NumAdmins();
                NMEN = db.NumMentors();
                NMEM = db.NumMembers();
                NP = db.NumParticipant();

                // Retrieve participants per activity
                part_per_activity = db.ParticipantsPerActivity() ?? new DataTable();

                // participants per activity /////


                var labels = new List<string>();
                var distributions = new List<string>();

                foreach (DataRow row in part_per_activity.Rows)
                {
                    labels.Add(row[0].ToString());
                    distributions.Add(row[1].ToString());

                    ActivitiesLabel = labels.ToArray();
                    ActivitiesDistribution = distributions.ToArray();
                }
                ///////////////////// End ////////////////////////////

                //////////// Activities along the year ///////////////
                months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            for (int month = 1; month <= 12; month++)
            {
                MonthlyJournalClubs[month - 1] = db.NumOfActivityPerMonth(month, "JournalClub");
                MonthlySeminars[month - 1] = db.NumOfActivityPerMonth(month, "Seminar");
                MonthlyWorkshops[month - 1] = db.NumOfActivityPerMonth(month, "Workshop");
            }
            //////////////////////////////////////////////////////

            //TopParticipants =db.TopFiveParticipants() ?? new DataTable();
            //TopActivities=db.TopFiveActivities() ?? new DataTable();

            var TopParticipants = db.TopFiveParticipants() ?? new DataTable();
            var topParticipantLabels = new List<string>();
            var topParticipantCounts = new List<string>();

            foreach (DataRow row in TopParticipants.Rows)
            {
                topParticipantLabels.Add(row[0].ToString());
                topParticipantCounts.Add(row[1].ToString());
            }

            TopParticipantLabels = topParticipantLabels.ToArray();
            TopParticipantCounts = topParticipantCounts.ToArray();

            var TopActivities = db.TopFiveActivities() ?? new DataTable();
            var topActivityLabels = new List<string>();
            var topActivityCounts = new List<string>();

            foreach (DataRow row in TopActivities.Rows)
            {
                topActivityLabels.Add(row[0].ToString());
                topActivityCounts.Add(row[1].ToString());
            }

            TopActivityLabels = topActivityLabels.ToArray();
            TopActivityCounts = topActivityCounts.ToArray();


                ///// 
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }

           
            
        }
    }
}
