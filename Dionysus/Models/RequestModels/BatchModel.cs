using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models.RequestModels
{
    public class BatchModel
    {
        public int BarrelCount { get; set; }
        public double TargetHumidity { get; set; }
        public double TargetTemperature { get; set; }
        public DateTime StoredOn { get; set; }
        public DateTime? FinishedStorage { get; set; }
    }
}
