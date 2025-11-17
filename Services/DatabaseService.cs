using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using PlayMatch.Models;

namespace PlayMatch.Services
{
    public class DatabaseService
    {
        private readonly string _dbPath;

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            conn.Open();

            string tableCmd = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT,
                    Email TEXT UNIQUE,
                    Password TEXT,
                    CreatedAt TEXT
                );";

            using var cmd = new SqliteCommand(tableCmd, conn);
            cmd.ExecuteNonQuery();
        }

        // Obtener usuario por email + password
        public async Task<User?> GetUserAsync(string email, string password)
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            string query = @"SELECT Id, FullName, Email, Password, CreatedAt 
                             FROM Users 
                             WHERE Email=@Email AND Password=@Password 
                             LIMIT 1";

            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email ?? string.Empty);
            cmd.Parameters.AddWithValue("@Password", password ?? string.Empty);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    CreatedAt = DateTime.Parse(reader.GetString(4))
                };
            }

            return null;
        }

        // Guardar usuario (Registrar)
        public async Task<bool> AddUserAsync(User user)
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            await conn.OpenAsync();

            string query = @"
                INSERT INTO Users (FullName, Email, Password, CreatedAt)
                VALUES (@FullName, @Email, @Password, @CreatedAt);";

            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@FullName", user.FullName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("@Password", user.Password ?? string.Empty);
            cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt.ToString("o"));

            try
            {
                var result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
            catch (SqliteException ex)
            {
                // posible clave única duplicada
                System.Diagnostics.Debug.WriteLine($"DB AddUserAsync error: {ex.Message}");
                return false;
            }
        }

        // Helper para compatibilidad (no usado si llamas AddUserAsync directamente)
        public async Task SaveUserAsync(User user) => await AddUserAsync(user);
    }
}
