using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models.RequestModels
{
    public class SetManualControlModel
    {
        [Required]
        public bool EnableManualControl { get; set; }
        [Required]
        public int PinNo { get; set; }
        [Required]
        public int BatchId { get; set; }
    }
}
