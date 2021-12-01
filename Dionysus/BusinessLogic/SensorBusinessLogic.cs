using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class SensorBusinessLogic : ISensorBusinessLogic
    {
        private readonly ISensorDBAccess sensorDBAccess;
        public SensorBusinessLogic(ISensorDBAccess sensorDBAccess)
        {
            this.sensorDBAccess = sensorDBAccess;
        }
        public async Task<int> addSensor(Sensor sensor)
        {
            var result = await sensorDBAccess.addSensor(sensor);
            return result;
        }
    }
}
