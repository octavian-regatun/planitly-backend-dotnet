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
        private IFileService _firebaseService;
        public EventsController(IEventRepository eventsRepository, IEventService eventService, IFileService firebaseService)
        {
            this._eventRepository = eventsRepository;
            this._eventService = eventService;
            this._firebaseService = firebaseService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Event @event)
        {
            if (@event.Picture != null)
            {
                @event.Picture = await _firebaseService.UploadEventPicture(_firebaseService.ImageBase64ToStream(@event.Picture));
            }

            var savedEvent = _eventService.CreateEvent(@event, User);

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery(Name = "isAuthor")] bool isAuthor, [FromQuery(Name = "isParticipating")] bool isParticipating)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            
            return Ok();
        }
    }
}