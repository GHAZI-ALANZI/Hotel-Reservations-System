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

namespace HotelReservations.Views.Rooms
{
    /// <summary>
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : Window
    {
        private ICollectionView view;
        public Rooms()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            var roomService = new RoomService();
            var rooms = Hotel.GetInstance().Rooms.Where(room => room.room_is_active).ToList();

            view = CollectionViewSource.GetDefaultView(rooms);
            view.Filter = DoFilter;

            RoomsDataGrid.ItemsSource = null;
            RoomsDataGrid.ItemsSource = view;
            RoomsDataGrid.IsSynchronizedWithCurrentItem = true;
            RoomsDataGrid.SelectedItem = null;
        }

        private bool DoFilter(object roomObject)
        {
            var room = roomObject as Room;

            var roomNumberSearchParam = RoomNumberSearchTextBox.Text;
            var roomTypeSearchParam = RoomTypeSearchTextBox.Text;

            bool isRoomNumberMatch = room.room_number.Contains(roomNumberSearchParam);
            bool isRoomTypeMatch = room.RoomType.ToString().Contains(roomTypeSearchParam);

            return isRoomNumberMatch && isRoomTypeMatch;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addRoomWindow = new AddEditRoom();
            Hide();
            if (addRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenRoom = (Room)RoomsDataGrid.SelectedItem;
            if (chosenRoom == null)
            {
                MessageBox.Show("Please select a Room.", "Select Room", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var editRoomWindow = new AddEditRoom(chosenRoom);
            Hide();
            if (editRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var chosenRoom = (Room)RoomsDataGrid.SelectedItem;
            if (chosenRoom == null)
            {
                MessageBox.Show("Please select a Room.", "Select Room", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var deleteRoomWindow = new DeleteRoom(chosenRoom);
            Hide();
            if (deleteRoomWindow.ShowDialog() == true)
            {
                FillData();
            }
            Show();
        }

        private void SearchTB_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private void RoomsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.ToLower() == "IsActive".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName.ToLower() == "Id".ToLower())
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

    }
}
