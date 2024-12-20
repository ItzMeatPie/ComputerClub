using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using BCrypt.Net; // Добавляем директиву для BCrypt

namespace Computer_Club_App
{
    public partial class Login : Form
    {
        private DataBase _data = new DataBase();

        public Login()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);
            label1.ForeColor = Color.White;

            checkBox1.BackColor = Color.FromArgb(204, 78, 78);
            button1.BackColor = Color.FromArgb(69, 168, 181);
            button2.BackColor = Color.FromArgb(69, 168, 181);

            textBox2.UseSystemPasswordChar = true; // Пароль скрыт по умолчанию

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button2.FlatAppearance.BorderSize = 1;
            button2.ForeColor = Color.White;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Показываем или скрываем пароль в зависимости от состояния CheckBox
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем пользователя в базе данных
            if (AuthenticateUser(login, password))
            {
                // Определяем роль пользователя и открываем соответствующую форму
                string role = GetUserRole(login);
                if (role == "админ")
                {
                    MessageBox.Show("Добро пожаловать, администратор!", "Успех!");
                    var adminForm = new Main();
                    adminForm.Show();
                }
                else if (role == "клиент")
                {
                    MessageBox.Show("Добро пожаловать, клиент!", "Успех!");
                    var clientForm = new Main_Client();
                    clientForm.Show();
                }
                else
                {
                    MessageBox.Show("Неизвестная роль пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Переход на форму регистрации
            var reg_form = new Registration();
            reg_form.Show();
            this.Hide();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
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

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private bool AuthenticateUser(string login, string password)
        {
            string sql = "SELECT Пароль FROM Пользователи WHERE Логин = @Логин";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _data.getConnection()))
                {
                    command.Parameters.AddWithValue("@Логин", login);

                    _data.openConnection();
                    string hashedPassword = command.ExecuteScalar()?.ToString();
                    _data.closeConnection();

                    if (hashedPassword != null)
                    {
                        // Проверяем, является ли пароль хешированным
                        if (!hashedPassword.StartsWith("$2a$") && !hashedPassword.StartsWith("$2b$"))
                        {
                            // Если пароль не хеширован, хешируем его и обновляем в базе данных
                            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                            UpdatePasswordInDatabase(login, newHashedPassword);
                            return BCrypt.Net.BCrypt.Verify(password, newHashedPassword);
                        }

                        // Проверяем пароль с использованием BCrypt
                        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при аутентификации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void UpdatePasswordInDatabase(string login, string hashedPassword)
        {
            string sql = "UPDATE Пользователи SET Пароль = @Пароль WHERE Логин = @Логин";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _data.getConnection()))
                {
                    command.Parameters.AddWithValue("@Пароль", hashedPassword);
                    command.Parameters.AddWithValue("@Логин", login);

                    _data.openConnection();
                    command.ExecuteNonQuery();
                    _data.closeConnection();

                    MessageBox.Show("Пароль успешно обновлён в базе данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пароля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetUserRole(string login)
        {
            string sql = "SELECT Роль FROM Пользователи WHERE Логин = @Логин";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _data.getConnection()))
                {
                    command.Parameters.AddWithValue("@Логин", login);

                    _data.openConnection();
                    string role = command.ExecuteScalar()?.ToString();
                    _data.closeConnection();

                    return role;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении роли: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}