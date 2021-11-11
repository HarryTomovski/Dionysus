
using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
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
    public class UIGatewayController : ControllerBase
    {
        private IUIBusinessLogic uIBusinessLogic;
        public UIGatewayController(IUIBusinessLogic uIBusinessLogic)
        {
            this.uIBusinessLogic = uIBusinessLogic;

        }
        [HttpGet("{date}")]
        public async Task<ActionResult<AvarageDataReadingDTO>> getAvarageReadingsForDate([FromHeader]DateTime date)
        {
           
            try
            {
                var readingForDate = await Task.Run(() => uIBusinessLogic.getAvarageReadingForDate(date));
                if (readingForDate != null)
                {
                    return StatusCode(StatusCodes.Status200OK, readingForDate);
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
        [Route("[controller]/setTemperatureTarget")]
        public async Task<ActionResult> setTemperatureTarget([FromHeader] double temperature)
        {

            try
            {
                int result = await uIBusinessLogic.setTemperatureTarget(temperature);
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

        [HttpPost]
        [Route("[controller]/setHumidityTarget")]
        public async Task<ActionResult> setHumidityTarget([FromHeader] double humidity)
        {

            try
            {
                int result = await uIBusinessLogic.setHumidityTarget(humidity);
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
    }
}
