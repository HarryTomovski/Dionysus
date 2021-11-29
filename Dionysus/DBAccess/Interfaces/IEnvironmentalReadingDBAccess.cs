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
        Task<List<EnvironmentalReading>> getReadingsSinceBeginning(DateTime date, int batchId, DateTime storedOn);

        Task<int> setTemperatureTarget(double temperature, int batchId);
        Task<double> getTemperatureTarget(int batchId);
        Task<int> setHumidityTarget(double humidity, int batchId);
        Task<double> getHumidityTarget(int batchId);
        Task<int> setManualControl(bool enableManualControl, int pin, int batchId);
        Task<int> setMachineState(bool machineState, int pin, int batchId);
        Task<bool> getMachineState(int pin, int batchId);
        Task<bool> getManualControl(int pin, int batchId);
        Task<int> addBatch(Batch batch);
        Task<int> addEnvironmentalController(EnvironmentalController controller);
        Task<int> addSensor(Sensor sensor);
        Task<int> addRating(Rating rating);
        //Task<User> addUser(User user);
        //Task<User> getUser(string username);
        //Task<bool> doesUsernameExsist(string username);
        //Task<string> getValidationCode(string validationCode);
        //Task removeValidationCode(string validationCode);
    }
}
