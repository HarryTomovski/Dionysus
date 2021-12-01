using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface ISensorDBAccess
    {
        Task<int> addSensor(Sensor sensor);
    }
}
