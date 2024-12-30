using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages
{
    public class ActivityDetailsModel : PageModel
    {
        public DB db { get; set; }
        public DataTable activity { get; set; }
        public ActivityDetailsModel(DB db)
        {
            this.db = db;
        }
        public void OnGet(int ActivityId)
        {
            activity = db.GetSpecificActivity(ActivityId) ?? new DataTable();
        }
    }
}
