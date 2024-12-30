using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllParticipantsModel : PageModel
    {
        public DB db { get; set; }
        public DataTable participants { get; set; }
        public DataTable participantsInActivity { get; set; }
        public AllParticipantsModel(DB db)
        {
            this.db = db;
        }
        public void OnGet(int activityId = -1)
        {
            if (activityId == -1) {
                participants = db.GetParticipants();

            }
            else
            {
               participants = db.GetParticipantsInActivity(activityId);

            }
            
        }
    }
}
