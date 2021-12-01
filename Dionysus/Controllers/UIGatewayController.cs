

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
        [HttpGet]
        [Route("getOverallAvarage")]
        [Authorize(Roles = "Administrator, Winemaker, Sommelier")]
        public async Task<ActionResult<AvarageDataReadingDTO>> getReadingsSinceBeginning([FromHeader] DateTime date,[FromHeader]int batchId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var overallAvarageReading = await uIBusinessLogic.getAvarageReadingSinceBeginning(date, batchId);
                    if (overallAvarageReading != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, overallAvarageReading);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound);
                    }
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
