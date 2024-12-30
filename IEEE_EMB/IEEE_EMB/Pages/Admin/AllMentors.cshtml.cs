using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllMentorsModel : PageModel
    {
        [BindProperty]
        public Mentor mentor { get; set; }

        public DB db { get; set; }
        public AllMentorsModel(DB db) { this.db = db; }
        public DataTable dt { get; set; }
        public void OnGet()
        {
            dt = db.GetMentor();
        }
    }
}
