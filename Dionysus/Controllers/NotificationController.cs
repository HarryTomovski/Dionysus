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
                        return StatusCode(StatusCodes.Status404NotFound, "No such batch exists!");
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
                        return StatusCode(StatusCodes.Status404NotFound,"No notificatoins have been found!");
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

        [HttpPut(nameof(ResolveNotification))]
        [Authorize(Roles = "Winemaker, Administrator")]
        //WHY? we can get this when the FE gets all notifications
        public async Task<ActionResult<List<NotificationDTO>>> ResolveNotification([FromQuery] int notificationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var notification = await notificationBusinessLogic.resolveNotification(notificationId);
                    if (notification)
                    {
                        return StatusCode(StatusCodes.Status200OK,"Notification resolved successfully!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound,"No such notitication has been found!");
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
