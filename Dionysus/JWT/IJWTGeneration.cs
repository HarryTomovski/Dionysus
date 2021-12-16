using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.JWT
{
    public interface IJWTGeneration
    {
        string GenerateJwt(User user);
    }
}
