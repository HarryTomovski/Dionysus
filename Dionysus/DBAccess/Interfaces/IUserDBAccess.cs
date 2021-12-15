using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.Models.ResponceModels;
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
        Task<bool> ChangeUserRole(string username, string role);
        Task<bool> userExsist(string username);
        Task<List<User>> getAllUsers();
    }
}
