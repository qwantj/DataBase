using System;
using System.Data;
using System.Windows.Forms;
using PartsManagementApp;

namespace DataBase
{
    public partial class TestForm : Form
    {
        private DatabaseManager _dbManager;

        public TestForm()
        {
            InitializeComponent();
            _dbManager = new DatabaseManager();
        }


        private void LoadParts()
        {
            string query = "SELECT * FROM parts_table;";
            var dataTable = _dbManager.ExecuteQuery(query);

            if (dataTable != null)
            {
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["name_part"].HeaderText = "Название";
                dataGridView1.Columns["part_number"].HeaderText = "Артикул";
                dataGridView1.Columns["status"].HeaderText = "Статус";
                dataGridView1.Columns["id_part"].Visible = false; // Скрываем ID, если нужно
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Пример запроса для проверки подключения
                string query = "SELECT * FROM service_table;";
                var dataTable = _dbManager.ExecuteQuery(query);

                if (dataTable != null)
                {
                    // Отображаем данные в DataGridView
                    dataGridView1.DataSource = dataTable;
                    MessageBox.Show("Подключение успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, на какую вкладку переключился пользователь
            if (tabControl1.SelectedTab == tabPage1) // Если вкладка "Детали"
            {
                // Загружаем данные для деталей
               LoadParts();
            }
            else if (tabControl1.SelectedTab == tabPage2) // Если вкладка "Сотрудники"
            {
                // Загружаем данные для сотрудников
              //  LoadWorkers();
            }
            else if (tabControl1.SelectedTab == tabPage3) // Если вкладка "Заказы"
            {
                // Загружаем данные для заказов
              //  LoadOrders();
            }
        }
    }
}
