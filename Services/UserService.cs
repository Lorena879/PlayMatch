using Microsoft.Data.Sqlite;
using PlayMatch.Models;

namespace PlayMatch.Services
{
    public class UserService
    {
        public bool ValidateUser(string email, string password)
        {
            using var conn = new SqliteConnection($"Data Source={App.DatabasePath}");
            conn.Open();

            string query = "SELECT COUNT(1) FROM Users WHERE Email=@Email AND Password=@Password";

            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email ?? string.Empty);
            cmd.Parameters.AddWithValue("@Password", password ?? string.Empty);

            var result = cmd.ExecuteScalar();
            long count = 0;
            if (result != null && long.TryParse(result.ToString(), out var parsed))
                count = parsed;

            return count > 0;
        }

        public bool RegisterUser(User user)
        {
            using var conn = new SqliteConnection($"Data Source={App.DatabasePath}");
            conn.Open();

            string query = @"INSERT INTO Users (FullName, Email, Password, CreatedAt) VALUES (@FullName, @Email, @Password, @CreatedAt)";

            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@FullName", user.FullName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@Password", user.Password ?? string.Empty);
            cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt.ToString("o"));

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}


