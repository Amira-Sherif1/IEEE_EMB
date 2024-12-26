using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;
using System.Data;
namespace IEEE_EMB.Pages
{
    public class ProfileModel : PageModel
    {
        public DB DB { get; set; }
        public ProfileModel(DB DB)
        {
            this.DB = DB;
        }
        
        public DataRow UserTable { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string University { get; set; }
        public string Photo { get; set; }
        public string Brief { get; set; }
        

        public void OnGet()
        {
            Email = HttpContext.Session.GetString("Email")!;
            UserTable = DB.GetProfileInfo(Email, HttpContext.Session.GetString("SSN")!).Rows[0];
            Name = UserTable["NAME"].ToString();
            Phone = UserTable["PHONE"].ToString();
            University = UserTable["UNIVERSITY"].ToString();
            Photo = UserTable["PHOTO"].ToString();
            Brief = UserTable["BRIEF"].ToString();

            // In a real application, this would be fetched from a database

        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("AuthenticationString");
            HttpContext.Session.Remove("SSN");
            HttpContext.Session.Remove("Email");

            return RedirectToPage("/Login");
        }
    }
}
