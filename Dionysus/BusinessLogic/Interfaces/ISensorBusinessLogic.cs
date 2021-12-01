using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface ISensorBusinessLogic
    {
        Task<int> addSensor(Sensor sensor);
    }
}
