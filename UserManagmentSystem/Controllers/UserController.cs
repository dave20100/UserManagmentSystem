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
    [Produces("application/json")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public JsonResult Register([FromBody] User accountInfo)
        {
            if (findAndReturnUserFromDb(accountInfo.Username) != null)
            {
                return Json(new { status = 101 });
            }
            
            accountInfo.RankingPoints = 1000;
            _context.Users.Add(accountInfo);
            _context.SaveChanges();
            return Json(new { status = 100, token = TokenManager.generateToken(accountInfo.Username) });
        }


        [HttpGet("Ranking")]
        public JsonResult GetRanking()
        {

            return Json(_context.Users.OrderBy((usr) => usr.RankingPoints).Select((usr) => new { username = usr.Username, points = usr.RankingPoints}));
        }

        [HttpGet("Info")]
        [Authorize]
        public ActionResult<User> GetAllInfo()
        {
            User currentUser = findAndReturnUserFromDb(User.Identity.Name);
            if (currentUser == null)
            {
                return BadRequest();
            }
            return Ok(currentUser);
        }

        
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteAccount()
        {
            var acc = findAndReturnUserFromDb(User.Identity.Name);
            if (acc == null)
            {
                return BadRequest("Error");
            }
            _context.Remove(acc);
            _context.SaveChanges();
            return Ok("User deleted succesfully");
        }
    
        [HttpPost("AddFriend")]
        [Authorize]
        public IActionResult AddFriend([FromBody] string friendUsername)
        {
            var acc = findAndReturnUserFromDb(User.Identity.Name);

            var friendAcc = findAndReturnUserFromDb(friendUsername);

            if (acc == null || friendAcc == null || acc == friendAcc)
            {
                return BadRequest();
            }

            var existingFriendship = _context.Friends.FirstOrDefault((friendship) => (friendship.FriendId1 == acc.Id || friendship.FriendId2 == acc.Id)
            && (friendship.FriendId2 == friendAcc.Id || friendship.FriendId2 == friendAcc.Id));
            if (existingFriendship != null){
                if (existingFriendship.Accepted == true)
                {
                    return BadRequest();
                }
                existingFriendship.Accepted = true;
                _context.SaveChanges();
                return Ok();
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
        public IEnumerable<string> ShowFriends()
        {
            var acc = findAndReturnUserFromDb(User.Identity.Name);
            if (acc == null)
            {
                return null;
            }

            List<string> usersFriends = new List<string>();

            foreach (Friends friendship in _context.Friends)
            {
                if (acc.Id != friendship.FriendId1 && acc.Id != friendship.FriendId2)
                {
                    continue;
                }
                if (!friendship.Accepted)
                {
                    continue;
                }
                int idOfFriend = friendship.FriendId1 != acc.Id ? friendship.FriendId1 : friendship.FriendId2;
                User user = _context.Users.FirstOrDefault((usr) => usr.Id == idOfFriend);
                usersFriends.Add(acc.Id + " " + idOfFriend + " " + user.Username);
            }
            return usersFriends;


        }

        private User findAndReturnUserFromDb(string username)
        {
            return _context.Users.FirstOrDefault((usr) => usr.Username == username);
        }
    }
}