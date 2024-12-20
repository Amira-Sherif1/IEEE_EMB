﻿using IEEE_EMB.Models.Utilities;
namespace IEEE_EMB.Models
{
    public class Member
    {
        public const string AuthenticationString = "Member";
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateOnly Currentyear { get; set; }
        public string Phone { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string PreviousExperience { get; set; }
        public string status { get; set; } = Status.Waiting;
        public string Major { get; set; }
        public string PersonalPhoto { get; set; }
        public string Brief { get; set; }
        public string University { get; set; }
    }
}
