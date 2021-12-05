﻿using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class NotificationBusinessLogic : INotificationBusinessLogic
    {
        private readonly INotificationDBAccess notificationDBAccess;

        public NotificationBusinessLogic(INotificationDBAccess notificationDBAccess)
        {
            this.notificationDBAccess = notificationDBAccess;
        }

        public async Task<int> getNotificationCount(int batchId)
        {
            var count = await notificationDBAccess.getNotificationCount(batchId);
            if (count!=-1)
            {
                return count;
            }
            else
            {
                return -1;
            }
        }

        public async Task<List<NotificationDTO>> getNotifications(int batchId)
        {
            var list = await notificationDBAccess.getNotificationsForBatch(batchId);
            if (list!=null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> resolveNotification(int notificationId)
        {
            try
            {
                var exist = await notificationDBAccess.notificationExists(notificationId);
                if (exist)
                {
                    var success = await notificationDBAccess.ResolveNotification(notificationId);
                    if (success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
