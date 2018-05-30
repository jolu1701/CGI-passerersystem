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
using static Test.Database.DatabaseConnections;
using Test.Database;
using Test.Model;
using Npgsql;

namespace Test
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public Admin()
        {
            InitializeComponent();
        
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                employees = db.GetAllEmployees();
                UpdateComboBoxes();
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<Employee> employees;
        Employee choosenEmployee;

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddEmployee(txtFirstName.Text, txtSurName.Text, txtPhoneNumber.Text, comboBoxDepartment.Text, comboBoxTeam.Text);
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void btnDelEmp_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.RemoveEmployee(choosenEmployee);
                employees = db.GetAllEmployees();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGrid.Items.Refresh();
            dataGrid.ItemsSource = employees;
        }


        public void UpdateComboBoxes()
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                comboBoxDepartment.ItemsSource = db.GetAllDepartments();
                comboBoxTeam.ItemsSource = db.GetAllTeams();
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choosenEmployee = (Employee)dataGrid.SelectedItem;
        }

        private void btnAdminTeam_Click(object sender, RoutedEventArgs e)
        {
            AdminTeam at = new AdminTeam();
            at.Show();
        }

        private void btnAdminDepartmen_Click(object sender, RoutedEventArgs e)
        {
            AdminDepartment ad = new AdminDepartment();
            ad.Show();
        }
    }
}
