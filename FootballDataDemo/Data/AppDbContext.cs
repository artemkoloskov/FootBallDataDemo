using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballDataDemo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace FootballDataDemo.Data
{
    class AppDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

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
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlite("Data Source=FootballDataDemo.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
