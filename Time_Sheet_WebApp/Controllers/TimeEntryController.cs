using Microsoft.AspNetCore.Mvc;
using Time_Sheet_WebApp.Services;

namespace Time_Sheet_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService) => _timeEntryService = timeEntryService 
            ?? throw new ArgumentNullException(nameof(timeEntryService));

        // Check-In endpoint
        [HttpPost("checkin")]
        public IActionResult CheckIn([FromQuery]Guid userId)
        {
            try
            {
                var timeEntry = _timeEntryService.CheckIn(userId);
                return Ok(timeEntry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Check-Out endpoint
        [HttpPost("checkout")]
        public IActionResult CheckOut([FromQuery]Guid userId)
        {
            try
            {
                var timeEntry = _timeEntryService.CheckOut(userId);
                return Ok(timeEntry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Exporting all entries to JSON
        [HttpGet("export/json")]
        public IActionResult ExportAllToJson()
        {
            var timeEntries = _timeEntryService.GetAll();
            var json = _timeEntryService.ExportTimeEntriesToJson(timeEntries);
            return Ok(json);
        }

        // Exporting specific entry to JSON by ID
        [HttpGet("export/json/{id}")]
        public IActionResult ExportToJsonById(Guid id)
        {
            var timeEntry = _timeEntryService.GetById(id);
            if (timeEntry == null)
            {
                return NotFound();
            }
            var json = _timeEntryService.ExportTimeEntriesToJson([timeEntry]);
            return Ok(json);
        }

        // Download all entries as JSON
        [HttpGet("download/json")]
        public IActionResult DownloadJson()
        {
            var timeEntries = _timeEntryService.GetAll();
            var json = _timeEntryService.ExportTimeEntriesToJson(timeEntries);
            var fileName = "time_entries.json";
            return File(new System.Text.UTF8Encoding().GetBytes(json), "application/json", fileName);
        }
    }
}
