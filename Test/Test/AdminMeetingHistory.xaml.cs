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
    /// Interaction logic for AdminMeetingHistory.xaml
    /// </summary>
    public partial class AdminMeetingHistory : Page
    {
            
        public AdminMeetingHistory()
        {
            InitializeComponent();
            comboNumRows.Items.Add("5");
            comboNumRows.Items.Add("10");
            comboNumRows.Items.Add("25");
            comboNumRows.Items.Add("50");
            comboNumRows.Items.Add("100");
            comboNumRows.SelectedItem = "10";

            try
            {
                DatabaseConnections db = new DatabaseConnections();
                List<MeetingHistory> listmh = db.GetMeetingHistory(comboNumRows.SelectedItem.ToString());
                lstMH.ItemsSource = listmh;
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        int CurrentPage = 1;
          
        private void txtBoxDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                lstMH.ItemsSource = db.MeetingHistFIlter(txtBoxDepartment.Text,txtBoxMeetingholder.Text,txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text,comboNumRows.SelectedItem.ToString());
                CurrentPage = 1;
                PageOf();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
                      
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            List<MeetingHistory> listmh = db.GetMeetingHistory(comboNumRows.SelectedItem.ToString());
            lstMH.ItemsSource = listmh;
            CurrentPage = 1;
            PageOf();
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            if (CurrentPage>=2 && txtBoxDepartment == null && txtBoxGhID == null && txtBoxGuest == null && txtBoxGuestCo == null && txtBoxMeetingholder == null)
            {
                List<MeetingHistory> listmh = db.GetMeetingHistoryNext(comboNumRows.SelectedItem.ToString(), CurrentPage.ToString(), "-2");
                lstMH.ItemsSource = listmh;
                CurrentPage--;
                PageOf();
            }
            else if (CurrentPage>=2 && (txtBoxDepartment != null || txtBoxGhID != null || txtBoxGuest != null || txtBoxGuestCo != null || txtBoxMeetingholder != null))
            {
                List<MeetingHistory> listmh = db.MeetingHistBrowseFIlter(comboNumRows.SelectedItem.ToString(), CurrentPage.ToString(), txtBoxDepartment.Text, txtBoxMeetingholder.Text, txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text, "-2");
                lstMH.ItemsSource = listmh;
                CurrentPage--;
                PageOf();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            int totalAmt = db.PageAmt(int.Parse(comboNumRows.SelectedItem.ToString()));

            if (CurrentPage<totalAmt && txtBoxDepartment==null&&txtBoxGhID==null&&txtBoxGuest==null&&txtBoxGuestCo==null&&txtBoxMeetingholder==null)
            {
                CurrentPage++;
                List<MeetingHistory> listmh = db.GetMeetingHistoryNext(comboNumRows.SelectedItem.ToString(), CurrentPage.ToString(),"-1");
                lstMH.ItemsSource = listmh;
                PageOf();
            }
            else if (CurrentPage<totalAmt&&(txtBoxDepartment != null || txtBoxGhID != null || txtBoxGuest != null || txtBoxGuestCo != null || txtBoxMeetingholder != null))
            {
                CurrentPage++;
                List<MeetingHistory> listmh = db.MeetingHistBrowseFIlter(comboNumRows.SelectedItem.ToString(), CurrentPage.ToString(), txtBoxDepartment.Text, txtBoxMeetingholder.Text, txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text, "-1");
                lstMH.ItemsSource = listmh;
                PageOf();
            }
            
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            int totalAmt = db.PageAmt(int.Parse(comboNumRows.SelectedItem.ToString()));
            CurrentPage = totalAmt;
            List<MeetingHistory> listmh = db.GetMeetingHistoryNext(comboNumRows.SelectedItem.ToString(), CurrentPage.ToString(),"-1");
            lstMH.ItemsSource = listmh;
            PageOf();
        }
        private void comboNumRows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseConnections db = new DatabaseConnections();
            if (txtBoxDepartment == null && txtBoxGhID == null && txtBoxGuest == null && txtBoxGuestCo == null && txtBoxMeetingholder == null)
            {
                List<MeetingHistory> listmh = db.GetMeetingHistory(comboNumRows.SelectedItem.ToString());
                lstMH.ItemsSource = listmh;
                PageOf();
            }
            else if((txtBoxDepartment != null || txtBoxGhID != null || txtBoxGuest != null || txtBoxGuestCo != null || txtBoxMeetingholder != null))
            {
                lstMH.ItemsSource = db.MeetingHistFIlter(txtBoxDepartment.Text, txtBoxMeetingholder.Text, txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text, comboNumRows.SelectedItem.ToString());
                CurrentPage = 1;
                PageOf();
            }

        }
        private void PageOf()
        {
            DatabaseConnections db = new DatabaseConnections();
            if(txtBoxDepartment == null && txtBoxGhID == null && txtBoxGuest == null && txtBoxGuestCo == null && txtBoxMeetingholder == null)
            {
                int totalAmt = db.PageAmt(int.Parse(comboNumRows.SelectedItem.ToString()));
                lblNumberOf.Content = "Sida " + CurrentPage + " av " + totalAmt.ToString();
            }
            else if(txtBoxDepartment != null || txtBoxGhID != null || txtBoxGuest != null || txtBoxGuestCo != null || txtBoxMeetingholder != null)
            {
                int totalAmt = db.PageAmtFIlter(int.Parse(comboNumRows.SelectedItem.ToString()),txtBoxDepartment.Text, txtBoxMeetingholder.Text, txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text);
                lblNumberOf.Content = "Sida " + CurrentPage + " av " + totalAmt.ToString();
            }
            
        }
     
    }
}
