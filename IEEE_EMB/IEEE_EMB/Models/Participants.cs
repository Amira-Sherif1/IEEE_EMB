using System.ComponentModel.DataAnnotations;

namespace IEEE_EMB.Models
{
    public class Participants
    {
       
        public int SSN { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Attendence_Count { get; set; }
        public string University { get; set; }
        public int MentorId { get; set; }
        public int TeamId { get; set; }

    }
}
