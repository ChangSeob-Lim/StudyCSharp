using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _51_EventWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("ClickEvent occurred");
        //    return;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Click += Button1Click;
            textBox1.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // CR = Enterkey
            {
                textBox2.Focus();
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            MessageBox.Show("ClickEvent occurred");
            return;
        }
    }
}
