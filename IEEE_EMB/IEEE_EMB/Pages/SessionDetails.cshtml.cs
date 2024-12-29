using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages
{
    public class SessionDetailsModel : PageModel
    {
        public DB db { get; set; }

        public DataTable session { get; set; }
        public SessionDetailsModel(DB db)
        {
            this.db = db;
            
        }
        public void OnGet(int SessionId)
        {
            session = db.getspecificsession(SessionId);
        }
    }
}
