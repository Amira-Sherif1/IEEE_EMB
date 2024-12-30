using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllMembersModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }

        public DB db { get; set; }
        public AllMembersModel(DB db) { this.db = db; }
        public DataTable dt { get; set; }
        public void OnGet()
        {
            dt = db.GetMember();
        }
    }
}
