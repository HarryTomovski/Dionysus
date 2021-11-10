using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class EnvironmentalReadingDBAccess : IEnvironmentalReadingDBAccess
    {
        public bool StoreReading(EnvironmentalReading reading)
        {
            try
            {
                using (var context = new DionysusContext())
                {
                    context.EnvironmentalReadings.Add(reading);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<EnvironmentalReading> getEnvironmentalValuesForPastMinute()
        {
            List<EnvironmentalReading> list = new();
            //DateTime now = DateTime.Now();
            DateTime timeOneMinuteAgo = DateTime.Now.AddMinutes(-1);
            using (var context = new DionysusContext())
            {
                try
                {
                    list = context.EnvironmentalReadings.Where(d => d.DateTime > timeOneMinuteAgo).ToList();
                    return list;
                }
                catch (Exception)
                {
                    return list;
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
                    list = await Task.Run(() => context.EnvironmentalReadings.Where(d => d.DateTime.Value.Date == date.Date).ToList());
                    return list;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return list;
                    
                }
            }
        }

    }
}
