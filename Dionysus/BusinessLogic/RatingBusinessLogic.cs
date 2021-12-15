using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class RatingBusinessLogic : IRatingBusinessLogic
    {
        private readonly IRatingDBAccess ratingDBAccess;
        public RatingBusinessLogic(IRatingDBAccess ratingDBAccess)
        {
            this.ratingDBAccess = ratingDBAccess;
        }
        public async Task<int> addRating(Rating rating)
        {
            var result = await ratingDBAccess.addRating(rating);
            return result;
        }

        public async Task<List<RatingDTO>> getRatings(int batchId)
        {
            var result = await ratingDBAccess.getRatings(batchId);
            
            List<RatingDTO> list = new();
            foreach (var r in result)
            {
                RatingDTO ratingDTO = new()
                {
                    RatingId = r.RatingId,
                    Username = r.Username,
                    BatchId = r.BatchId,
                    SweatnessRating = r.SweatnessRating,
                    AcidityRating = r.AcidityRating,
                    BitternessRating = r.BitternessRating,
                    OverallRating = r.OverallRating,
                    Feedback = r.Feedback,
                    RatedOn = r.RatedOn
                };
                list.Add(ratingDTO);
            }
            return list;
        }
    }
}