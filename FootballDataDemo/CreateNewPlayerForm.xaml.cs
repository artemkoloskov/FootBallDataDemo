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
    /// Логика взаимодействия для CreateNewPlayerForm.xaml
    /// </summary>
    public partial class CreateNewPlayerForm : Window
    {
        private AppDbContext db;

        private int teamId;

        public CreateNewPlayerForm(int teamId)
        {
            InitializeComponent();

            db = new AppDbContext();

            db.Players.Load();

            this.teamId = teamId;

            rolesList.SelectedIndex = 0;

            Closing += MainWindow_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource playerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playerViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // playerViewSource.Source = [универсальный источник данных]
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

        private void PopulateRoleList ()
        {
            List<string> teams = new List<string>();

            foreach (Role role in db.Roles.OrderBy(r => r.Id).ToList())
            {
                teams.Add(role.Title);
            }

            rolesList.ItemsSource = teams;
        }

        private void CreateNewPlayersButton_Click(object sender, RoutedEventArgs e)
        {
            Player newPlayer = new Player
            {
                Name = nameTextBox.Text,
                Number = int.TryParse(numberTextBox.Text, out int result) ? result : 0,
                Role = db.Roles.Where(r => r.Id == rolesList.SelectedIndex).SingleOrDefault(),
                Team = db.Teams.Where(t => t.Id == teamId).SingleOrDefault()
            };

            db.Players.Local.Add(newPlayer);

            db.SaveChanges();

            ClearForm();
        }

        /// <summary>
        /// Очищает форму для нового игрока
        /// </summary>
        private void ClearForm()
        {
            nameTextBox.Text = "";
            numberTextBox.Text = "";
            rolesList.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
