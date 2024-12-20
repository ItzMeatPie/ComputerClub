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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_Club_App
{
    public partial class Tariffs_Add : Form
    {

        string connectionString = null;
        string sql = null;

        public Tariffs_Add()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            connectionString = "Data Source=LAPTOP-04Q21HO6;Initial Catalog=Орлов_Д_419-4_УП;Integrated Security=True";

            sql = "Insert into Тарифы (Номер_Тарифа, Тариф, Стоимость) " +
                 "values (@Номер_Тарифа, @Тариф, @Стоимость)";

            int номерТарифа = int.Parse(textBox1.Text);
            string тариф = textBox2.Text;
            decimal стоимость = decimal.Parse(textBox3.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Добавляем параметры в запрос
                    command.Parameters.AddWithValue("@Номер_Тарифа", номерТарифа);
                    command.Parameters.AddWithValue("@Тариф", тариф);
                    command.Parameters.AddWithValue("@Стоимость", стоимость);

                    // Открываем соединение и выполняем запрос
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Проверяем, успешно ли выполнена вставка
                    if (rowsAffected > 0)
                    {
                        DialogResult result = MessageBox.Show("Данные успешно добавлены. Хотите добавить еще? \nСосал?", "Добавление данных", MessageBoxButtons.YesNo);

                        if (result == DialogResult.No)
                        {
                            var tarrifs_form = new Tariffs();
                            tarrifs_form.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить данные.");
                    }
                }
            }
        }
    }
}
