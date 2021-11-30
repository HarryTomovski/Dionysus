using Dionysus.DBModels;
using Dionysus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface IUserBusinessLogic
    {
        
        Task<string> RegisterUser(UserRegisterModel model);
        Task<string> LoginUser(UserLoginModel model);
        
    }
}
