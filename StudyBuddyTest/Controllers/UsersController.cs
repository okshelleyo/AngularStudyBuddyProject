using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyBuddyTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyBuddyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StudyDBContext _context;
        public UsersController(StudyDBContext context)
        {
            _context = context;
        }

        #region Get/Read
        //GET api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        //GET api/Users/{UserName}
        [HttpGet("{UserName}")]
        public async Task<ActionResult<User>> GetUser(string UserName)
        {
            var user = await _context.Users.FindAsync(UserName);
            if (user.UserName == null)
            {
                return NotFound();
            }
            else
            {
                return user;
            }
        }
        #endregion

        #region Create/Post/Add

        //POST api/Users
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { UserName = user.UserName }, user);
        }


        #endregion

        #region Delete
        //DELETE api/Users/{UserName}
        [HttpDelete("{UserName}")]
        public async Task<ActionResult<User>> DeleteUser(string UserName)
        {
            var user = await _context.Users.FindAsync(UserName);
            if (UserName == null)
            {
                return NotFound();
            }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }

        #endregion

    }
}
