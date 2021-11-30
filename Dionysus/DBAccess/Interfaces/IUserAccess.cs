using Dionysus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public interface IUserAccess
    {
        Task<string> RegisterAsync(UserRegisterModel model);

        Task<string> LoginAsync(UserLoginModel model);
    }
}
