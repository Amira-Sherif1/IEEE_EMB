using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages
{

    public class DetailsPageModel : PageModel
    {
        public DB db { get; set; }

        public DataTable session { get; set; }

        public Session sessions { get; set; }

        List<Session> sessionList = new List<Session>();

        public Activity Activity { get; set; }

        public DetailsPageModel(DB db)
        {
            this.db = db;
        }
       
        public void OnGet(int activityId)
        {
          session =  db.GetSession(activityId);
          
        }
    }
}