using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Test.Database.DatabaseConnections;
using Test.Database;
using Test.Model;
using Npgsql;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                employees = db.GetAllEmployees();
                leftListBox.Items.Refresh();
                leftListBox.ItemsSource = employees;
                UpdateComboBoxes();
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<Employee> employees;

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

        private void btnDelEmp_Click(object sender, RoutedEventArgs e)
        {
            Employee choosenEmployee;
            choosenEmployee = (Employee)leftListBox.SelectedItem;

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

            leftListBox.Items.Refresh();
            leftListBox.ItemsSource = employees;
        }

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
    }
}
