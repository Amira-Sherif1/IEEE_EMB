using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;
using System.Data;
namespace IEEE_EMB.Pages.Admin
{
    public class FeedbackModel : PageModel
    {
        public DB db { get; set; }
        public FeedbackModel(DB db)
        {
            this.db = db;
        }
        [BindProperty]
        public DataTable feedbacks { get; set; }

        public void OnGet(string participantSSN)
        {

        }
    }
}
