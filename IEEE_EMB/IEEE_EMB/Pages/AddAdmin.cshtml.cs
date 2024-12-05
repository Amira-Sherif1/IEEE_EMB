using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages
{
    public class AddAdminModel : PageModel
    {
        //public Admin MyProperty { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/AllAdmins");
        }
    }
}
