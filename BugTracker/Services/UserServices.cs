
using BugTracker.Models;
using BugTracker.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BugTracker.Services
{

    public class UserServices
    {
        private Util util = new Util(); 
        
        public void CreateNewUsers(UserViewModel user)
            {
               

                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(util.connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (type, username,password,defaultPw) VALUES (@type, @username, @password,@defaultPw)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@type", user.type);
                    command.Parameters.AddWithValue("@username", user.username);
                    command.Parameters.AddWithValue("@password", EncryptPassword("TicketPlease2024"));
                command.Parameters.AddWithValue("@defaultPw", true);
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
            using (SqlConnection connection = new SqlConnection(util.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE username=@username ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", user.username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader != null)
                        {
                            if (ValidatePassword(user.password, reader["password"].ToString()))
                            {
                                user.Id = Convert.ToInt32(reader["Id"]);

                                user.username = reader["username"].ToString();
                                user.type = Enum.Parse<UserViewModel.UserType>(reader["type"].ToString());
                                user.password = reader["password"].ToString();
                                user.defaultPw = Convert.ToBoolean(reader["defaultPW"]);
                            }
                        }
                    }
                    return user;
                }
            }


        }   
        public List<UserViewModel> Read()
        {
            
            List<UserViewModel> users = new List<UserViewModel>();

            using (SqlConnection connection = new SqlConnection(util.connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM users";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserViewModel user = new UserViewModel
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        username = reader["username"].ToString(),
                        type = Enum.Parse<UserViewModel.UserType>(reader["type"].ToString()),
                        password = reader["password"].ToString()

                    };
                    users.Add(user);
                }

                reader.Close();
            }
            return users;
        }

        public void UpdatePassword(UserViewModel User, int Id)
        {

            using (SqlConnection connection = new SqlConnection(util.connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET password = @password, defaultPw = @defaultPw WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@password", EncryptPassword(User.password));
                    command.Parameters.AddWithValue("@defaultPw", false);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateUser(UserViewModel User, int Id)
        {

            using (SqlConnection connection = new SqlConnection(util.connectionString))
            {
                connection.Open();
                string query = "UPDATE Users SET password = @password, username = @username, defaultPw = @defaultPw WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@username", User.username);
                    command.Parameters.AddWithValue("@password", EncryptPassword(User.password)); command.Parameters.AddWithValue("@defaultPw", false);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void createAdmin()
        {
            using (SqlConnection connection = new SqlConnection(util.connectionString))
            {
                connection.Open();
                string query = "INSERT INTO users (type, username,password,defaultPw) VALUES (@type, @username, @password, @defaultPw)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", UserViewModel.UserType.Administrator);
                command.Parameters.AddWithValue("@username", "superAdmin");
                command.Parameters.AddWithValue("@password", EncryptPassword("TicketPlease2024"));
                command.Parameters.AddWithValue("@defaultPw", true);
                command.ExecuteNonQuery();
            }
        }
    }
}
