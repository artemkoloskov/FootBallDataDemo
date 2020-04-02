using FootballDataDemo.Data;
using FootballDataDemo.Model;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page_Loaded (object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                playerList.ItemsSource = db.Players.ToList();
            }
        }

        private void addBtn_Click (object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                Player player = new Player { Name = nametxt.Text };
                db.Players.Add(player);
                db.SaveChanges();
                playerList.ItemsSource = db.Players.ToList();
            }
        }
    }
}
