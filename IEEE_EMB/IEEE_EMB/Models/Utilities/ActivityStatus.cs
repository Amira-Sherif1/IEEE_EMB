namespace IEEE_EMB.Models.Utilities
{
    public class ActivityStatus
    {
        public static string Upcoming { get; } = "Upcoming"; // announced 
        public static string Pending { get; } = "Pending"; // not announced yet / shouldn't be displayed on the website until the admin change its status to be upcoming / finished / onProgress
        public static string OnProgress { get; } = "OnProgress";
        public static string Finished { get; } = "Finished";
       
    }
}
