using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void Post([FromBody] User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
        }
        //public 
    }
}