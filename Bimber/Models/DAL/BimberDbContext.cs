using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL
{
    class BimberDbContext : DbContext
    {
        public BimberDbContext() : base("BimberConnectionString")
        {

        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
