using Time_Sheet_WebApp.Entities;

namespace Time_Sheet_WebApp.Services
{
    public interface ITimeEntryService
    {
        TimeEntry? GetById(Guid id);
        List<TimeEntry> GetAll();
        TimeEntry CheckIn(Guid userId);
        TimeEntry CheckOut(Guid userId);
        string ExportTimeEntriesToJson(IEnumerable<TimeEntry> timeEntries);
    }
}
