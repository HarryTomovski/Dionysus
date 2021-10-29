using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class EnvironmentalReadingBusinessLogic : IEnvironmentalreadingBusinessLogic
    {
        private IEnvironmentalReadingDBAccess environmentalReadingDBAccess;

        public EnvironmentalReadingBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
        }
        public bool storeReading(EnvironmentalReading reading)
        {
            var success = environmentalReadingDBAccess.StoreReading(reading);
   
            return success;   
        }

        public Command getCommand()
        {
            //temperature values for the past minute
            var environmentalValues = environmentalReadingDBAccess.getEnvironmentalValuesForPastMinute();

            //maybe store average environmental values in db so they can be updated
            //for now have them static here
            double targetedTemp = 20.5;
            double targetedHum = 60.5;

            double? averageTemp = environmentalValues.Select(t => t.TemperatureReading).ToList().Average();
            double? averageHum = environmentalValues.Select(h => h.HumidityReading).ToList().Average();

            //this is only if the average is higher than the desired one
            Command command = new Command(averageTemp > targetedTemp, averageHum > targetedHum);
            return command;
        }
    }
}
