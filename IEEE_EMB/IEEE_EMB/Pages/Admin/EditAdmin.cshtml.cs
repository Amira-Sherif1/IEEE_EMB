using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class EditAdminModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
            return RedirectToPage("/Admin/AllAdmins");
            }
            return RedirectToPage("/Admin/errorpage");
        }
    }
}
