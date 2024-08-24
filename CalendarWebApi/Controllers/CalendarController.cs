using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarWebApi.DataAccess;
using CalendarWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalendarWebApi.DTO;
using Mapster;
using CalendarWebApi.Services;

namespace CalendarWebApi.Controllers
{
    [ApiController]
    [Route("api/calendar")]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;

        private readonly ICalendarService _calendarService;

        public CalendarController(ILogger<CalendarController> logger, ICalendarService calendarService)
        {
            _logger = logger;
            _calendarService = calendarService;
        }
        // 1. Adding a new event
        [HttpPost]
        public async Task<IActionResult> AddEventAsync([FromBody] Event newEvent)
        {
            if (newEvent.Name == null)
            {
                return BadRequest("Event details are required.");
            }
            CreateForm createForm = new()
            {
                Name = newEvent.Name,
                Location = newEvent.Location,
                Time = (long)newEvent.Time,
                EventOrganizer = newEvent.EventOrganizer,
                Members = newEvent.Members
            };
            Calendar createdEvent = await _calendarService.AddEvent(createForm);


            var resourceUri = Url.Action(nameof(GetEventById), new { id = createdEvent.Id });


            return Created(resourceUri, createdEvent);
        }
        // 2. Deleting an event by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            Calendar eventToDelete = await _calendarService.DeleteEvent(id);
            if (eventToDelete.Id == 0)
            {
                return NotFound("Event not found.");
            }
            return NoContent();
        }

        // 3. Editing the event
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(int id, [FromBody] Event updatedEvent)
        {
            UpdateForm updateForm = new()
            {
                Name = updatedEvent.Name,
                Location = updatedEvent.Location,
                EventOrganizer = updatedEvent.EventOrganizer,
                Time = updatedEvent.Time,
                Members = updatedEvent.Members

            };

            var c = await _calendarService.UpdateEvent(id, updateForm);


            return NoContent();
        }
        // 4. Getting all events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _calendarService.GetEventsSorted();
            return Ok(events);
        }
        // 6. Getting event by id
        [HttpGet("query")]
        public async Task<IActionResult> GetEventById([FromQuery] Query query)
        {
            if (query.Location != null || query.EventOrganizer != null || query.Name != null)
            {
                var r = await _calendarService.GetCalendar(query);
                return Ok(r);
            }
            if (query.Id == null)
            {
                return NotFound("Event not found.");
            }
            var c = await _calendarService.GetCalendar(query.Id.Value);
            if (c == null)
            {
                return Ok(new List<Calendar>());
            }

            return Ok(new List<Calendar>() { c });
        }
        // 9. Sort events by time
        [HttpGet("sort")]
        public async Task<IActionResult> SortEvents()
        {
            var c = await _calendarService.GetEventsSorted();
            return Ok(c);
        }

    }
}
