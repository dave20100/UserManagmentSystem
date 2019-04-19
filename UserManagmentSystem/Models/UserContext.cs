using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagmentSystem.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }

        public DbSet<WaitingRoom> waitingRooms { get; set; }

    }
}
