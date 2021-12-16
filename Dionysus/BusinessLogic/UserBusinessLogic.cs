using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.DTO_s;
using Dionysus.JWT;
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
        private readonly IJWTGeneration tokenGenerator;

        public UserBusinessLogic(IUserDBAccess userDBAccess, IJWTGeneration tokenGenerator)
        {
            this.userDBAccess = userDBAccess;
            this.tokenGenerator = tokenGenerator;
        }
        public async Task<string> RegisterUser(UserRegisterModel model)
        {
            var user = await userDBAccess.RegisterAsync(model.Name, model.Username, model.Password);
            if(user is not null)
            {
                var token = tokenGenerator.GenerateJwt(user);
                return token;
            }
            return string.Empty;
        }

        public async Task<string> LoginUser(UserLoginModel model)
        {
            var user = await userDBAccess.LoginAsync(model.Username, model.Password);
            if(user is not null)
            {
                var token = tokenGenerator.GenerateJwt(user);
                return token;
            }
            return string.Empty;
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

        public async Task<List<UserDTO>> getAllUsers()
        {
            var users = await userDBAccess.getAllUsers();
            if (users != null)
            {
                List<UserDTO> userDTOs = new();
                foreach (var u in users)
                {
                    UserDTO userDTO = new()
                    {
                        Username = u.Username,
                        Role = u.Role
                    };
                    userDTOs.Add(userDTO);
                }
                return userDTOs;
            }
            return null;
        }
    }

        
}
