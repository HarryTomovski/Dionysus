using Dionysus.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dionysus.DBModels;
using Dionysus.Models.RequestModels;

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
        [HttpPut(nameof(SetManualControl))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> SetManualControl(SetManualControlModel model)
        {

            try
            {
                int result = await environmentalController.setManualControl(model.EnableManualControl, model.PinNo, model.BatchId);
                if (result == 1)
                {
                    return StatusCode(StatusCodes.Status200OK);
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

        [HttpGet(nameof(GetManualControl))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult<bool>> GetManualControl(GetManualControlModel model)
        {
            try
            {
                var result = await environmentalController.getManualControl(model.PinNo, model.BatchId);
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut(nameof(SetMachineState))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> SetMachineState(SetMachineStateModel model)
        {

            try
            {
                int result = await environmentalController.setMachineState(model.MachineState, model.PinNo, model.BatchId);
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

        [HttpGet(nameof(GetMachineStates))]
        [Authorize(Roles = "Administrator, Winemaker")]
        public async Task<ActionResult> GetMachineStates(GetMachineStatesModel model)
        {
            try
            {
                var result = await environmentalController.getMachineState(model.PinNo, model.BatchId);
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpPost(nameof(AddController))]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AddController(EnvironmentalController controller)
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
