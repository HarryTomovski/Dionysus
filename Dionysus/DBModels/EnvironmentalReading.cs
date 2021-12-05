using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("environmental_readings")]
    public partial class EnvironmentalReading
    {
        public EnvironmentalReading()
        {
            Notifications = new HashSet<Notification>();
        }

        [Key]
        [Column("reading_id")]
        public int ReadingId { get; set; }
        [Column("sensorPinNumber")]
        public int SensorPinNumber { get; set; }
        [Column("batch_id")]
        public int BatchId { get; set; }
        [Column("temperature_reading")]
        public double TemperatureReading { get; set; }
        [Column("humidity_reading")]
        public double HumidityReading { get; set; }
        [Column("date_time", TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("EnvironmentalReadings")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(SensorPinNumber))]
        [InverseProperty(nameof(Sensor.EnvironmentalReadings))]
        public virtual Sensor SensorPinNumberNavigation { get; set; }
        [InverseProperty(nameof(Notification.Reading))]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
