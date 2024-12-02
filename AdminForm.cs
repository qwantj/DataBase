using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataBase
{
    public partial class AdminForm : Form
    {
        private string _connectionString;
        

        public AdminForm()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            button5.Click += button5_Click;
            // Подписываемся на событие изменения выбранной вкладки
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
            // Загрузка данных для первоначально выбранной вкладки
            LoadDataForSelectedTab();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataForSelectedTab();
        }

        private void LoadDataForSelectedTab()
        {
            int selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex == 4)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
            switch (selectedIndex)
            {
                case 0:
                    LoadFirstTabData();
                    break;
                case 1:
                    LoadSecondTabData();
                    break;
                // Добавьте case для каждой вкладки
                case 2:
                    LoadThirdTabData();
                    break;
                case 3:
                    LoadFourthTabData();
                    break;
                case 4:
                    LoadFifthTabData();
                    break;
                default:
                    break;
            }
        }

        private void CustomizeDataGridView(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoGenerateColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Dock = DockStyle.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Tahoma", 12);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.ReadOnly = true;
        }

        private void LoadFirstTabData()
        {
            string query = "SELECT * FROM parts_table"; // Укажите название первой таблицы

            DataTable dataTable = GetData(query);
            dataGridView1.DataSource = dataTable;
        }

        private void LoadSecondTabData()
        {
            string query = "SELECT * FROM service_table"; // Укажите название второй таблицы

            DataTable dataTable = GetData(query);
            dataGridView1.DataSource = dataTable;
        }

        // Добавьте аналогичные методы для остальных вкладок
        private void LoadThirdTabData()
        {
            string query = "SELECT * FROM worker_table"; // Укажите название второй таблицы

            DataTable dataTable = GetData(query);
            dataGridView1.DataSource = dataTable;
        }

        private void LoadFourthTabData()
        {
            string query = "SELECT * FROM service_orders_table"; // Укажите название второй таблицы

            DataTable dataTable = GetData(query);
            dataGridView1.DataSource = dataTable;
        }
        private void LoadFifthTabData()
        {
            string query = "SELECT * FROM parts_with_details"; // Укажите название первой таблицы
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

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDataForSelectedTab();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;

            switch (selectedIndex)
            {
                case 0:
                    var newForm = new EditForm();
                    newForm.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 1:
                    var newForm1 = new EditForm1();
                    newForm1.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 2:
                    var newForm2 = new EditForm2();
                    newForm2.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 3:
                    var newForm3 = new EditForm3();
                    newForm3.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 4:
                    LoadDataForSelectedTab();
                    break;
                default:
                    break;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    var newForm = new AddForm();
                    newForm.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 1:
                    var newForm1 = new AddForm1();
                    newForm1.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 2:
                    var newForm2 = new AddForm2();
                    newForm2.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 3:
                    var newForm3 = new AddForm3();
                    newForm3.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 4:
                    LoadDataForSelectedTab();
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    var newForm = new DeleteForm();
                    newForm.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 1:
                    var newForm1 = new DeleteForm1();
                    newForm1.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 2:
                    var newForm2 = new DeleteForm2();
                    newForm2.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 3:
                    var newForm3 = new DeleteForm3();
                    newForm3.ShowDialog();
                    LoadDataForSelectedTab();
                    break;
                case 4:
                    LoadDataForSelectedTab();
                    break;
                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, заполните поле ID.");
                return;
            }
            int selectedIndex = tabControl1.SelectedIndex;
            int id = int.Parse(textBox1.Text); // ID детали
            switch (selectedIndex)
            {
                case 0:
                    //int id = int.Parse(textBox1.Text); // ID детали

                    try
                    {
                        string query = "SELECT * FROM parts_table WHERE id_part = @id;";
                        DataTable dataTable = new DataTable();

                        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                // Передача параметра в команду
                                cmd.Parameters.AddWithValue("@id", id);

                                // Использование адаптера для заполнения DataTable
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                        }

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Уведомление об успешной загрузке данных
                        MessageBox.Show("Данные успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                    break;

                case 1:
                    //int id = int.Parse(textBox1.Text); // ID детали

                    try
                    {
                        string query = "SELECT * FROM service_table WHERE id_service = @id;";
                        DataTable dataTable = new DataTable();

                        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                // Передача параметра в команду
                                cmd.Parameters.AddWithValue("@id", id);

                                // Использование адаптера для заполнения DataTable
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                        }

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Уведомление об успешной загрузке данных
                        MessageBox.Show("Данные успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                    break;

                case 2:
                    //int id = int.Parse(textBox1.Text); // ID детали

                    try
                    {
                        string query = "SELECT * FROM worker_table WHERE id_worker = @id;";
                        DataTable dataTable = new DataTable();

                        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                // Передача параметра в команду
                                cmd.Parameters.AddWithValue("@id", id);

                                // Использование адаптера для заполнения DataTable
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                        }

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Уведомление об успешной загрузке данных
                        MessageBox.Show("Данные успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                    break;

                case 3:
                    //int id = int.Parse(textBox1.Text); // ID детали

                    try
                    {
                        string query = "SELECT * FROM service_orders_table WHERE id_order = @id;";
                        DataTable dataTable = new DataTable();

                        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                // Передача параметра в команду
                                cmd.Parameters.AddWithValue("@id", id);

                                // Использование адаптера для заполнения DataTable
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                        }

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Уведомление об успешной загрузке данных
                        MessageBox.Show("Данные успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                    break;

                case 4:
                    //int id = int.Parse(textBox1.Text); // ID детали

                    try
                    {
                        string query = "SELECT * FROM parts_with_details WHERE id_part = @id;";
                        DataTable dataTable = new DataTable();

                        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                // Передача параметра в команду
                                cmd.Parameters.AddWithValue("@id", id);

                                // Использование адаптера для заполнения DataTable
                                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                                {
                                    adapter.Fill(dataTable);
                                }
                            }
                        }

                        // Привязка данных к DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Уведомление об успешной загрузке данных
                        MessageBox.Show("Данные успешно загружены!");
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                    break;

                default:
                    break;
            }
        }

    }
}
