

using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
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
    public class UIGatewayController : ControllerBase
    {
        private IUIBusinessLogic uIBusinessLogic;
        public UIGatewayController(IUIBusinessLogic uIBusinessLogic)
        {
            this.uIBusinessLogic = uIBusinessLogic;

        }
        [HttpGet(nameof(GetAvarageReadingsForDate))]
        public async Task<ActionResult<AvarageDataReadingDTO>> GetAvarageReadingsForDate([FromQuery]DateTime date)
        {
           //might not need try catch here necause we have it in the db access class
            try
            {
                var readingForDate = await uIBusinessLogic.getAvarageReadingForDate(date);
                if (readingForDate is not null)
                {
                    return StatusCode(StatusCodes.Status200OK, readingForDate);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,"No readings for that date!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet(nameof(GetReadingsSinceBeginning))]
        [Authorize(Roles = "Administrator, Winemaker, Sommelier")]
        public async Task<ActionResult<AvarageDataReadingDTO>> GetReadingsSinceBeginning([FromQuery]int batchId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var overallAvarageReading = await uIBusinessLogic.getAvarageReadingSinceBeginning(batchId);
                    if (overallAvarageReading != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, overallAvarageReading);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound,"No readings have been found!");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet(nameof(GetReadingsForBatch))]
        [Authorize(Roles = "Administrator, Winemaker, Sommelier")]
        public async Task<ActionResult<List<AvarageDataReadingDTO>>> GetReadingsForBatch([FromQuery] int batchId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var overallAvarageReading = await uIBusinessLogic.GetReadingsForBatch(batchId);
                    if (overallAvarageReading != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, overallAvarageReading);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound,"No such has been found!");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"The information you've provided cannot be processed!");
                }
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
