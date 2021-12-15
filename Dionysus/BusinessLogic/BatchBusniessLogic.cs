﻿using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using Dionysus.Models.RequestModels;
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
        public async Task<int> addBatch(BatchModel batch)
        {
            var dbBatch = new Batch()
            {
                BarrelCount = batch.BarrelCount,
                TargetHumidity = batch.TargetHumidity,
                TargetTemperature = batch.TargetTemperature,
                StoredOn = batch.StoredOn
            };
            var result = await batchDBAccess.addBatch(dbBatch);
            return result;
        }

        public async Task<List<BatchDTO>> getAllBatches()
        {
            List<BatchDTO> list = new();
            var batches = await batchDBAccess.getAllBatches();
            foreach (var b in batches)
            {
                BatchDTO batchDTO = new()
                {
                    BatchId = b.BatchId,
                    BarrelCount = b.BarrelCount,
                    StoredOn = b.StoredOn,
                    TargetHumidity = b.TargetHumidity,
                    TargetTemperature = b.TargetTemperature,
                    FinishedStorage = b.FinishedStorage
                };
                list.Add(batchDTO);
            }
            return list;
        }

        public async Task<BatchDTO> getBatch(int batchId)
        {
            var result = await batchDBAccess.getBatch(batchId);
            var ratings = await ratingDBAccess.getRatings(batchId);
           
            BatchDTO batchDTO = new()
            {
                BatchId = result.BatchId,
                BarrelCount = result.BarrelCount,
                StoredOn = result.StoredOn,
                TargetHumidity = result.TargetHumidity,
                TargetTemperature = result.TargetTemperature,
                FinishedStorage = result.FinishedStorage,
                Ratings = ratings
            };
            return batchDTO;
        }
    }
}
