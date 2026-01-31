namespace Demo_App.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string CreatorName { get; set; }
        public DateTime EventStartTime { get; set; }
        public string EventDescription { get; set; }
    }
}
