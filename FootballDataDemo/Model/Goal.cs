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
    public class Goal
    {
        public int Id { get; set; }
        public int GoalTime { get; set; } // Время прошедшее с начала матча, в секундах

        public virtual Match Match { get; set; }
        public virtual Team ScoringTeam { get; set; }
        public virtual Team ConcedingTeam { get; set; }
        public virtual Player ScoringPlayer { get; set; }
        public virtual Player Goalkeeper { get; set; }

        public Goal ()
        {

        }
    }
}
