﻿using Dionysus.DBAccess.Interfaces;
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

        public async Task<Command> getCommand(int temperaturePin, int humidityPin)
        {
            var manualControlTemp = await environmentalReadingDBAccess.getManualControl(temperaturePin);
            var manualControlHum = await environmentalReadingDBAccess.getManualControl(humidityPin);

            double targetedTemp = await environmentalReadingDBAccess.getTemperatureTarget();
            double targetedHum = await environmentalReadingDBAccess.getHumidityTarget();

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
                command.ActivateTemperatureDevice = await environmentalReadingDBAccess.getMachineState(temperaturePin);
            }
            if (manualControlHum == false)
            {
                averageHum = environmentalValues.Select(h => h.HumidityReading).ToList().Average();
                //activate dehumidifier
                command.ActivateHumidityDevice = averageHum > targetedHum;
            }
            else
            {
                command.ActivateHumidityDevice = await environmentalReadingDBAccess.getMachineState(humidityPin);
            }

            return command;
        }
    }
}