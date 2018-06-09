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
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGridMh.ItemsSource = db.GetMeetingHistory();
                
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //dataGridMh.Columns[0].Header="Besöksmottagare";
            //dataGridMh.Columns[1].Header="Anställningsnummer";
            //dataGridMh.Columns[3].Header = "Avdelning";
        }

   
        private void txtBoxDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                dataGridMh.ItemsSource = db.MeetingHistFIlter(txtBoxDepartment.Text,txtBoxMeetingholder.Text,txtBoxGuest.Text, txtBoxGuestCo.Text, txtBoxGhID.Text);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

    }
}
