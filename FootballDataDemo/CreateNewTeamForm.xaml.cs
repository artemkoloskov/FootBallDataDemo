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
    /// Логика взаимодействия для CreateNewTeamForm.xaml
    /// </summary>
    public partial class CreateNewTeamForm : Window
    {
        private AppDbContext db;

        /// <summary>
        /// Форма создания новой команды
        /// </summary>
        public CreateNewTeamForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Сохраняет новую команду, открывает окно с деталями этой команды, закрывает форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            db = new AppDbContext();
            db.Teams.Load();

            Team newTeam = new Team
            {
                Name = nameTextBox.Text,
                Abbreviation = abbreviationTextBox.Text
            };

            db.Teams.Local.Add(newTeam);

            db.SaveChanges();

            TeamDataForm teamDataForm = new TeamDataForm(newTeam.Id, newTeam.LongName);
            teamDataForm.Show();

            db.Dispose();

            Close();
        }
    }
}
