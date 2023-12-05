using HotelReservations.Model;
using HotelReservations.Views.Menus;
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

namespace HotelReservations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var findUser = Hotel.GetInstance().Users.Find(user => user.username == username && user.password == password);
            if (findUser == null)
            {
                MessageBox.Show("Try again.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {


                // now checking which menu item to show

                // administrator doesnt have permission for working with reservations, rest is available.
                if (findUser.user_type == UserType.Administrator)
                {
                    UsernameTextBox.Text = string.Empty;
                    PasswordBox.Password = string.Empty;
                    this.Hide();
                    var menuAdmin = new MenuAdministrator();
                    menuAdmin.Show();
                };

                // if user is receptionist, it will have access to worki with reservations only.
                if (findUser.user_type == UserType.Receptionist)
                {
                    UsernameTextBox.Text = string.Empty;
                    PasswordBox.Password = string.Empty;
                    this.Hide();
                    var menu = new MenuReceptionist();
                    menu.Show();

                };

                MessageBox.Show("Logged in. Welcome " + username + ".", "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // setting auth user
                Hotel.GetInstance().loggedInUser = findUser;

            }
        }



    }
}
