﻿namespace IEEE_EMB.Models
{
    public class Sessioncs
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Title { get; set; }
        public string? Task { get; set; }
        public string? TaskAnswer { get; set; }
        public int ActivityId { get; set; }
    }
}
