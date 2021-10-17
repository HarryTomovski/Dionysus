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
        [Key]
        [Column("reading_id")]
        public int ReadingId { get; set; }
        [Column("temperature_reading", TypeName = "decimal(3, 1)")]
        public decimal? TemperatureReading { get; set; }
        [Column("humidity_reading", TypeName = "decimal(3, 1)")]
        public decimal? HumidityReading { get; set; }
        [Column("date_time", TypeName = "datetime")]
        public DateTime? DateTime { get; set; }
    }
}
