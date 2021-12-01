
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
        Task<AvarageDataReadingDTO> getAvarageReadingSinceBeginning(int batchId);

        
        
        
    }
}
