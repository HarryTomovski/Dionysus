using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public interface IRaspberryBusinessLogic
    {
        Task<bool> storeReading(EnvironmentalReading reading);

        Task<Command> getCommand(int temperaturePin, int humidityPin, int batchId);

    }
}
