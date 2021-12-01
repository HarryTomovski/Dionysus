using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class RaspberryBusinessLogic : IRaspberryBusinessLogic
    {
        private readonly IEnvironmentalReadingDBAccess environmentalReadingDBAccess;
        private readonly IEnvironmentalControllerDBAccess environmentalControllerDBAccess;
        private readonly IBatchDBAccess batchDBAccess;

        public RaspberryBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess, IEnvironmentalControllerDBAccess environmentalControllerDBAccess, IBatchDBAccess batchDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
            this.environmentalControllerDBAccess = environmentalControllerDBAccess;
            this.batchDBAccess = batchDBAccess;
        }


        public async Task<bool> storeReading(EnvironmentalReading reading)
        {
            var success = await environmentalReadingDBAccess.StoreReading(reading);
   
            return success;   
        }

        //maybe make the targeted value a range of +/- few percentages
        //have a lower and upper limit 
        public async Task<Command> getCommand(int temperaturePin, int humidityPin, int batchId)
        {
            var manualControlTemp = await environmentalControllerDBAccess.getManualControl(temperaturePin, batchId);
            var manualControlHum = await environmentalControllerDBAccess.getManualControl(humidityPin, batchId);

            double targetedTemp = await batchDBAccess.getTemperatureTarget(batchId);
            double targetedHum = await batchDBAccess.getHumidityTarget(batchId);

            //temperature values for the past minute
            var environmentalValues = await environmentalReadingDBAccess.getEnvironmentalValuesForPastMinute();

            double? averageTemp;
            double? averageHum;

            Command command = new();

            if(manualControlTemp == false)
            {
                averageTemp = environmentalValues.Select(t => t.TemperatureReading).ToList().Average();
                //activate heater
                command.ActivateTemperatureDevice = averageTemp < targetedTemp;
            }
            else
            {
                command.ActivateTemperatureDevice = await environmentalControllerDBAccess.getMachineState(temperaturePin, batchId);
            }
            if (manualControlHum == false)
            {
                averageHum = environmentalValues.Select(h => h.HumidityReading).ToList().Average();
                //activate dehumidifier
                command.ActivateHumidityDevice = averageHum > targetedHum;
            }
            else
            {
                command.ActivateHumidityDevice = await environmentalControllerDBAccess.getMachineState(humidityPin, batchId);
            }

            return command;
        }
    }
}
