using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IRatingDBAccess
    {
        Task<int> addRating(Rating rating);
        Task<List<Rating>> getRatings(int batchId);
    }
}
