using FootballDataDemo.Data;
using FootballDataDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootballDataDemo
{
    /// <summary>
    /// Логика взаимодействия для TeamDataForm.xaml
    /// </summary>
    public partial class TeamDataForm : Window
    {
        private AppDbContext db;

        public TeamDataForm(int id, string teamName)
        {
            InitializeComponent();

            Update(id, teamName);
        }

        /// <summary>
        /// Обновить данные формы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teamName"></param>
        private void Update(int id, string teamName)
        {
            db = new AppDbContext();

            var goals = db.Goals.Include(g => g.ScoringPlayer);
            var goalPasses = db.GoalPasses.Include(g => g.PassingPlayer);
            var tackles = db.Tackles.Include(g => g.TacklingPlayer);
            var defences = db.Defences.Include(g => g.Goalkeeper);

            IQueryable<Player> playersQuery = db.Players
                .Include(p => p.Role)
                .Include(p => p.Team)
                .Where(p => p.Team.Id == id);

            List<Player> players = playersQuery.ToList();
            
            // Подсчет статистки игрока
            foreach (Player p in players)
            {
                int goalsNum = 0;
                int defencesNum = 0;
                int tacklesNum = 0;
                int goalPassesNum = 0;

                foreach (Goal g in goals.ToList())
                {
                    if (p.Id == g.ScoringPlayer.Id)
                    {
                        goalsNum++;
                    }
                }

                foreach (Defence d in defences.ToList())
                {
                    if (p.Id == d.Goalkeeper.Id)
                    {
                        defencesNum++;
                    }
                }

                foreach (Tackle t in tackles.ToList())
                {
                    if (p.Id == t.TacklingPlayer.Id)
                    {
                        tacklesNum++;
                    }
                }

                foreach (GoalPass g in goalPasses.ToList())
                {
                    if (p.Id == g.PassingPlayer.Id)
                    {
                        goalPassesNum++;
                    }
                }

                p.GoalsNum = goalsNum;
                p.DefencesNum = defencesNum;
                p.TacklesNum = tacklesNum;
                p.GoalPassesNum = goalPassesNum;
            }

            teamLabel.Content = teamName;

            //  Заполнить таблицу команд
            playersDataGrid.AutoGenerateColumns = false;
            playersDataGrid.CanUserAddRows = false;
            playersDataGrid.ItemsSource = players;

            Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Удаляет контекст из памяти.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
    }
}
