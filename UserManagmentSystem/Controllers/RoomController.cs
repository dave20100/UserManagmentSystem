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
    [Route("api/[controller]/{id?}")]
    [Produces("application/json")]

    [ApiController]
    public class RoomController : Controller
    {
        private readonly UserContext _context;
        public RoomController(UserContext context)
        {
            _context = context;
        }

        [HttpGet("Rooms")]
        [Authorize]
        public JsonResult GetRooms()
        {
            return Json(_context.Rooms.Select(room => (new { room})));
        }

        [HttpPost("Create")]
        [Authorize]
        public void CreateRoom([FromBody] Room roomToCreate)
        {
            Room newRoom = roomToCreate;
            newRoom.Player1Name = User.Identity.Name;
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();
        }

        [Authorize]
        [HttpPost("Join")]
        public void JoinRoom([FromBody]Room roomToJoin)
        {
            var joinedRoom =_context.Rooms.FirstOrDefault(room => room.Id == roomToJoin.Id);
            joinedRoom.Player2Name = User.Identity.Name;
            
            _context.Rooms.Remove(joinedRoom);
            _context.SaveChanges();
        }

        [HttpGet]
        public JsonResult GetSpecificInfo(int id)
        {
            var roomInfo = _context.Rooms.FirstOrDefault(room => room.Id == id);
            var player1 = _context.Users.FirstOrDefault(user => user.Username == roomInfo.Player1Name);

            var player2 = _context.Users.FirstOrDefault(user => user.Username == roomInfo.Player2Name);
            return Json(new { Room = roomInfo, Player1Name = player1?.Username, Player1Id = player1?.Id, Player2Name = player2?.Username, Player2Id = player2?.Id });
        }
            
    }
}