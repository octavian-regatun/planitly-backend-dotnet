using System.Linq;
using System.Security.Claims;
using Planitly.Backend.Models;
using Planitly.Backend.Repositories;

namespace Planitly.Backend.Services
{
    public interface IEventService
    {
        public Event? CreateEvent(Event @event, ClaimsPrincipal user);
        public List<Event> GetEvents(ClaimsPrincipal user, bool isAuthor, bool isParticipating);
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

        public List<Event> GetEvents(ClaimsPrincipal user, bool isAuthor, bool isParticipating)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Event> eventList = new List<Event>();
            if (isAuthor)
            {
                var eventsAsAuthor = this._eventParticipantRepository.GetByAuthorId(userId);
                eventList.Union(eventsAsAuthor);
            }
            if (isParticipating)
            {
                var eventsAsParticipant = this._eventParticipantRepository.GetByParticipantId(userId);
                eventList.Union(eventsAsParticipant);
            }

            return eventList;
        }
    }
}