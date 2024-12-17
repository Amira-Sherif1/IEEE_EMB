namespace IEEE_EMB.Models.Utilities
{
    public class ActivityStatus
    {
        public static string Upcoming { get; } = "Waiting";
        public static string Pending { get; } = "Pending";
        public static string OnProgress { get; } = "OnProgress";
        public static string Finished { get; } = "Finished";
       
    }
}
