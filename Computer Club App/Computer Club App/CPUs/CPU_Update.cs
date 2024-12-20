using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_Club_App.CPUs
{
    public partial class CPU_Update : Form
    {
        private DataBase db = new DataBase();

        public CPU_Update()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Инициализация обработчиков событий
        }

        private void CPU_Update_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(204, 78, 78);
            button1.BackColor = Color.FromArgb(69, 168, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            comboBox1.Items.Add("Intel Core i5 9600k");
            comboBox1.Items.Add("Intel Core i7 9700k");
            comboBox1.Items.Add("Intel Core i9 9900k");

            comboBox1.Items.Add("Intel Core i5 10600k");
            comboBox1.Items.Add("Intel Core i7 10700k");
            comboBox1.Items.Add("Intel Core i9 10900k");

            comboBox1.Items.Add("Intel Core i5 11600k");
            comboBox1.Items.Add("Intel Core i7 11700k");
            comboBox1.Items.Add("Intel Core i9 11900k");

            comboBox1.Items.Add("Intel Core i5 12600k");
            comboBox1.Items.Add("Intel Core i7 12700k");
            comboBox1.Items.Add("Intel Core i9 12900k");

            comboBox1.Items.Add("Intel Core i5 13600k");
            comboBox1.Items.Add("Intel Core i7 13700k");
            comboBox1.Items.Add("Intel Core i9 13900k");

            comboBox1.Items.Add("Intel Core i5 14600k");
            comboBox1.Items.Add("Intel Core i7 14700k");
            comboBox1.Items.Add("Intel Core i9 14900k");
        }

        private void InitializeEventHandlers()
        {
            // Подключение обработчиков событий для ограничения ввода
            textBox1.KeyPress += textBox1_KeyPress;
            comboBox1.KeyPress += comboBox1_KeyPress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустоту полей
            if (!ValidateInput())
            {
                return;
            }

            // Проверка, что поле с номером процессора не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер процессора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера процессора
            int номерПроцессора;
            if (!int.TryParse(textBox1.Text, out номерПроцессора))
            {
                MessageBox.Show("Номер процессора должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на обновление
            string sql = "UPDATE Процессоры SET ";
            bool hasUpdates = false;

            // Проверяем, заполнены ли поля, и добавляем их в запрос
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                sql += "Процессор = @Процессор, ";
                hasUpdates = true;
            }

            // Убираем последнюю запятую
            if (hasUpdates)
            {
                sql = sql.TrimEnd(',', ' ');
            }
            else
            {
                MessageBox.Show("Не указаны данные для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Добавляем условие WHERE
            sql += " WHERE Номер_Процессора = @Номер_Процессора";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Номер_Процессора", номерПроцессора);

                    if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                        command.Parameters.AddWithValue("@Процессор", comboBox1.Text);

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем DataGridView на форме CPUsForm
                        CPUsForm cpuForm = Application.OpenForms["CPUsForm"] as CPUsForm;
                        if (cpuForm != null)
                        {
                            cpuForm.LoadData(); // Метод для обновления DataGridView
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Запись с таким номером не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрываем соединение
                db.closeConnection();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и клавишу Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только буквы, пробелы и клавишу Backspace
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер процессора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите название процессора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}