using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class EnvironmentalControllerBusinessLogic : IEnvironmentalControllerBusinessLogic
    {
        private readonly IEnvironmentalControllerDBAccess environmentalController;
        public EnvironmentalControllerBusinessLogic(IEnvironmentalControllerDBAccess environmentalController)
        {
            this.environmentalController = environmentalController;          
        }
        public async Task<int> setManualControl(bool enableManualControl, int pin, int batchId)
        {
            int result = await environmentalController.setManualControl(enableManualControl, pin, batchId);
            return result;
        }

        public async Task<int> setMachineState(bool machineState, int pin, int batchId)
        {
            int result = await environmentalController.setMachineState(machineState, pin, batchId);
            return result;
        }

        public async Task<bool> getMachineState(int pin, int batchId)
        {
            var result = await environmentalController.getMachineState(pin, batchId);
            return result;
        }



        public async Task<int> addEnvironmentalController(EnvironmentalController controller)
        {
            var result = await environmentalController.addEnvironmentalController(controller);
            return result;
        }
    }
}
