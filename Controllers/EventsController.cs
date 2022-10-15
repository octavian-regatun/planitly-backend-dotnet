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
        private IFileService _fileService;
        public EventsController(IEventRepository eventsRepository, IEventService eventService, IFileService fileService)
        {
            this._eventRepository = eventsRepository;
            this._eventService = eventService;
            this._fileService = fileService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Event @event)
        {
            var savedEvent = _eventService.CreateEvent(@event, User);

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery(Name = "isAuthor")] bool isAuthor, [FromQuery(Name = "isParticipating")] bool isParticipating)
        {
            return Ok();
        }

        [HttpGet("save-file")]
        public async Task<ActionResult> SaveFile()
        {
            await _fileService.UploadImage();
            return Ok();
        }
    }
}