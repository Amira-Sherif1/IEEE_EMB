using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class AddAdminModel : PageModel
    {
        //public Admin MyProperty { get; set; }
        public void OnGet()
        {
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
