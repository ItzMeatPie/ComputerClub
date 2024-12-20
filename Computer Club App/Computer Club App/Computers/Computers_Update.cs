using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Computer_Club_App.Computers
{
    public partial class Computers_Update : Form
    {
        private DataBase db = new DataBase();

        public Computers_Update()
        {
            InitializeComponent();
        }

        private void Computers_Update_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);

            button1.BackColor = Color.FromArgb(69, 168, 181);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            // Настройка MaskedTextBox для объема оперативной памяти
            maskedTextBox1.Mask = "00 GB";
            maskedTextBox1.PromptChar = ' ';

            // Настройка MaskedTextBox для места на диске
            maskedTextBox2.Mask = "00 TB";
            maskedTextBox2.PromptChar = ' ';

            // Заполняем ComboBox данными из базы данных
            FillComboBox(comboBox1, "Тарифы", "Номер_Тарифа", "Номер_Тарифа"); // Отображаем номер тарифа
            FillComboBox(comboBox2, "Процессоры", "Номер_Процессора", "Номер_Процессора"); // Отображаем номер процессора
            FillComboBox(comboBox3, "Видеокарты", "Номер_Видеокарты", "Номер_Видеокарты"); // Отображаем номер видеокарты

            // Подписываемся на события изменения выбора в ComboBox
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            // Инициализация текстовых полей как пустых
            ClearTextBoxes();

            textBox3.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox7.ReadOnly = true;
        }

        private void FillComboBox(ComboBox comboBox, string tableName, string valueMember, string displayMember)
        {
            try
            {
                using (SqlCommand command = new SqlCommand($"SELECT {valueMember}, {displayMember} FROM {tableName}", db.getConnection()))
                {
                    db.openConnection();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable table = new DataTable();
                    table.Load(reader);

                    comboBox.DataSource = table;
                    comboBox.ValueMember = valueMember;
                    comboBox.DisplayMember = displayMember;

                    db.closeConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Автоматическое заполнение текстовых полей на основе выбранных значений в ComboBox
            if (comboBox1.SelectedItem != null)
            {
                // Получаем имя тарифа по номеру тарифа
                string tariffName = GetTariffName((int)comboBox1.SelectedValue);
                textBox3.Text = tariffName; // Заполняем "Тариф"
            }
            else
            {
                textBox3.Clear(); // Очищаем поле "Тариф", если ничего не выбрано
            }

            if (comboBox2.SelectedItem != null)
            {
                // Получаем имя процессора по номеру процессора
                string processorName = GetProcessorName((int)comboBox2.SelectedValue);
                textBox5.Text = processorName; // Заполняем "Процессор"
            }
            else
            {
                textBox5.Clear(); // Очищаем поле "Процессор", если ничего не выбрано
            }

            if (comboBox3.SelectedItem != null)
            {
                // Получаем имя видеокарты по номеру видеокарты
                string videoCardName = GetVideoCardName((int)comboBox3.SelectedValue);
                textBox7.Text = videoCardName; // Заполняем "Видеокарта"
            }
            else
            {
                textBox7.Clear(); // Очищаем поле "Видеокарта", если ничего не выбрано
            }
        }

        private string GetTariffName(int tariffId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT Тариф FROM Тарифы WHERE Номер_Тарифа = @Номер_Тарифа", db.getConnection()))
                {
                    command.Parameters.AddWithValue("@Номер_Тарифа", tariffId);
                    db.openConnection();
                    object result = command.ExecuteScalar();
                    db.closeConnection();

                    return result != null ? result.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении имени тарифа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private string GetProcessorName(int processorId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT Процессор FROM Процессоры WHERE Номер_Процессора = @Номер_Процессора", db.getConnection()))
                {
                    command.Parameters.AddWithValue("@Номер_Процессора", processorId);
                    db.openConnection();
                    object result = command.ExecuteScalar();
                    db.closeConnection();

                    return result != null ? result.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении имени процессора: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private string GetVideoCardName(int videoCardId)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT Видеокарта FROM Видеокарты WHERE Номер_Видеокарты = @Номер_Видеокарты", db.getConnection()))
                {
                    command.Parameters.AddWithValue("@Номер_Видеокарты", videoCardId);
                    db.openConnection();
                    object result = command.ExecuteScalar();
                    db.closeConnection();

                    return result != null ? result.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении имени видеокарты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }


        private void ClearTextBoxes()
        {
            // Очищаем текстовые поля
            textBox3.Clear();
            textBox5.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка, что поле с номером компьютера не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер компьютера.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера компьютера
            int номерКомпьютера;
            if (!int.TryParse(textBox1.Text, out номерКомпьютера))
            {
                MessageBox.Show("Номер компьютера должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на обновление
            string sql = "UPDATE Компьютеры SET ";
            bool hasUpdates = false;

            // Проверяем, заполнены ли поля, и добавляем их в запрос
            if (comboBox1.SelectedValue != null)
            {
                sql += "Номер_Тарифа = @Номер_Тарифа, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                sql += "Тариф = @Тариф, ";
                hasUpdates = true;
            }

            if (comboBox2.SelectedValue != null)
            {
                sql += "Номер_Процессора = @Номер_Процессора, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox5.Text))
            {
                sql += "Процессор = @Процессор, ";
                hasUpdates = true;
            }

            if (comboBox3.SelectedValue != null)
            {
                sql += "Номер_Видеокарты = @Номер_Видеокарты, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox7.Text))
            {
                sql += "Видеокарта = @Видеокарта, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                sql += "Количество_Оперативной_Памяти = @Количество_Оперативной_Памяти, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                sql += "Место_На_Диске = @Место_На_Диске, ";
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

            // Извлечение числовых значений из маскированных полей
            string объемОперативнойПамяти = maskedTextBox1.Text.Replace(" GB", "");
            string местоНаДиске = maskedTextBox2.Text.Replace(" TB", "");

            // Удаление нуля в начале, если он есть
            if (объемОперативнойПамяти.StartsWith("0"))
            {
                объемОперативнойПамяти = объемОперативнойПамяти.TrimStart('0');
            }

            if (местоНаДиске.StartsWith("0"))
            {
                местоНаДиске = местоНаДиске.TrimStart('0');
            }

            // Добавляем условие WHERE
            sql += " WHERE Номер_Компьютера = @Номер_Компьютера";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Номер_Компьютера", номерКомпьютера);

                    if (comboBox1.SelectedValue != null)
                        command.Parameters.AddWithValue("@Номер_Тарифа", comboBox1.SelectedValue);

                    if (!string.IsNullOrWhiteSpace(textBox3.Text))
                        command.Parameters.AddWithValue("@Тариф", textBox3.Text);

                    if (comboBox2.SelectedValue != null)
                        command.Parameters.AddWithValue("@Номер_Процессора", comboBox2.SelectedValue);

                    if (!string.IsNullOrWhiteSpace(textBox5.Text))
                        command.Parameters.AddWithValue("@Процессор", textBox5.Text);

                    if (comboBox3.SelectedValue != null)
                        command.Parameters.AddWithValue("@Номер_Видеокарты", comboBox3.SelectedValue);

                    if (!string.IsNullOrWhiteSpace(textBox7.Text))
                        command.Parameters.AddWithValue("@Видеокарта", textBox7.Text);

                    if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                        command.Parameters.AddWithValue("@Количество_Оперативной_Памяти", объемОперативнойПамяти);

                    if (!string.IsNullOrWhiteSpace(maskedTextBox2.Text))
                        command.Parameters.AddWithValue("@Место_На_Диске", местоНаДиске);

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем DataGridView на форме ComputersForm
                        ComputersForm computersForm = Application.OpenForms["ComputersForm"] as ComputersForm;
                        if (computersForm != null)
                        {
                            computersForm.LoadData(); // Метод для обновления DataGridView
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
            // Разрешаем только цифры, клавишу Backspace и клавишу Delete
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }
    }
}