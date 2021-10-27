using Dionysus.BusinessLogic;
using Dionysus.DBModels;
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

    
    public class EnvironmentalReadingController : ControllerBase
    {
        private IEnvironmentalreadingBusinessLogic environmentalreadingBusinessLogic;
        public EnvironmentalReadingController(IEnvironmentalreadingBusinessLogic environmentalreadingBusinessLogic)
        {
            this.environmentalreadingBusinessLogic = environmentalreadingBusinessLogic;
        }

        [HttpPost]
        public async Task<ActionResult> storeReading(EnvironmentalReading reading)
        {
            try
            {
                var success = await Task.Run(() => environmentalreadingBusinessLogic.storeReading(reading));
                if (success)
                {
                    return StatusCode(StatusCodes.Status200OK, success);
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
    }
}
