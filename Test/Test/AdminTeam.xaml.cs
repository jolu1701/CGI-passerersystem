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
using static Test.Database.DatabaseConnections;
using Test.Database;
using Test.Model;
using Npgsql;

namespace Test
{
    /// <summary>
    /// Interaction logic for AdminTeam.xaml
    /// </summary>
    public partial class AdminTeam : Window
    {
        public AdminTeam()
        {
            InitializeComponent();

            DatabaseConnections db = new DatabaseConnections();
            List<Team> teams = db.GetAllTeams();

            listBoxTeam.ItemsSource = teams;
        }

        Team selectedteam;


        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddTeam(txtTeam.Text);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.RemoveTeam(selectedteam);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedteam = (Team)listBoxTeam.SelectedItem;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.EditTeam(selectedteam,textBoxNewName.Text);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
