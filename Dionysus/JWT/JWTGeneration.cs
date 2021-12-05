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

namespace Dionysus.JWT
{
    public class JWTGeneration : IJWTGeneration
    {

        private readonly ApplicationSettings applicationSettings;

        public JWTGeneration(IOptions<ApplicationSettings> applicationSettings)
        {
            this.applicationSettings = applicationSettings.Value;
        }
        public string GenerateJwtAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
            };

            //we are gonna check every role, for now we only have if its admin
            var isAdministrator = user.Role.Contains(UserEnums.Administrator.ToString());
            var isWinemaker = user.Role.Contains(UserEnums.Winemaker.ToString());
            var isSommelier = user.Role.Contains(UserEnums.Somelier.ToString());
            var isDilletant = user.Role.Contains(UserEnums.Dilletant.ToString());
            if (isAdministrator)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserEnums.Administrator.ToString()));
            }
            if (isWinemaker)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserEnums.Winemaker.ToString()));
            }
            if (isSommelier)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserEnums.Somelier.ToString()));
            }
            if (isDilletant)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserEnums.Dilletant.ToString()));
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
