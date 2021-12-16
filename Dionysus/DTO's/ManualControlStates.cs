using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DTO_s
{
    public class ManualControlStates
    {
        public bool manualControl { get; private set; }
        public int batchId { get; private set; }


        public ManualControlStates(bool manualControl, int batchId)
        {
            this.manualControl = manualControl;
            this.batchId = batchId;
        }
    }
}
