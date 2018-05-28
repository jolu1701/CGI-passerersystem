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
using Test.Database;
using Test.Model;
using Npgsql;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Kan ni se detta pojkar och flickor :)

        public MainWindow()
        {
            InitializeComponent();
        }

        public void TestarLite()
        {
            //Här händer det grejer!
        }

        public void test2()
        {
            //blabla
        }

        public void testadam()
        {
            //blabla
        }

        public void jontetest3()
        {
            // funkar ju kung det här
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                List<Employee> employees = db.GetAllEmployees();
                leftListBox.Items.Refresh();
                leftListBox.ItemsSource = employees;
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddEmployee(txtFirstName.Text, txtSurName.Text, txtPhoneNumber.Text, comboBoxDepartment.SelectedIndex + 1, comboBoxTeam.SelectedIndex + 1);
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddguest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddGuest(txtFirstNameGuest.Text, txtLastNameGuest.Text, txtCompany.Text);
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
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
