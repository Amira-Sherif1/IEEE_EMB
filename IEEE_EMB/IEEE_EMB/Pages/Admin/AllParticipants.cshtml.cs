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



        public IActionResult OnGet(int activityId = -1)
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                if (activityId == -1)
                {
                    participants = db.GetParticipants();

                }
                else
                {
                    participants = db.GetParticipantsInActivity(activityId);

                }

                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
         
        

        public IActionResult OnPostDelete(string participantSSN)
        {
            db.DeleteParticipant(participantSSN);
            return RedirectToPage();
        }
    }
}
