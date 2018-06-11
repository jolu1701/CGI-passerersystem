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
    /// Interaction logic for GuestReportin.xaml
    /// </summary>
    public partial class GuestReportin : Window
    {
        public GuestReportin()
        {
            InitializeComponent();
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                lstMeetings.ItemsSource = db.GetTodaysMeetings(); 
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        Model.Meeting selMeet;

        private void btnAddGuestSelf_Click(object sender, RoutedEventArgs e)
        {
            selMeet = (Meeting)lstMeetings.SelectedItem;
            if (txtFirstName.Text == "" || txtCompany.Text == "" || txtSurName.Text == "")
            {
                MessageBox.Show("Du måste skriva förnamn, efternamn & företag");
            }
            else
            {
                try
                {

                    DatabaseConnections db = new DatabaseConnections();
                    int guestid = db.AddGuest(txtFirstName.Text, txtSurName.Text, txtCompany.Text);
                    db.AddMeetingGuest(guestid, selMeet.MeetingID, " ");
                    MessageBox.Show("Välkommen " + txtFirstName.Text + "!\rTa gärna en kopp kaffe och slå dig ned så kommer " + selMeet.MeetingHolder + " och hämtar dig inom kort.");
                    txtFirstName.Text = string.Empty;
                    txtSurName.Text = string.Empty;
                    txtCompany.Text = string.Empty;
                }
                catch (PostgresException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
