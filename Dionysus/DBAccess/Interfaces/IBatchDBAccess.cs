using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IBatchDBAccess
    {
        Task<DateTime?> getStoredOn(int batchid);

        Task<bool> batchExists(int batchId);
    }
}
