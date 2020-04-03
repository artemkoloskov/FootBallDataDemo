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
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        public ICollection<Goal> Goals { get; set; }
        public ICollection<GoalPass> GoalPasses { get; set; }
        public ICollection<Tackle> Tackles { get; set; }
        public ICollection<Defence> Defences { get; set; }

        [NotMapped]
        /// <summary>
        /// Название матча по названиям команд в длинной форме
        /// </summary>
        public string MatchTitleLong
        {
            get
            {
                string title = Team1.Name + " (" + Team1.Abbreviation +
                    ") - " + Team2.Name + " (" + Team2.Abbreviation + ")";

                return title;
            }
        }
        
        /// <summary>
        /// Название матча по аббревиатурам команд в короткой форме
        /// </summary>
        public string MatchTitleShort
        {
            get
            {
                string title = Team1.Abbreviation + " - " + Team2.Abbreviation;

                return title;
            }
        }
    
        /// <summary>
        /// Результаты матча в читаемой форме
        /// </summary>
        public string MatchResults
        {
            get
            {
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
