using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}