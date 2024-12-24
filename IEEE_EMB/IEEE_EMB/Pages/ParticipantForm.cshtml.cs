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

        //[BindProperty]
        public Participants participant { get; set; }

        [BindProperty]
        public int SSN { get; set; }
        [BindProperty]
        public string Name { get; set; }
      
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        
        [BindProperty]
        public string University { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            participant = new Participants();
            // Add your logic to save the member application
            // For example: _memberService.CreateApplication(Member);
            participant.Email = Email;
            participant.Phone = Phone;
            participant.University = University;
            participant.Name = Name;
            participant.SSN = SSN;
           
            db.AddParticipant(participant);
            
            return RedirectToPage("/Index");
        }
    }
}
