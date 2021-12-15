using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class RatingDTO
    {
        public int RatingId { get; set; }
        public string Username { get; set; }
        public int BatchId { get; set; }
        public byte SweatnessRating { get; set; }
        public byte AcidityRating { get; set; }
        public byte BitternessRating { get; set; }
        public byte OverallRating { get; set; }
        public string Feedback { get; set; }
        public DateTime RatedOn { get; set; }
    }
}
