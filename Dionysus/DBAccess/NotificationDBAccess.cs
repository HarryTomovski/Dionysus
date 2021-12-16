using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<NotificationDTO>> GetAllNotification()
        {

            using (var context = new DionysusContext())
            {
                List<NotificationDTO> list = new();
                try
                {
                    var notificationsBatchId = await context.Notifications.Select(n => n.BatchId).ToListAsync();
                    foreach(int id in notificationsBatchId)
                    {
                        list.AddRange(await getNotificationsForBatch(id));
                    }
                    return list;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return list;
                }
            }
        }

        public async Task<List<NotificationDTO>> getNotificationsForBatch(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var list = new List<NotificationDTO>();
                    var unresolvednotificationList = await context.Notifications.Where(b => b.BatchId == batchId && b.Resolved==false).ToListAsync();
                    if (unresolvednotificationList != null)
                    {
                        
                        foreach (var notification in unresolvednotificationList)
                        {
                            var readingId = await context.Notifications.Where(b => b.BatchId == batchId).Select(r => r.ReadingId).FirstOrDefaultAsync();

                            var sensorPinNumber = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId && b.ReadingId == readingId).Select(s=>s.SensorPinNumber).FirstOrDefaultAsync();
                            var tempReading = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId && b.ReadingId == readingId).Select(t => t.TemperatureReading).FirstOrDefaultAsync(); 
                            var humidityReading = await context.EnvironmentalReadings.Where(b => b.BatchId == batchId && b.ReadingId == readingId).Select(h=>h.HumidityReading).FirstOrDefaultAsync();
                            var targetTemp = await context.Batches.Where(b => b.BatchId == batchId).Select(t => t.TargetTemperature).FirstOrDefaultAsync();
                            var targetHum = await context.Batches.Where(b => b.BatchId == batchId).Select(h=>h.TargetHumidity).FirstOrDefaultAsync();
                            var receivedOn= await context.Notifications.Where(b => b.BatchId == batchId && b.ReadingId == readingId).Select(d=>d.ReceivedOn).FirstOrDefaultAsync();
                            var resolved = await context.Notifications.Where(b => b.BatchId == batchId && b.ReadingId == readingId).Select(d => d.Resolved).FirstOrDefaultAsync();
                            list.Add(new NotificationDTO(batchId, sensorPinNumber, humidityReading, tempReading, targetHum, targetTemp, receivedOn, resolved));
                        }
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                    
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
                    return exists != null; 
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
                    
                    notification.Resolved = true;
                    context.Notifications.Attach(notification);
                    context.Entry(notification).Property(n => n.Resolved).IsModified = true;
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
    }
}
