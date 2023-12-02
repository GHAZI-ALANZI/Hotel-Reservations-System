using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    [Serializable]
    public class RoomType
    {
        [Key]
        public int room_type_id { get; set; }

        public string room_type_name { get; set; } = "";

        public bool room_type_is_active { get; set; } = true;

        public override string ToString()
        {
            return room_type_name;
        }

        public RoomType Clone()
        {
            var clone = new RoomType();
            clone.room_type_id = room_type_id;
            clone.room_type_name = room_type_name;
            clone.room_type_is_active = room_type_is_active;
            return clone;
        }
    }
}
