using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Computer_Club_App
{
    public partial class GPU_Add : Form
    {

        public TextBox TextBox1 => textBox1;
        public ComboBox ComboBox1 => comboBox1;

        public DataBase db = new DataBase();
        string sql = null;

        public GPU_Add()
        {
            InitializeComponent();
            InitializeEventHandlers(); // Инициализация обработчиков событий
        }

        private void GPU_Add_Load(object sender, EventArgs e)
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

        public void button1_Click(object sender, EventArgs e)
        {
            // Проверка на пустоту полей
            if (!ValidateInput())
            {
                return;
            }

            sql = "Insert into Видеокарты (Номер_Видеокарты, Видеокарта) " +
                  "values (@Номер_Видеокарты, @Видеокарта)";

            int номерВидеокарты = int.Parse(textBox1.Text);

            using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
            {
                // Добавляем параметры в запрос
                command.Parameters.AddWithValue("@Номер_Видеокарты", номерВидеокарты);
                command.Parameters.AddWithValue("@Видеокарта", comboBox1.Text);

                // Открываем соединение и выполняем запрос
                db.openConnection();
                int rowsAffected = command.ExecuteNonQuery();

                // Проверяем, успешно ли выполнена вставка
                if (rowsAffected > 0)
                {
                    DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще?", "Добавление данных", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        var gpu_form = new GPUsForm();
                        gpu_form.Show();
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