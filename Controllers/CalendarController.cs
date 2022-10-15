using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Planitly.Backend.Services;

namespace Planitly.Backend.Controllers
{
    [Route("calendar")]
    public class CalendarController : ControllerBase
    {
        private ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            this._calendarService = calendarService;
        }

        [HttpGet("{year}/{month}")]
        public ActionResult<List<string>> GetCalendar(int year, int month)
        {
            var calendar = _calendarService.GetCalendarByMonth(year, month);

            return Ok(calendar);
        }
    }
}