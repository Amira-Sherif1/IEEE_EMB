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
        public AllParticipantsModel(DB db)
        {
            this.db = db;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                participants = db.GetParticipants();
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
