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

        [HttpGet("Rooms")]
        [Authorize]
        public JsonResult GetRooms()
        {
            return Json(_context.Rooms.Select(room => (new { Player1 = room.Player1Name, Player2 = room.Player2Name, Cash = room.Cash})));
        }

        [HttpPost("Create")]
        [Authorize]
        public void CreateRoom([FromBody] Room roomToCreate)
        {
            Room newRoom = new Room() { Player1Name = User.Identity.Name, Cash = roomToCreate.Cash };
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();
        }

        [Authorize]
        [HttpPost("Join")]
        public void JoinRoom([FromBody]int roomId)
        {
            var joinedRoom =_context.Rooms.FirstOrDefault(room => room.Id == roomId);
            joinedRoom.Player2Name = User.Identity.Name;
            
            _context.Rooms.Remove(joinedRoom);
            _context.SaveChanges();
        }
            
    }
}