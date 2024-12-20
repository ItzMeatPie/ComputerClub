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
    public partial class Main : Form
    {

        private DataBase db = new DataBase();

        public Main()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = Color.FromArgb(204, 78, 78);

            button1.BackColor = Color.FromArgb(69, 168, 181);
            button2.BackColor = Color.FromArgb(69, 168, 181);
            button3.BackColor = Color.FromArgb(69, 168, 181);
            button4.BackColor = Color.FromArgb(69, 168, 181);
            button5.BackColor = Color.FromArgb(69, 168, 181);
            button6.BackColor = Color.FromArgb(69, 168, 181);
            button7.BackColor = Color.FromArgb(69, 168, 181);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button2.FlatAppearance.BorderSize = 1;
            button2.ForeColor = Color.White;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button3.FlatAppearance.BorderSize = 1;
            button3.ForeColor = Color.White;

            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button4.FlatAppearance.BorderSize = 1;
            button4.ForeColor = Color.White;

            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button5.FlatAppearance.BorderSize = 1;
            button5.ForeColor = Color.White;

            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button6.FlatAppearance.BorderSize = 1;
            button6.ForeColor = Color.White;

            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderColor = Color.FromArgb(69, 168, 181);
            button7.FlatAppearance.BorderSize = 1;
            button7.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var help_form = new Help();
            help_form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var request_form = new Requests();
            request_form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var comp_form = new ComputersForm();
            comp_form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tariffs_form = new TariffsForm();
            tariffs_form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var CPUs_form = new CPUsForm();
            CPUs_form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var GPUs_form = new GPUsForm();
            GPUs_form.Show();
            this.Hide();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var log_form = new Login();
            log_form.Show();
            this.Hide();
        }
    }
}