using System;
namespace CalendarWebApi.DTO
{
    public class Query
    {
        public int? Id { get; set; }
        public string? EventOrganizer { get; set; }
        public string? Location { get; set; }
        public string? Name { get; set; }
    }
}
