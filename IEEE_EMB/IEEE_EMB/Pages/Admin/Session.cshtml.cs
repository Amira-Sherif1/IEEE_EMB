using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class SessionModel : PageModel
    {
        public DB db { get; set; }
        public DataTable sessions { get; set; }

        [BindProperty]
        public int ActivityId { get; set; }
        public SessionModel(DB db)
        {
            this.db = db;
            
        }
        public void OnGet(int activityId)
        {
            ActivityId = activityId;
            sessions=db.GetSession(activityId);
        }
        public IActionResult OnPostDelete(int sessionId  ,int activityId)
        {
           db.DeleteSeesion(sessionId);
            return RedirectToPage("/Admin/Session", new { activityId = activityId });
        }


    }
}
