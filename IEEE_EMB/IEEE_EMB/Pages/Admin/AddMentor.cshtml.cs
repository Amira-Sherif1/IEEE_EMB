using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class AddMentorModel : PageModel
    {
        [BindProperty]
        public Mentor mentor { get; set; }

        public DB dB { get; set; }
        public AddMentorModel(DB dB)
        {
            this.dB = dB;
        }
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
            dB.AddMentor(mentor);
            return RedirectToPage("/Admin/AllMentors");
            }
            else
            {
                return RedirectToPage("/Admin/errorpage");
            }
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
