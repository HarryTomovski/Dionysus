using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models
{
    public class Command
    {
        public bool? ActivateTemperatureDevice { get; set; }
        public bool? ActivateHumidityDevice { get; set; }


        public Command()
        {
        }
    }
}
