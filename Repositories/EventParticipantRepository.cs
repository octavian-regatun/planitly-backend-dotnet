using Planitly.Backend.Contexts;
using Planitly.Backend.Models;

namespace Planitly.Backend.Repositories
{
    public interface IEventParticipantRepository
    {
        public EventParticipant? Save(EventParticipant eventParticipant);
    }

    public class EventParticipantRepository : IEventParticipantRepository
    {
        private DatabaseContext _dbContext;

        public EventParticipantRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public EventParticipant? Save(EventParticipant eventParticipant)
        {
            var savedEventParticipant = _dbContext.EventParticipant.Add(eventParticipant).Entity;
            _dbContext.SaveChanges();

            return savedEventParticipant;
        }
    }
}