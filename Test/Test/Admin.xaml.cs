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
                UpdateDatagrid();
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
            if (txtFirstName.Text == null || txtFirstName.Text == "" || txtSurName.Text == null || txtSurName.Text == "" || txtPhoneNumber.Text == null || txtPhoneNumber.Text == "" || comboBoxDepartment.SelectedIndex < 0 || comboBoxTeam.SelectedIndex < 0)
                MessageBox.Show("Du måste fylla i samtliga fält");
            else
            {
                try
                {
                    DatabaseConnections db = new DatabaseConnections();
                    Department dep = (Department)comboBoxDepartment.SelectedItem;
                    Team tem = (Team)comboBoxTeam.SelectedItem;
                    db.AddEmployee(txtFirstName.Text, txtSurName.Text, txtPhoneNumber.Text, comboBoxDepartment.Text, dep.DepartmentID, comboBoxTeam.Text, tem.id);
                    UpdateDatagrid();
                    ClearTextBoxes();
                }

                catch (PostgresException ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
            UpdateDatagrid();
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

        public void UpdateDatagrid()
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetAllEmployees();
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choosenEmployee = (Employee)dataGrid.SelectedItem;
            btnDelEmp.IsEnabled = true;
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

        private void comboBoxTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
                btnAddEmployee.IsEnabled = true;
        }

        private void ClearTextBoxes()
        {
            txtFirstName.Text = String.Empty;
            txtSurName.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;
            comboBoxDepartment.SelectedIndex = -1;
            comboBoxTeam.SelectedIndex = -1;
            btnAddEmployee.IsEnabled = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
       
        }
    }
}
