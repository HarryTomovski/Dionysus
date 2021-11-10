
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
    }
}
