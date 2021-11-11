using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class ManualControlStates
    {
        public bool manualControl { get; private set; }
        public bool temperatureControl { get; private set; }
        public bool humidityControl { get; private set; }

        public ManualControlStates(bool manualControl, bool temperatureControl, bool humidityControl)
        {
            this.manualControl = manualControl;
            this.temperatureControl = temperatureControl;
            this.humidityControl = humidityControl;
        }
    }
}
