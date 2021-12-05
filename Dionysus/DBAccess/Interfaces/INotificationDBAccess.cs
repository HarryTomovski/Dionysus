
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface INotificationDBAccess
    {
        Task<bool> CreateNotification(int batchId, int readingId);
        Task<bool> ResolveNotification(int notificationId);
        Task<bool> notificationExists(int notificationId);

        Task<List<NotificationDTO>> getNotificationsForBatch(int batchId);

        Task<int> getNotificationCount(int batchId);

    }
}
