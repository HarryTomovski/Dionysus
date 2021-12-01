using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class SensorDBAccess : ISensorDBAccess
    {
        public async Task<int> addSensor(Sensor sensor)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await context.Sensors.AddAsync(sensor);
                    await context.SaveChangesAsync();
                    return sensor.SensorPinNumber;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
        }
    }
}
