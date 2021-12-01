using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class SensorBusinessLogic : ISensorBusinessLogic
    {
        private ISensorBusinessLogic sensorBusinessLogic;
        public SensorBusinessLogic(ISensorBusinessLogic sensorBusinessLogic)
        {
            this.sensorBusinessLogic = sensorBusinessLogic;
        }
        public async Task<int> addSensor(Sensor sensor)
        {
            var result = await sensorBusinessLogic.addSensor(sensor);
            return result;
        }
    }
}
