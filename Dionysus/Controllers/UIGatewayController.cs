
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
           //might not need try catch here necause we have it in the db access class
            try
            {
                var readingForDate = await Task.Run(() => uIBusinessLogic.getAvarageReadingForDate(date));
                if (readingForDate is not null)
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

        [HttpPost]
        [Route("[controller]/setManualControl")]
        public async Task<ActionResult> setManualControl([FromHeader] bool enableManualControl)
        {

            try
            {
                int result = await uIBusinessLogic.setManualControl(enableManualControl);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, enableManualControl);
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
        [Route("[controller]/setMachineState")]
        public async Task<ActionResult> setMachineState([FromHeader] bool setTemperatureControl, [FromHeader] bool setHumidityControl)
        {

            try
            {
                int result = await uIBusinessLogic.setMachineState(setTemperatureControl, setHumidityControl);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK, setTemperatureControl || setHumidityControl);
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
        [Route("[controller]/getMachineStates")]
        public async Task<ActionResult> getMachineStates()
        {
            try
            {
                ManualControlStates result = await uIBusinessLogic.getMachineState();
                if (result is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
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
