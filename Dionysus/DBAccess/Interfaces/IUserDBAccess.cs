using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess.Interfaces
{
    public interface IUserDBAccess
    {
        Task<User> addUser(User user);
        Task<User> getUser(string username);
        Task<bool> userExsist(string username);
        
    }
}
