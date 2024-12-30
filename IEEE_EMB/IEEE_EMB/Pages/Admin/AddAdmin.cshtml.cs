
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;
namespace IEEE_EMB.Pages.Admin
{
    public class AddAdminModel : PageModel
    {
        public DB db { get; set; }
        //public Admin MyProperty { get; set; }
        public AddAdminModel(DB dB) { db = dB; }
        [BindProperty]
        public IEEE_EMB.Models.Admin admin { get; set; }
        public void OnGet()
        {
            admin = new Models.Admin();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Models.Admin admin, IFormFile CV)
        {
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
    }
}
