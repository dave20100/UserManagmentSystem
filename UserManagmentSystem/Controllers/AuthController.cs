﻿using System.Linq;
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

        [HttpPost("LogIn")]
        public IActionResult LogIn([FromForm] User credentials)
        {
            var existingAccount = _context.Users.FirstOrDefault((user) => user.Username == credentials.Username);
            if(existingAccount == null)
            {
                return BadRequest();
            }
            if (existingAccount.Password.Equals(credentials.Password))
            {
                return Ok(TokenManager.generateToken(credentials.Username));
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("LogOut")]
        public IActionResult LogOut([FromForm] User c)
        {
            return Ok(c);
        }
    }
}