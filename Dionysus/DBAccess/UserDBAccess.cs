using Dionysus.DBAccess.Interfaces;
using Dionysus.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.DBAccess
{
    public class UserDBAccess : IUserDBAccess
    {
        public async Task<User> addUser(User user)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    await Task.Run(() => context.Users.Add(user));
                    context.SaveChanges();
                    return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
        public async Task<User> getUser(string username)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var user = await Task.Run(() => context.Users.Find(username));
                    return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
        public async Task<bool> userExsist(string username)
        {
            using (var context = new DionysusContext())
            {
                try
                {
                    var validUsername = await Task.Run(() => context.Users.Find(username));
                    if (validUsername is null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }
}
