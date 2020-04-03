using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс "отбора"
    /// </summary>
    public class Tackle
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public Team TacklingTeam { get; set; }
        public Team TackledTeam { get; set; }
        public Player TacklingPlayer { get; set; }
        public Player TackledPlayer { get; set; }
        public int TackleTime { get; set; } // Время прошедшее с начала матча, в секундах

        public Tackle ()
        {

        }
    }
}
