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


        [Authorize]
        [HttpPost]
        public JsonResult GetSpecificInfo(int id, [FromBody] Room game)
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
                var foundRoom = _context.Rooms.FirstOrDefault(room => (room.Player1Name == User.Identity.Name && room.Player2Name != null) || (room.Player2Name == User.Identity.Name && room.Player1Name != null));
                if (foundRoom != null)
                {
                    return Json(foundRoom);
                }
                var joinedRoom = _context.Rooms.FirstOrDefault(room => room.Id == id);
                joinedRoom.Player2Name = User.Identity.Name;

                //_context.Rooms.Remove(joinedRoom);
                _context.SaveChanges();
                return null;
            }
        }

    }
}