using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс команды
    /// </summary>
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        /// <summary>
        /// Данные по составу команд в читаемой форме
        /// </summary>
        public string TeamStats
        {
            get 
            {
                if (Players == null)
                {
                    return "";
                }

                string stats = "";
                
                int goalkeepersNum = 0;
                int defendersNum = 0;
                int attackersNum = 0;

                // подсчет игроков
                foreach (Player p in Players)
                {
                    switch (p.Role.RoleType)
                    {
                        case RoleType.Goalkeeper:
                            goalkeepersNum++;
                            break;
                        case RoleType.Defender:
                            defendersNum++;
                            break;
                        case RoleType.Attacker:
                            attackersNum++;
                            break;
                    }
                }

                int totalPlayersNum = goalkeepersNum + defendersNum + attackersNum;

                stats = "Вратарей: " + goalkeepersNum + ", защитников: " + defendersNum +
                    ", нападающих: " + attackersNum;

                return stats;
            }
        }

        public string LongName => Name + " (" + Abbreviation + ")";

        public Team ()
        {

        }
    }
}