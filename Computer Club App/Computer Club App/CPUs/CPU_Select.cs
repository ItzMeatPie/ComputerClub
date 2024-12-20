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

namespace Computer_Club_App.CPUs
{
    public partial class CPU_Select : Form
    {

        private DataBase db = new DataBase();

        public CPU_Select()
        {
            InitializeComponent();
        }

        private void CPU_Select_Load(object sender, EventArgs e)
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
                MessageBox.Show("Пожалуйста, введите номер процессора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получение номера заявки
            int номерПроцессора;
            if (!int.TryParse(textBox1.Text, out номерПроцессора))
            {
                MessageBox.Show("Номер процессора должен быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос на выборку
            string sql = "SELECT * FROM Процессоры WHERE Номер_Процессора = @Номер_Процессора";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Добавляем параметр
                    command.Parameters.AddWithValue("@Номер_Процессора", номерПроцессора);

                    // Создаем адаптер данных
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // Создаем таблицу данных
                    DataTable dataTable = new DataTable();

                    // Заполняем таблицу данных
                    adapter.Fill(dataTable);

                    // Проверяем, есть ли данные
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Запись с таким номером не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Проверяем, открыта ли форма ComputersForm
                        CPUsForm cpusForm = Application.OpenForms["CPUsForm"] as CPUsForm;
                        if (cpusForm == null)
                        {
                            // Если форма не открыта, открываем её
                            cpusForm = new CPUsForm();
                            cpusForm.Show();
                        }

                        // Обновляем DataGridView на форме ComputersForm
                        cpusForm.UpdateDataGridView(dataTable);

                        // Скрываем текущую форму
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
