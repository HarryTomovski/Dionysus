using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class RatingDBAccess : IRatingDBAccess
    {
        public async Task<int> addRating(Rating rating)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await context.Ratings.AddAsync(rating);
                    await context.SaveChangesAsync();
                    return 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }
        }

        public async Task<List<Rating>> getRatings(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var ratings = await context.Ratings.Select(r => r).Where(r => r.BatchId == batchId).ToListAsync();
                    return ratings;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
