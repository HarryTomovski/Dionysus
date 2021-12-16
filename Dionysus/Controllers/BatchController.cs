using Dionysus.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dionysus.DBModels;
using Dionysus.Models.RequestModels;
using Dionysus.DTO_s;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchBusnessLogic batchBusnessLogic;
        public BatchController(IBatchBusnessLogic batchBusnessLogic)
        {
            this.batchBusnessLogic = batchBusnessLogic;

        }

        //make get target based on batch id
        //check if the target is set, if yes then update
        [HttpPut(nameof(SetTemperatureTarget))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> SetTemperatureTarget(SetTemperatureModel model)
        {
            try
            {
                int result = await batchBusnessLogic.setTemperatureTarget(model.TemperatureTarget, model.BatchId);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK,"Temperature Target set successfully!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,"Such batch does not exists!");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //make get target based on batch id
        //check if the target is set, if yes then update
        [HttpPut (nameof(SetHumidityTarget))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> SetHumidityTarget(SetHumidityModel model)
        {

            try
            {
                int result = await batchBusnessLogic.setHumidityTarget(model.HumidityTarget, model.BatchId);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK,"Humidity target set successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Such batch does not exists!");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost(nameof(AddBatch))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> AddBatch(BatchModel batch)
        {
            try
            {
                var result = await batchBusnessLogic.addBatch(batch);
                if (result != -1)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you have provided cannot be processed!");
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet(nameof(GetAllBatches))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult<List<BatchDTO>>> GetAllBatches()
        {
            try
            {
                var list = await batchBusnessLogic.getAllBatches();
                if (list!=null)
                {
                    return StatusCode(StatusCodes.Status200OK, list);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet(nameof(GetBatch))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult<BatchDTO>> GetBatch([FromQuery]int batchId)
        {
            try
            {
                var batch = await batchBusnessLogic.getBatch(batchId);
                if (batch != null)
                {
                    return StatusCode(StatusCodes.Status200OK, batch);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"Such batch deos not exist!");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
