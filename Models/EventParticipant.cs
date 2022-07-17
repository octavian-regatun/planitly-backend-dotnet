namespace Planitly.Backend.Models
{
    public class EventParticipant
    {
        public EventParticipant()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public long Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public long EventId { get; set; }
        public Event Event { get; set; } = null!;
        public bool IsAuthor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}