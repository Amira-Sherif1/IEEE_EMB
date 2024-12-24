using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace IEEE_EMB.Pages
{
    public class LoginModel : PageModel
    {
        public string ssn { get; set; }
        public DB db { get; set; }
        public LoginModel(DB db) {  this.db = db; }
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public bool userIsFound { get; set; } = true;
        public async Task<IActionResult> OnPostAsync()
        {
            string userType = db.getUserType(Email, Password); 
            if (userType == null) // Check if the user is a verified user in our website 
            {
                userIsFound = false;
                return Page();
            } 
            else 
            { 
                userIsFound = true;
                HttpContext.Session.SetString("AuthenticationString", userType);
                
                HttpContext.Session.SetString("SSN", db.GetUserSSN(Email));
                
                HttpContext.Session.SetString("Email", Email);
            }
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
          


            
           

           // ViewData[""]
            

            // Here you would typically:
            // 1. Validate the user's credentials
            // 2. Create an authentication cookie/token
            // 3. Redirect to the dashboard or home page

            // For demo purposes, we'll just redirect to home
            return RedirectToPage("/Index");
        }
    }
}
