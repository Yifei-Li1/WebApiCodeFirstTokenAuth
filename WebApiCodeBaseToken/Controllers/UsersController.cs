using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiCodeBaseToken.DAL;
using WebApiCodeBaseToken.DTO;
using WebApiCodeBaseToken.Models;

namespace WebApiCodeBaseToken.Controllers
{
    public class UsersController : ApiController
    {
        private ProductContext db = new ProductContext();

        // GET: api/Users
        [Authorize(Roles = "Admin")]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [Authorize(Roles ="Admin")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }
        

        [HttpPost]
        [Route("api/register")]
        public async Task<IHttpActionResult> Register(UserRegisterModel user)
        {
            if(string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Username))
            {
                return BadRequest("Username nad password is required");
            }
            var userExists = await db.Users.FirstOrDefaultAsync(u => user.Username == u.Username);
            if (userExists != null)
            {
                BadRequest("username already taken");
            }
            var newUser = new User { Username = user.Username, Password = user.Password };
            db.Users.Add(newUser);
            await db.SaveChangesAsync();
            return Ok(new { newUser.Id, newUser.Username });
          
        }


        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}