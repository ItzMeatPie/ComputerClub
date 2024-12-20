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

namespace Computer_Club_App
{
    public partial class Requests_CliDelete : Form
    {

        private DataBase db = new DataBase();

        public Requests_CliDelete()
        {
            InitializeComponent();
        }

        private void Requests_CliDelete_Load(object sender, EventArgs e)
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
            // Проверка, что поле не пустое
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер заявки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера заявки
            int номерЗаявки;
            if (!int.TryParse(textBox1.Text, out номерЗаявки))
            {
                MessageBox.Show("Номер заявки должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на удаление
            string sql = "DELETE FROM Заявки WHERE Номер_Заявки = @Номер_Заявки";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметр
                    command.Parameters.AddWithValue("@Номер_Заявки", номерЗаявки);

                    // Открываем соединение
                    db.openConnection();

                    // Выполняем запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Запись с таким номером не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрываем соединение
                db.closeConnection();

                var req_from = new Requests();
                req_from.Show();
                this.Hide();
            }
        }
    }
}
