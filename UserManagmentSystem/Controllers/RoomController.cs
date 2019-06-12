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
    public class RoomController : Controller
    {
        private readonly UserContext _context;
        public RoomController(UserContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("Rooms")]
        public JsonResult GetRooms()
        {
            return Json(_context.Rooms.Select(room => (new { room})));
        }


        [Authorize]
        [HttpPost("Manage")]
        public JsonResult RoomManaging(int id, [FromBody] Room game)
        {
            var roomInfo = _context.Rooms.FirstOrDefault(room => room.Id == id);
            if(roomInfo == null)
            {
                Room newRoom = new Room() { Id = id, Player1Name = User.Identity.Name, timeControl = game.timeControl, timeControlBonus = game.timeControlBonus, Cash = game.Cash, GameName = game.GameName };
                _context.Rooms.Add(newRoom);
                _context.SaveChanges();
                return Json(newRoom);
            }
            else
            {
                var joinedRoom = _context.Rooms.FirstOrDefault(room => room.Id == id);
                if(joinedRoom.Player1Name == User.Identity.Name)
                {
                    return Json(new { status = 101 });
                }
                if(joinedRoom.Player1Name != null && joinedRoom.Player2Name != null)
                {
                    return Json(new { status = 102 });
                }
                joinedRoom.Player2Name = User.Identity.Name;

                //_context.Rooms.Remove(joinedRoom);
                _context.SaveChanges();
                return Json(joinedRoom);
            }
        }

        [HttpGet("checkroom")]
        public JsonResult CheckRoom(int Id)
        {
            var gameroom = _context.Rooms.FirstOrDefault(room => room.Id == Id && room.Player1Name != null && room.Player2Name != null);
            if (gameroom != null)
            {
                return Json(new { Username1 = gameroom.Player1Name, Username2 = gameroom.Player2Name });
            }
            return Json(new { status = 101 });
        }

    }
}