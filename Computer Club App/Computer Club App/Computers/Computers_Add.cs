using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Computer_Club_App
{
    public partial class Computers_Add : Form
    {
        private DataBase db = new DataBase();
        private string sql = null;

        public Computers_Add()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Computers_Add_Load(object sender, EventArgs e)
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
            maskedTextBox1.Mask = "00 GB"; // Разрешаем ввод только целых чисел
            maskedTextBox1.PromptChar = ' ';

            // Настройка MaskedTextBox для места на диске
            maskedTextBox2.Mask = "00 TB"; // Разрешаем ввод только целых чисел
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
            // Проверка введенных данных
            if (!int.TryParse(textBox1.Text, out int номерКомпьютера))
            {
                MessageBox.Show("Номер компьютера должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка маскированного ввода для объема оперативной памяти
            if (!maskedTextBox1.MaskCompleted)
            {
                MessageBox.Show("Пожалуйста, заполните объем оперативной памяти в формате 'XX GB'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка маскированного ввода для места на диске
            if (!maskedTextBox2.MaskCompleted)
            {
                MessageBox.Show("Пожалуйста, заполните место на диске в формате 'XX TB'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Извлечение числовых значений из маскированных полей
            string объемОперативнойПамяти = maskedTextBox1.Text.Replace(" GB", " GB");
            string местоНаДиске = maskedTextBox2.Text.Replace(" TB", " TB");

            // Удаление нуля в начале, если он есть
            if (объемОперативнойПамяти.StartsWith("0"))
            {
                объемОперативнойПамяти = объемОперативнойПамяти.TrimStart('0');
            }

            if (местоНаДиске.StartsWith("0"))
            {
                местоНаДиске = местоНаДиске.TrimStart('0');
            }

            sql = "Insert into Компьютеры (Номер_Компьютера, Номер_Тарифа, Тариф, Номер_Процессора, Процессор, " +
                  "Номер_Видеокарты, Видеокарта, Количество_Оперативной_Памяти, Место_На_Диске) " +
                  "values (@Номер_Компьютера, @Номер_Тарифа, @Тариф, @Номер_Процессора, @Процессор," +
                  "@Номер_Видеокарты, @Видеокарта, @Количество_Оперативной_Памяти, @Место_На_Диске)";

            using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
            {
                // Добавляем параметры в запрос
                command.Parameters.AddWithValue("@Номер_Компьютера", номерКомпьютера);
                command.Parameters.AddWithValue("@Номер_Тарифа", comboBox1.SelectedValue);
                command.Parameters.AddWithValue("@Тариф", textBox3.Text);
                command.Parameters.AddWithValue("@Номер_Процессора", comboBox2.SelectedValue);
                command.Parameters.AddWithValue("@Процессор", textBox5.Text);
                command.Parameters.AddWithValue("@Номер_Видеокарты", comboBox3.SelectedValue);
                command.Parameters.AddWithValue("@Видеокарта", textBox7.Text);
                command.Parameters.AddWithValue("@Количество_Оперативной_Памяти", объемОперативнойПамяти);
                command.Parameters.AddWithValue("@Место_На_Диске", местоНаДиске);

                // Открываем соединение и выполняем запрос
                db.openConnection();
                int rowsAffected = command.ExecuteNonQuery();

                // Проверяем, успешно ли выполнена вставка
                if (rowsAffected > 0)
                {
                    DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще?", "Добавление данных", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        var comp_form = new ComputersForm();
                        comp_form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить данные.");
                }

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