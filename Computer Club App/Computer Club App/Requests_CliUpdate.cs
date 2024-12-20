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

namespace Computer_Club_App
{
    public partial class Requests_CliUpdate : Form
    {

        private DataBase db = new DataBase();

        public Requests_CliUpdate()
        {
            InitializeComponent();
        }

        private void Requests_CliUpdate_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Создана");
            comboBox1.Items.Add("Завершена");

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);

            button1.BackColor = Color.FromArgb(69, 168, 181);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            LoadComputersFromDatabase();

            textBox1.KeyPress += textBox1_KeyPress;
            textBox8.KeyPress += textBox8_KeyPress;

            textBox9.KeyPress += textBox9_KeyPress;
            textBox10.KeyPress += textBox10_KeyPress;

            maskedTextBox1.KeyPress += maskedTextBox1_KeyPress;
            maskedTextBox2.KeyPress += maskedTextBox2_KeyPress;

            button1.Click += button1_Click;

            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
        }



        private bool ValidateInput()
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void LoadTariffsFromDatabase()
        {
            string query = "SELECT Номер_Тарифа FROM Тарифы";

            using (SqlCommand command = new SqlCommand(query, db.getConnection()))
            {
                db.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                comboBox1.Items.Clear(); // Очищаем текущие элементы

                while (reader.Read())
                {
                    int номерТарифа = reader.GetInt32(0);
                    comboBox1.Items.Add(номерТарифа); // Добавляем номер тарифа в comboBox
                }

                db.closeConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка, что поле с номером заявки не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер заявки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка корректности ввода
            if (!ValidateInput())
            {
                return;
            }

            // Получение номера заявки
            int номерЗаявки;
            if (!int.TryParse(textBox1.Text, out номерЗаявки))
            {
                MessageBox.Show("Номер заявки должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на обновление
            string sql = "UPDATE Заявки SET ";
            bool hasUpdates = false;

            // Проверяем, заполнены ли поля, и добавляем их в запрос
            if (!string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                sql += "Статус_Заявки = @Статус_Заявки, ";
                hasUpdates = true;
            }

            DateTime датаЗаявки;
            if (!string.IsNullOrWhiteSpace(maskedTextBox2.Text))
            {
                // Проверка корректности даты
                if (!DateTime.TryParseExact(maskedTextBox2.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out датаЗаявки))
                {
                    MessageBox.Show("Дата заявки введена некорректно. Используйте формат ДД.ММ.ГГГГ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                sql += "Дата_Заявки = @Дата_Заявки, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sql += "Номер_Тарифа = @Номер_Тарифа, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox5.Text))
            {
                sql += "Тариф = @Тариф, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox6.Text))
            {
                sql += "Стоимость_Тарифа = @Стоимость_Тарифа, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                sql += "Номер_Компьютера = @Номер_Компьютера, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox8.Text))
            {
                sql += "Количество_Часов = @Количество_Часов, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            {
                sql += "Имя = @Имя, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox10.Text))
            {
                sql += "Фамилия = @Фамилия, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                sql += "Номер_Телефона = @Номер_Телефона, ";
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
            sql += " WHERE Номер_Заявки = @Номер_Заявки";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Номер_Заявки", номерЗаявки);

                    if (!string.IsNullOrWhiteSpace(comboBox1.Text))
                        command.Parameters.AddWithValue("@Статус_Заявки", comboBox1.Text);

                    if (!string.IsNullOrWhiteSpace(maskedTextBox2.Text))
                        command.Parameters.AddWithValue("@Дата_Заявки", DateTime.Parse(maskedTextBox2.Text));

                    if (!string.IsNullOrWhiteSpace(textBox2.Text))
                        command.Parameters.AddWithValue("@Номер_Тарифа", int.Parse(textBox2.Text));

                    if (!string.IsNullOrWhiteSpace(textBox5.Text))
                        command.Parameters.AddWithValue("@Тариф", textBox5.Text);

                    if (!string.IsNullOrWhiteSpace(textBox6.Text))
                        command.Parameters.AddWithValue("@Стоимость_Тарифа", decimal.Parse(textBox6.Text));

                    if (!string.IsNullOrWhiteSpace(comboBox2.Text))
                        command.Parameters.AddWithValue("@Номер_Компьютера", int.Parse(comboBox2.Text));

                    if (!string.IsNullOrWhiteSpace(textBox8.Text))
                        command.Parameters.AddWithValue("@Количество_Часов", int.Parse(textBox8.Text));

                    if (!string.IsNullOrWhiteSpace(textBox9.Text))
                        command.Parameters.AddWithValue("@Имя", textBox9.Text);

                    if (!string.IsNullOrWhiteSpace(textBox10.Text))
                        command.Parameters.AddWithValue("@Фамилия", textBox10.Text);

                    if (!string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                        command.Parameters.AddWithValue("@Номер_Телефона", maskedTextBox1.Text);

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем DataGridView на форме Requests
                        Requests requestsForm = Application.OpenForms["Requests"] as Requests;
                        if (requestsForm != null)
                        {
                            requestsForm.LoadData(); // Метод для обновления DataGridView
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
