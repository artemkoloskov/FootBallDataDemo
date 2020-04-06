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
        public int TackleTime { get; set; } // Время прошедшее с начала матча, в секундах

        public virtual Match Match { get; set; }
        public virtual Team TacklingTeam { get; set; }
        public virtual Team TackledTeam { get; set; }
        public virtual Player TacklingPlayer { get; set; }
        public virtual Player TackledPlayer { get; set; }

        // Для столбцов таблиц
        public string TacklingTeamName => TacklingTeam.Name;
        public string TackledTeamName => TackledTeam.Name;
        public string TacklingPlayerName => TacklingPlayer.Name;
        public string TackledPlayerName => TackledPlayer.Name;

        public Tackle ()
        {

        }
    }
}
