using HotelReservations.Model;
using HotelReservations.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace HotelReservations.Service
{
    public class GuestService
    {
        IGuestRepository guestRepository;

        public GuestService()
        {
            guestRepository = new GuestRepositoryDB();
        }

        public void SaveGuest(Guest guest, bool editing = false)
        {
            // this means guest will be in memory because reservation isn't created yet.
            if (guest.guest_id == 0 && editing == false)
            {
                guest.guest_reservation_id = 0;
                Hotel.GetInstance().Guests.Add(guest);
            }

            // otherwise, its editing guest
            else
            {
                int resId = Hotel.GetInstance().Guests.Find(g => g.guest_id == guest.guest_id).guest_reservation_id;
                guest.guest_reservation_id = resId;
                var index = Hotel.GetInstance().Guests.FindIndex(g => (g.guest_id == guest.guest_id) &&(g.guest_jmbg == guest.guest_jmbg));
                Hotel.GetInstance().Guests[index] = guest;

                guestRepository.Update(guest);
                RefreshGuestsInReservation(guest.guest_reservation_id);
            }
        }

        public void RewriteGuestIdAfterReservationIsCreated(int newReservationId)
        {
            var guestsToRewriteId = Hotel.GetInstance().Guests.Where(guest => guest.guest_reservation_id == 0);
            foreach (Guest guest in guestsToRewriteId)
            {
                guest.guest_reservation_id = newReservationId;
                // it will be added to database after getting real reservation id(after reservation is created).
                guestRepository.Insert(guest);
            }
        }

        public void MakeGuestInactive(Guest guest)
        {
            var makeGuestInactive = Hotel.GetInstance().Guests.Find(g => (g.guest_id == guest.guest_id) && (g.guest_jmbg == guest.guest_jmbg));
            makeGuestInactive.guest_is_active = false;

            guestRepository.Delete(guest.guest_id);
            RefreshGuestsInReservation(guest.guest_reservation_id);
        }

        // helper function for refresh in memory after database is updated/deleted :)
        public void RefreshGuestsInReservation(int forThisReservationId)
        {
            foreach (var reservation in Hotel.GetInstance().Reservations)
            {
                if (reservation.reservation_id == forThisReservationId)
                {
                    reservation.Guests = new List<Guest>();

                    foreach (var guest in Hotel.GetInstance().Guests)
                    {
                        if (guest.guest_reservation_id == reservation.reservation_id)
                        {
                            reservation.Guests.Add(guest);
                        }
                    }
                }
            }
        }


    }
}
