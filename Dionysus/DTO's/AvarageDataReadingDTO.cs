using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class AvarageDataReadingDTO
    {
        public double? HumidityReading { get; private set; }
        public double? TemperatureReading { get; private set; }
        public DateTime Date{ get; private set; }

        public AvarageDataReadingDTO(double? HumidityReading,double? TemperatureReading,DateTime Date)
        {
            this.HumidityReading = HumidityReading;
            this.TemperatureReading = TemperatureReading;
            this.Date = Date;
        }
    }
}
