namespace Time_Sheet_WebApp.Controllers.Requests
{
    public class TimeEntryRequestDTO
    {
        public Guid? Id { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? Date { get; set; }
    }
}
