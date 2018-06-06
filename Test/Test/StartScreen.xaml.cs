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

namespace Test
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void btnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void btnToGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestReportin guestreportin = new GuestReportin();
            guestreportin.Show();
        }

        private void btnadminnologin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
        }
    }
}
