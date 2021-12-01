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

        public BatchBusniessLogic(IBatchDBAccess batchDBAccess)
        {
            this.batchDBAccess = batchDBAccess;
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
    }
}
