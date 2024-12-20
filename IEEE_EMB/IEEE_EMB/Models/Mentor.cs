﻿namespace IEEE_EMB.Models
{
    public class Mentor
    {
        public const string AuthenticationString = "Mentor";
        public string SSN { get; set; }
        public string Name { get; set; }

        public string Education { get; set; }
        public string CV { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BIO { get; set; }
        public string Password { get; set; }
        public string PersonalPhoto { get; set; }

    }
}
