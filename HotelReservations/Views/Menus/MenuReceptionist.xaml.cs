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
    /// Interaction logic for MenuReceptionist.xaml
    /// </summary>
    public partial class MenuReceptionist : Window
    {
        public MenuReceptionist()
        {
            InitializeComponent();
        }
        private void RoomsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var roomsWindow = new Rooms.Rooms();
            roomsWindow.Show();
        }

        private void RoomTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {

            var roomTypeWindow = new RoomTypes.RoomTypes();
            roomTypeWindow.Show();
        }

        private void PricesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var pricesWindow = new Prices.Prices();
            pricesWindow.Show();
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new Users.Users();
            usersWindow.Show();
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
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
