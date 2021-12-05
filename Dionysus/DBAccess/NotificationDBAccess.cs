﻿using Dionysus.DBAccess.Interfaces;
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

        public async Task<int> getNotificationCount(int batchId)
        {

            using (var context = new DionysusContext())
            {
                try
                {
                    var count = await context.Notifications.Where(b => b.BatchId == batchId).CountAsync();
                    return count;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return -1;
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
                            list.Add(new NotificationDTO(batchId, sensorPinNumber, humidityReading, tempReading, targetHum, targetTemp, receivedOn));
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