using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballDataDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace FootballDataDemo.Data
{
    class AppDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalPass> GoalPasses { get; set; }
        public DbSet<Tackle> Tackles { get; set; }
        public DbSet<Defence> Defences { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FootballDataDemo.db");
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}
