using Dionysus.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dionysus.DBModels;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private IBatchBusnessLogic batchBusnessLogic;
        public BatchController(IBatchBusnessLogic batchBusnessLogic)
        {
            this.batchBusnessLogic = batchBusnessLogic;

        }

        //make get target based on batch id
        //check if the target is set, if yes then update
        [HttpPut]
        [Route("setTemperatureTarget")]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> setTemperatureTarget([FromHeader] double temperature, [FromHeader] int batchId)
        {
            try
            {
                int result = await batchBusnessLogic.setTemperatureTarget(temperature, batchId);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, temperature);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //make get target based on batch id
        //check if the target is set, if yes then update
        [HttpPut]
        [Route("setHumidityTarget")]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> setHumidityTarget([FromHeader] double humidity, [FromHeader] int batchId)
        {

            try
            {
                int result = await batchBusnessLogic.setHumidityTarget(humidity, batchId);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, humidity);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        [Route("addBatch")]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> addBatch(Batch batch)
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
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet(nameof(GetAllBatches))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult<List<Batch>>> GetAllBatches()
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
    }
}
