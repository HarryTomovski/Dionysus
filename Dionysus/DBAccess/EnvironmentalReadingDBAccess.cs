using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using Microsoft.EntityFrameworkCore;
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
                    await context.EnvironmentalReadings.AddAsync(reading);
                    await context.SaveChangesAsync();

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
                    list = await context.EnvironmentalReadings.Where(d => d.DateTime > timeOneMinuteAgo).ToListAsync();
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
                    list = await context.EnvironmentalReadings.Where(d => d.DateTime.Date == date.Date).ToListAsync();
                    return list;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task<int> setTemperatureTarget(double temperaturen, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var target = await context.Batches.Where(p => p.BatchId == batchId).FirstOrDefaultAsync();
                    target.TargetTemperature = temperaturen;
                    //update db
                    context.Batches.Attach(target);
                    context.Entry(target).Property(m => m.TargetTemperature).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<double> getTemperatureTarget(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var tempTarget = await context.Batches.Where(p => p.BatchId == batchId).Select(t => t.TargetTemperature).FirstOrDefaultAsync();
                    return tempTarget;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setHumidityTarget(double humidity, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var target = await context.Batches.Where(p => p.BatchId == batchId).FirstOrDefaultAsync();
                    target.TargetHumidity = humidity;
                    //update db
                    context.Batches.Attach(target);
                    context.Entry(target).Property(m => m.TargetHumidity).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<double> getHumidityTarget(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var humTarget = await context.Batches.Where(p => p.BatchId == batchId).Select(h => h.TargetHumidity).FirstOrDefaultAsync();
                    return humTarget;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setManualControl(bool enableManualControl, int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefaultAsync();
                    machine.Mode = enableManualControl;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(m => m.Mode).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<int> setMachineState(bool machineState, int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefaultAsync();
                    machine.State = machineState;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(s => s.State).IsModified = true;

                    //save changes
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<bool> getMachineState(int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var state = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.State).FirstOrDefaultAsync();
                    return state;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<bool> getManualControl(int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    bool state = await context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.Mode).FirstOrDefaultAsync();
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
                    await context.Batches.AddAsync(batch);
                    await context.SaveChangesAsync();
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
                    await context.EnvironmentalControllers.AddAsync(controller);
                    await context.SaveChangesAsync();
                    return controller.ControllerPinNumber;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

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

        public async Task<int> addRating(Rating rating)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await context.Ratings.AddAsync(rating);
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }
        public async Task<List<EnvironmentalReading>> getReadingsSinceBeginning(DateTime date, int batchId, DateTime storedOn)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //Maybe we need a finished storage period in batch
                    var readingsSinceStoredOn = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId && storedOn.Date <= date.Date).ToListAsync();
                    return readingsSinceStoredOn;
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
