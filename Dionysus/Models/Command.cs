using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models
{
    public class Command
    {
        public bool ActivateTemperatureDevice { get; private set; }
        public bool ActivateHumidityDevice { get; private set; }

        public Command(bool temperature, bool humidity)
        {
            ActivateTemperatureDevice = temperature;
            ActivateHumidityDevice = humidity;
        }
    }
}
