using HotelReservations.Model;
using HotelReservations.Service;
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

namespace HotelReservations.Views.Reservations.Guests
{
    /// <summary>
    /// Interaction logic for DeleteGuest.xaml
    /// </summary>
    public partial class DeleteGuest : Window
    {

        private GuestService guestService;
        private Guest guestToDelete;
        public DeleteGuest(Guest guest)
        {
            InitializeComponent();
            guestService = new GuestService();
            guestToDelete = guest;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // add modal
            guestService.MakeGuestInactive(guestToDelete);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


    }
}
