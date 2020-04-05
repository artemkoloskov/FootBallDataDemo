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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballDataDemo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext db;

        public MainWindow()
        {
            InitializeComponent();

            Update();
        }

        private void Update()
        {
            db = new AppDbContext();
            db.Teams.Include(t => t.Players).ThenInclude(p => p.Role).Load();
            db.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Goals).ThenInclude(g => g.ScoringTeam)
                .Include(m => m.Goals).ThenInclude(g => g.ConcedingTeam)
                .Load();

            //  Заполнить таблицу команд
            teamsDataGrid.AutoGenerateColumns = false;
            teamsDataGrid.CanUserAddRows = false;
            teamsDataGrid.ItemsSource = db.Teams.Local.ToBindingList();

            // Заполнить таблицу матчей
            matchesDataGrid.AutoGenerateColumns = false;
            matchesDataGrid.CanUserAddRows = false;
            matchesDataGrid.ItemsSource = db.Matches.Local.ToBindingList();

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

        /// <summary>
        /// Открыть окно с деталями о команде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTeamDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenTeamDetails();
        }

        /// <summary>
        /// Открыть окно с деталями о команде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenTeamDetails();
        }

        /// <summary>
        /// Открыть окно с деталями об игре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMatchDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMatchDetails();
        }

        /// <summary>
        /// Открыть окно с деталями об игре 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MatchesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenMatchDetails();
        }

        /// <summary>
        /// Открыть окно создания нового матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewMatchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Удалить выбранный матч
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMatchButton_Click(object sender, RoutedEventArgs e)
        {
            if (matchesDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < matchesDataGrid.SelectedItems.Count; i++)
                {
                    if (matchesDataGrid.SelectedItems[i] is Match match)
                    {
                        db.Matches.Remove(match);
                    }
                }
            }

            db.SaveChanges();

            Update();
        }

        /// <summary>
        /// Открыть окно создания новой команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTeamForm createNewTeamForm = new CreateNewTeamForm();
            createNewTeamForm.Show();
        }

        /// <summary>
        /// Удалить выбранную команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (teamsDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < teamsDataGrid.SelectedItems.Count; i++)
                {
                    if (teamsDataGrid.SelectedItems[i] is Team team)
                    {
                        db.Teams.Remove(team);
                    }
                }
            }

            db.SaveChanges();

            Update();
        }

        private void Page_Loaded (object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Открыть окно с деталями о команде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTeamDetails()
        {
            if (teamsDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < teamsDataGrid.SelectedItems.Count; i++)
                {
                    if (teamsDataGrid.SelectedItems[i] is Team team)
                    {
                        TeamDataForm teamDataForm = new TeamDataForm(team.Id, team.LongName);
                        teamDataForm.Show();
                    }
                }
            }
        }

        /// <summary>
        /// Открыть окно с деталями матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMatchDetails()
        {
            if (matchesDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < matchesDataGrid.SelectedItems.Count; i++)
                {
                    if (matchesDataGrid.SelectedItems[i] is Match match)
                    {
                        MatchDataForm matchDataForm = new MatchDataForm(match.Id, match.TitleLong);
                        matchDataForm.Show();
                    }
                }
            }
        }
    }
}
