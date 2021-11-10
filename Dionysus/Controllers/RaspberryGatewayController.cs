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

        [HttpPost]
        public async Task<ActionResult> storeReading(EnvironmentalReading reading)
        {
            try
            {
                //For debug without DB purposes
               // var success = true;
                var success = await Task.Run(() => environmentalreadingBusinessLogic.storeReading(reading));
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
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpGet]
        public async Task<ActionResult> getCommand()
        {
            try
            {
                //get command based of average from last 1 minute
                var command = await Task.Run(() => environmentalreadingBusinessLogic.getCommand());
                if (command is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, command);
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
        
    }
}
