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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataBase
{
    public partial class EngineerForm : Form
    {
        private string _connectionString;
        public EngineerForm()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            LoadFirstTabData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Получение данных из текстовых полей
            int id = int.Parse(textBox1.Text);
            string status = comboBox1.Text; // Название детали
            if (status != "TEST" && status != "BAD" && status != "GOOD")
            {
                MessageBox.Show("Значение статуса должно быть TEST, GOOD или BAD.");
                return;
            }

            try
            {
                // Строка подключения к PostgreSQL (замените данные своими)
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))

                {
                    conn.Open();
                    string sql = "SELECT update_part_status(@id, @status);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Передача параметров в функцию
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("status", status);
                        // Выполнение команды
                        cmd.ExecuteNonQuery();

                        // Уведомление об успешном выполнении
                        MessageBox.Show("Статус успешно изменен в базе данных!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при изменении статуса детали: {ex.Message}");
            }
            LoadFirstTabData();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            LoadFirstTabData();
        }

        private void LoadFirstTabData()
        {
            string query = "SELECT * FROM parts_table;"; // Укажите название первой таблицы

            DataTable dataTable = GetData(query);
            dataGridView1.DataSource = dataTable;
        }
        private DataTable GetData(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

        private void EngineerForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
