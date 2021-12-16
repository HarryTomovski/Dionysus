using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class NotificationDTO
    {
        public int BatchId { get; private set; }

        public int PinNumber { get; private set; }
        public double HumidityReading { get; private set; }
        public double TemperatureReading { get; private set; }
        public double HumidityTarget { get; private set; }
        public double TemperatureTarget { get; private set; }

        public DateTime PostedOn { get; private set; }
        public bool? Resolved { get; private set; }

        public NotificationDTO(int BatchId,int PinNumber,double HumidityReading, double TemperatureReading, double HumidityTarget, double TemperatureTarget, DateTime PostedOn, bool? resolved)
        {
            this.BatchId = BatchId;
            this.PinNumber = PinNumber;
            this.HumidityReading = HumidityReading;
            this.TemperatureReading = TemperatureReading;
            this.HumidityTarget = HumidityTarget;
            this.TemperatureTarget = TemperatureTarget;
            this.PostedOn = PostedOn;
            this.Resolved = resolved;
        }

    }
}
