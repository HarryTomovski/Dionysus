﻿using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
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

        public async Task<Command> getCommand()
        {
            //temperature values for the past minute
            var environmentalValues = await environmentalReadingDBAccess.getEnvironmentalValuesForPastMinute();

            //maybe store average environmental values in db so they can be updated
            //for now have them static here
            double targetedTemp = 20.5;
            double targetedHum = 60.5;

            double? averageTemp = environmentalValues.Select(t => t.TemperatureReading).ToList().Average();
            double? averageHum = environmentalValues.Select(h => h.HumidityReading).ToList().Average();

            Command command = new Command(averageTemp > targetedTemp, averageHum > targetedHum);
            return command;
        }

        
    }
}
