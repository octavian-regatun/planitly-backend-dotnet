using Microsoft.EntityFrameworkCore;
using Planitly.Backend.Models;

namespace Planitly.Backend.Contexts
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration _config;

        public DatabaseContext(IConfiguration config)
        {
            this._config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            optionsBuilder.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors();
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Event> Event { get; set; } = null!;
        public DbSet<EventParticipant> EventParticipant { get; set; } = null!;
        public DbSet<Friendship> Friendship { get; set; } = null!;

    }
}