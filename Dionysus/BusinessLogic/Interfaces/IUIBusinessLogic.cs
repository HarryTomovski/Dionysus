
using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
   public interface IUIBusinessLogic
    {
        Task<AvarageDataReadingDTO> getAvarageReadingForDate(DateTime date);
        Task<AvarageDataReadingDTO> getAvarageReadingSinceBeginning(DateTime date,int batchId);
        Task<int> setTemperatureTarget(double temperature, int batchId);
        Task<int> setHumidityTarget(double humidity, int batchId);
        Task<int> setManualControl(bool enableManualControl, int devicePin, int batchId);
        Task<int> setMachineState(bool setEnvironmentalControl, int devicePin, int batchId);
        Task<bool> getMachineState(int devicePin, int batchId);
        Task<int> addBatch(Batch batch);
        Task<int> addEnvironmentalController(EnvironmentalController controller);
        Task<int> addSensor(Sensor sensor);
        Task<int> addRating(Rating rating);
        Task<User> addUser(User user, string? validationCode);
        Task<User> getUser(string username, string password);
    }
}
