using Dionysus.BusinessLogic.Interfaces;
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
    public class UIBusinessLogic : IUIBusinessLogic
    {
        private readonly IEnvironmentalReadingDBAccess environmentalReadingDBAccess;
        private readonly IBatchDBAccess batchDBAccess;
        

        public UIBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess, IBatchDBAccess batchDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
            this.batchDBAccess = batchDBAccess;
        }
        public async Task<AvarageDataReadingDTO> getAvarageReadingForDate(DateTime date)
        {
            var readingsList = await environmentalReadingDBAccess.getReadingsForDate(date);
            if (readingsList is not null)
            {
                double? avarageTemperature = readingsList.Select(t => t.TemperatureReading).Average();
                double? avarageHumidity = readingsList.Select(h => h.HumidityReading).Average();

                var dto = new AvarageDataReadingDTO(avarageHumidity, avarageTemperature, date.Date);

                return dto;
            }
            else 
            {
                return null;
            }
        }

       

        

       

        

        public async Task<AvarageDataReadingDTO> getAvarageReadingSinceBeginning(int batchId)
        {
            var exists = await  batchDBAccess.batchExists(batchId);
            if (exists)
            {
                //if the batch exsists there is going to be a value
                var storedOn = await batchDBAccess.getStoredOn(batchId);
                var finishedStorage = await batchDBAccess.getFinishedOn(batchId);
                if (storedOn.HasValue && finishedStorage.HasValue)
                {
                    //dont need to pass the stored on date because we can get in from the batch id
                    
                    var readingsList = await environmentalReadingDBAccess.getReadingsSinceBeginning(finishedStorage.Value, batchId, storedOn.Value);

                    double? avarageTemperature = readingsList.Select(t => t.TemperatureReading).Average();
                    double? avarageHumidity = readingsList.Select(h => h.HumidityReading).Average();

                    var dto = new AvarageDataReadingDTO(avarageHumidity, avarageTemperature, DateTime.Today);
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
