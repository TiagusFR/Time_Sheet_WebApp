namespace Time_Sheet_WebApp.Entities
{
    public class TimeEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? Date { get; set; }

        //Navigation
        public User User { get; set; }
    }
}
