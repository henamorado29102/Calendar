using System;
namespace CalendarWebApi.Models
{
    public class EventQueryModel
    {
        public int Id { get; set; }
        public string? EventOrganizer { get; set; }
        public string? Location { get; set; }
        public string? Name { get; set; }
    }
}
