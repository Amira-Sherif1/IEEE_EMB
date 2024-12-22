namespace IEEE_EMB.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Title { get; set; }
        public string? Task { get; set; }
        public string? TaskAnswer { get; set; }
        public string? Video { get; set; }
        public string? Document { get; set; }

        public string Description { get; set; }
        public int ActivityId { get; set; }
    }
}
