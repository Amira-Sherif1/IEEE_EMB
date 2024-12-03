using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IEEE_EMB.Models;
namespace IEEE_EMB.Pages
{
    public class ProfileModel : PageModel
    {
        public Member User { get; set; }
        public string ProfileImageLink { get; set; }
        public void OnGet()
        {
            // In a real application, this would be fetched from a database
            User = new Member
            {
                Name = "Mohammad Maher Ibrahim Ahmed",
                Email = "IEEE-Member32@gmail.com",
                University = "University of Science and Technology",
                Major = "Participated_Committee_Name",
                Phone = "Lorem ipsum dolor sit amet",
                //Currentyear = "Lorem ipsum dolor sit amet",
                Brief = "Lorem ipsum dolor sit amet",
                //Phone = "Lorem ipsum dolor sit amet"
            };
        }

    }
}
