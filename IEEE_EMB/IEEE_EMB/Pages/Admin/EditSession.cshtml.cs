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
        public IActionResult OnGet(int SessionId)
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                this.SessionId = SessionId;
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }   
          
          
        }
        public IActionResult OnPost(Session session)
        {
            if (ModelState.IsValid)
            {
                session.Id = SessionId;
                db.EditSeesion(session);
                return RedirectToPage("/Admin/Session");
            }
            return RedirectToPage("/Admin/errorpage");
        }

    }
}
