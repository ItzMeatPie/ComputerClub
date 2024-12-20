using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_Club_App
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Help_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);

            button1.BackColor = Color.FromArgb(69, 168, 181);

            pictureBox1.BackColor = Color.FromArgb(68, 148, 194);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            label1.BackColor = Color.FromArgb(68, 148, 194);
            label1.ForeColor = Color.White;

            label1.Text = "На каждой форме с таблицами есть кнопки.\nИз их названия понятно, как они работают.\nПриложение может удалять, изменять, добавлять, выбирать данные из таблиц.\nТакже можно выгружать отчет в Excel файл.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var main_form = new Main();
            main_form.Show();
            this.Hide();
        }

        private void Help_KeyDown(object sender, KeyEventArgs e)
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

        private void Help_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
