using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс "голевой передачи"
    /// </summary>
    class GoalPass
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public Team PassingTeam { get; set; }
        public Player PassingPlayer { get; set; }
        public Player RecievingPlayer { get; set; }
        public TimeSpan PassTime { get; set; }

        public GoalPass ()
        {
            
        }
    }
}
