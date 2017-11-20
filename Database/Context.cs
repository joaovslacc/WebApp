using Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Context : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
