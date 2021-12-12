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
       
        public async Task<List<EnvironmentalReading>> getReadingsSinceBeginning(DateTime finishedStorage, int batchId, DateTime storedOn)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //Maybe we need a finished storage period in batch
                    var readingsSinceStoredOn = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId && storedOn.Date <= finishedStorage.Date).ToListAsync();
                    
                    return readingsSinceStoredOn;
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task<List<EnvironmentalReading>> GetReadingsForBatch(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //Maybe we need a finished storage period in batch
                    var readingsSinceStoredOn = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId).ToListAsync();

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
