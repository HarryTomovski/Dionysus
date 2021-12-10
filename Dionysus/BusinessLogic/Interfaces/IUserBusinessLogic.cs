using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.Models.ResponceModels;
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
        Task<bool> ChangeUserRole(string username, string role);
        public Task<List<UserResponceModels>> getAllUsers();
    }
}
