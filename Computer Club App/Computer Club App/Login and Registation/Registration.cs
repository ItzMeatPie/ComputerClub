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
using BCrypt.Net;

namespace Computer_Club_App
{
    public partial class Registration : Form
    {

        DataBase db = new DataBase();

        public Registration()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Registration_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);


            button1.BackColor = Color.FromArgb(69, 168, 181);
            button2.BackColor = Color.FromArgb(69, 168, 181);


            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button2.FlatAppearance.BorderSize = 1;
            button2.ForeColor = Color.White;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string role = "клиент"; // По умолчанию роль "клиент"

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RegisterUser(login, password, role);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var log_form = new Login();
            log_form.Show();
            this.Hide();
        }

        private void Registration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Закрыть приложение", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RegisterUser(string login, string password, string role)
        {

            DataBase db = new DataBase();

            // Хешируем пароль
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            string sql = "INSERT INTO Пользователи (Логин, Пароль, Роль) VALUES (@Логин, @Пароль, @Роль)";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    command.Parameters.AddWithValue("@Логин", login);
                    command.Parameters.AddWithValue("@Пароль", hashedPassword);
                    command.Parameters.AddWithValue("@Роль", role);

                    db.openConnection();
                    command.ExecuteNonQuery();
                    db.closeConnection();

                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
