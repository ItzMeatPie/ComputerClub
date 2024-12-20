using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Computer_Club_App
{
    public partial class Tariffs_Add : Form
    {
        public DataBase db = new DataBase();
        private string sql = null;

        public TextBox TextBox1 => textBox1;
        public TextBox TextBox2 => textBox2;
        public TextBox TextBox3 => textBox3;

        public Tariffs_Add()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Инициализация обработчиков событий
        }

        private void Tariffs_Add_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(204, 78, 78);
            button1.BackColor = Color.FromArgb(69, 168, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;
        }

        private void InitializeEventHandlers()
        {
            // Подключение обработчиков событий для ограничения ввода
            textBox1.KeyPress += textBox1_KeyPress;
            textBox2.KeyPress += textBox2_KeyPress;
            textBox3.KeyPress += textBox3_KeyPress;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустоту полей
            if (!ValidateInput())
            {
                return;
            }

            sql = "Insert into Тарифы (Номер_Тарифа, Тариф, Стоимость) " +
                  "values (@Номер_Тарифа, @Тариф, @Стоимость)";

            int номерТарифа = int.Parse(textBox1.Text);
            string тариф = textBox2.Text;
            decimal стоимость = decimal.Parse(textBox3.Text);

            using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
            {
                // Добавляем параметры в запрос
                command.Parameters.AddWithValue("@Номер_Тарифа", номерТарифа);
                command.Parameters.AddWithValue("@Тариф", тариф);
                command.Parameters.AddWithValue("@Стоимость", стоимость);

                // Открываем соединение и выполняем запрос
                db.openConnection();
                int rowsAffected = command.ExecuteNonQuery();

                // Проверяем, успешно ли выполнена вставка
                if (rowsAffected > 0)
                {
                    DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще?", "Добавление данных", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        var tarrifs_form = new TariffsForm();
                        tarrifs_form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить данные.");
                }

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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только буквы, пробелы и клавишу Backspace
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры, точку и клавишу Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true; // Отменяем ввод символа
            }

            // Разрешаем только одну точку
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        public bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер тарифа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, введите название тарифа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, введите стоимость тарифа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка, что стоимость является числом
            if (!decimal.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Стоимость тарифа должна быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}