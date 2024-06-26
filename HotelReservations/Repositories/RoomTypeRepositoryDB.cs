﻿using HotelReservations.Model;
using System.Collections.Generic;

using System.Data;
using System;
using Microsoft.Data.SqlClient;

namespace HotelReservations.Repositories
{
    public class RoomTypeRepositoryDB : IRoomTypeRepository
    {
        public List<RoomType> GetAll()
        {
            try
            {
                var roomTypes = new List<RoomType>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.room_type";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "room_type");

                    foreach (DataRow row in dataSet.Tables["room_type"]!.Rows)
                    {
                        var roomType = new RoomType()
                        {
                            room_type_id = (int)row["room_type_id"],
                            room_type_name = (string)row["room_type_name"],
                            room_type_is_active = (bool)row["room_type_is_active"],
                        };

                        roomTypes.Add(roomType);
                    }
                }

                return roomTypes;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public int Insert(RoomType roomType)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.room_type (room_type_name, room_type_is_active)
                    OUTPUT inserted.room_type_id
                    VALUES (@room_type_name, @room_type_is_active)
                ";

                command.Parameters.Add(new SqlParameter("room_type_name", roomType.room_type_name));
                command.Parameters.Add(new SqlParameter("room_type_is_active", roomType.room_type_is_active));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(RoomType roomType)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.room_type
                    SET room_type_name=@room_type_name, room_type_is_active=@room_type_is_active
                    WHERE room_type_id = @room_type_id
                ";

                command.Parameters.Add(new SqlParameter("room_type_id", roomType.room_type_id));
                command.Parameters.Add(new SqlParameter("room_type_name", roomType.room_type_name));
                command.Parameters.Add(new SqlParameter("room_type_is_active", roomType.room_type_is_active));

                command.ExecuteNonQuery();
            }
        }
        public void Delete(int roomTypeId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.room_type
                    SET room_type_is_active = 0
                    WHERE room_type_id = @room_type_id
                ";

                command.Parameters.Add(new SqlParameter("room_type_id", roomTypeId));

                command.ExecuteNonQuery();

            }
        }
    }
}
