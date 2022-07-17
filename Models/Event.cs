using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planitly.Backend.Models
{
    public class Event
    {
        public Event()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; } = null!;
        public string? Color { get; set; } = null!;
        public bool AllDay { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public long? LocationId { get; set; }
        public Location? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}