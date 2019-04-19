using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagmentSystem.Models
{
    public class WaitingRoom
    {
        [Key]
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int BetAmount { get; set; }
    }
}
