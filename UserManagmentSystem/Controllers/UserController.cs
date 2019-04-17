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

        [HttpPost]
        public IActionResult Register([FromBody] User accountInfo)
        {
            var acc = _context.Users.FirstOrDefault((usr) => usr.Username == accountInfo.Username);
            if (!ModelState.IsValid || acc != null)
            {
                return BadRequest("Username already taken");
            }
            Random r = new Random();
            accountInfo.Money = r.Next(0, 100);
            _context.Users.Add(accountInfo);
            _context.SaveChanges();
            return Ok(TokenManager.generateToken(accountInfo.Username));
        }


        [HttpGet("Ranking")]
        public IEnumerable<string> GetRanking()
        {
            return _context.Users.OrderBy((usr) => usr.Money).Select((usr) => usr.Username + " " + usr.Money);
        }

        [HttpGet("Info")]
        [Authorize]
        public ActionResult<User> GetAllInfo()
        {
            User currentUser = _context.Users.FirstOrDefault((usr) => usr.Username == User.Identity.Name);
            if(currentUser == null)
            {
                return BadRequest();
            }
            return Ok(currentUser);
        }

        
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteAccount()
        {
            var acc = _context.Users.FirstOrDefault((usr) => usr.Username == User.Identity.Name);
            if(acc == null)
            {
                return BadRequest("Error");
            }
            _context.Remove(acc);
            _context.SaveChanges();
            return Ok("User deleted succesfully");
        }
    }
}