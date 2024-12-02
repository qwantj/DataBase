using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PartsManagementApp
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            // Получаем строку подключения из App.config
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }

        // Метод для выполнения SQL-запросов
        public DataTable ExecuteQuery(string query)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
                    return null;
                }
            }
        }

        // Метод для выполнения команд (INSERT, UPDATE, DELETE)
        public bool ExecuteNonQuery(string query)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выполнении команды: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
