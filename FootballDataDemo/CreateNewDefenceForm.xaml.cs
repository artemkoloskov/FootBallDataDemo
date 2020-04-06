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
    /// Логика взаимодействия для CreateNewDefenceForm.xaml
    /// </summary>
    public partial class CreateNewDefenceForm : Window
    {
        private AppDbContext db;
        private Match match;

        public CreateNewDefenceForm(int id)
        {
            InitializeComponent();

            db = new AppDbContext();

            match = db.Matches
                .Include(m => m.Team1).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Include(m => m.Team2).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Where(m => m.Id == id).SingleOrDefault();

            PopulateDefendingTeamList();

            Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Наполняет даными выпадающий список выбора защищающейся команды 
        /// </summary>
        private void PopulateDefendingTeamList()
        {
            List<string> teams = new List<string>
            {
                match.Team1.Name,
                match.Team2.Name
            };

            defendingTeamList.ItemsSource = teams;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource defenceViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("defenceViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // defenceViewSource.Source = [универсальный источник данных]
        }

        /// <summary>
        /// При выборе одного из вариантов в выпадающем списке -заполняет лэйбл с противоположной командой,
        /// а так же вызывает наполнение выпадающих списков нападающего игрока и вратаря
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefendingTeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            attemptingTeamNameLabel.Content =
                match.Team1.Name == defendingTeamList.SelectedValue.ToString() ?
                match.Team2.Name : match.Team1.Name;

            PopulateAttemptingPlayerList();
            PopulateGoalkeeperList();
        }

        /// <summary>
        /// заполняет выпадающий список вратарей вратарями из команды противоположной защищающимся
        /// </summary>
        private void PopulateGoalkeeperList()
        {
            List<string> goalkeepres = new List<string>();

            if (match.Team1.Name == defendingTeamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team1.Players.Where(p => p.Role.RoleType == RoleType.Goalkeeper))
                {
                    goalkeepres.Add(p.Name);
                }
            }
            else
            {
                foreach (Player p in match.Team2.Players.Where(p => p.Role.RoleType == RoleType.Goalkeeper))
                {
                    goalkeepres.Add(p.Name);
                }
            }

            goalkeeperList.ItemsSource = goalkeepres;
        }

        /// <summary>
        /// заполняет выпадающий список нападающих игроков нападающей команды
        /// </summary>
        private void PopulateAttemptingPlayerList()
        {
            List<string> players = new List<string>();

            if (match.Team1.Name == defendingTeamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team2.Players)
                {
                    players.Add(p.Name);
                }
            }
            else
            {
                foreach (Player p in match.Team1.Players)
                {
                    players.Add(p.Name);
                }
            }

            attemptingPlayerList.ItemsSource = players;
        }

        /// <summary>
        /// Открыть окно создания новой защиты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewDefenceButton_Click(object sender, RoutedEventArgs e)
        {
            Defence newDefence = new Defence
            {
                Match = match,
                DefendingTeam = db.Teams.Where(t => t.Name == defendingTeamList.SelectedValue.ToString()).SingleOrDefault(),
                AttemptingTeam = db.Teams.Where(t => t.Name == attemptingTeamNameLabel.Content.ToString()).SingleOrDefault(),
                AttemtingPlayer = db.Players.Where(p => p.Name == attemptingPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                Goalkeeper = db.Players.Where(p => p.Name == goalkeeperList.SelectedValue.ToString()).SingleOrDefault(),
                DefenceTime = int.TryParse(defenceTimeTextBox.Text, out int time) ? time : 0
            };

            db.Defences.Local.Add(newDefence);

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
