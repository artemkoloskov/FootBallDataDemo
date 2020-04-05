using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataDemo.Model
{
    /// <summary>
    /// Класс матча
    /// </summary>
    public class Match
    {
        public int Id { get; set; }

        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<GoalPass> GoalPasses { get; set; }
        public virtual ICollection<Tackle> Tackles { get; set; }
        public virtual ICollection<Defence> Defences { get; set; }

        /// <summary>
        /// Название матча по названиям команд в длинной форме
        /// </summary>
        public string TitleLong => Team1 == null || Team2 == null ? "" :
                    Team1.Name + " (" + Team1.Abbreviation + ") - " +
                    Team2.Name + " (" + Team2.Abbreviation + ")";

        /// <summary>
        /// Название матча по аббревиатурам команд в короткой форме
        /// </summary>
        public string TitleShort => Team1 == null || Team2 == null ? "" :
                    Team1.Abbreviation + " - " + Team2.Abbreviation;
    
        /// <summary>
        /// Результаты матча в читаемой форме
        /// </summary>
        public string Results
        {
            get
            {
                if (Goals == null)
                {
                    return "";
                }

                string results = "";

                int team1Score = 0;
                int team2Score = 0;

                // Подсчет голов
                foreach (Goal g in Goals)
                {
                    if (g.ScoringTeam.Id == Team1.Id)
                    {
                        team1Score++;
                    }
                    else if (g.ScoringTeam.Id == Team2.Id)
                    {
                        team2Score++;
                    }
                }

                results = team1Score + " - " + team2Score;

                if (team1Score > team2Score)
                {
                    results += ", победа " + Team1.Name;
                }
                else if (team2Score > team1Score)
                {
                    results += ", победа " + Team2.Name;
                }
                else
                {
                    results += ", ничья";
                }

                return results;
            }
        }
 
        public Match ()
        {

        }
    }
}
