using IEEE_EMB.Models;
using IEEE_EMB.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllMentorsModel : PageModel
    {
        [BindProperty]
        public Mentor mentor { get; set; }

        public DB db { get; set; }
        public AllMentorsModel(DB db) { this.db = db; }
        public DataTable dt { get; set; }
        public void OnGet()
        {

            dt = db.GetMentor();
        }

        public Task<IActionResult> OnPostUpdateStatusAsync(string id, string status)
        {
           
            if (status == Status.Approved)
            {
                db.ChangeMentorStatus(id, status);
            }
            else
            {
                db.DeleteMentor(id);
            }
            return Task.FromResult<IActionResult>(RedirectToPage());
        }
    }
}
