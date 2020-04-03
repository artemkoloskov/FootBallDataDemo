using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс игрока
    /// </summary>
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public Team Team { get; set; }

        public Player ()
        {

        }
    }
}
