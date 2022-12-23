using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Tournament.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace API_Tournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "administrator,moderator,player")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var username = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user");
            if (username.Value != user.Username && !this.HttpContext.User.IsInRole("administrator"))
            {
                return this.Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "administrator,moderator,player")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var username = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user");
            if (username.Value != user.Username && !this.HttpContext.User.IsInRole("administrator"))
            {
                return this.Forbid();
            }

            if (id != user.PkUserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (await _context.Users.Where(x => x.Username == user.Username || x.Email == user.Email).AnyAsync())
            {
                return BadRequest("Username/Email taken");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.PkUserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "administrator,moderator,player")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
                return NotFound();
            var username = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user");
            if (username.Value != user.Username && !this.HttpContext.User.IsInRole("administrator"))
            {
                return this.Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.PkUserId == id);
        }
    }
}
