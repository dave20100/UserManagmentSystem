using System.Linq;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserContext _context;

        public AuthController(UserContext context)
        {
            _context = context;
        }
        [HttpPost("Login")]
        public JsonResult Login([FromBody] User credentials)
        {
            var existingAccount = _context.Users.FirstOrDefault((user) => user.Username == credentials.Username);
            if(existingAccount == null)
            {
                return Json(1);
                //return BadRequest($"User {credentials.Username} does not exist");
            }
            if (existingAccount.Password.Equals(credentials.Password))
            {
                return Json(TokenManager.generateToken(credentials.Username));
            }
            else
            {
                return Json(2);
            }
        }

        [HttpPost("Logout")]
        public IActionResult LogOut([FromForm] User c)
        {
            return Ok(c);
        }
    }
}