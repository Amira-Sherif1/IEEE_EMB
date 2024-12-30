
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;
namespace IEEE_EMB.Pages.Admin
{
    public class AddAdminModel : PageModel
    {
        public DB db { get; set; }
        //public Admin MyProperty { get; set; }


        [BindProperty]
        public IEEE_EMB.Models.Admin admin { get; set; }

        public AddAdminModel(DB dB) { db = dB; }


        public IActionResult OnGet()
        {
            admin = new Models.Admin();
            if (HttpContext.Session.GetString("AuthenticationString") == "Admin")
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }

        }


        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Models.Admin admin, IFormFile CV)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ;
            //}

            if (CV != null && CV.Length > 0)
            {
                var CVFileName = Guid.NewGuid() + Path.GetExtension(CV.FileName);
                var CVFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", CVFileName);

                using (var stream = System.IO.File.Create(CVFilePath))
                {
                    await CV.CopyToAsync(stream);
                }

                admin.CV = CVFileName;

            }
            admin.SSN = this.admin.SSN;

            db.AddAdmin(admin);
            return RedirectToPage("/Admin/AllAdmins");

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
