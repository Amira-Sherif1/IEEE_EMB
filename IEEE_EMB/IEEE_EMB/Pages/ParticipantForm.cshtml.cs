using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages
{
    public class ParticipantFormModel : PageModel
    {
        public DB db { get; set; }
        public ParticipantFormModel(DB dB)
        {
            db=dB;
        }   

        [BindProperty]
        public Participants participant { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add your logic to save the member application
            // For example: _memberService.CreateApplication(Member);
            db.AddParticipant(participant);
            
            return RedirectToPage("/Index");
        }
    }
}
