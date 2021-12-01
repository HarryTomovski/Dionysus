using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IBatchDBAccess
    {
        
        Task<DateTime?> getStoredOn(int batchid);

        Task<DateTime?> getFinishedOn(int batchId);
        Task<int> addBatch(Batch batch);
        Task<bool> batchExists(int batchId);

        Task<int> setTemperatureTarget(double temperature, int batchId);
        Task<double> getTemperatureTarget(int batchId);
        Task<int> setHumidityTarget(double humidity, int batchId);
        Task<double> getHumidityTarget(int batchId);
    }
}
