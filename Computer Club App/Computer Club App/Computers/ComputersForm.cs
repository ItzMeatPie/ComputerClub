﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Computer_Club_App.Computers;

namespace Computer_Club_App
{
    public partial class ComputersForm : Form
    {

        private DataBase db = new DataBase();

        public ComputersForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }


        private void Computers_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_Орлов_Д_419_4_УПDataSet.Компьютеры". При необходимости она может быть перемещена или удалена.
            this.компьютерыTableAdapter.Fill(this._Орлов_Д_419_4_УПDataSet.Компьютеры);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_Орлов_Д_419_4_УПDataSet.Компьютеры". При необходимости она может быть перемещена или удалена.
            this.компьютерыTableAdapter.Fill(this._Орлов_Д_419_4_УПDataSet.Компьютеры);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_Орлов_Д_419_4_УПDataSet.Компьютеры". При необходимости она может быть перемещена или удалена.
            this.компьютерыTableAdapter.Fill(this._Орлов_Д_419_4_УПDataSet.Компьютеры);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "_Орлов_Д_419_4_УПDataSet.Компьютеры". При необходимости она может быть перемещена или удалена.
            this.компьютерыTableAdapter.Fill(this._Орлов_Д_419_4_УПDataSet.Компьютеры);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;


            this.BackColor = Color.FromArgb(204, 78, 78);
            dataGridView1.BackgroundColor = Color.FromArgb(204, 78, 78);
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(68, 148, 194);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(68, 148, 194);
            dataGridView1.EnableHeadersVisualStyles = false;


            button1.BackColor = Color.FromArgb(69, 168, 181);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            button2.BackColor = Color.FromArgb(69, 168, 181);
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button2.FlatAppearance.BorderSize = 1;
            button2.ForeColor = Color.White;

            button3.BackColor = Color.FromArgb(69, 168, 181);
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button3.FlatAppearance.BorderSize = 1;
            button3.ForeColor = Color.White;

            button4.BackColor = Color.FromArgb(69, 168, 181);
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button4.FlatAppearance.BorderSize = 1;
            button4.ForeColor = Color.White;

            button5.BackColor = Color.FromArgb(69, 168, 181);
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button5.FlatAppearance.BorderSize = 1;
            button5.ForeColor = Color.White;

            button6.BackColor = Color.FromArgb(69, 168, 181);
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button6.FlatAppearance.BorderSize = 1;
            button6.ForeColor = Color.White;

            button7.BackColor = Color.FromArgb(69, 168, 181);
            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button7.FlatAppearance.BorderSize = 1;
            button7.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var main_form = new Main();
            main_form.Show();
            this.Hide();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            var comp_add = new Computers_Add();
            comp_add.Show();
            this.Hide();
        }

        private void Computers_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var comp_sel = new Computers_Select();
            comp_sel.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var comp_upd = new Computers_Update();
            comp_upd.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var comp_del = new Computers_Delete();
            comp_del.Show();
            this.Hide();
        }

        public void LoadData()
        {
            string sql = "SELECT * FROM Компьютеры";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        // Метод для обновления DataGridView
        public void UpdateDataGridView(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // SQL - запрос для выборки всех записей
            string sql = "SELECT * FROM Компьютеры";

            try
            {
                // Используем класс DataBase для управления соединением
                using (SqlCommand command = new SqlCommand(sql, db.getConnection()))
                {
                    // Создаем адаптер данных
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    // Создаем таблицу данных
                    DataTable dataTable = new DataTable();

                    // Заполняем таблицу данных
                    adapter.Fill(dataTable);

                    // Обновляем DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Закрываем соединение
                db.closeConnection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var help = new Help();
            help.Show();
            this.Hide();
        }
    }
}