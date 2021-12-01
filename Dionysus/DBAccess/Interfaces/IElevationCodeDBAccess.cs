using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
   public  interface IElevationCodeDBAccess
    {
        Task<string> getValidationCode(string validationCode);
        Task removeValidationCode(string validationCode);
    }
}
