using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class UIBusinessLogic : IUIBusinessLogic
    {
        private IEnvironmentalReadingDBAccess environmentalReadingDBAccess;

        public UIBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
        }
        public async Task<AvarageDataReadingDTO> getAvarageReadingForDate(DateTime date)
        {
            
            double avarageHumidity = 0;
            double avarageTemperature = 0;
            var count = 0;
            var readingsList = await Task.Run(()=> environmentalReadingDBAccess.getReadingsForDate(date));

            foreach (var reading in readingsList)
            {
                avarageHumidity += reading.HumidityReading.Value;
                avarageTemperature += reading.TemperatureReading.Value;
                count++;
            }

            avarageHumidity /= count;
            avarageTemperature /= count;
            var dto = new AvarageDataReadingDTO(avarageHumidity, avarageTemperature, date.Date);

            return dto;

        }
    }
}
