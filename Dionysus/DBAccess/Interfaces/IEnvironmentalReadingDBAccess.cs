using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IEnvironmentalReadingDBAccess
    {
        public Task<bool> StoreReading(EnvironmentalReading reading);

        public Task<List<EnvironmentalReading>> getEnvironmentalValuesForPastMinute();
        public Task<List<EnvironmentalReading>> getReadingsForDate(DateTime date);

        public Task<int> setTemperatureTarget(double temperature);
    }
}
