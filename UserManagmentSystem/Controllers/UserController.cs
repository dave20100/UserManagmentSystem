using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return _context.Users.Select((usr) => usr.Username);
        }

        [HttpGet("Info")]
        [Authorize]
        public User GetSpecific()
        {
            return _context.Users.First((usr) => usr.Username == User.Identity.Name);
        }

        [HttpPost]
        public IActionResult Register([FromBody] User accountInfo)
        {
            var acc = _context.Users.FirstOrDefault((usr) => usr.Username == accountInfo.Username);
            if (acc != null)
            {
                return BadRequest("Username already taken");
            }
            _context.Users.Add(accountInfo);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public void DeleteAccount()
        {
            var acc = _context.Users.First((usr) => usr.Username == User.Identity.Name);
            _context.Remove(acc);
            _context.SaveChanges();
        }
    }
}