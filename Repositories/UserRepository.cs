using Planitly.Backend.Contexts;
using Planitly.Backend.Models;

namespace Planitly.Backend.Repositories
{
    public interface IUserRepository
    {
        public User? GetById(string id);
        public User? GetByUsername(string username);
        public User? GetByEmail(string email);
        public void Save(User user);
        public void UpdateUsername(string oldUsername, string newUsername);
    }

    public class UserRepository : IUserRepository
    {
        private DatabaseContext _dbContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            this._dbContext = databaseContext;
        }

        public User? GetByEmail(string email)
        {
            return _dbContext.User.FirstOrDefault(x => x.Email == email);
        }

        public User? GetById(string id)
        {
            return _dbContext.User.FirstOrDefault(x => x.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return _dbContext.User.FirstOrDefault(x => x.Username == username);
        }

        public void Save(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUsername(string oldUsername, string newUsername)
        {
            var user = GetByUsername(oldUsername);

            if (user is not null)
            {
                user.Username = newUsername;

                _dbContext.User.Update(user);
                _dbContext.SaveChanges();
            }
        }
    }
}