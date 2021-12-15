using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class BatchDTO
    {
        public int BatchId { get; set; }
        public int BarrelCount { get; set; }
        public double TargetHumidity { get; set; }
        public double TargetTemperature { get; set; }
        public DateTime StoredOn { get; set; }
        public DateTime? FinishedStorage { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
