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
    public class FriendsController : ControllerBase
    {
        private readonly UserContext _context;

        public FriendsController(UserContext context)
        {
            _context = context;
        }

        [HttpPost("AddFriend")]
        [Authorize]
        public IActionResult AddFriend([FromBody] string friendUsername)
        {
            var acc = _context.Users.FirstOrDefault((usr) => usr.Username == User.Identity.Name);

            var friendAcc = _context.Users.FirstOrDefault((usr) => usr.Username == friendUsername);

            if (acc == null || friendAcc == null)
            {
                return BadRequest();
            }
            Friends friends = new Friends()
            {
                FriendId1 = acc.Id,
                FriendId2 = friendAcc.Id
            };
            _context.Friends.Add(friends);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("ShowFriends")]
        [Authorize]
        public IEnumerable<Friends> ShowFriends()
        {
            return _context.Friends;
        }
    }
}