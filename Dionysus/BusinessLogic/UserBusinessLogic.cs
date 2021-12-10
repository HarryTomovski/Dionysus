using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.Models;
using Dionysus.Models.ResponceModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {

        private readonly IUserDBAccess userDBAccess;
        
        public UserBusinessLogic(IUserDBAccess userDBAccess)
        {
            this.userDBAccess = userDBAccess;
            
        }
        public Task<string> RegisterUser(UserRegisterModel model)
        {
            var token = userDBAccess.RegisterAsync(model);
            return token;
        }

        public Task<string> LoginUser(UserLoginModel model)
        {
            var token = userDBAccess.LoginAsync(model);
            return token;
        }

        public async Task<bool> ChangeUserRole(string username, string role)
        {
            var exsists = await userDBAccess.userExsist(username);
            var validRole = Enum.IsDefined(typeof(UserEnums), role);
            if(exsists && validRole)
            {
                var success = await userDBAccess.ChangeUserRole(username, role);
                return success;
            }
            return false;
        }

        public Task<List<UserResponceModels>> getAllUsers()
        {
            var users = userDBAccess.getAllUsers();
            if (users != null)
            {
                return users;
            }
            return null;
        }
    }

        
}
