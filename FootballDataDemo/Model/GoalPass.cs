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
    public class GoalPass
    {
        public int Id { get; set; }
        public int PassTime { get; set; } // Время прошедшее с начала матча, в секундах

        public virtual Match Match { get; set; }
        public virtual Team PassingTeam { get; set; }
        public virtual Player PassingPlayer { get; set; }
        public virtual Player RecievingPlayer { get; set; }

        // Для столбцов таблиц
        public string PassingTeamName => PassingTeam.Name;
        public string PassingPlayerName => PassingPlayer.Name;
        public string RecievingPlayerName => RecievingPlayer.Name;

        public GoalPass ()
        {
            
        }
    }
}
