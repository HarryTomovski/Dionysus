using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingBusinessLogic ratingBusinessLogic;
        public RatingController(IRatingBusinessLogic ratingBusinessLogic)
        {
            this.ratingBusinessLogic = ratingBusinessLogic;
        }

        [HttpPost]
        [Route("addRating")]
        [Authorize(Roles = "Administrator, Winemaker, Sommelier")]
        public async Task<ActionResult> addRating(Rating rating)
        {
            try
            {
                var result = await ratingBusinessLogic.addRating(rating);
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
