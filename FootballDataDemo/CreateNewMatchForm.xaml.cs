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
    /// Логика взаимодействия для CreateNewMatchForm.xaml
    /// </summary>
    public partial class CreateNewMatchForm : Window
    {
        private AppDbContext db;

        public CreateNewMatchForm()
        {
            InitializeComponent();

            Update();

            Closing += MainWindow_Closing;
        }

        private void Update()
        {
            db = new AppDbContext();

            db.Matches.Load();

            PopulateTeam1List();

            team2List.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Заполнение списка команд для первой команды
        /// </summary>
        private void PopulateTeam1List ()
        {
            List<string> teams = new List<string>();

            foreach (Team team in db.Teams.ToList())
            {
                teams.Add(team.Name);
            }

            team1List.ItemsSource = teams;
        }

        /// <summary>
        /// Заполнение списка команд для второй команды
        /// </summary>
        private void PopulateTeam2List ()
        {
            List<string> teams = new List<string>();

            foreach (Team team in db.Teams.ToList())
            {
                if (team.Name != team1List.SelectedValue.ToString())
                {
                    teams.Add(team.Name);
                }
            }

            team2List.ItemsSource = teams;
        }


        private void Team1List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateTeam2List();

            team2List.Visibility = Visibility.Visible;
        }

        private void CreateNewMatchButton_Click(object sender, RoutedEventArgs e)
        {
            Match match = new Match
            {
                Team1 = db.Teams.Where(t => t.Name == team1List.SelectedValue.ToString()).SingleOrDefault(),
                Team2 = db.Teams.Where(t => t.Name == team2List.SelectedValue.ToString()).SingleOrDefault()
            };

            db.Matches.Local.Add(match);

            db.SaveChanges();

            Update();

            MatchDataForm matchDataForm = new MatchDataForm(match.Id, match.TitleLong);
            matchDataForm.Show();

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
