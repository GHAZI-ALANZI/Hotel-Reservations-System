using HotelReservations.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

using System.Data;

namespace HotelReservations.Repositories
{
    public class PriceRepositoryDB : IPriceRepository
    {
        public List<Price> GetAll()
        {
            try
            {
                var prices = new List<Price>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT p.*, rt.* FROM dbo.price p\r\nINNER JOIN dbo.room_type rt ON p.room_type_id = rt.room_type_id";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "price");

                    foreach (DataRow row in dataSet.Tables["price"]!.Rows)
                    {
                        var price = new Price()
                        {
                            price_id = (int)row["price_id"],
                            price_value = (Double)row["price_value"],
                            price_is_active = (bool)row["price_is_active"],
                            room_type_id = new RoomType()
                            {
                                room_type_id = (int)row["room_type_id"],
                                room_type_name = (string)row["room_type_name"],
                                room_type_is_active = (bool)row["room_type_is_active"]
                            }
                        };

                        if (Enum.TryParse<ReservationType>(row["price_reservation_type"]?.ToString(), out ReservationType resType))
                        {
                            price.price_reservation_type = resType;
                        }

                        prices.Add(price);
                    }
                }

                return prices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public int Insert(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.price (price_value, price_is_active, price_reservation_type, room_type_id)
                    OUTPUT inserted.price_id
                    VALUES (@price_value, @price_is_active, @price_reservation_type, @room_type_id)
                ";

                command.Parameters.Add(new SqlParameter("room_type_id", price.room_type_id.room_type_id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.price_reservation_type.ToString()));
                command.Parameters.Add(new SqlParameter("price_is_active", price.price_is_active));
                command.Parameters.Add(new SqlParameter("price_value", price.price_value));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Price price)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.price 
                    SET price_value=@price_value, price_is_active=@price_is_active, price_reservation_type=@price_reservation_type, room_type_id=@room_type_id
                    WHERE price_id=@price_id
                ";

                command.Parameters.Add(new SqlParameter("price_id", price.price_id));
                command.Parameters.Add(new SqlParameter("room_type_id", price.room_type_id.room_type_id));
                command.Parameters.Add(new SqlParameter("price_reservation_type", price.price_reservation_type.ToString()));
                command.Parameters.Add(new SqlParameter("price_is_active", price.price_is_active));
                command.Parameters.Add(new SqlParameter("price_value", price.price_value));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int priceId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.price
                    SET price_is_active = 0
                    WHERE price_id = @price_id
                ";

                command.Parameters.Add(new SqlParameter("price_id", priceId));

                command.ExecuteNonQuery();

            }
        }
    }
}
