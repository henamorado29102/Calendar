using System;
using Newtonsoft.Json;
namespace CalendarWebApi.DTO
{
    public class CreateForm
    {
        public string? Name { get; set; }
        public long? Time { get; set; }
        public string? Location { get; set; }
        public string? EventOrganizer { get; set; }
        public string? Members { get; set; }
    }
}
