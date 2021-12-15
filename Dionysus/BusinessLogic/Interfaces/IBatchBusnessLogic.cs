using Dionysus.DBModels;
using Dionysus.DTO_s;
using Dionysus.Models.RequestModels;
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
        Task<int> addBatch(BatchModel batch);
        Task<List<BatchDTO>> getAllBatches();
        Task<BatchDTO> getBatch(int batchId);
    }
}
