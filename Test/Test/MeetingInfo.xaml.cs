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

namespace Test
{
    /// <summary>
    /// Interaction logic for MeetingInfo.xaml
    /// </summary>
    public partial class MeetingInfo : Window
    {


        public MeetingInfo(Model.Meeting selectedMeeting)
        {
            InitializeComponent();
            selMeet = selectedMeeting;
            UpdateListbox(selectedMeeting);
            this.Title = selectedMeeting.ToString();
        }

        Model.Meeting selMeet;
        Model.GuestExtras selGuest;
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                int guestid = db.AddGuest(txtFirstName.Text, txtSurName.Text, txtCompany.Text);
                db.AddMeetingGuest(guestid, selMeet.MeetingID, " ", DateTime.Now);
                UpdateListbox(selMeet);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateListbox(Model.Meeting selectedMeeting)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.Items.Refresh();
                dataGrid.ItemsSource = db.GetMeetingGuestsExtras(selectedMeeting.MeetingID);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.CheckOutGuest(selGuest);
                dataGrid.Items.Refresh();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selGuest = (Model.GuestExtras)dataGrid.SelectedItem;
        }
    }
}
