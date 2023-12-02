using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class User
    {
        [Key]
        public int user_id  { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; } = "";
        [Required]
        public string JMBG { get; set; } = "";
        [Required]
        public string password { get; set; } = "";
        [Required]
        public string username { get; set; } = "";
        public UserType  user_type { get; set; }

        public bool user_is_active { get; set; } = true;

        public User Clone()
        {
            var clone = new User();
            clone.user_id = user_id;
            clone.first_name = first_name;
            clone.last_name = last_name;
            clone.last_name = last_name;
            clone.JMBG = JMBG;
            clone.password = password;
            clone.username = username;
            clone.user_type = user_type;
            return clone;
        }
    }
}
