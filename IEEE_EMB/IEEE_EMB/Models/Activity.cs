using IEEE_EMB.Models.Utilities;

namespace IEEE_EMB.Models
{
    public struct ParticipantCounter
    {
        public int activityId { get; set; }
        public int counter { get; set; }
    }

    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string startdate { get; set; } // it may be dateonly
        public string Enddate { get; set; } // it may be dateonly
        public int Capacity { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        //public Assign assign { get; set; }
        public string status { get; set; } = Status.Waiting;
        // public int MemberId { get; set; }
        public string mentorName { get; set; }
        public int participantsCounter { get; set;}
    }
}
