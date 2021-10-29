using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public interface IEnvironmentalreadingBusinessLogic
    {
        public bool storeReading(EnvironmentalReading reading);

        public Command getCommand();
    }
}
