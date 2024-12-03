using IEEE_EMB.Models.Utilities;

namespace IEEE_EMB.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Tilte { get; set; }
        public DateTime startdate { get; set; } // it may be dateonly
        public DateTime Enddate { get; set; } // it may be dateonly
        public int Capacity { get; set; }
        public string Type { get; set; }
        public string status { get; set; } = Status.Wating;
        public int MemberId { get; set; }
    }
}
