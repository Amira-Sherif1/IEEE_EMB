using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace IEEE_EMB.Pages.Admin
{
    public class AllAdminsModel : PageModel
    {
        public DB db { get; set; }
        public AllAdminsModel(DB dB) { db = dB; }
        [BindProperty]
        public DataTable adminsTable { get; set; }
        public string adminSSN {  get; set; }
        public void OnGet()
        {
            adminsTable = db.GetAdmins();
        }
    }
}
