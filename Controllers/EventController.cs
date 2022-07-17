using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Planitly.Backend.Models;
using Planitly.Backend.Repositories;
using Planitly.Backend.Services;

namespace Planitly.Backend.Controllers
{
    [Authorize]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private IEventRepository _eventRepository;
        private IEventService _eventService;
        public EventsController(IEventRepository eventsRepository, IEventService eventService)
        {
            this._eventRepository = eventsRepository;
            this._eventService = eventService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Event @event)
        {
            var savedEvent = _eventService.CreateEvent(@event, User);

            return Ok();
        }
    }
}