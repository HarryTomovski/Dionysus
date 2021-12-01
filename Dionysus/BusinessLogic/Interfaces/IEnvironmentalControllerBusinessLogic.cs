using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
     public interface IEnvironmentalControllerBusinessLogic
    {
        Task<int> setManualControl(bool enableManualControl, int devicePin, int batchId);
        Task<int> setMachineState(bool setEnvironmentalControl, int devicePin, int batchId);
        Task<bool> getMachineState(int devicePin, int batchId);

        Task<int> addEnvironmentalController(EnvironmentalController controller);
    }
}
