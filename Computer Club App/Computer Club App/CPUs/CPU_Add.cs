using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Computer_Club_App
{
    public partial class CPU_Add : Form
    {
        public DataBase db = new DataBase();
        private string sql = null;

        public TextBox TextBox1 => textBox1;
        public ComboBox ComboBox1 => comboBox1;

        public CPU_Add()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Инициализация обработчиков событий
        }

        private void CPU_Add_Load(object sender, EventArgs e)
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

        public void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустоту полей
            if (!ValidateInput())
            {
                return;
            }

            sql = "Insert into Процессоры (Номер_Процессора, Процессор) " +
                  "values (@Номер_Процессора, @Процессор)";

            int номерПроцессора = int.Parse(textBox1.Text);

            using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
            {
                // Добавляем параметры в запрос
                command.Parameters.AddWithValue("@Номер_Процессора", номерПроцессора);
                command.Parameters.AddWithValue("@Процессор", comboBox1.Text);

                // Открываем соединение и выполняем запрос
                db.openConnection();
                int rowsAffected = command.ExecuteNonQuery();

                // Проверяем, успешно ли выполнена вставка
                if (rowsAffected > 0)
                {
                    DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще?", "Добавление данных", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        var cpu_form = new CPUsForm();
                        cpu_form.Show();
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

        public bool ValidateInput()
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