using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MentorFormModel : PageModel
{
    [BindProperty]
    public Mentor Mentor { get; set; }
    public DB db { get; set; }
    public MentorFormModel(DB db)
    {

        this.db = db;
    }
    public void OnGet()
    {
        Mentor = new Mentor();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}
        db.MentorJoin(Mentor);

        // Add your logic to save the mentor application and handle file upload
        // For example: await _mentorService.CreateApplicationAsync(Mentor);

        return RedirectToPage("/Login");
    }
}