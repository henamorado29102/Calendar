using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarWebApi.Models
{
    public class Event
    {
        public string? Name { get; set;}
        public string? Location { get; set;}
        public string? Members { get; set;}
        public string? EventOrganizer { get; set;}
        public long Time { get; set;}
    }
}