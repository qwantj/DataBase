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
    public partial class EditForm2 : Form
    {
        
        private string _connectionString;
        public EditForm2()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустые строки
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(txtIdService.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            // Получение данных из текстовых полей
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string number = txtNumber.Text;
            string role = comboBox1.Text.Trim().ToLower();
            int id_service = int.Parse(txtIdService.Text);
            string login = txtLogin.Text;
            string pass = txtPass.Text;
            if (role != "admin" && role != "engineer")
            {
                MessageBox.Show("Роль должна быть admin или engineer.");
                return;
            }
            try
            {

                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))

                {
                    conn.Open();
                    string sql = "SELECT update_worker(@id, @name, @number, @id_service, @login, @pass, @role::role_type);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("number", number);
                        cmd.Parameters.AddWithValue("id_service", id_service);
                        cmd.Parameters.AddWithValue("login", login);
                        cmd.Parameters.AddWithValue("pass", pass);
                        cmd.Parameters.AddWithValue("role", NpgsqlTypes.NpgsqlDbType.Varchar, role);
                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Уведомление об успешном выполнении
                        MessageBox.Show("Работник успешно изменен в базе данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при изменении работника: {ex.Message}");
            }
        }
    }
}
