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
        public Task<AvarageDataReadingDTO> getAvarageReadingForDate(DateTime date);

        public Task<int> setTemperatureTarget(double temperature);
        Task<int> setHumidityTarget(double humidity);
        Task<int> setManualControl(bool enableManualControl, int devicePin);
        Task<int> setMachineState(bool setEnvironmentalControl, int devicePin);
        Task<bool> getMachineState(int devicePin);
        Task<int> addBatch(Batch batch);

        Task<int> addEnvironmentalController(EnvironmentalController controller);
        Task<int> addSensor(Sensor sensor);
        Task<int> addRating(Rating rating);
        Task<User> addUser(User user, string? validationCode);
        Task<User> getUser(string username);
    }
}
