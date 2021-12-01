using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class BatchDBAccess : IBatchDBAccess
    {
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
        public async Task<bool> batchExists(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var exists = await context.Batches.AnyAsync(b => b.BatchId == batchId);
                    
                    return exists;
                }
                catch (Exception e )
                {

                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<DateTime?> getStoredOn(int batchid)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var storedOn = await Task.Run(() => context.Batches.Where(b => b.BatchId == batchid).Select(s => s.StoredOn).FirstOrDefault());
                    return storedOn;
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

        public async Task<DateTime?> getFinishedOn(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var finishedStorage = await Task.Run(() => context.Batches.Where(b => b.BatchId == batchId).Select(s => s.FinishedStorage).FirstOrDefault());
                    return finishedStorage;
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
