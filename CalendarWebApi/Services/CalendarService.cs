using CalendarWebApi.DataAccess;
using CalendarWebApi.DTO;
using CalendarWebApi.Models;

namespace CalendarWebApi.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IRepository repository;

        public CalendarService(IRepository repository)
        {
            this.repository = repository;
        }
        public Task<Calendar> AddEvent(CreateForm calendarEvent)
        {
            Calendar calendar = new Calendar()
            {
                Name = calendarEvent.Name,
                Location = calendarEvent.Location,
                Time = (long)calendarEvent.Time,
                EventOrganizer = calendarEvent.EventOrganizer,
                Members = calendarEvent.Members
            };
            return this.repository.AddEvent(calendar);

        }

        public async Task<Calendar> DeleteEvent(int id)
        {
            var c = await repository.GetCalendar(id);
            if (c == null)
            {
                return new Calendar();
            }
            return await repository.DeleteEvent(c);
        }

        public Task<List<Calendar>> GetCalendar()
        {
            throw new NotImplementedException();
        }

        public Task<Calendar> GetCalendar(int id)
        {
            return repository.GetCalendar(id);
        }

        public Task<List<Calendar>> GetCalendar(Query query)
        {
            EventQueryModel eventQuery = new()
            {
                Name = query.Name,
                Id = query.Id == null ? 0 : (int)query.Id,
                Location = query.Location,
                EventOrganizer = query.EventOrganizer
            };
            return repository.GetCalendar(eventQuery);
        }

        public Task<List<Calendar>> GetEventsSorted()
        {
            return repository.GetEventsSorted();
        }

        public async Task<Calendar> UpdateEvent(int id, UpdateForm calendarEvent)
        {
            Calendar c = await repository.GetCalendar(id);
            if (c == null)
            {
                return new Calendar();
            }

            c.Location = !string.IsNullOrEmpty(calendarEvent.Location) ? calendarEvent.Location : c.Location;
            c.Time = calendarEvent.Time.HasValue ? (long)calendarEvent.Time : c.Time;
            c.Name = !string.IsNullOrEmpty(calendarEvent.Name) ? calendarEvent.Name : c.Name;
            c.Members = calendarEvent.Members ?? c.Members;
            c.EventOrganizer = !string.IsNullOrEmpty(calendarEvent.EventOrganizer) ? calendarEvent.EventOrganizer : c.EventOrganizer;

            c = await repository.UpdateEvent(c);
            return c;
        }
    }
}
