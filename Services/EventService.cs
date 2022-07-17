using System.Security.Claims;
using Planitly.Backend.Models;
using Planitly.Backend.Repositories;

namespace Planitly.Backend.Services
{
    public interface IEventService
    {
        public Event? CreateEvent(Event @event, ClaimsPrincipal user);
    }
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;
        private IEventParticipantRepository _eventParticipantRepository;

        public EventService(IEventRepository eventRepository, IEventParticipantRepository eventParticipantRepository)
        {
            this._eventRepository = eventRepository;
            this._eventParticipantRepository = eventParticipantRepository;
        }

        public Event? CreateEvent(Event @event, ClaimsPrincipal user)
        {
            var savedEvent = _eventRepository.Save(@event);

            if (savedEvent is not null)
            {
                var eventAuthor = new EventParticipant()
                {
                    EventId = savedEvent.Id,
                    UserId = user.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsAuthor = true,
                };

                _eventParticipantRepository.Save(eventAuthor);
            }

            return savedEvent;
        }
    }
}