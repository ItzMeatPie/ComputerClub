using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Computer_Club_App.GPUs
{
    public partial class GPU_Update : Form
    {
        private DataBase db = new DataBase();

        public GPU_Update()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Инициализация обработчиков событий
        }

        private void GPU_Update_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(204, 78, 78);
            button1.BackColor = Color.FromArgb(69, 168, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            comboBox1.Items.Add("RTX 2060");
            comboBox1.Items.Add("RTX 2060s");
            comboBox1.Items.Add("RTX 3060");
            comboBox1.Items.Add("RTX 3060ti");
            comboBox1.Items.Add("RTX 3070");
            comboBox1.Items.Add("RTX 3090ti");
            comboBox1.Items.Add("RTX 4060ti");
            comboBox1.Items.Add("RTX 4090");
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

            // Проверка, что поле с номером видеокарты не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер видеокарты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера видеокарты
            int номерВидеокарты;
            if (!int.TryParse(textBox1.Text, out номерВидеокарты))
            {
                MessageBox.Show("Номер видеокарты должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на обновление
            string sql = "UPDATE Видеокарты SET ";
            bool hasUpdates = false;

            // Проверяем, заполнены ли поля, и добавляем их в запрос
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                sql += "Видеокарта = @Видеокарта, ";
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
            sql += " WHERE Номер_Видеокарты = @Номер_Видеокарты";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Номер_Видеокарты", textBox1.Text);

                    if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                        command.Parameters.AddWithValue("@Видеокарта", comboBox1.Text);

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем DataGridView на форме GPUsForm
                        GPUsForm gpuForm = Application.OpenForms["GPUsForm"] as GPUsForm;
                        if (gpuForm != null)
                        {
                            gpuForm.LoadData(); // Метод для обновления DataGridView
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
                MessageBox.Show("Пожалуйста, введите номер видеокарты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите название видеокарты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}