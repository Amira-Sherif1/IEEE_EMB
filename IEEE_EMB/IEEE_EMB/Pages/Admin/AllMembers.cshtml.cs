using IEEE_EMB.Models;
using IEEE_EMB.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllMembersModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }
        [BindProperty]
        Status currentStatus { get; set; }
        public DB db { get; set; }
        public AllMembersModel(DB db) { this.db = db; }
        public DataTable dt { get; set; }
        public void OnGet()
        {
            currentStatus = new Status();
            dt = db.GetMember();
        }

        public Task<IActionResult> OnPostUpdateStatusAsync(string id, string status)
        {
            if (status == Status.Approved)
            {
                db.ChangeMemberStatus(id, status);
            }
            else
            {
                db.DeleteMember(id);
            }
            return Task.FromResult<IActionResult>(RedirectToPage());
        }
    }
}
