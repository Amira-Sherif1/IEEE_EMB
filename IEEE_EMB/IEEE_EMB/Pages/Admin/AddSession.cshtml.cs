using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class AddSessionModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public Session session { get; set; }
        [BindProperty]
        public int ActivityId { get; set; }

        public AddSessionModel(DB db)
        {
            this.db = db;
        }
        public IActionResult OnGet(int ActivityId)
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                this.ActivityId = ActivityId;
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Session session, IFormFile Video, IFormFile Document , IFormFile Task)
        {
            if (ModelState.IsValid) 
            {
                if (Document != null && Document.Length > 0 && Video !=null && Video.Length > 0&& Task.Length > 0)
                {
                    var filename1 = Guid.NewGuid() + Path.GetExtension(Document.FileName);
                    var filename2 = Guid.NewGuid() + Path.GetExtension(Task.FileName);
                    var filename3 = Guid.NewGuid() + Path.GetExtension(Video.FileName);
                    var filepath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", filename1);
                    var filepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Task", filename1);

                    var filepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", filename3);

                    using (var stream = System.IO.File.Create(filepath1))
                    {
                        await Document.CopyToAsync(stream);
                    }
                    session.Document = filename1;
                    using (var stream = System.IO.File.Create(filepath2))
                    {
                        await Task.CopyToAsync(stream);
                    }
                    session.Task = filename2;
                    using (var stream = System.IO.File.Create(filepath3))
                    {
                        await Video.CopyToAsync(stream);
                    }
                    session.Video = filename3;

                }
                session.ActivityId = this.ActivityId;

                db.AddSession(session);
                return RedirectToPage("/Admin/Session", new { ActivityId = ActivityId });

            }
            return RedirectToPage("/Admin/errorpage");



        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("AuthenticationString");
            HttpContext.Session.Remove("SSN");
            HttpContext.Session.Remove("Email");

            return RedirectToPage("/Login");
        }
    }
}
