using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MentorFormModel : PageModel
{
    [BindProperty]
    public Mentor Mentor { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Add your logic to save the mentor application and handle file upload
        // For example: await _mentorService.CreateApplicationAsync(Mentor);

        return RedirectToPage("/Apply/Success");
    }
}