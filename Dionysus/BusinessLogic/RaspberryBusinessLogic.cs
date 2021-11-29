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
        private IEnvironmentalReadingDBAccess environmentalReadingDBAccess;

        public RaspberryBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
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
            var manualControlTemp = await environmentalReadingDBAccess.getManualControl(temperaturePin, batchId);
            var manualControlHum = await environmentalReadingDBAccess.getManualControl(humidityPin, batchId);

            double targetedTemp = await environmentalReadingDBAccess.getTemperatureTarget(batchId);
            double targetedHum = await environmentalReadingDBAccess.getHumidityTarget(batchId);

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
                command.ActivateTemperatureDevice = await environmentalReadingDBAccess.getMachineState(temperaturePin, batchId);
            }
            if (manualControlHum == false)
            {
                averageHum = environmentalValues.Select(h => h.HumidityReading).ToList().Average();
                //activate dehumidifier
                command.ActivateHumidityDevice = averageHum > targetedHum;
            }
            else
            {
                command.ActivateHumidityDevice = await environmentalReadingDBAccess.getMachineState(humidityPin, batchId);
            }

            return command;
        }
    }
}
