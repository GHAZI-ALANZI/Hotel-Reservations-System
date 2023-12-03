using HotelReservations.Model;
using HotelReservations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    internal class ReservationService
    {
        IReservationRepository reservationRepository;
        PriceService priceService;
        GuestService guestService;
        RoomService roomService;

        public ReservationService()
        {
            reservationRepository = new ReservationRepositoryDB();
            guestService = new GuestService();
            priceService = new PriceService();
            roomService = new RoomService();
        }

        public List<Reservation> GetAllReservations()
        {
            return Hotel.GetInstance().Reservations;
        }

        public void SaveReservation(Reservation reservation, Room room)
        {
            reservation.reservation_room_number = room.room_number;

            // checking is date equal for deciding what type reservation is. if its equal then its day, if its not equal then its night
            if (reservation.start_date_time == reservation.end_date_time)
            {
                reservation.reservation_type = ReservationType.Day;
            }
            else
            {
                reservation.reservation_type = ReservationType.Night;
            }

            // if reservation id is "0"(doesnt exist yet), then its adding
            if (reservation.reservation_id == 0)
            {
                Hotel.GetInstance().Reservations.Add(reservation);
                reservation.total_price= CountPrice(reservation);

                reservation.reservation_id = reservationRepository.Insert(reservation);

                // this will rewrite guests ID(because all have fake id(reservation not added yet so i have to rewrite all guest's reservation ID to this one ));
                guestService.RewriteGuestIdAfterReservationIsCreated(reservation.reservation_id);
                reservation.Guests = Hotel.GetInstance().Guests.Where(guest => guest.guest_reservation_id == reservation.reservation_id).ToList();
            }

            // otherwise, update reservation.
            else
            {
                reservation.total_price = CountPrice(reservation);
                var r = Hotel.GetInstance().Reservations.First(r => r.reservation_id == reservation.reservation_id);

                r.total_price = reservation.total_price;
                r.start_date_time = reservation.start_date_time;
                r.end_date_time = reservation.end_date_time;

                reservationRepository.Update(reservation);
            }
        }

        // delete or finishing res;
        public void MakeReservationInactive(Reservation reservation, bool finish = false)
        {
            var res = Hotel.GetInstance().Reservations.Find(r => r.reservation_id == reservation.reservation_id);
            res.reservation_is_active = false;

            // so now if finish is true i will just update state otherwise i will delete(make inactive) :)
            if (finish == true)
            {
                res.reservation_is_finished = true;
                reservationRepository.Update(res);
                return;
            }

            reservationRepository.Delete(res.reservation_id);
        }

        public double CountPrice(Reservation reservation)
        {
            var resType = reservation.reservation_type;
            int dateDifference = GetDateDifference(reservation.start_date_time, reservation.end_date_time);

            Room room = roomService.GetRoomByRoomNumber(reservation.reservation_room_number);
            Price price = priceService.GetAllPrices().Find(price => price.room_type_id.ToString() == room.RoomType.ToString() && price.price_reservation_type == resType);

            reservation.total_price = dateDifference * price.price_value;
            return reservation.total_price;
        }

        public double FinishReservation(Reservation reservation)
        {
            MakeReservationInactive(reservation, true);
            return reservation.total_price;
        }

        // count date difference for reservation
        public int GetDateDifference(DateTime start, DateTime end)
        {
            if (start.Date == end.Date)
            {
                return 1;
            }
            
            TimeSpan difference = end.Date - start.Date;
            return (int)difference.TotalDays;
        }

    }
}
