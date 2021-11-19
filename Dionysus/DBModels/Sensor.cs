using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("sensor")]
    public partial class Sensor
    {
        public Sensor()
        {
            EnvironmentalReadings = new HashSet<EnvironmentalReading>();
        }

        [Key]
        [Column("sensorPinNumber")]
        public int SensorPinNumber { get; set; }
        [Column("state")]
        public bool State { get; set; }

        [InverseProperty(nameof(EnvironmentalReading.SensorPinNumberNavigation))]
        public virtual ICollection<EnvironmentalReading> EnvironmentalReadings { get; set; }
    }
}
