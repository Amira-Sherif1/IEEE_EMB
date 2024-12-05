using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages
{
    public class AllMembersModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }
        public void OnGet()
        {
        }
    }
}
