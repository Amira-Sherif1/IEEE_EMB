using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class EditAdminModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }

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
