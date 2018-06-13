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
            if (txtDate.Text == "" || txtTime.Text == "")
            {
                MessageBox.Show("Du måste ange datum och tid för att skapa ett nytt möte.");
            }
            else if(txtDate.Text.Length < 10 || txtTime.Text.Length < 5)
            {
                MessageBox.Show("Du måste ange datum och tid enligt instruktionerna");
            }
            else
            {
                try
                {
                DatabaseConnections db = new DatabaseConnections();
                db.AddMeeting(DateTime.Parse(txtDate.Text), DateTime.Parse(txtTime.Text), selectedEmployee, txtNote.Text);
                MessageBox.Show("Möte skapat\rDatum: " + txtDate.Text + "\rTid: " + txtTime.Text + "\rMötesansvarig: " + selectedEmployee.firstName + " " + selectedEmployee.surName);
                txtDate.Text = string.Empty;
                txtTime.Text = string.Empty;
                txtNote.Text = string.Empty;
                }

                catch (PostgresException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void lstEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = (Employee)lstEmployees.SelectedItem;
            btnCreateMeeting.IsEnabled = true;
        }
        
    }
}
