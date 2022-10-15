namespace Planitly.Backend.Services
{
    public interface ICalendarService
    {
        public List<string> GetCalendarByMonth(int year, int month);
        public List<string> GetDaysFromNextMonth(DateOnly lastDayFromCurrentMonth);
        public List<string> GetDaysFromPreviousMonth(DateOnly firstDayFromCurrentMonth);
    }

    public class CalendarService : ICalendarService
    {
        public List<string> GetCalendarByMonth(int year, int month)
        {
            var calendar = new List<string>();

            var firstDayOfMonth = new DateOnly(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var currentDay = firstDayOfMonth;

            while (currentDay <= lastDayOfMonth)
            {
                calendar.Add(currentDay.ToShortDateString());

                currentDay = currentDay.AddDays(1);
            }

            var daysFromNextMonth = GetDaysFromNextMonth(lastDayOfMonth);
            var daysFromPreviousMonth = GetDaysFromPreviousMonth(firstDayOfMonth);
            daysFromPreviousMonth.Reverse();

            calendar.AddRange(daysFromNextMonth);
            calendar.InsertRange(0, daysFromPreviousMonth);

            return calendar;
        }

        public List<string> GetDaysFromNextMonth(DateOnly lastDayFromCurrentMonth)
        {
            var daysList = new List<string>();
            var day = lastDayFromCurrentMonth;

            while (day.DayOfWeek >= DayOfWeek.Monday && day.DayOfWeek <= DayOfWeek.Saturday)
            {
                day = day.AddDays(1);
                daysList.Add(day.ToShortDateString());
            }

            return daysList;
        }

        public List<string> GetDaysFromPreviousMonth(DateOnly firstDayFromCurrentMonth)
        {
            var daysList = new List<string>();
            var day = firstDayFromCurrentMonth;

            while (day.DayOfWeek > DayOfWeek.Monday && day.DayOfWeek <= DayOfWeek.Saturday)
            {
                day = day.AddDays(-1);
                daysList.Add(day.ToShortDateString());
            }

            return daysList;
        }

        enum FirstDayOfWeekSystem
        {
            SUNDAY, MONDAY
        }
    }
}