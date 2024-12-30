using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MemberFormModel : PageModel
{
    [BindProperty]
    public Member member { get; set; }
    public DB db { get; set; }
    public MemberFormModel(DB db)
    {
        
        this.db = db;
    }

    public void OnGet()
    {
        member = new Member();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        db.MemberJoin(member);
        // Add your logic to save the member application
        // For example: _memberService.CreateApplication(Member);

        return RedirectToPage("/Login");
    }
}