using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MemberFormModel : PageModel
{
    [BindProperty]
    public Member member { get; set; }

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

        return RedirectToPage("/Apply/Success");
    }
}