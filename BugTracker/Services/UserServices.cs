
using BugTracker.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace BugTracker.Services
{
    
    public class UserServices
    {

        public const string DBcon =
          "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BTDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public void CreateNewUsers(UserViewModel user)
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                string connectionString = configuration.GetConnectionString(DBcon);


                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (type, username,password) VALUES (@type, @username, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);
                 //   command.Parameters.AddWithValue("@isAdmin", user.isAdmin);
                    command.Parameters.AddWithValue("@username", user.username);
               
                    command.Parameters.AddWithValue("@password", EncryptPassword(user.password));
                    command.ExecuteNonQuery();
                }
            }
            public static string EncryptPassword(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {

                    byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));


                    StringBuilder stringBuilder = new StringBuilder();


                    for (int i = 0; i < data.Length; i++)
                    {
                        stringBuilder.Append(data[i].ToString("x2"));
                    }

                    return stringBuilder.ToString();
                }
            }

            public static bool ValidatePassword(string inputPassword, string hashedPassword)
            {
                string hashedInput = EncryptPassword(inputPassword);
                return String.Equals(hashedInput, hashedPassword, StringComparison.Ordinal);
            }
            public UserViewModel Login(UserViewModel user)
            {
                var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

                string connectionString = configuration.GetConnectionString(DBcon);


                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(DBcon))
                {
                    connection.Open();
                    string query = "SELECT * FROM users WHERE username=@username ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", user.username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader != null)
                            {
                                if (user.password== reader["password"].ToString())
                                {
                                    user.username = reader["username"].ToString();
                                user.type = Enum.Parse<UserViewModel.UserType>(reader["type"].ToString());



                                    user.password = reader["password"].ToString();
                                    return user;
                                }
                            }
                        }
                        return null;
                    }


                }

            }

        }
    
    

    

}
