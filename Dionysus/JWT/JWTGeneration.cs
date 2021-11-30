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
            var isAdministrator = user.Role.Contains("Administrator");
            if (isAdministrator)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
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
