using Dionysus.DBModels;
using Dionysus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IUserDBAccess
    {
        Task<string> RegisterAsync(UserRegisterModel model);
        Task<string> LoginAsync(UserLoginModel model);
        Task<bool> userExsist(string username);
        
    }
}
