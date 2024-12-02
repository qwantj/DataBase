using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DataBase
{
    public partial class DeleteForm1 : Form
    {


        private string _connectionString;
        public DeleteForm1()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("ID должно быть числом.");
                return;
            }
            // Получение данных из текстовых полей
            int id = int.Parse(textBox1.Text); // ID детали

            try
            {
                // Строка подключения к PostgreSQL (замените данные своими)
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))

                {
                    conn.Open();
                    string sql = "SELECT delete_service(@id);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id", id);


                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Уведомление об успешном выполнении
                        MessageBox.Show("Сервис успешно удален из базы данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при удалении сервиса: {ex.Message}");
            }
        }
    }
}
