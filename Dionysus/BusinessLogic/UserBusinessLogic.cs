using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IUserDBAccess userDBAccess;
        private IElevationCodeDBAccess elevationCodeDBAccess;
        public UserBusinessLogic(IUserDBAccess userDBAccess,IElevationCodeDBAccess elevationCodeDBAccess)
        {
            this.userDBAccess = userDBAccess;
            this.elevationCodeDBAccess = elevationCodeDBAccess;
        }
        public async Task<User> addUser(User user, string? validationCode)
        {
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
                    var result = await userDBAccess.addUser(user);
                    return result;
                }
            }
            return null;
        }


        public async Task<User> getUser(string username,string password)
        {
            var user = await userDBAccess.getUser(username);
            if (user is not null && user.Password.Equals(password))
            {
                return user;
            }
            return null;
        }


    }
}
