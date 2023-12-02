using HotelReservations.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace HotelReservations.Repository
{
    public class RoomRepositoryDB : IRoomRepository
    {
        public List<Room> GetAll()
        {
            try
            {
                var rooms = new List<Room>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT r.*, rt.* FROM dbo.room r\r\nINNER JOIN dbo.room_type rt ON r.room_type_id = rt.room_type_id";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "room");

                    foreach (DataRow row in dataSet.Tables["room"]!.Rows)
                    {
                        var room = new Room()
                        {
                            room_id = (int)row["room_id"],
                            room_number = row["room_number"] as string,
                            has_TV = (bool)row["has_TV"],
                            has_mini_bar = (bool)row["has_mini_bar"],
                            room_is_active = (bool)row["room_is_active"],
                            RoomType = new RoomType()
                            {
                                room_type_id = (int)row["room_type_id"],
                                room_type_name = (string)row["room_type_name"],
                                room_type_is_active = (bool)row["room_type_is_active"]
                            }
                        };

                        rooms.Add(room);
                    }
                }

                return rooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public int Insert(Room room)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.room (room_number, has_TV, has_mini_bar, room_is_active, room_type_id)
                    OUTPUT inserted.room_id
                    VALUES (@room_number, @has_TV, @has_mini_bar, @room_is_active, @room_type_id)
                ";

                command.Parameters.Add(new SqlParameter("room_number", room.room_number));
                command.Parameters.Add(new SqlParameter("has_TV", room.has_TV));
                command.Parameters.Add(new SqlParameter("has_mini_bar", room.has_mini_bar));
                command.Parameters.Add(new SqlParameter("room_is_active", room.room_is_active));
                command.Parameters.Add(new SqlParameter("room_type_id", room.RoomType.room_type_id));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Room room)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.room 
                    SET room_number=@room_number, has_TV=@has_TV, has_mini_bar=@has_mini_bar, room_is_active=@room_is_active, room_type_id=@room_type_id
                    WHERE room_id=@room_id
                ";

                command.Parameters.Add(new SqlParameter("room_id", room.room_id));
                command.Parameters.Add(new SqlParameter("room_number", room.room_number));
                command.Parameters.Add(new SqlParameter("has_TV", room.has_TV));
                command.Parameters.Add(new SqlParameter("has_mini_bar", room.has_mini_bar));
                command.Parameters.Add(new SqlParameter("room_is_active", room.room_is_active));
                command.Parameters.Add(new SqlParameter("room_type_id", room.RoomType.room_type_id));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.room
                    SET room_is_active = 0
                    WHERE room_id = @room_id
                ";

                command.Parameters.Add(new SqlParameter("room_id", roomId));

                command.ExecuteNonQuery();

            }
        }
    }
}
