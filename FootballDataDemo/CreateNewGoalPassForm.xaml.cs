﻿using FootballDataDemo.Data;
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
    /// Логика взаимодействия для CreateNewGoalPassForm.xaml
    /// </summary>
    public partial class CreateNewGoalPassForm : Window
    {
        private AppDbContext db;
        private Match match;

        /// <summary>
        /// форма создание новой голевой передачи матча
        /// </summary>
        /// <param name="id">Идентификатор матча</param>
        public CreateNewGoalPassForm(int id)
        {
            InitializeComponent();

            db = new AppDbContext();

            match = db.Matches
                .Include(m => m.Team1).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Include(m => m.Team2).ThenInclude(t => t.Players).ThenInclude(p => p.Role)
                .Where(m => m.Id == id).SingleOrDefault();

            PopulateTeamList();

            Closing += MainWindow_Closing;
        }

        /// <summary>
        /// Заполнить выпадающий список команды
        /// </summary>
        private void PopulateTeamList()
        {
            List<string> teams = new List<string>
            {
                match.Team1.Name,
                match.Team2.Name
            };

            teamList.ItemsSource = teams;
        }

        /// <summary>
        /// запускается при выборе команды в выпадающем списке команд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнить список игроков для пасующего
            PopulatePassingPlayerList();
        }

        /// <summary>
        /// заполняет выпадающий список для выбора пасующего
        /// </summary>
        private void PopulatePassingPlayerList()
        {
            List<string> players = new List<string>();

            if (match.Team1.Name == teamList.SelectedValue.ToString())
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

            passingPlayerList.ItemsSource = players;
        }

        /// <summary>
        /// запускается при выборе пасующего
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassingPlayerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateRecievingPlayerList();
        }

        /// <summary>
        /// заполняет список игроков для выбора принимающего
        /// </summary>
        private void PopulateRecievingPlayerList()
        {
            List<string> receivingPlayers = new List<string>();

            if (match.Team1.Name == teamList.SelectedValue.ToString())
            {
                foreach (Player p in match.Team1.Players)
                {
                    // игрок добавляется в список, если он не выбран в качестве пасующего
                    if (p.Name != passingPlayerList.SelectedValue.ToString())
                    {
                        receivingPlayers.Add(p.Name);
                    }
                }
            }
            else
            {
                foreach (Player p in match.Team2.Players)
                {
                    // игрок добавляется в список, если он не выбран в качестве пасующего
                    if (p.Name != passingPlayerList.SelectedValue.ToString())
                    {
                        receivingPlayers.Add(p.Name);
                    }
                }
            }

            receivingPlayerList.ItemsSource = receivingPlayers;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource goalPassViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("goalPassViewSource")));
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // goalPassViewSource.Source = [универсальный источник данных]
        }

        /// <summary>
        /// Сохраняет новую голевую передачу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewGoalPassButton_Click(object sender, RoutedEventArgs e)
        {
            GoalPass newGoalPass = new GoalPass
            {
                Match = match,
                PassingTeam = db.Teams.Where(t => t.Name == teamList.SelectedValue.ToString()).SingleOrDefault(),
                PassingPlayer = db.Players.Where(p => p.Name == passingPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                RecievingPlayer = db.Players.Where(p => p.Name == receivingPlayerList.SelectedValue.ToString()).SingleOrDefault(),
                PassTime = int.TryParse(passTimeTextBox.Text, out int time) ? time : 0
            };

            db.GoalPasses.Local.Add(newGoalPass);

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
