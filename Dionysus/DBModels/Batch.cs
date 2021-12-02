using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("batch")]
    public partial class Batch
    {
        public Batch()
        {
            EnvironmentalControllers = new HashSet<EnvironmentalController>();
            EnvironmentalReadings = new HashSet<EnvironmentalReading>();
            Ratings = new HashSet<Rating>();
        }

        [Key]
        [Column("batch_id")]
        public int BatchId { get; set; }
        [Column("barrel_count")]
        public int BarrelCount { get; set; }
        [Column("target_humidity")]
        public double TargetHumidity { get; set; }
        [Column("target_temperature")]
        public double TargetTemperature { get; set; }
        [Column("stored_on", TypeName = "datetime")]
        public DateTime StoredOn { get; set; }
        [Column("finished_storage", TypeName = "datetime")]
        public DateTime? FinishedStorage { get; set; }

        [InverseProperty(nameof(EnvironmentalController.Batch))]
        public virtual ICollection<EnvironmentalController> EnvironmentalControllers { get; set; }
        [InverseProperty(nameof(EnvironmentalReading.Batch))]
        public virtual ICollection<EnvironmentalReading> EnvironmentalReadings { get; set; }
        [InverseProperty(nameof(Rating.Batch))]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
