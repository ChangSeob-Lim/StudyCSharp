using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _57_WinThreadExam
{
    public partial class Form1 : Form
    {
        const int MAXVALUE = 1_000_000;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(Progress));
            th.Start();
            th.Join();
        }

        void Progress()
        {
            PrgSample.Minimum = 0;
            PrgSample.Maximum = MAXVALUE;

            for (int i = 0; i <= MAXVALUE; i++)
            {
                PrgSample.Value = i;
            }
        }
    }
}
