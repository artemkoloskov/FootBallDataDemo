using FootballDataDemo.Data;
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
    /// Логика взаимодействия для MatchDataForm.xaml
    /// </summary>
    public partial class MatchDataForm : Window
    {
        private AppDbContext db;
        private int matchId;

        /// <summary>
        /// Форма с деталями матча
        /// </summary>
        /// <param name="id"></param>
        /// <param name="matchTitle"></param>
        public MatchDataForm(int id, string matchTitle)
        {
            matchId = id;

            InitializeComponent();

            Update(matchTitle);
        }

        private void Update(string matchTitle)
        {
            db = new AppDbContext();
            
            // загрузить матч, вместе с его командами, голами, защитами, и т.д.
            // а так же их дополнительными свойствами.
            db.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Goals).ThenInclude(g => g.ScoringTeam)
                .Include(m => m.Goals).ThenInclude(g => g.ConcedingTeam)
                .Include(m => m.Goals).ThenInclude(g => g.ScoringPlayer)
                .Include(m => m.Goals).ThenInclude(g => g.Goalkeeper)
                .Include(m => m.GoalPasses).ThenInclude(g => g.PassingPlayer)
                .Include(m => m.GoalPasses).ThenInclude(g => g.PassingTeam)
                .Include(m => m.GoalPasses).ThenInclude(g => g.RecievingPlayer)
                .Include(m => m.Defences).ThenInclude(d => d.AttemptingTeam)
                .Include(m => m.Defences).ThenInclude(d => d.AttemtingPlayer)
                .Include(m => m.Defences).ThenInclude(d => d.DefendingTeam)
                .Include(m => m.Defences).ThenInclude(d => d.Goalkeeper)
                .Include(m => m.Tackles).ThenInclude(d => d.TackledPlayer)
                .Include(m => m.Tackles).ThenInclude(d => d.TackledTeam)
                .Include(m => m.Tackles).ThenInclude(d => d.TacklingTeam)
                .Include(m => m.Tackles).ThenInclude(d => d.TacklingPlayer)
                .Where(m => m.Id == matchId)
                .Load();

            // Отобразить название матча и его результат
            matchLabel.Content = matchTitle;
            matchResultLabel.Content = "Результат матча: " + db.Matches.Where(m => m.Id == matchId).SingleOrDefault().Results;

            // Заполнить таблицу голов матча
            goalDataGrid.AutoGenerateColumns = false;
            goalDataGrid.CanUserAddRows = false;
            goalDataGrid.ItemsSource = db.Matches.Where(m => m.Id == matchId).SingleOrDefault().Goals;

            // Заполнить таблицу голевых передач матча
            goalPassDataGrid.AutoGenerateColumns = false;
            goalPassDataGrid.CanUserAddRows = false;
            goalPassDataGrid.ItemsSource = db.Matches.Where(m => m.Id == matchId).SingleOrDefault().GoalPasses;

            //заполнить таблицу защит матча
            defenceDataGrid.AutoGenerateColumns = false;
            defenceDataGrid.CanUserAddRows = false;
            defenceDataGrid.ItemsSource = db.Matches.Where(m => m.Id == matchId).SingleOrDefault().Defences;

            // Заполнить таблицу отборов матча
            tackleDataGrid.AutoGenerateColumns = false;
            tackleDataGrid.CanUserAddRows = false;
            tackleDataGrid.ItemsSource = db.Matches.Where(m => m.Id == matchId).SingleOrDefault().Tackles;

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
        /// ОТкрыть окно создания нового гола матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGoalButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGoalForm createNewGoalForm = new CreateNewGoalForm(matchId);
            createNewGoalForm.Show();
        }

        /// <summary>
        /// Открыть окно создания новой защиты матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDefenceButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewDefenceForm createNewDefenceForm = new CreateNewDefenceForm(matchId);
            createNewDefenceForm.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource goalViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("goalViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // goalViewSource.Source = [универсальный источник данных]
            System.Windows.Data.CollectionViewSource goalPassViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("goalPassViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // goalPassViewSource.Source = [универсальный источник данных]
            System.Windows.Data.CollectionViewSource defenceViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("defenceViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // defenceViewSource.Source = [универсальный источник данных]
            System.Windows.Data.CollectionViewSource tackleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tackleViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // tackleViewSource.Source = [универсальный источник данных]
        }

        /// <summary>
        /// Открыть окно создания новой голевой передачи матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGoalPassButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGoalPassForm createNewGoalPassForm = new CreateNewGoalPassForm(matchId);
            createNewGoalPassForm.Show();
        }

        /// <summary>
        /// Открыть окно создания нового отбора матча
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTackleButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTackleForm createNewTackleForm = new CreateNewTackleForm(matchId);
            createNewTackleForm.Show();
        }
    }
}
