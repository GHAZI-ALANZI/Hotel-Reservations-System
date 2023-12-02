using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Guest
    {
        [Key]
        public int guest_id { get; set; }
        [Required]
        public string guest_name { get; set; } = "";
        [Required]
        public string guest_surname { get; set; } = "";
        [Required]
        public string guest_jmbg { get; set; } = "";
        public bool guest_is_active { get; set; } = true;
        public int guest_reservation_id { get; set; }
        public Reservation? Reservation { get; set; } 

    

        public Guest Clone()
        {
            var clone = new Guest();
            clone.guest_id = guest_id;
            clone.guest_name = guest_name;
            clone.guest_surname = guest_surname;
            clone.guest_jmbg = guest_jmbg;
            return clone;
        }
    }
}
