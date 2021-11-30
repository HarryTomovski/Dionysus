using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.Models;
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

        private readonly ApplicationSettings applicationSettings;

        public UserBusinessLogic(IOptions<ApplicationSettings> applicationSettings)
        private IUserDBAccess userDBAccess;
        private IElevationCodeDBAccess elevationCodeDBAccess;
        public UserBusinessLogic(IUserDBAccess userDBAccess,IElevationCodeDBAccess elevationCodeDBAccess)
        {
            this.applicationSettings = applicationSettings.Value;
            this.userDBAccess = userDBAccess;
            this.elevationCodeDBAccess = elevationCodeDBAccess;
        }

        public string GenerateJwtAsync(User user)
        public async Task<User> addUser(User user, string? validationCode)
        {
            var claims = new List<Claim>
            var usernameValid = await userDBAccess.userExsist(user.Username);
            if (usernameValid == false)
            {
                if (user.Role == UserEnums.Somelier.ToString())
                {
                    var valCode = await elevationCodeDBAccess.getValidationCode(validationCode);
                    if (valCode is not null && valCode.Equals(validationCode))
                    {
                        var result = await userDBAccess.addUser(user);
                        if (result is not null)
                        {
                            await elevationCodeDBAccess.removeValidationCode(validationCode);
                            return result;
                        }
                    }
                }
                else
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
            };
                    var result = await userDBAccess.addUser(user);
                    return result;
                }
            }
            return null;
        }

            var isAdministrator = user.Role.Contains("Administrator");

            if (isAdministrator)
        public async Task<User> getUser(string username,string password)
        {
            var user = await userDBAccess.getUser(username);
            if (user is not null && user.Password.Equals(password))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                return user;
            }
            return null;
            }

            var secret = Encoding.UTF8.GetBytes(this.applicationSettings.Secret);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
