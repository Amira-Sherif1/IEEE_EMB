using IEEE_EMB.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class IndexModel : PageModel
{
    public DB db { get; set; }
    public List<NewsItem> NewsItems { get; set; }

    public IndexModel(DB dB)
    {
        db = dB;
    }
    public void OnGet()
    {
      //  db.GetAnnouncements();
        NewsItems = new List<NewsItem>
        {

            new NewsItem
            {
                Title = "Latest Research Breakthrough",
                Description = "New developments in bioinformatics research...",
                ImageUrl = "/images/news/news-1.jpg"
            },
            new NewsItem
            {
               
        Title = "Upcoming Workshop",        
        Description = "Join us for an exciting workshop on...",
                ImageUrl = "/images/news/news-2.jpg"
            },
            new NewsItem
            {
                Title = "Conference Announcement",
                Description = "Annual IEEE BioInformatics conference...",
                ImageUrl = "/images/news/news-3.jpg"
            }
        };
    
        
    }
}

public class NewsItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}