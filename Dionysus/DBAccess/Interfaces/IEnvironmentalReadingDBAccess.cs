using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IEnvironmentalReadingDBAccess
    {
        Task<bool> StoreReading(EnvironmentalReading reading);
        Task<List<EnvironmentalReading>> getEnvironmentalValuesForPastMinute();
        Task<List<EnvironmentalReading>> getReadingsForDate(DateTime date);
        Task<List<EnvironmentalReading>> getReadingsSinceBeginning(DateTime finishedStorage, int batchId, DateTime storedOn);
        Task<List<EnvironmentalReading>> GetReadingsForBatch(int batchId);
    }
}
