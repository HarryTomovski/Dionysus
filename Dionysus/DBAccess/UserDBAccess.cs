using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using Dionysus.JWT;
using Dionysus.Models;
using Dionysus.Models.ResponceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class UserDBAccess : IUserDBAccess
    {
        private readonly IJWTGeneration tokenGenerator;
        public UserDBAccess(IJWTGeneration tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }
        public async Task<User> LoginAsync(UserLoginModel model)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                    return user;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task<User> RegisterAsync(UserRegisterModel model)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

                    if (user != null)
                    {
                        return null;
                    }

                    user = new User
                    {
                        Name = model.Name,
                        Username = model.Username,
                        Password = model.Password,
                        Role = UserEnums.Dilletant.ToString()
                    };

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();

                    return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public async Task<bool> ChangeUserRole(string username, string role)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    user.Role = role;
                    //update db
                    context.Users.Attach(user);
                    context.Entry(user).Property(u => u.Role).IsModified = true;
                    //save changes
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<bool> userExsist(string username)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var validUsername = await context.Users.FindAsync(username);
                    return validUsername is not null; 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public async Task<List<User>> getAllUsers()
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var users = await context.Users.ToListAsync();
                    return users;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
   

