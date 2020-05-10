using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
                textBox2.Text = "Здравствуй, Мир!";
            else
                textBox2.Text = "Здравствуй, " + textBox1.Text + "!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
