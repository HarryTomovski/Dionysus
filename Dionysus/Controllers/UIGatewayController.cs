
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
        [HttpGet]
        [Route("getReadingsForDate/{date}")]
        public async Task<ActionResult<AvarageDataReadingDTO>> getAvarageReadingsForDate(DateTime date)
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
        [Route("setTemperatureTarget")]
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
        [Route("setHumidityTarget")]
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
        [Route("setManualControl")]
        public async Task<ActionResult> setManualControl([FromHeader] bool enableManualControl, [FromHeader] int pinNo)
        {

            try
            {
                int result = await uIBusinessLogic.setManualControl(enableManualControl, pinNo);
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
        [Route("setMachineState")]
        public async Task<ActionResult> setMachineState([FromHeader] bool setTemperatureControl, [FromHeader] int pin)
        {

            try
            {
                int result = await uIBusinessLogic.setMachineState(setTemperatureControl, pin);
                if (result == 1)
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

        [HttpPost]
        [Route("getMachineStates")]
        public async Task<ActionResult> getMachineStates([FromHeader] int pin)
        {
            try
            {
                var result = await uIBusinessLogic.getMachineState(pin);
                return StatusCode(StatusCodes.Status200OK, result);
                
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("addBatch")]
        public async Task<ActionResult> addBatch(Batch batch)
        {
            try
            {
                var result = await uIBusinessLogic.addBatch(batch);
                if(result != -1)
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

        [HttpPost]
        [Route("addController")]
        public async Task<ActionResult> addController(EnvironmentalController controller)
        {
            try
            {
                var result = await uIBusinessLogic.addEnvironmentalController(controller);
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

        [HttpPost]
        [Route("addSensor")]
        public async Task<ActionResult> addSensor(Sensor sensor)
        {
            try
            {
                var result = await uIBusinessLogic.addSensor(sensor);
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
