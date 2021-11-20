using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
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

        public async Task<int> setHumidityTarget(double humidity)
        {
            int result = await environmentalReadingDBAccess.setHumidityTarget(humidity);
            return result;
        }

        public async Task<int> setTemperatureTarget(double temperature)
        {

            int result = await environmentalReadingDBAccess.setTemperatureTarget(temperature);
            return result;
        }

        public async Task<int> setManualControl(bool enableManualControl, int pin)
        {
            int result = await environmentalReadingDBAccess.setManualControl(enableManualControl, pin);
            return result;
        }

        public async Task<int> setMachineState(bool machineState, int pin)
        {
            int result = await environmentalReadingDBAccess.setMachineState(machineState, pin);
            return result;
        }

        public async Task<bool> getMachineState(int pin)
        {
            var result = await environmentalReadingDBAccess.getMachineState(pin);
            return result;
        }

        public async Task<int> addBatch(Batch batch)
        {
            var result = await environmentalReadingDBAccess.addBatch(batch);
            return result;
        }

        public async Task<int> addEnvironmentalController(EnvironmentalController controller)
        {
            var result = await environmentalReadingDBAccess.addEnvironmentalController(controller);
            return result;
        }

        public async Task<int> addSensor(Sensor sensor)
        {
            var result = await environmentalReadingDBAccess.addSensor(sensor);
            return result;
        }
    }
}
