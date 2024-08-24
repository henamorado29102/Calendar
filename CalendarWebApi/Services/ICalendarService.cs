using CalendarWebApi.DTO;
using CalendarWebApi.Models;

namespace CalendarWebApi.Services
{
    public interface ICalendarService
    {
        Task<List<Calendar>> GetCalendar();
        Task<Calendar> GetCalendar(int id);
        Task<Calendar> AddEvent(CreateForm calendarEvent);
        Task<List<Calendar>> GetCalendar(Query query);
        Task<Calendar> DeleteEvent(int id);
        Task<List<Calendar>> GetEventsSorted();
        Task<Calendar> UpdateEvent(int id, UpdateForm calendarEvent);
    }
}
