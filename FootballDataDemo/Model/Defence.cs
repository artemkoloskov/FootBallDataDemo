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
    public class Defence
    {
        public int Id { get; set; }
        public int DefenceTime { get; set; } // Время прошедшее с начала матча, в секундах

        public virtual Match Match { get; set; }
        public virtual Team DefendingTeam { get; set; }
        public virtual Team AttemptingTeam { get; set; }
        public virtual Player Goalkeeper { get; set; }
        public virtual Player AttemtingPlayer { get; set; }
        
        public Defence ()
        {

        }
    }
}
