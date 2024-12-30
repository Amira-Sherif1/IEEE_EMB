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
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            dB.AddMentor(mentor);
            return RedirectToPage("/Admin/AllMentors");
        }
    }
}
