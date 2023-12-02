using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations
{  
    //Db Connection
    public static class Config
    {
        public static string CONNECTION_STRING { get; } = "Server=DESKTOP-H9NQTJ3;Database=HotelReservations;Trusted_Connection=True;TrustServerCertificate=True";
    }
}
