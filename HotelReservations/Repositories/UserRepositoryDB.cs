using HotelReservations.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

using System.Data;

namespace HotelReservations.Repositories
{
    public class UserRepositoryDB : IUserRepository
    {
        public List<User> GetAll()
        {
            try
            {
                var users = new List<User>();
                using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    var commandText = "SELECT * FROM dbo.[user]";
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "user");

                    foreach (DataRow row in dataSet.Tables["user"]!.Rows)
                    {
                        var user = new User()
                        {
                            user_id = (int)row["user_id"],
                            first_name = (string)row["first_name"],
                            last_name = (string)row["last_name"],
                            JMBG = (string)row["JMBG"],
                            password = (string)row["password"],
                            username = (string)row["username"],
                            user_is_active = (bool)row["user_is_active"],
                        };
                        if (Enum.TryParse<UserType>(row["user_type"]?.ToString(), out UserType userType))
                        {
                            user.user_type = userType;
                        }

                        users.Add(user);
                    }
                }
 
                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int Insert(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.[user] (first_name, last_name, JMBG, username, password, user_type, user_is_active)
                    OUTPUT inserted.user_id
                    VALUES (@first_name, @last_name, @JMBG, @username, @password, @user_type, @user_is_active)
                ";

                command.Parameters.Add(new SqlParameter("first_name", user.first_name));
                command.Parameters.Add(new SqlParameter("last_name", user.last_name));
                command.Parameters.Add(new SqlParameter("JMBG", user.JMBG));
                command.Parameters.Add(new SqlParameter("username", user.username));
                command.Parameters.Add(new SqlParameter("password", user.password));
                command.Parameters.Add(new SqlParameter("user_type", user.user_type.ToString()));
                command.Parameters.Add(new SqlParameter("user_is_active", user.user_is_active));

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.[user] 
                    SET first_name=@first_name, last_name=@last_name, JMBG=@JMBG, username=@username, password=@password, user_type=@user_type, user_is_active=@user_is_active
                    WHERE user_id=@user_id
                ";

                command.Parameters.Add(new SqlParameter("user_id", user.user_id));
                command.Parameters.Add(new SqlParameter("first_name", user.first_name));
                command.Parameters.Add(new SqlParameter("last_name", user.last_name));
                command.Parameters.Add(new SqlParameter("JMBG", user.JMBG));
                command.Parameters.Add(new SqlParameter("username", user.username));
                command.Parameters.Add(new SqlParameter("password", user.password));
                command.Parameters.Add(new SqlParameter("user_type", user.user_type.ToString()));
                command.Parameters.Add(new SqlParameter("user_is_active", user.user_is_active));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int userId)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    UPDATE dbo.[user]
                    SET user_is_active = 0
                    WHERE user_id = @user_id
                ";

                command.Parameters.Add(new SqlParameter("user_id", userId));

                command.ExecuteNonQuery();

            }
        }
    }
}
