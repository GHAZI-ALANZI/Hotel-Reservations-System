using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Guest> guest { get; set; } 
        //public DbSet<Hotel> Hotels { get; set; } 
        public DbSet<Price> price { get; set; } 
        public DbSet<Reservation> reservation { get; set; } 
        public DbSet<Room> room { get; set; } 
        public DbSet<RoomType> room_type { get; set; } 
        public DbSet<User> user { get; set; }
      

    }
}
