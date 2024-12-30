using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IEEE_EMB.Pages.Admin
{
    public class EditActivityModel : PageModel
    {
        [BindProperty]
        public Activity activity {  get; set; } = new Activity();

        public DB DB { get; set; }
        public EditActivityModel(DB dB) { 
            DB = dB;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            //activity = new Activity();
            if (ModelState.IsValid) 
            {
            DB.EditActivity(activity);
            return RedirectToPage("/Admin/Activities");
            }
            return RedirectToPage("/Admin/errorpage");
        }
    }
}
