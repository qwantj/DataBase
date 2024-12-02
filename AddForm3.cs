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
    public partial class AddForm3 : Form
    {
        private string _connectionString;
        public AddForm3()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверка на пустые строки
            if (string.IsNullOrWhiteSpace(txtId1.Text) ||
                string.IsNullOrWhiteSpace(txtIdPart1.Text) ||
                string.IsNullOrWhiteSpace(txtIdWorker1.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            // Получение данных из текстовых полей
            int id = int.Parse(txtId1.Text); // ID детали
            int id_part = int.Parse(txtIdPart1.Text); // ID детали
            int id_worker = int.Parse(txtIdWorker1.Text); // ID сотрудника
            string status = comboBox2.Text; // Статус
            if (status != "Open" && status != "Close")
            {
                MessageBox.Show("Статус должен быть Open или Close.");
                return;
            }
            try
            {
                // Строка подключения к PostgreSQL (замените данные своими)
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))

                {
                    conn.Open();
                    string sql = "SELECT add_service_order( @id_part, @id_part, @id, @status);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id_part", id_part);
                        cmd.Parameters.AddWithValue("id_part", id_worker);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("status", NpgsqlTypes.NpgsqlDbType.Varchar, status);

                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Уведомление об успешном выполнении
                        MessageBox.Show("Заказ успешно добавлен в базу данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при добавлении заказа: {ex.Message}");
            }
        }
    }
}
