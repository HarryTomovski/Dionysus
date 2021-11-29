using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic.Interfaces
{
    public interface IUserBusinessLogic
    {
        Task<User> addUser(User user, string? validationCode);
        Task<User> getUser(string username, string password);
        
    }
}
