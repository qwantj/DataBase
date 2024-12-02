using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration; // Добавлено для доступа к строке подключения
using Npgsql;
using PartsManagementApp;

namespace DataBase
{
    public partial class LoginForm : Form
    {
        private DatabaseManager _dbManager;

        public LoginForm()
        {
            InitializeComponent();
            this.textBox2.AutoSize = false;
            this.textBox2.Size = new Size(this.textBox1.Size.Width, 63);
            _dbManager = new DatabaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка авторизации
            string role = AuthenticateUser(username, password);
            if (role == "admin")
            {
                this.Hide();
                AdminForm adminForm = new AdminForm();
                adminForm.ShowDialog();
                this.Close();
            }
            else if (role == "engineer")
            {
                this.Hide();
                EngineerForm engineerForm = new EngineerForm();
                engineerForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string AuthenticateUser(string username, string password)
        {
            string role = null;

            try
            {
                // Получаем строку подключения из App.config
                string connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT role FROM worker_table WHERE username = @username AND password_hash = @password";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("password", password);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            role = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return role;
        }
    }
}
