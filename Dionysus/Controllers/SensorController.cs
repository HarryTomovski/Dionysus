using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private ISensorBusinessLogic sensorBusinessLogic;
        public SensorController(ISensorBusinessLogic sensorBusinessLogic)
        {
            this.sensorBusinessLogic = sensorBusinessLogic;
        }
        [HttpPost(nameof(AddSensor))]
        [Authorize(Roles = "Administrator")]
        //add batch id in sensor info
        public async Task<ActionResult> AddSensor(Sensor sensor, [FromHeader] int batchId)
        {
            try
            {
                var result = await sensorBusinessLogic.addSensor(sensor);
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
    }
}
