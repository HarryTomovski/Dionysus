using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface IBatchBusnessLogic
    {
        Task<int> setTemperatureTarget(double temperature, int batchId);
        Task<int> setHumidityTarget(double humidity, int batchId);
        Task<int> addBatch(Batch batch);
        Task<List<Batch>> getAllBatches();
        Task<Batch> getBatch(int batchId);
    }
}
