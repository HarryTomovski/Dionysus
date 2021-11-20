using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class EnvironmentalReadingDBAccess : IEnvironmentalReadingDBAccess
    {
        public async Task<bool> StoreReading(EnvironmentalReading reading)
        {
            try
            {
                using (var context = new DionysusContext())
                {
                    await Task.Run(() =>
                    {
                        context.EnvironmentalReadings.Add(reading);
                        context.SaveChanges();
                    });
                    return true;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        //for raspberry
        public async Task<List<EnvironmentalReading>> getEnvironmentalValuesForPastMinute()
        {
            List<EnvironmentalReading> list = new();
       
            DateTime timeOneMinuteAgo = DateTime.Now.AddMinutes(-1);
            using (var context = new DionysusContext())
            {
                try
                {
                    list = await Task.Run(() => context.EnvironmentalReadings.Where(d => d.DateTime > timeOneMinuteAgo).ToList());
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }

        public async Task<List<EnvironmentalReading>> getReadingsForDate(DateTime date)
        {
            List<EnvironmentalReading> list = new();
            DateTime yesterday = date.AddDays(-1);
            using (var context = new DionysusContext())
            {
                try
                {
                    list = await Task.Run(() => context.EnvironmentalReadings.Where(d => d.DateTime.Date == date.Date).ToList());
                    return list;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task<int> setTemperatureTarget(double temperature)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    //await Task.Run(() => context.Batches.Update;
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<double> getTemperatureTarget()
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var tempTarget = await Task.Run(() => context.Batches.Select(t => t.TargetTemperature).FirstOrDefault());
                    return tempTarget;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setHumidityTarget(double humidity)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    //await Task.Run(() => context.EnvironmentalReadings.Where(d => d.DateTime.Value.Date == date.Date).ToList());
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<double> getHumidityTarget()
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var humTarget = await Task.Run(() => context.Batches.Select(h => h.TargetHumidity).FirstOrDefault());
                    return humTarget;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setManualControl(bool enableManualControl, int pin)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin).FirstOrDefault());
                    machine.Mode = enableManualControl;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(m => m.Mode).IsModified = true;

                    //save changes
                    context.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setMachineState(bool machineState, int pin)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    //await Task.Run(() => context.EnvironmentalReadings.Where(d => d.DateTime.Value.Date == date.Date).ToList());
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<bool> getMachineState(int pin)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var state = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin).Select(s => s.State).FirstOrDefault());
                    return state;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<bool> getManualControl(int pin)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    bool state = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin).Select(s => s.Mode).FirstOrDefault());
                    return state;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<int> addBatch(Batch batch)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    await Task.Run(() => context.Batches.Add(batch));
                    context.SaveChanges();
                    return batch.BatchId;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
        }

        public async Task<int> addEnvironmentalController(EnvironmentalController controller)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    await Task.Run(() => context.EnvironmentalControllers.Add(controller));
                    context.SaveChanges();
                    return controller.ControllerPinNumber;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
        }

        public async Task<int> addSensor(Sensor sensor)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    await Task.Run(() => context.Sensors.Add(sensor));
                    context.SaveChanges();
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
