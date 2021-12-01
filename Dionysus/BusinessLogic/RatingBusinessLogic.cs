using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class RatingBusinessLogic : IRatingBusinessLogic
    {
        private IRatingDBAccess ratingDBAccess;
        public RatingBusinessLogic(IRatingDBAccess ratingDBAccess)
        {
            this.ratingDBAccess = ratingDBAccess;
        }
        public async Task<int> addRating(Rating rating)
        {
            var result = await ratingDBAccess.addRating(rating);
            return result;
        }

       
    }
}
