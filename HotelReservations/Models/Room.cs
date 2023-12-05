using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    [Serializable]
    public class Room
    {
        [Key]
        public int room_id { get; set; }
        [Required]
        public string room_number { get; set; } = "";
        public bool has_TV { get; set; }
        public bool has_mini_bar { get; set; }
        public bool room_is_active { get; set; } = true;
        public int room_type_id { get; set; } // Foreign key referencing room_type table
       
        public RoomType RoomType { get; set; } = null;

        public override string ToString()
        {
            //return "Room number: " + RoomNumber; // ...
            return $"Room number: {room_number}";
        }

        public Room Clone()
        {
            var clone = new Room();
            clone.room_id = room_id;
            clone.room_number = room_number;
            clone.has_TV = has_TV;
            clone.has_mini_bar = has_mini_bar;
            clone.RoomType = RoomType;
            clone.room_is_active = room_is_active;

            return clone;
        }
    }
}
