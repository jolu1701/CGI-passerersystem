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
    /// Interaction logic for Changepassword.xaml
    /// </summary>
    public partial class Changepassword : Window
    {
        public Changepassword()
        {
            InitializeComponent();
        }

        public void Update()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtNewpassword.Clear();
            MessageBox.Show("Lösenordet är ändrat");
            this.Close();
        }
        public void NewPassword()
        {
            DatabaseConnections db = new DatabaseConnections();
            string number = db.LogintoAdmin(txtUserName.Text, txtPassword.Text);
            if (number == "1")
            {
                db.ChangePassword(txtUserName.Text, txtPassword.Text, txtNewpassword.Text);
                Update();
            }
            else
                MessageBox.Show("Fel användarnamn eller lösenord");
        }
        

        private void btnChangepassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewPassword();
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
