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
        public void OnGet(int activityId=1)
        {
            ActivityId = activityId;
            sessions=db.GetSession(activityId);
        }
        public void OnPostDelete(int sessionId  ,int activityId)
        {
           db.DeleteSeesion(sessionId);
           RedirectToPage("/Admin/Session", new { ActivityId = activityId });
            //return new JsonResult(new { success = true });
        }


    }
}
