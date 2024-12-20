using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Club_App.Tariffs
{
    public partial class Tariffs_Update : Form
    {
        private DataBase db = new DataBase();

        public Tariffs_Update()
        {
            InitializeComponent();
        }

        private void Tariffs_Update_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка, что поле с номером компьютера не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер тарифа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера компьютера
            int номерТарифа;
            if (!int.TryParse(textBox1.Text, out номерТарифа))
            {
                MessageBox.Show("Номер тарифа должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на обновление
            string sql = "UPDATE Тарифы SET ";
            bool hasUpdates = false;

            // Проверяем, заполнены ли поля, и добавляем их в запрос
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sql += "Тариф = @Тариф, ";
                hasUpdates = true;
            }

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                sql += "Стоимость = @Стоимость, ";
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
            sql += " WHERE Номер_Тарифа = @Номер_Тарифа";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметры
                    command.Parameters.AddWithValue("@Номер_Тарифа", номерТарифа);

                    if (!string.IsNullOrWhiteSpace(textBox2.Text))
                        command.Parameters.AddWithValue("@Тариф", textBox2.Text);

                    if (!string.IsNullOrWhiteSpace(textBox3.Text))
                        command.Parameters.AddWithValue("@Стоимость", decimal.Parse(textBox3.Text));

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Обновляем DataGridView на форме ComputersForm
                        TariffsForm tariffsForm = Application.OpenForms["TariffsForm"] as TariffsForm;
                        if (tariffsForm != null)
                        {
                            tariffsForm.LoadData(); // Метод для обновления DataGridView
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только буквы, пробелы, клавишу Backspace и клавишу Delete
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры, точку, клавишу Backspace и клавишу Delete
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true; // Отменяем ввод символа
            }

            // Ограничиваем ввод только одной точкой
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true; // Отменяем ввод символа
            }
        }
    }
}