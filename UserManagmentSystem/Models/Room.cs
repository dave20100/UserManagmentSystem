﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagmentSystem.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public int Cash { get; set; }
        public string GameName { get; set; }
        public int timeControl { get; set; }
        public int timeControlBonus { get; set; }
    }
}
