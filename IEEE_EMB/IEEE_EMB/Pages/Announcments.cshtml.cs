using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

public class AnnouncementsModel : PageModel
{
    public List<Announcement> Announcements { get; set; }

    public void OnGet()
    {
        Announcements = new List<Announcement>
        {
            new Announcement
            {
                Title = "Annual Conference 2024",
                Date = DateTime.Parse("2024-03-15"),
                Description = "Join us for our annual conference on bioinformatics innovations...",
                ImageUrl = "/images/announcements/conference.jpg"
            },
            new Announcement
            {
                Title = "Workshop Series: Machine Learning in Genomics",
                Date = DateTime.Parse("2024-02-20"),
                Description = "A comprehensive workshop series on applying ML to genomic data...",
                ImageUrl = "/images/announcements/workshop.jpg"
            }
        };
    }
}

public class Announcement
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}