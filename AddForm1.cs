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
    public partial class AddForm1 : Form
    {
        private string _connectionString;
        public AddForm1()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Получение данных из текстовых полей
            int id = int.Parse(txtId.Text); // ID сервиса
            string location = txtPartName.Text; // Название сервиса
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtPartName.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // Переменная для хранения сообщения от триггера
                    string triggerMessage = "";

                    // Обработчик события Notice для получения сообщений от триггера
                    conn.Notice += (o, args) =>
                    {
                        triggerMessage += args.Notice.MessageText + Environment.NewLine;
                    };

                    string sql = "SELECT add_service(@location, @id);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("location", location);

                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Формирование сообщения для пользователя
                        string message = "Сервис успешно добавлен в базе данных!";
                        if (!string.IsNullOrEmpty(triggerMessage))
                        {
                            message += $"\n\nСообщение от триггера:\n{triggerMessage}";
                        }

                        // Вывод сообщения пользователю
                        MessageBox.Show(message, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при добавлении сервиса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
