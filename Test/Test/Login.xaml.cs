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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                string number = db.LogintoAdmin(txtUserName.Text, txtPassword.Text);
                if (number == "12")
                {
                    MainWindow mainwindow = new MainWindow();
                    mainwindow.Show();
                    this.Close();

                }

                else if(number == "11")
                {
                    MeetingHolder mh = new MeetingHolder();
                    mh.Show();
                    this.Close();
                }

                else
                    MessageBox.Show("Fel användarnamn eller lösenord");
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangepw_Click(object sender, RoutedEventArgs e)
        {
            Changepassword changepassword = new Changepassword();
            changepassword.Show();
        }
    }
}
