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
    /// Логика взаимодействия для CreateNewGoalForm.xaml
    /// </summary>
    public partial class CreateNewGoalForm : Window
    {
        private AppDbContext db;
        private Match match;

        /// <summary>
        /// Форма создания нового гола
        /// </summary>
        /// <param name="id">Идентификатор матча которому будет принадлежать гол</param>
        public CreateNewGoalForm(int id)
        {
            InitializeComponent();

            db = new AppDbContext();

            match = db.Matches
                .Include(m => m.Team1).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Include(m => m.Team2).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Where(m => m.Id == id).SingleOrDefault();

            PopulateScoringTeamList();

            Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Заполнить выпадающий список команд забивающих гол
        /// </summary>
        private void PopulateScoringTeamList()
        {
            List<string> teams = new List<string>
            {
                match.Team1.Name,
                match.Team2.Name
            };

            scoringTeamList.ItemsSource = teams;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource goalViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("goalViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // goalViewSource.Source = [универсальный источник данных]
        }

        /// <summary>
        /// Вызывается при выборе команды в в         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoringTeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            concedingTeamNameLabel.Content =
                match.Team1.Name == scoringTeamList.SelectedValue.ToString() ?
                match.Team2.Name : match.Team1.Name;

            PopulateScoringPlayerList();
            PopulateGoalkeeperList();
        }

        private void PopulateGoalkeeperList()
        {
            List<string> goalkeepres = new List<string>();

            if (match.Team1.Name == scoringTeamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team2.Players.Where(p => p.Role.RoleType == RoleType.Goalkeeper))
                {
                    goalkeepres.Add(p.Name);
                }
            }
            else
            {
                foreach (Player p in match.Team1.Players.Where(p => p.Role.RoleType == RoleType.Goalkeeper))
                {
                    goalkeepres.Add(p.Name);
                }
            }

            goalkeeperList.ItemsSource = goalkeepres;
        }

        private void PopulateScoringPlayerList()
        {
            List<string> players = new List<string>();

            if (match.Team1.Name == scoringTeamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team1.Players)
                {
                    players.Add(p.Name);
                }
            }
            else
            {
                foreach (Player p in match.Team2.Players)
                {
                    players.Add(p.Name);
                }
            }

            scoringPlayerList.ItemsSource = players;
        }

        private void CreateNewGoalButton_Click(object sender, RoutedEventArgs e)
        {
            Goal newGoal = new Goal
            {
                Match = match,
                ScoringTeam = db.Teams.Where(t => t.Name == scoringTeamList.SelectedValue.ToString()).SingleOrDefault(),
                ConcedingTeam = db.Teams.Where(t => t.Name == concedingTeamNameLabel.Content.ToString()).SingleOrDefault(),
                ScoringPlayer = db.Players.Where(p => p.Name == scoringPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                Goalkeeper = db.Players.Where(p => p.Name == goalkeeperList.SelectedValue.ToString()).SingleOrDefault(),
                GoalTime = int.TryParse(goalTimeTextBox.Text, out int time) ? time : 0
            };

            db.Goals.Local.Add(newGoal);

            db.SaveChanges();

            Close();
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
