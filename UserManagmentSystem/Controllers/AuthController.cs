using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;

        public AuthController(UserContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public IActionResult LogIn([FromForm] User credentials)
        {
            var existingAccount = _context.Users.FirstOrDefault((user) => user.Username == credentials.Username);
            if(existingAccount == null)
            {
                return BadRequest($"User {credentials.Username} does not exist");
            }
            if (existingAccount.Password.Equals(credentials.Password))
            {
                return Ok(TokenManager.generateToken(credentials.Username));
            }
            else
            {
                return Forbid("Incorrect password");
            }
        }

        [HttpPost("Logout")]
        public IActionResult LogOut([FromForm] User c)
        {
            return Ok(c);
        }
    }
}