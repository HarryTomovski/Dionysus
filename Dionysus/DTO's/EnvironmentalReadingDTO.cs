using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class EnvironmentalReadingDTO
    {
        public double TemperatureReading { get; set; }
        public double HumidityReading { get; set; }
        public DateTime DateTime { get; set; }
    }
}
