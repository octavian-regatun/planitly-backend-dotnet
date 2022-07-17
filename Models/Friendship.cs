
namespace Planitly.Backend.Models
{
    public class Friendship
    {
        public long Id { get; set; }
        public string RequesterId { get; set; } = null!;
        public User Requester { get; set; } = null!;
        public string RecipientId { get; set; } = null!;
        public User Recipient { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}