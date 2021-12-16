using Dionysus.BusinessLogic;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaspberryGatewayController : ControllerBase
    {
        private readonly ILogger<RaspberryGatewayController> _logger;

        private IRaspberryBusinessLogic environmentalreadingBusinessLogic;
        public RaspberryGatewayController(IRaspberryBusinessLogic environmentalreadingBusinessLogic, ILogger<RaspberryGatewayController> logger)
        {
            this.environmentalreadingBusinessLogic = environmentalreadingBusinessLogic;
            _logger = logger;
        }

        //add batch id here and in the embedded layer
        [HttpPost(nameof(StoreReading))]
        public async Task<ActionResult> StoreReading(EnvironmentalReading reading)
        {
            try
            {
                var success = await environmentalreadingBusinessLogic.storeReading(reading);
                _logger.LogInformation(reading.DateTime.ToString());
                if (success)
                {
                    return StatusCode(StatusCodes.Status200OK, reading);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, success);
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }


        [HttpGet(nameof(GetCommand))]
        public async Task<ActionResult> GetCommand([FromHeader] int temperaturePin, [FromHeader] int humidityPin, [FromHeader] int batchId)
        {
            try
            {
                //get command based of average from last 1 minute
                var command = await environmentalreadingBusinessLogic.getCommand(temperaturePin, humidityPin, batchId);
                if (command is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, command);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No such exists!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
        
    }
}
