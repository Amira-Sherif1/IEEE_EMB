using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class EditSessionModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public Session session { get; set; }
        [BindProperty]
        public int SessionId { get; set; }
        public EditSessionModel(DB db)
        {
            this.db = db;
        }
        public void OnGet(int SessionId)
        {
            this.SessionId = SessionId;
          
        }
        public IActionResult OnPost(Session session)
        {
            session.Id = SessionId;
            db.EditSeesion(session);
            return RedirectToPage("/Admin/Session");
        }

    }
}
