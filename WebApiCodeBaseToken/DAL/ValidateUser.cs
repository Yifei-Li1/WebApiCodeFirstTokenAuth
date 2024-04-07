using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCodeBaseToken.DAL
{
    public class ValidateUser
    {
        public static bool Login(string username,string password)
        {
            UserRepo userRepo = new UserRepo();
            return userRepo.CheckExists(username, password);

        }

    }
}