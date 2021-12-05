using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class NotificationDBAccess : INotificationDBAccess
    {
        public async Task<bool> CreateNotification(int batchId, int readingId)
        {
            using (var context = new DionysusContext())
            {
                try
                {

                    var notificaton = new Notification() { BatchId = batchId, ReadingId = readingId, ReceivedOn = DateTime.Now };
                    await context.Notifications.AddAsync(notificaton);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<bool> notificationExists(int notificationId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var exists = await context.Notifications.FindAsync(notificationId);
                    if (exists!=null)
                    {
                        return true;
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

        public async Task<bool> ResolveNotification(int notificatoinID)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var notification = await context.Notifications.FindAsync(notificatoinID);
                    if (notification!=null)
                    {
                        notification.Resolved = true;
                        context.Notifications.Attach(notification);
                        context.Entry(notification).Property(n => n.Resolved).IsModified = true;
                        await context.SaveChangesAsync();
                        return true;
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
}
