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

namespace HotelReservations.Views.Prices
{
    /// <summary>
    /// Interaction logic for AddEditPrices.xaml
    /// </summary>
    public partial class AddEditPrices : Window
    {
        private PriceService priceService;
        private RoomTypeService roomTypeService;
        private Price contextPrice;
        private bool isEditing;
        public AddEditPrices(Price? price = null)
        {
            if (price == null)
            {
                contextPrice = new Price();
                isEditing = false;
            }
            else
            {
                contextPrice = price.Clone();
                isEditing = true;
            }
            InitializeComponent();
            priceService = new PriceService();
            roomTypeService = new RoomTypeService();
            AdjustWindow(price);
            this.DataContext = contextPrice;
        }

        private void AdjustWindow(Price? price = null)
        {
            var roomTypeList = Hotel.GetInstance().RoomTypes.Where(roomType => roomType.room_type_is_active).ToList();
            var reservationTypeList = Hotel.GetInstance().ReservationTypes.ToList();
            RoomTypeCB.ItemsSource = roomTypeList;
            ReservationTypeCB.ItemsSource = reservationTypeList;

            if (price != null)
            {
                Title = "Edit price";
                RoomTypeCB.SelectedValue = roomTypeService.GetRoomTypeByName(price.room_type_id.room_type_name);
                RoomTypeCB.IsEnabled = false;
                ReservationTypeCB.IsEnabled = false;
            }
            else
            {
                Title = "Add price";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // basic validations
            if (contextPrice.room_type_id == null)
            {
                MessageBox.Show("Please select RoomType.", "RoomType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (contextPrice.price_reservation_type == null)
            {
                MessageBox.Show("Please select ReservationType.", "ReservationType Not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!double.TryParse(PriceValueTextBox.Text, out double priceValue) || priceValue < 0)
            {
                MessageBox.Show("Please enter a valid positive Price", "Price Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // this will be only if editing is false because while editing i will overwrite existing one
            if (!isEditing)
            {
                var allPrices = priceService.GetAllPrices().Where(price => price.price_is_active == true);
                foreach (var currentPrice in allPrices)
                {
                    if (currentPrice.price_reservation_type.ToString() == contextPrice.price_reservation_type.ToString() && currentPrice.room_type_id.ToString() == contextPrice.room_type_id.ToString())
                    {
                        MessageBox.Show("Price Combination for this RoomType and ReservationType already exist!", "Price Exist Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
            }

            // validation passed
            priceService.SavePrice(contextPrice);
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
