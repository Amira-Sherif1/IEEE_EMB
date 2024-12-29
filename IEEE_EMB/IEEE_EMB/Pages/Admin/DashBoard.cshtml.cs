using ChartExample.Models.Chart;
using IEEE_EMB.Models;
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
        public DataTable TopParticipants { get; set; }
        public DataTable TopActivities { get; set; }
        public DashBoardModel(DB db)
        {
            this.db = db;
        }

        public void OnGet()
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
            
            TopParticipants =db.TopFiveParticipants() ?? new DataTable();
            TopActivities=db.TopFiveActivities() ?? new DataTable();


            ///// 
            
        }
    }
}
