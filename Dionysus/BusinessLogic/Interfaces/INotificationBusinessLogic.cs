using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface INotificationBusinessLogic
    {
        Task<List<NotificationDTO>> getNotifications(int batchId);
        Task<bool> resolveNotification(int notificationId);
        Task<List<NotificationDTO>> GetALLNotifications();
    }
}
