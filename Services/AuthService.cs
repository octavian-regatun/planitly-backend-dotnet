using Google.Apis.Auth;
using Planitly.Backend.Contexts;
using Planitly.Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Planitly.Backend.Repositories;

namespace Planitly.Backend.Services
{
    public interface IAuthService
    {
        public User Authenticate(GoogleJsonWebSignature.Payload payload);
        public User? GetLoggedUser(ClaimsPrincipal user);
    }

    public class AuthService : IAuthService
    {
        private DatabaseContext _db;
        private IUserRepository _userRepository;
        public AuthService(DatabaseContext db, IUserRepository userRepository)
        {
            this._db = db;
            this._userRepository = userRepository;
        }

        public User Authenticate(GoogleJsonWebSignature.Payload payload)
        {
            var user = FindUserOrAdd(payload);

            return user;
        }

        public User? GetLoggedUser(ClaimsPrincipal user)
        {
            return _userRepository.GetById(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public string GenerateRandomUsername()
        {
            var charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var username = new char[8];
            var random = new Random();

            for (int i = 0; i < username.Length; i++)
            {
                username[i] = charList[random.Next(charList.Length)];
            }

            return new String(username);
        }

        private User FindUserOrAdd(GoogleJsonWebSignature.Payload payload)
        {
            var user = _db.User.Where(user => user.Id == payload.Subject).FirstOrDefault();

            if (user is not null) return user;

            User newUser = new User()
            {
                Id = payload.Subject,
                Username = GenerateRandomUsername(),
                Email = payload.Email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                Picture = payload.Picture,
                Locale = payload.Locale,
                AuthProvider = "google",
                Gender = Gender.Unknown,
                Role = Role.User,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _db.Add<User>(newUser);
            _db.SaveChanges();

            return newUser;
        }

    }
}