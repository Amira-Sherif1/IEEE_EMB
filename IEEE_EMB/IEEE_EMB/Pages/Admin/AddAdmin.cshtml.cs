using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;

namespace IEEE_EMB.Pages.Admin
{
    public class AddAdminModel : PageModel
    {
        //public Admin MyProperty { get; set; }
        [BindProperty]
        public Models.Admin admin { get; set; }
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
            return RedirectToPage("/Admin/AllAdmins");
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
