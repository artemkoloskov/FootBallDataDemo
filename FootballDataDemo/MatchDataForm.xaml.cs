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

        public MatchDataForm(int id, string matchTitle)
        {
            InitializeComponent();

            Update(id, matchTitle);
        }

        private void Update(int id, string matchTitle)
        {
            db = new AppDbContext();
            
            db.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Goals).ThenInclude(g => g.ScoringTeam)
                .Include(m => m.Goals).ThenInclude(g => g.ConcedingTeam)
                .Where(m => m.Id == id)
                .Load();

            matchLabel.Content = matchTitle;

            // Заполнить таблицу матчей
            matchDataGrid.AutoGenerateColumns = false;
            matchDataGrid.CanUserAddRows = false;
            matchDataGrid.ItemsSource = db.Matches.Local.ToBindingList();

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
