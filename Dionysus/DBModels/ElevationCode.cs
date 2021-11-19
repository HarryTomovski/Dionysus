using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dionysus.DBModels
{
    [Table("elevation_code")]
    public partial class ElevationCode
    {
        [Key]
        [Column("code")]
        [StringLength(30)]
        public string Code { get; set; }
    }
}
