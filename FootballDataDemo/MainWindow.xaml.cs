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
            teamsDataGrid.ItemsSource = db.Teams.Local.ToBindingList();

            // Заполнить таблицу матчей
            matchesDataGrid.AutoGenerateColumns = false;
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

        private void Page_Loaded (object sender, RoutedEventArgs e)
        {
            
        }
    }
}
