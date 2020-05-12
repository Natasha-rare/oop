using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Формы_практика3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Нажми меня!";
        }
      
        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Нажми меня!")
            {
                button1.Text = "Вы нажа-а-а-а-ли меня!!!!";
            }
            else
            {
                this.Close();
            }
        }
    }
}
