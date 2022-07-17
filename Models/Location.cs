using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planitly.Backend.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}