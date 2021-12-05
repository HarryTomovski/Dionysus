using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Models.RequestModels
{
    public class SetTemperatureModel
    {
        [Required]
        public double TemperatureTarget { get; set; }
        [Required]
        public int BatchId { get; set; }
    }
}
