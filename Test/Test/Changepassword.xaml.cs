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

        

        private void btnChangepassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseConnections db = new DatabaseConnections();
                db.ChangePassword(txtUserName.Text, txtPassword.Text, txtNewpassword.Text);
            }

            catch (PostgresException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
