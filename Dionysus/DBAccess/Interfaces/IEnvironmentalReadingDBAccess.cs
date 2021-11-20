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
        Task<bool> StoreReading(EnvironmentalReading reading);

        Task<List<EnvironmentalReading>> getEnvironmentalValuesForPastMinute();
        Task<List<EnvironmentalReading>> getReadingsForDate(DateTime date);

        Task<int> setTemperatureTarget(double temperature);
        Task<double> getTemperatureTarget();
        Task<int> setHumidityTarget(double humidity);
        Task<double> getHumidityTarget();
        Task<int> setManualControl(bool enableManualControl, int pin);
        Task<int> setMachineState(bool machineState, int pin);
        Task<bool> getMachineState(int pin);
        Task<bool> getManualControl(int pin);
        Task<int> addBatch(Batch batch);
        Task<int> addEnvironmentalController(EnvironmentalController controller);
        Task<int> addSensor(Sensor sensor);
    }
}
