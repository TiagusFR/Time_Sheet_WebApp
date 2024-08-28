using Time_Sheet_WebApp.Database;
using Time_Sheet_WebApp.Entities;

namespace Time_Sheet_WebApp.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly AppDbContext _context;
        private readonly ExportService _exportService;

        public TimeEntryService(AppDbContext context, ExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }

        // CRUD Services
        public TimeEntry? GetById(Guid id)
        {
            return _context.TimeEntries.FirstOrDefault(t => t.Id == id);
        }

        public List<TimeEntry> GetAll()
        {
            return _context.TimeEntries.ToList();
        }

        public TimeEntry CheckIn(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var today = DateTime.Today;

            var existingEntry = _context.TimeEntries
                .FirstOrDefault(i => i.UserId == userId && i.Date == today);

            if (existingEntry != null)
            {
                throw new Exception("There's already a check-in for today.");
            }

            var timeEntry = new TimeEntry
            {
                UserId = userId,
                CheckInTime = DateTime.Now,
                Date = today,
            };

            try
            {
                _context.TimeEntries.Add(timeEntry);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the check-in entry.", ex);
            }

            return timeEntry;
        }

        public TimeEntry CheckOut(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var today = DateTime.Today;

            var existingEntry = _context.TimeEntries
                .FirstOrDefault(i => i.UserId == userId && i.Date == today);

            if (existingEntry == null)
            {
                throw new Exception("No check-in found for today.");
            }

            if (existingEntry.CheckOutTime != null)
            {
                throw new Exception("There's already a check-out for today.");
            }

            existingEntry.CheckOutTime = DateTime.Now;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the check-out entry.", ex);
            }

            return existingEntry;
        }

        // Exporting Services
        public string ExportTimeEntriesToJson(IEnumerable<TimeEntry> timeEntries)
        {
            return _exportService.ExportToJson(timeEntries);
        }
    }
}
