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
    /// Interaction logic for AdminDepartment.xaml
    /// </summary>
    public partial class AdminDepartment : Window
    {
        public AdminDepartment()
        {
            InitializeComponent();

            DatabaseConnections db = new DatabaseConnections();
            List<Department> departments = db.GetAllDepartments();

            listBoxDepartment.ItemsSource = departments;       
        }

               

        private void btnAddDep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddDepartment(txtDepartment.Text);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
