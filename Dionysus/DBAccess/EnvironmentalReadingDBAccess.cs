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

        public async Task<int> setTemperatureTarget(double temperaturen, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var target = await Task.Run(() => context.Batches.Where(p => p.BatchId == batchId).FirstOrDefault());
                    target.TargetTemperature = temperaturen;
                    //update db
                    context.Batches.Attach(target);
                    context.Entry(target).Property(m => m.TargetTemperature).IsModified = true;

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

        public async Task<double> getTemperatureTarget(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var tempTarget = await Task.Run(() => context.Batches.Where(p => p.BatchId == batchId).Select(t => t.TargetTemperature).FirstOrDefault());
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
                    var target = await Task.Run(() => context.Batches.Where(p => p.BatchId == batchId).FirstOrDefault());
                    target.TargetHumidity = humidity;
                    //update db
                    context.Batches.Attach(target);
                    context.Entry(target).Property(m => m.TargetHumidity).IsModified = true;

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

        public async Task<double> getHumidityTarget(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var humTarget = await Task.Run(() => context.Batches.Where(p => p.BatchId == batchId).Select(h => h.TargetHumidity).FirstOrDefault());
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
                    var machine = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefault());
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

        public async Task<int> setMachineState(bool machineState, int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //add the db access for setting the targeted  value
                    var machine = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).FirstOrDefault());
                    machine.State = machineState;
                    //update db
                    context.EnvironmentalControllers.Attach(machine);
                    context.Entry(machine).Property(s => s.State).IsModified = true;

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

        public async Task<bool> getMachineState(int pin, int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var state = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.State).FirstOrDefault());
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
                    bool state = await Task.Run(() => context.EnvironmentalControllers.Where(p => p.ControllerPinNumber == pin && p.BatchId == batchId).Select(s => s.Mode).FirstOrDefault());
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
                    await Task.Run(() => context.EnvironmentalControllers.Add(controller));
                    context.SaveChanges();
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

        public async Task<int> addRating(Rating rating)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await Task.Run(() => context.Ratings.Add(rating));
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
        ////To be removed since moved to UserDBAccess

        //public async Task<User> addUser(User user)
        //{
        //    using (var context = new DionysusContext())
        //    {
        //        try
        //        {
        //            await Task.Run(() => context.Users.Add(user));
        //            context.SaveChanges();
        //            return user;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return null;
        //        }
        //    }
        //}
        ////To be removed since moved to UserDBAccess

        //public async Task<User> getUser(string username)
        //{
        //    using (var context = new DionysusContext())
        //    {
        //        try
        //        {
        //            var user = await Task.Run(() => context.Users.Find(username));
        //            return user;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return null;
        //        }
        //    }
        //}
        ////To be removed since moved to IElevationCodeDBAccess
        //public async Task<string> getValidationCode(string validationCode)
        //{
        //    using (var context = new DionysusContext())
        //    {
        //        try
        //        {
        //            var code = await Task.Run(() => context.ElevationCodes.Find(validationCode));
        //            return code.Code;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return null;
        //        }
        //    }
        //}
        ////To be removed since moved to IElevationCodeDBAccess
        //public async Task removeValidationCode(string validationCode)
        //{
        //    using (var context = new DionysusContext())
        //    {
        //        try
        //        {
        //            var code = await Task.Run(() => context.ElevationCodes.Find(validationCode));
        //            await Task.Run(() => context.ElevationCodes.Remove(code));
        //            context.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}
        ////To be removed since moved to UserDBAccess && renamed to userExists
        //public async Task<bool> doesUsernameExsist(string username)
        //{
        //    using (var context = new DionysusContext())
        //    {
        //        try
        //        {
        //            var validUsername = await Task.Run(() => context.Users.Find(username));
        //            if(validUsername is null)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return false;
        //        }
        //    }
        //}

        public async Task<List<EnvironmentalReading>> getReadingsSinceBeginning(DateTime date, int batchId, DateTime storedOn)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //Maybe we need a finished storage period in batch
                    var readingsSinceStoredOn = await Task.Run(() => context.EnvironmentalReadings.Where(b => b.BatchId == batchId && storedOn.Date <= date.Date).ToList());
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
