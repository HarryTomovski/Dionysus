using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class EnvironmentalReadingBusinessLogic : IEnvironmentalreadingBusinessLogic
    {
        private IEnvironmentalReadingDBAccess environmentalReadingDBAccess;

        public EnvironmentalReadingBusinessLogic(IEnvironmentalReadingDBAccess environmentalReadingDBAccess)
        {
            this.environmentalReadingDBAccess = environmentalReadingDBAccess;
        }
        public bool storeReading(EnvironmentalReading reading)
        {
            var success = environmentalReadingDBAccess.StoreReading(reading);
   
            return success;
            
            
        }
    }
}
