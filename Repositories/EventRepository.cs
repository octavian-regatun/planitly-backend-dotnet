using Planitly.Backend.Contexts;
using Planitly.Backend.Models;

namespace Planitly.Backend.Repositories
{
    public interface IEventRepository
    {
        public Event? GetById(long id);
        public Event? Save(Event @event);

    }

    public class EventRepository : IEventRepository
    {
        private DatabaseContext _dbContext;

        public EventRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Event? GetById(long id)
        {
            return _dbContext.Event.FirstOrDefault(x => x.Id == id);
        }

        public Event? Save(Event @event)
        {
            var savedEvent = _dbContext.Add(@event).Entity;

            _dbContext.SaveChanges();

            return savedEvent;
        }
    }
}