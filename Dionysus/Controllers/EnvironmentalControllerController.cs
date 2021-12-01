using Dionysus.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dionysus.DBModels;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentalControllerController : ControllerBase
    {
        private IEnvironmentalControllerBusinessLogic environmentalController;
        public EnvironmentalControllerController(IEnvironmentalControllerBusinessLogic environmentalController)
        {
            this.environmentalController = environmentalController;
        }
        [HttpPut]
        [Route("setManualControl")]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> setManualControl([FromHeader] bool enableManualControl, [FromHeader] int pinNo, [FromHeader] int batchId)
        {

            try
            {
                int result = await environmentalController.setManualControl(enableManualControl, pinNo, batchId);
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

        [HttpPut]
        [Route("setMachineState")]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> setMachineState([FromHeader] bool machineState, [FromHeader] int pinNo, [FromHeader] int batchId)
        {

            try
            {
                int result = await environmentalController.setMachineState(machineState, pinNo, batchId);
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

        [HttpGet]
        [Route("getMachineStates")]
        public async Task<ActionResult> getMachineStates([FromHeader] int pin, [FromHeader] int batchId)
        {
            try
            {
                var result = await environmentalController.getMachineState(pin, batchId);
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpPost]
        [Route("addController")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> addController(EnvironmentalController controller, [FromHeader] int batchId)
        {
            try
            {
                var result = await environmentalController.addEnvironmentalController(controller);
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
