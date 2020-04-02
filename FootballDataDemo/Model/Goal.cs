using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс "гола"
    /// </summary>
    class Goal
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public Team ScoringTeam { get; set; }
        public Team ConcedingTeam { get; set; }
        public Player ScoringPlayer { get; set; }
        public Player Goalkeeper { get; set; }
        public TimeSpan GoalTime { get; set; }

        public Goal ()
        {

        }
    }
}
