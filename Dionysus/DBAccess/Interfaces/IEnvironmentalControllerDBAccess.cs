using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IEnvironmentalControllerDBAccess
    {
        Task<int> setManualControl(bool enableManualControl, int pin, int batchId);
        Task<int> setMachineState(bool machineState, int pin, int batchId);
        Task<bool> getMachineState(int pin, int batchId);
        Task<bool> getManualControl(int pin, int batchId);

        Task<int> addEnvironmentalController(EnvironmentalController controller);
    }
}
