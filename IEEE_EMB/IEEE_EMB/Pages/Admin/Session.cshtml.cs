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
            sessions=db.GetSession(activityId) ?? new DataTable();
        }
        public IActionResult OnPostDelete(int sessionId, int activityId)
        {
            var OldSession = db.getspecificsession(sessionId);

            if (OldSession.Rows.Count > 0) // Ensure there is at least one row
            {
                if (OldSession.Rows[0]["Document"] != DBNull.Value && OldSession.Rows[0]["Document"] != null)
                {
                    var document = (string)OldSession.Rows[0]["Document"];
                    var oldfilepath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", document);
                    if (System.IO.File.Exists(oldfilepath1))
                    {
                        System.IO.File.Delete(oldfilepath1);
                    }
                }

                if (OldSession.Rows[0]["TASK"] != DBNull.Value && OldSession.Rows[0]["TASK"] != null)
                {
                    var Task = (string)OldSession.Rows[0]["TASK"];
                    var oldfilepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Task", Task);
                    if (System.IO.File.Exists(oldfilepath2))
                    {
                        System.IO.File.Delete(oldfilepath2);
                    }
                }

                if (OldSession.Rows[0]["Video"] != DBNull.Value && OldSession.Rows[0]["Video"] != null)
                {
                    var Video = (string)OldSession.Rows[0]["Video"];
                    var oldfilepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", Video);
                    if (System.IO.File.Exists(oldfilepath3))
                    {
                        System.IO.File.Delete(oldfilepath3);
                    }
                }

                db.DeleteSeesion(sessionId);
            }

            return RedirectToPage("/Admin/Session", new { activityId });
        }

       

    }
}
