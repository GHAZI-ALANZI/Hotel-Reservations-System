using HotelReservations.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

using System.Data;


namespace HotelReservations.Repositories
{
    public class ReservationRepositoryDB : IReservationRepository
    {

        public List<Reservation> GetAll()
        {
            try
            {
                var reservations = new List<Reservation>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.reservation";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "reservation");

                    foreach (DataRow row in dataSet.Tables["reservation"]!.Rows)
                    {
                        var reservation = new Reservation()
                        {
                            reservation_id = (int)row["reservation_id"],
                            reservation_room_number = row["reservation_room_number"] as string,
                            start_date_time = (DateTime)row["start_date_time"],
                            end_date_time = (DateTime)row["end_date_time"],
                            total_price = (double)row["total_price"],
                            reservation_is_active = (bool)row["reservation_is_active"],
                            reservation_is_finished = (bool)row["reservation_is_finished"],
                        };
                        if (Enum.TryParse<ReservationType>(row["reservation_type"]?.ToString(), out ReservationType reservationType))
                        {
                            reservation.reservation_type = reservationType;
                        }

                        reservations.Add(reservation);
                    }
                }

                return reservations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public int Insert(Reservation res)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.reservation (reservation_room_number, reservation_type, start_date_time, end_date_time, total_price, reservation_is_active, reservation_is_finished)
                    OUTPUT inserted.reservation_id
                    VALUES (@reservation_room_number, @reservation_type, @start_date_time, @end_date_time, @total_price, @reservation_is_active, @reservation_is_finished)
                ";

                command.Parameters.Add(new SqlParameter("reservation_room_number", res.reservation_room_number));
                command.Parameters.Add(new SqlParameter("reservation_type", res.reservation_type.ToString()));
                command.Parameters.Add(new SqlParameter("start_date_time", res.start_date_time));
                command.Parameters.Add(new SqlParameter("end_date_time", res.end_date_time));
                command.Parameters.Add(new SqlParameter("total_price", res.total_price));
                command.Parameters.Add(new SqlParameter("reservation_is_active", res.reservation_is_active));
                command.Parameters.Add(new SqlParameter("reservation_is_finished", res.reservation_is_finished));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Reservation res)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.reservation
                    SET reservation_room_number=@reservation_room_number, reservation_type=@reservation_type, start_date_time=@start_date_time, end_date_time=@end_date_time,
                        total_price=@total_price, reservation_is_active=@reservation_is_active, reservation_is_finished=@reservation_is_finished
                    WHERE reservation_id=@reservation_id
                ";

                command.Parameters.Add(new SqlParameter("reservation_id", res.reservation_id));
                command.Parameters.Add(new SqlParameter("reservation_room_number", res.reservation_room_number));
                command.Parameters.Add(new SqlParameter("reservation_type", res.reservation_type.ToString()));
                command.Parameters.Add(new SqlParameter("start_date_time", res.start_date_time));
                command.Parameters.Add(new SqlParameter("end_date_time", res.end_date_time));
                command.Parameters.Add(new SqlParameter("total_price", res.total_price));
                command.Parameters.Add(new SqlParameter("reservation_is_active", res.reservation_is_active));
                command.Parameters.Add(new SqlParameter("reservation_is_finished", res.reservation_is_finished));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int resId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.reservation
                    SET reservation_is_active = 0
                    WHERE reservation_id = @reservation_id
                ";

                command.Parameters.Add(new SqlParameter("reservation_id", resId));

                command.ExecuteNonQuery();
            }
        }

    }
}
