using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class BatchDBAccess : IBatchDBAccess
    {
        public async Task<bool> batchExists(int batchId)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    //var exists = await context.Batches.AnyAsync(b => b.BatchId == batchId);
                    //var exists = await context.Batches.
                    return exists;
                }
                catch (Exception e )
                {

                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<DateTime?> getStoredOn(int batchid)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var storedOn = await Task.Run(() => context.Batches.Where(b => b.BatchId == batchid).Select(s => s.StoredOn).FirstOrDefault());
                    return storedOn;
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
