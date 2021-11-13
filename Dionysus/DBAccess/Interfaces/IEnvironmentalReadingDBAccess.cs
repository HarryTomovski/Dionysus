using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IEnvironmentalReadingDBAccess
    {
        public Task<bool> StoreReading(EnvironmentalReading reading);

        public Task<List<EnvironmentalReading>> getEnvironmentalValuesForPastMinute();
        public Task<List<EnvironmentalReading>> getReadingsForDate(DateTime date);

        public Task<int> setTemperatureTarget(double temperature);
        public Task<int> setHumidityTarget(double humidity);
        Task<int> setManualControl(bool enableManualControl);
        Task<int> setMachineState(bool setTemperatureControl, bool setHumidityControl);
        Task<ManualControlStates> getMachineState();

        Task<bool> getManualControl();
    }
}
