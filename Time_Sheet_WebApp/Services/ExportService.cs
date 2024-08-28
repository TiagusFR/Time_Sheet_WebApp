using Time_Sheet_WebApp.Entities;
using System.Text.Json;
using System.Xml.Serialization;
namespace Time_Sheet_WebApp.Services
{
    public class ExportService
    {
        public string ExportToJson(IEnumerable<TimeEntry> timeEntries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(timeEntries, options);
        }
    }
}
