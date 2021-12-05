using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("notification")]
    public partial class Notification
    {
        [Key]
        [Column("notification_id")]
        public int NotificationId { get; set; }
        [Column("batch_id")]
        public int BatchId { get; set; }
        [Column("reading_id")]
        public int ReadingId { get; set; }
        [Column("received_on", TypeName = "datetime")]
        public DateTime ReceivedOn { get; set; }
        [Column("resolved")]
        public bool? Resolved { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("Notifications")]
        public virtual Batch Batch { get; set; }
        [ForeignKey(nameof(ReadingId))]
        [InverseProperty(nameof(EnvironmentalReading.Notifications))]
        public virtual EnvironmentalReading Reading { get; set; }
    }
}
