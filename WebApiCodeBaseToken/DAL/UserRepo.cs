using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCodeBaseToken.Models;

namespace WebApiCodeBaseToken.DAL
{
    public class UserRepo
    {
        private readonly ProductContext _productContext;
        public UserRepo()
        {
            _productContext = new ProductContext();
        }
        public bool CheckExists(string username, string password)
        {
            return _productContext.Users.Any(u => u.Username == username && u.Password == password);
        }
        public User GetUserWithUsername(string username)
        {
            var user = _productContext.Users.FirstOrDefault(u => u.Username == username );
            return user;
        }
        public async Task<User> GetUserWithId(int id)
        {
            var user = await _productContext.Users.FindAsync(id);
            return user;
        }
    }
}