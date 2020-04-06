using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Number { get; set; }

        public virtual Role Role { get; set; }
        public virtual Team Team { get; set; }

        [NotMapped]
        public int GoalsNum { get; set; } = 0;
        [NotMapped]
        public int DefencesNum { get; set; } = 0;
        [NotMapped]
        public int TacklesNum { get; set; } = 0;
        [NotMapped]
        public int GoalPassesNum { get; set; } = 0;

        // Для столбцов таблиц
        public string RoleTitle => Role == null ? "" : Role.Title;

        public Player ()
        {

        }
    }
}
