using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("environmental_Controllers")]
    public partial class EnvironmentalController
    {
        [Key]
        [Column("controllerPinNumber")]
        public int ControllerPinNumber { get; set; }
        [Column("batch_id")]
        public int? BatchId { get; set; }
        [Column("mode")]
        public bool Mode { get; set; }
        [Column("state")]
        public bool State { get; set; }

        [ForeignKey(nameof(BatchId))]
        [InverseProperty("EnvironmentalControllers")]
        public virtual Batch Batch { get; set; }
    }
}
