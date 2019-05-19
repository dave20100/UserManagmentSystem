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
    public class GameController : ControllerBase
    {
        private readonly UserContext _context;

        public GameController(UserContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("Lose")]
        public void Lose()
        {
            User loser = _context.Users.FirstOrDefault(usr => usr.Username == User.Identity.Name);
            loser.RankingPoints -= 20;
            _context.SaveChanges();
        }

        [Authorize]
        [HttpGet("Win")]
        public void Win()
        {
            User loser = _context.Users.FirstOrDefault(usr => usr.Username == User.Identity.Name);
            loser.RankingPoints += 20;
            _context.SaveChanges();
        }
    }
}