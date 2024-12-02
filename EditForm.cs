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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataBase
{
    public partial class EditForm : Form
    {
        private string _connectionString;
        public EditForm()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtPartName.Text) ||
                string.IsNullOrWhiteSpace(txtPartNumber.Text) ||
                string.IsNullOrWhiteSpace(txtAssignedTo.Text) ||
                string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }
            // Получение данных из текстовых полей
            int id = int.Parse(txtId.Text); // ID детали
            string name = txtPartName.Text; // Название детали
            string number = txtPartNumber.Text; // Уникальный номер детали
            int assigned = int.Parse(txtAssignedTo.Text); // Назначено сотруднику
            int location = int.Parse(txtLocation.Text); // ID местоположения
            
            try
            {
                // Строка подключения к PostgreSQL (замените данные своими)
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                
                {
                    conn.Open();
                    string sql = "SELECT update_part_admin(@id, @name, @number, @assigned, @location);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("number", number);
                        cmd.Parameters.AddWithValue("assigned", assigned);
                        cmd.Parameters.AddWithValue("location", location);

                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Уведомление об успешном выполнении
                        MessageBox.Show("Деталь успешно изменена в базе данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при изменении детали: {ex.Message}");
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPartName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAssignedTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPartNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
