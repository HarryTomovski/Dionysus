using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface IRatingBusinessLogic
    {
        Task<int> addRating(Rating rating);
    }
}
