using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_Club_App
{
    public partial class Request_AddClie : Form
    {

        private DataBase db = new DataBase();
        private string sql = null;

        private string sql_cli = null;

        public Request_AddClie()
        {
            InitializeComponent();
        }

        private void Request_AddClie_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(204, 78, 78);
            button1.BackColor = Color.FromArgb(69, 168, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;


            LoadComputersFromDatabase();

            textBox1.KeyPress += textBox1_KeyPress;

            textBox8.KeyPress += textBox8_KeyPress;
            textBox9.KeyPress += textBox9_KeyPress;
            textBox10.KeyPress += textBox10_KeyPress;

            maskedTextBox1.KeyPress += maskedTextBox1_KeyPress;
            maskedTextBox2.KeyPress += maskedTextBox2_KeyPress;
        }

        public bool ValidateInput()
        {
            if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("Номер заявки должен быть числом.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            if (!DateTime.TryParse(maskedTextBox2.Text, out _))
            {
                MessageBox.Show("Неверный формат даты.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            if (!int.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Номер тарифа должен быть числом.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            if (!decimal.TryParse(textBox6.Text, out _))
            {
                MessageBox.Show("Стоимость тарифа должна быть числом.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            if (!int.TryParse(comboBox2.Text, out _))
            {
                MessageBox.Show("Номер компьютера должен быть числом.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            if (!int.TryParse(textBox8.Text, out _))
            {
                MessageBox.Show("Количество часов должно быть числом.", "Ошибка", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }


        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void LoadComputersFromDatabase()
        {
            string query = "SELECT Номер_Компьютера FROM Компьютеры";

            using (SqlCommand command = new SqlCommand(query, db.getConnection()))
            {
                db.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                comboBox2.Items.Clear(); // Очищаем текущие элементы

                while (reader.Read())
                {
                    int номерКомпьютера = reader.GetInt32(0);

                    comboBox2.Items.Add(номерКомпьютера); // Добавляем номер тарифа в comboBox
                }

                db.closeConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            sql = "Insert into Заявки (Номер_Заявки, Статус_Заявки, Дата_Заявки, Номер_Тарифа, Тариф, Стоимость_Тарифа, Номер_Компьютера, Количество_Часов, Имя, Фамилия, Номер_Телефона, Номер_Пользователя) " +
                 "values (@Номер_Заявки, @Статус_Заявки, @Дата_Заявки, @Номер_Тарифа, @Тариф, @Стоимость_Тарифа, @Номер_Компьютера, @Количество_Часов, @Имя, @Фамилия, @Номер_Телефона, @Номер_Пользователя)";

            int номерЗаявки = int.Parse(textBox1.Text);
            DateTime датаЗаявки = DateTime.Parse(maskedTextBox2.Text);
            int номерТарифа = int.Parse(textBox2.Text);
            string тариф = textBox5.Text;
            decimal стоимостьТарифа = decimal.Parse(textBox6.Text);
            int номерКомпьютера = int.Parse(comboBox2.Text);
            int количествоЧасов = int.Parse(textBox8.Text);
            string имя = textBox9.Text;
            string фамилия = textBox10.Text;
            string номерТелефона = maskedTextBox1.Text;

            using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
            {
                command.Parameters.AddWithValue("@Номер_Заявки", номерЗаявки);
                command.Parameters.AddWithValue("@Статус_Заявки", "Создана");
                command.Parameters.AddWithValue("@Дата_Заявки", датаЗаявки);
                command.Parameters.AddWithValue("@Номер_Тарифа", номерТарифа);
                command.Parameters.AddWithValue("@Тариф", тариф);
                command.Parameters.AddWithValue("@Стоимость_Тарифа", стоимостьТарифа);
                command.Parameters.AddWithValue("@Номер_Компьютера", номерКомпьютера);
                command.Parameters.AddWithValue("@Количество_Часов", количествоЧасов);
                command.Parameters.AddWithValue("@Имя", имя);
                command.Parameters.AddWithValue("@Фамилия", фамилия);
                command.Parameters.AddWithValue("@Номер_Телефона", номерТелефона);
                command.Parameters.AddWithValue("@Номер_Пользователя", 1);

                db.openConnection();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще?", "Добавление данных", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        var req_form = new Requests();
                        req_form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось добавить данные.", "Ошибка");
                }

                db.closeConnection();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "1")
            {
                textBox5.Text = "Стандратный";
                textBox6.Text = "50";
            }


            if (textBox2.Text == "2")
            {
                textBox5.Text = "Вип";
                textBox6.Text = "100";
            }


            if (textBox2.Text == "3")
            {
                textBox5.Text = "Премиум";
                textBox6.Text = "200";
            }

            if (textBox2.Text != "1" && textBox2.Text != "2" && textBox2.Text != "3")
            {
                MessageBox.Show("Ошибка! Тариф не найден.");
            }
        }
    }
}