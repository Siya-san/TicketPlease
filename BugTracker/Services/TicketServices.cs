using BugTracker.Utilities;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using BugTracker.Models;
using static BugTracker.Models.TicketViewModel;

namespace BugTracker.Services
{
    public class TicketServices
    {
        public const string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BTDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public void CreateBugTicket(TicketViewModel ticket)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Tickets (Status, TicketSummary, TicketDescription, Severity, Priority, Type) VALUES (@Status, @TicketSummary, @TicketDescription, @Severity, @Priority, @Type)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", TicketViewModel.TicketStatus.Underway.ToString());
                    command.Parameters.AddWithValue("@TicketSummary", ticket.TicketSummary);
                    command.Parameters.AddWithValue("@TicketDescription", ticket.TicketDescription);
                    command.Parameters.AddWithValue("@Severity", ticket.Severity.ToString());
                    command.Parameters.AddWithValue("@Priority", ticket.Priority.ToString());
                    command.Parameters.AddWithValue("@Type", TicketViewModel.TicketType.Bug);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void CreateRequestTicket(TicketViewModel ticket)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Tickets (Status, TicketSummary, TicketDescription, Severity, Priority, Type) VALUES (@Status, @TicketSummary, @TicketDescription, @Severity, @Priority, @Type)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", TicketViewModel.TicketStatus.Underway.ToString());
                    command.Parameters.AddWithValue("@TicketSummary", ticket.TicketSummary);
                    command.Parameters.AddWithValue("@TicketDescription", ticket.TicketDescription);
                    command.Parameters.AddWithValue("@Severity", ticket.Severity.ToString());
                    command.Parameters.AddWithValue("@Priority", ticket.Priority.ToString());
                    command.Parameters.AddWithValue("@Type", TicketViewModel.TicketType.Request);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void CreateTestTicket(TicketViewModel ticket)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Tickets (Status, TicketSummary, TicketDescription, Severity, Priority, Type) VALUES (@Status, @TicketSummary, @TicketDescription, @Severity, @Priority, @Type)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", TicketViewModel.TicketStatus.Underway.ToString());
                    command.Parameters.AddWithValue("@TicketSummary", ticket.TicketSummary);
                    command.Parameters.AddWithValue("@TicketDescription", ticket.TicketDescription);
                    command.Parameters.AddWithValue("@Severity", ticket.Severity.ToString());
                    command.Parameters.AddWithValue("@Priority", ticket.Priority.ToString());
                    command.Parameters.AddWithValue("@Type", TicketViewModel.TicketType.Test);
                    command.ExecuteNonQuery();
                }
            }
        }
        public TicketViewModel GetTicketById(int id)
        {
            TicketViewModel ticket = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Tickets WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ticket = new TicketViewModel
                            {
                                Id = (int)reader["Id"],
                                Status = Enum.Parse<TicketViewModel.TicketStatus>(reader["Status"].ToString()),
                                TicketSummary = reader["TicketSummary"].ToString(),
                                TicketDescription = reader["TicketDescription"].ToString(),
                              
                                Severity = Enum.Parse<TicketViewModel.TicketSeverity>(reader["Severity"].ToString()),
                                Priority = Enum.Parse<TicketViewModel.TicketPriority>(reader["Priority"].ToString()),
                                Type = Enum.Parse<TicketViewModel.TicketType>(reader["Type"].ToString())
                            };
                        }
                    }
                }
            }

            return ticket;
        }

        public void UpdateTicket(TicketViewModel ticket, int Id)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Tickets SET TicketSummary = @TicketSummary, TicketDescription = @TicketDescription, Severity = @Severity, Priority = @Priority WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@TicketSummary", ticket.TicketSummary);
                    command.Parameters.AddWithValue("@TicketDescription", ticket.TicketDescription);
             
                    command.Parameters.AddWithValue("@Severity", ticket.Severity.ToString());
                    command.Parameters.AddWithValue("@Priority", ticket.Priority.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
        public void ResolveTicket(int  Id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Tickets SET Status = @Status  WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Status", TicketStatus.Resolved.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<TicketViewModel> Read(TicketType type)
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Tickets WHERE Type = @Type AND Status = @Status";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Status", TicketViewModel.TicketStatus.Underway.ToString());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TicketViewModel ticket = new TicketViewModel
                            {
                                Id = (int)reader["Id"],
                                Status = Enum.Parse<TicketViewModel.TicketStatus>(reader["Status"].ToString()),
                                TicketSummary = reader["TicketSummary"].ToString(),
                                TicketDescription = reader["TicketDescription"].ToString(),

                                Severity = Enum.Parse<TicketViewModel.TicketSeverity>(reader["Severity"].ToString()),
                                Priority = Enum.Parse<TicketViewModel.TicketPriority>(reader["Priority"].ToString()),
                                Type = Enum.Parse<TicketViewModel.TicketType>(reader["Type"].ToString())
                            };
                            tickets.Add(ticket);
                        }
                    }
                }
            }
            return tickets;
        }

        public void DeleteTicket(int Id)
        {
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Tickets WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
