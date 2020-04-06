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

            teamsDataGrid.AutoGenerateColumns = false;
            teamsDataGrid.CanUserAddRows = false;

            matchesDataGrid.AutoGenerateColumns = false;
            matchesDataGrid.CanUserAddRows = false;

            Update();

            Closing += MainWindow_Closing;
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
            teamsDataGrid.ItemsSource = db.Teams.Local.ToBindingList();

            // Заполнить таблицу матчей
            matchesDataGrid.ItemsSource = db.Matches.Local.ToBindingList();

            // очистить блок для отчетов
            reportsTextBlock.Text = "";
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
            CreateNewMatchForm createNewMatchForm = new CreateNewMatchForm();
            createNewMatchForm.Show();
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

                db.SaveChanges();

                Update();
            }
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

                db.SaveChanges();

                Update();
            }
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

        /// <summary>
        /// Обновить данные окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// Отобразить отчет по лучшему защитнику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDefendersReport_Click(object sender, RoutedEventArgs e)
        {
            var defenders = db.Players.Where(p => p.Role.RoleType == RoleType.Defender).ToList();

            var tackles = db.Tackles.Include(g => g.TacklingPlayer).ToList();

            string bestDefenderName = "";
            int mostTackles = 0;

            foreach (Player p in defenders)
            {
                int currentPlayerTackles = tackles.Where(t => t.TacklingPlayer.Id == p.Id).Count();

                if (currentPlayerTackles > mostTackles)
                {
                    bestDefenderName = p.Name;
                    mostTackles = currentPlayerTackles;
                }
            }

            reportsTextBlock.Text = "Лучший защитник: " + bestDefenderName + ", " + mostTackles + " отборов.";
        }

        /// <summary>
        /// Отобразить отчет по лучшему нападающему
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAttackersReport_Click(object sender, RoutedEventArgs e)
        {
            var attackers = db.Players.Where(p => p.Role.RoleType == RoleType.Attacker).ToList();

            var goals = db.Goals.Include(g => g.ScoringPlayer).ToList();

            string bestAttackerName = "";
            int mostGoals = 0;

            foreach (Player p in attackers)
            {
                int currentPlayerGoals = goals.Where(t => t.ScoringPlayer.Id == p.Id).Count();

                if (currentPlayerGoals > mostGoals)
                {
                    bestAttackerName = p.Name;
                    mostGoals = currentPlayerGoals;
                }
            }

            reportsTextBlock.Text = "Лучший нападающий: " + bestAttackerName + ", " + mostGoals + " голов.";
        }

        /// <summary>
        /// Отобразить отчет по количеству голов забитых разными ролями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowGoalsDistributionReport_Click(object sender, RoutedEventArgs e)
        {
            var goals = db.Goals.Include(g => g.ScoringPlayer).ThenInclude(p => p.Role).ToList();

            string result = "";

            result += "Вратари забили: " + goals.Where(g => g.ScoringPlayer.Role.RoleType == RoleType.Goalkeeper).Count() + " голов\n";
            result += "Защитники забили: " + goals.Where(g => g.ScoringPlayer.Role.RoleType == RoleType.Defender).Count() + " голов\n";
            result += "Нападающие забили: " + goals.Where(g => g.ScoringPlayer.Role.RoleType == RoleType.Attacker).Count() + " голов\n";

            reportsTextBlock.Text = result;
        }
    }
}
