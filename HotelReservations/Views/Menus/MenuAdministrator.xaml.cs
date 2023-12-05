using HotelReservations.Model;
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

namespace HotelReservations.Views.Menus
{
    /// <summary>
    /// Interaction logic for MenuAdministrator.xaml
    /// </summary>
    public partial class MenuAdministrator : Window
    {
        public MenuAdministrator()
        {
            InitializeComponent();
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            var reservationsWindow = new Reservations.Reservations();
            reservationsWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // clear auth
            Hotel.GetInstance().loggedInUser = new User();
            //close window
            this.Hide();
            var mainWindow = new MainWindow();
            mainWindow.Show();

        }
    }
}
