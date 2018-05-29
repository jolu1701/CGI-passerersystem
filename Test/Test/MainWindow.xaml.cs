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
        Meeting selectedMeeting;
        
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "CGI - Customer Guestbook Interface";
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
                db.AddEmployee(txtFirstName.Text, txtSurName.Text, txtPhoneNumber.Text, comboBoxDepartment.Text , comboBoxTeam.Text);
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

        private void btnLoadMeeting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetAllMeetings();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MeetingInfo minfo = new MeetingInfo(selectedMeeting);
            minfo.Show();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMeeting = (Meeting)dataGrid.SelectedItem;
        }

        private void btnAddmeetingguest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.AddMeetingGuest(Int32.Parse(txtGuestid.Text), Int32.Parse(txtMeetingid.Text), txtBadgey.Text, DateTime.Parse(txtCheckin.Text));
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
