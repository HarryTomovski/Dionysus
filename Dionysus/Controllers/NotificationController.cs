using Dionysus.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Dionysus.DTO_s;

namespace Dionysus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly INotificationBusinessLogic notificationBusinessLogic;

        public NotificationController(INotificationBusinessLogic notificationBusinessLogic)
        {
            this.notificationBusinessLogic = notificationBusinessLogic;
        }

        [HttpGet(nameof(GetNotificationsForBatch))]
        [Authorize(Roles = "Winemaker, Administrator")]
        public async Task<ActionResult<List<NotificationDTO>>> GetNotificationsForBatch([FromQuery]int batchId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var list = await notificationBusinessLogic.getNotifications(batchId);
                    if (list != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, list);
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

        [HttpGet(nameof(GetAllNotifications))]
        [Authorize(Roles = "Winemaker, Administrator")]
        //WHY? we can get this when the FE gets all notifications
        public async Task<ActionResult<List<NotificationDTO>>> GetAllNotifications()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var notifications = await notificationBusinessLogic.GetALLNotifications();
                    if (notifications is not null)
                    {
                        return StatusCode(StatusCodes.Status200OK, notifications);
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
