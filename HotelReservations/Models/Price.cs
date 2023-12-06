using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelReservations.Model
{
    public class Price
    {
        [Key]
        public int price_id { get; set; }
        [Column("room_type_id")]
        public RoomType room_type_id { get; set; }
        public ReservationType price_reservation_type { get; set; }
        public double price_value { get; set; } = 0;
        public bool price_is_active { get; set; } = true;

        public Price Clone()
        {
            var clone = new Price();
            clone.price_id = price_id;
            clone.room_type_id = room_type_id;
            clone.price_reservation_type = price_reservation_type;
            clone.price_value = price_value;
            clone.price_is_active = price_is_active;
            return clone;
        }
    }
}
