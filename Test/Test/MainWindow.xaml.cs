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

            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetTodaysMeetings();                
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
            try
            {
                selectedMeeting = (Meeting)dataGrid.SelectedItem;
            }

            catch (Exception)
            {
                
            }
        }

        //private void btnAddmeetingguest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        DatabaseConnections db = new DatabaseConnections();
        //        db.AddMeetingGuest(Int32.Parse(txtGuestid.Text), Int32.Parse(txtMeetingid.Text), txtBadgey.Text);
        //    }

        //    catch (PostgresException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            this.Content = admin;
        }
                                        
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            AdminMeetingHistory mh = new AdminMeetingHistory();
            this.Content = mh;
        }

        private void btnCreateaMeeting_Click(object sender, RoutedEventArgs e)
        {
            AddMeeting am = new AddMeeting();
            am.Show();
        }

        private void btnGuestReport_Click(object sender, RoutedEventArgs e)
        {
            GuestReportin guestreportin = new GuestReportin();
            guestreportin.Show();
        }

        private void btnTologin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();

        }

        private void btnViewAllMeetings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetAllMeetings();
                btnViewAllMeetings.IsEnabled = false;
                btnViewTodaysMeetings.IsEnabled = true;
                btnViewUpcomingMeetings.IsEnabled = true;
                lblMeeting.Content = "Alla möten";
                SetLablesOnDatagrid();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewTodaysMeetings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetTodaysMeetings();
                btnViewAllMeetings.IsEnabled = true;
                btnViewTodaysMeetings.IsEnabled = false;
                btnViewUpcomingMeetings.IsEnabled = true;
                lblMeeting.Content = "Dagens möten";

                dataGrid.Columns[0].Header = "Mötesnr";
                dataGrid.Columns[1].Header = "Tid";
                dataGrid.Columns[2].Header = "Mötesansvarig";
                dataGrid.Columns[3].Header = "Noteringar";
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewUpcomingMeetings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGrid.ItemsSource = db.GetUpcomingMeetings();
                btnViewAllMeetings.IsEnabled = true;
                btnViewTodaysMeetings.IsEnabled = true;
                btnViewUpcomingMeetings.IsEnabled = false;
                lblMeeting.Content = "Kommande möten";
                SetLablesOnDatagrid();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetLablesOnDatagrid()
        {
            dataGrid.Columns[0].Header = "Mötesnr";
            dataGrid.Columns[1].Header = "Datum och tid";
            dataGrid.Columns[2].Header = "Mötesansvarig";
            dataGrid.Columns[3].Header = "Noteringar";
        }
    }
}
