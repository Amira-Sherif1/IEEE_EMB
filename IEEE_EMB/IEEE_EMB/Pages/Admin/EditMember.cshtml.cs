using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class EditMemberModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
            return RedirectToPage("/Admin/AllMembers");
            }
            return RedirectToPage("/Admin/errorpage");
        }
    }
}
