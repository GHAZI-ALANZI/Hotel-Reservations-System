using HotelReservations.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

using System.Data;

namespace HotelReservations.Repositories
{
    public class GuestRepositoryDB : IGuestRepository
    {

        public List<Guest> GetAll()
        {
            try
            {
                var guests = new List<Guest>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.guest";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "guest");

                    foreach (DataRow row in dataSet.Tables["guest"]!.Rows)
                    {
                        var guest = new Guest()
                        {
                            guest_id = (int)row["guest_id"],
                            guest_name = (string)row["guest_name"],
                            guest_surname = (string)row["guest_surname"],
                            guest_jmbg = (string)row["guest_jmbg"],
                            guest_is_active = (bool)row["guest_is_active"],
                            guest_reservation_id = (int)row["guest_reservation_id"]
                        };

                        guests.Add(guest);
                    }
                }

                return guests;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int Insert(Guest guest)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.guest (guest_name, guest_surname, guest_jmbg, guest_is_active, guest_reservation_id)
                    OUTPUT inserted.guest_id
                    VALUES (@guest_name, @guest_surname, @guest_jmbg, @guest_is_active, @guest_reservation_id)
                ";

                command.Parameters.Add(new SqlParameter("guest_name", guest.guest_name));
                command.Parameters.Add(new SqlParameter("guest_surname", guest.guest_surname));
                command.Parameters.Add(new SqlParameter("guest_jmbg", guest.guest_jmbg));
                command.Parameters.Add(new SqlParameter("guest_is_active", guest.guest_is_active));
                command.Parameters.Add(new SqlParameter("guest_reservation_id", guest.guest_reservation_id));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Guest guest)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.guest
                    SET guest_name=@guest_name, guest_surname=@guest_surname, guest_jmbg=@guest_jmbg, guest_is_active=@guest_is_active, guest_reservation_id=@guest_reservation_id
                    WHERE guest_id=@guest_id
                ";

                command.Parameters.Add(new SqlParameter("guest_id", guest.guest_id));
                command.Parameters.Add(new SqlParameter("guest_name", guest.guest_name));
                command.Parameters.Add(new SqlParameter("guest_surname", guest.guest_surname));
                command.Parameters.Add(new SqlParameter("guest_jmbg", guest.guest_jmbg));
                command.Parameters.Add(new SqlParameter("guest_is_active", guest.guest_is_active));
                command.Parameters.Add(new SqlParameter("guest_reservation_id", guest.guest_reservation_id));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int guestId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.guest
                    SET guest_is_active = 0
                    WHERE guest_id = @guest_id
                ";

                command.Parameters.Add(new SqlParameter("guest_id", guestId));

                command.ExecuteNonQuery();
            }
        }
    }
}
