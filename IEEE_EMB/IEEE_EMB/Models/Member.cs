using IEEE_EMB.Models.Utilities;
namespace IEEE_EMB.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateOnly Currentyear { get; set; }
        public string Phone { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string PreviousExperience { get; set; }
        public string status { get; set; } = Status.Wating;
        public string Major { get; set; }

        public string Brief { get; set; }
        public string University { get; set; }
    }
}
