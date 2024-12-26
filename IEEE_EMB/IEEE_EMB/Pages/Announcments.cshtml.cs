using IEEE_EMB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

public class Announcement
{
    public string Title { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
   // public string Type { get; set; }
}
public class AnnouncementsModel : PageModel
{
    public DB db { get; set; }
    public AnnouncementsModel(DB dB)
    {
        db = dB;
    }
    public DataTable ann_table { get; set; }
    public List<Announcement> Announcements { get; set; }






    public void OnGet()
    {
        ann_table = db.GetAnnouncements();
        Announcements = new List<Announcement>();

        foreach (DataRow row in ann_table.Rows)
        {

            Announcements.Add(new Announcement
            {
                Title = row["TITLE"].ToString(),
                Date = row["START_DATE"].ToString(),
                //Type = row["TYPE"].ToString(),
                Description = row["DESCRIPTION"].ToString(),
            }) ;
        }



        //new Announcement
        //{
        //    Title = "Workshop Series: Machine Learning in Genomics",
        //    Date = DateTime.Parse("2024-02-20"),
        //    Description = "A comprehensive workshop series on applying ML to genomic data...",
        //    ImageUrl = "/images/announcements/workshop.jpg"
        //}
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove("AuthenticationString");
        HttpContext.Session.Remove("SSN");
        HttpContext.Session.Remove("Email");

        return RedirectToPage("/Login");
    }
}




