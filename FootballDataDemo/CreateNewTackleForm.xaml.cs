using FootballDataDemo.Data;
using FootballDataDemo.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootballDataDemo
{
    /// <summary>
    /// Логика взаимодействия для CreateNewTackleForm.xaml
    /// </summary>
    public partial class CreateNewTackleForm : Window
    {
        private AppDbContext db;
        private Match match;

        public CreateNewTackleForm(int id)
        {
            InitializeComponent();

            db = new AppDbContext();

            match = db.Matches
                .Include(m => m.Team1).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Include(m => m.Team2).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Where(m => m.Id == id).SingleOrDefault();

            PopulateTacklingTeamList();

            Closing += MainWindow_Closing;
        }

        private void PopulateTacklingTeamList()
        {
            List<string> teams = new List<string>
            {
                match.Team1.Name,
                match.Team2.Name
            };

            tacklingTeamList.ItemsSource = teams;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource tackleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tackleViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // tackleViewSource.Source = [универсальный источник данных]
        }

        private void TacklingTeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tackledTeamNameLabel.Content =
                match.Team1.Name == tacklingTeamList.SelectedValue.ToString() ?
                match.Team2.Name : match.Team1.Name;

            PopulateTacklingPlayerList();
            PopulateTackledPlayerList();
        }

        private void PopulateTackledPlayerList()
        {
            List<string> goalkeepres = new List<string>();

            if (match.Team1.Name == tacklingTeamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team2.Players)
                {
                    goalkeepres.Add(p.Name);
                }
            }
            else
            {
                foreach (Player p in match.Team1.Players)
                {
                    goalkeepres.Add(p.Name);
                }
            }

            tackledPlayerList.ItemsSource = goalkeepres;
        }

        private void PopulateTacklingPlayerList()
        {
            List<string> players = new List<string>();

            if (match.Team1.Name == tacklingTeamList.SelectedValue.ToString())
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

            tacklingPlayerList.ItemsSource = players;
        }

        private void CreateNewTackleButton_Click(object sender, RoutedEventArgs e)
        {
            Tackle newTackle = new Tackle
            {
                Match = match,
                TacklingTeam = db.Teams.Where(t => t.Name == tacklingTeamList.SelectedValue.ToString()).SingleOrDefault(),
                TackledTeam = db.Teams.Where(t => t.Name == tackledTeamNameLabel.Content.ToString()).SingleOrDefault(),
                TacklingPlayer = db.Players.Where(p => p.Name == tacklingPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                TackledPlayer = db.Players.Where(p => p.Name == tackledPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                TackleTime = int.TryParse(tackleTimeTextBox.Text, out int time) ? time : 0
            };

            db.Tackles.Local.Add(newTackle);

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
