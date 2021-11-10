using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public interface IRaspberryBusinessLogic
    {
        public bool storeReading(EnvironmentalReading reading);

        public Command getCommand();
        
    }
}
