using Npgsql;
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
using Test.Database;
using Test.Model;

namespace Test
{
    /// <summary>
    /// Interaction logic for AddMeeting.xaml
    /// </summary>
    public partial class AddMeeting : Window
    {
        public AddMeeting()
        {
            InitializeComponent();
            ShowMEmployees();
        }

        Employee selectedEmployee;

        public void ShowMEmployees()
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                List<Employee> employees = db.GetAllEmployees();
                lstEmployees.Items.Refresh();
                lstEmployees.ItemsSource = employees;
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateMeeting_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            db.AddMeeting(DateTime.Parse(txtDate.Text), DateTime.Parse(txtTime.Text), selectedEmployee, txtNote.Text);
        }

        private void lstEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = (Employee)lstEmployees.SelectedItem;
        }
    }
}
