using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class ElevationCodeDBAccess : IElevationCodeDBAccess
    {
        public async Task<string> getValidationCode(string validationCode)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var code = await Task.Run(() => context.ElevationCodes.Find(validationCode));
                    return code.Code;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task removeValidationCode(string validationCode)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var code = await Task.Run(() => context.ElevationCodes.Find(validationCode));
                    await Task.Run(() => context.ElevationCodes.Remove(code));
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
