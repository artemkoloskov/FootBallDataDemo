using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс "защиты ворот"
    /// </summary>
    class Defence
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public Team DefendingTeam { get; set; }
        public Team AttemptingTeam { get; set; }
        public Player Goalkeeper { get; set; }
        public Player AttemtingPlayer { get; set; }
        public TimeSpan DefenceTime { get; set; }

        public Defence ()
        {

        }
    }
}
