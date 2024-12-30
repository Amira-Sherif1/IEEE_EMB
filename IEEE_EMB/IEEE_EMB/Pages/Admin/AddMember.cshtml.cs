using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class AddMemberModel : PageModel
    {
        [BindProperty]
        public Member member { get; set; }
        public DB db { get; set; }
        public AddMemberModel(DB db) { this.db = db; }


        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
            db.AddMember(member);
            return RedirectToPage("/Admin/AllMembers");
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
