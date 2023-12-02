using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservations.Model
{
    public class Reservation
    {
        [Key]
        public int reservation_id { get; set; }
        public string reservation_room_number { get; set; }
        public ReservationType reservation_type { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public DateTime start_date_time { get; set; }
        public DateTime end_date_time { get; set; }
        public double total_price { get; set; } = 0;
        public bool reservation_is_active { get; set; } = true;
        public bool reservation_is_finished { get; set; } = false;//reservation_is_finished

        public Reservation Clone()
        {
            var clone = new Reservation();
            clone.reservation_id = reservation_id;
            clone.reservation_room_number = reservation_room_number;
            clone.reservation_type = reservation_type;
            clone.Guests = Guests;
            clone.start_date_time = start_date_time;
            clone.end_date_time = end_date_time;
            clone.total_price = total_price;
            clone.reservation_is_active = reservation_is_active;
            clone.reservation_is_finished = reservation_is_finished;

            return clone;
        }
    }
}