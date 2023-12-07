using HotelReservations.Model;
using HotelReservations.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HotelReservations.Views.Reservations
{
    /// <summary>
    /// Interaction logic for ActiveReservations.xaml
    /// </summary>
    public partial class ActiveReservations : Window
    {
        private ICollectionView view;
        public ActiveReservations()
        {
            InitializeComponent();
            FillData();
        }
        public void FillData()
        {
            var reservationService = new ReservationService();
            var reservations = Hotel.GetInstance().Reservations.Where(res => res.reservation_is_active).ToList();

            ReservationsDataGrid.ItemsSource = null; // Clear existing items

            foreach (var reservation in reservations)
            {
                ReservationsDataGrid.Items.Add(reservation);

            }

            ReservationsDataGrid.IsSynchronizedWithCurrentItem = true;
            ReservationsDataGrid.SelectedItem = null;
        }

        private void GuestsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "guest_reservation_id")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }


    }
}
