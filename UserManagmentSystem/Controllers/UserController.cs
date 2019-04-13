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
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public User GetSpecific(int id)
        {
            return _context.Users.First((usr) => usr.Id == id);
        }

        [HttpPost]
        public void Register([FromBody] User accountInfo)
        {
            _context.Users.Add(accountInfo);
            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteAccount([FromForm] User accountToDelete)
        {
            var acc = _context.Users.First((usr) => usr.Username == accountToDelete.Username);
            _context.Remove(acc);
            _context.SaveChanges();
        }
    }
}