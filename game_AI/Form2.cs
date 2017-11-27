using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace game_AI
{
    public partial class Form2 : Form
    {
        public int amount;
        public Form2()
        {
            InitializeComponent();
        }

        public void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int f = 0;
            amount = -1;
            if (e.KeyCode == Keys.Enter)
            {
                foreach (var element in textBox1.Text)
                    if (!Char.IsDigit(element)) f = 1;
                if (f != 1)
                {
                    amount = Convert.ToInt32(textBox1.Text);
                    Close();
                }
                else
                {
                    MessageBox.Show("Введите натуральное число");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            amount = -1;
            Close();
        }
    }
}
