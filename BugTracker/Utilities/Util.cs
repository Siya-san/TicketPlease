using Microsoft.Data.SqlClient;

namespace BugTracker.Utilities
{
    public class Util
    {
         public string connectionString { get; set; } 

    public Util()
        {
            connectionString= "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BTDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        }
        public bool IsUserTableEmpty()
        {
            string query = "SELECT COUNT(*) FROM users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int recordCount = (int)command.ExecuteScalar();
                return recordCount == 0;
            }
        }

    }
}
