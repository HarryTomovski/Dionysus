using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class BatchBusniessLogic : IBatchBusnessLogic
    {
        private readonly IBatchDBAccess batchDBAccess;
        private readonly IRatingDBAccess ratingDBAccess;

        public BatchBusniessLogic(IBatchDBAccess batchDBAccess, IRatingDBAccess ratingDBAccess)
        {
            this.batchDBAccess = batchDBAccess;
            this.ratingDBAccess = ratingDBAccess;
        }
        public async Task<int> setHumidityTarget(double humidity, int batchId)
        {
            int result = await batchDBAccess.setHumidityTarget(humidity, batchId);
            return result;
        }

        public async Task<int> setTemperatureTarget(double temperature, int batchId)
        {

            int result = await batchDBAccess.setTemperatureTarget(temperature, batchId);
            return result;
        }
        public async Task<int> addBatch(Batch batch)
        {
            var result = await batchDBAccess.addBatch(batch);
            return result;
        }

        public async Task<List<Batch>> getAllBatches()
        {
            return await batchDBAccess.getAllBatches();
        }

        public async Task<Batch> getBatch(int batchId)
        {
            var result = await batchDBAccess.getBatch(batchId);
            result.Ratings = await ratingDBAccess.getRatings(batchId);
            return result;
        }
    }
}
