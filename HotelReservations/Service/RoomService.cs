using HotelReservations.Model;
using HotelReservations.Repository;
using System.Collections.Generic;

namespace HotelReservations.Service
{
    public class RoomService
    {
        IRoomRepository roomRepository;
        public RoomService() 
        { 
            roomRepository = new RoomRepositoryDB();
        }

        public List<Room> GetAllRooms()
        {
            return Hotel.GetInstance().Rooms;
        }

        public Room GetRoomByRoomNumber(string roomNumber)
        {
            Room room = Hotel.GetInstance().Rooms.Find(rn => rn.room_number == roomNumber);
            return room;
        }

        public void SaveRoom(Room room)
        {
            if (room.room_id == 0)
            {
                Hotel.GetInstance().Rooms.Add(room);
                room.room_id = roomRepository.Insert(room);
            }
            else
            {
                var index = Hotel.GetInstance().Rooms.FindIndex(r => r.room_id == room.room_id);
                Hotel.GetInstance().Rooms[index] = room;
                roomRepository.Update(room);
            }
        }

        public void MakeRoomInactive(Room room)
        {
            var makeRoomInactive = Hotel.GetInstance().Rooms.Find(r => r.room_id == room.room_id);
            makeRoomInactive.room_is_active = false;
            roomRepository.Delete(room.room_id);
        }
    }
}
