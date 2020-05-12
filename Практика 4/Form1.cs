using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Практика_4
{
    public partial class Form1 : Form
    {
        public Random rnd = new Random();
        public int number, count;
        public double index;
        
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Введите число от 1 до 10 ";
            number = rnd.Next(1, 11);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = String.Format("Загаданное число: {0}", number);
            label2.Text = String.Format("Ваш коэффицент невезучести равен: {0:f1}", count / index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            if (int.Parse(textBox1.Text) == number)
            {
                label2.Text = "Вы угадали ^.^";
                count += 1;
                number = rnd.Next(1, 11);
            }
            else
            {
                label2.Text = "Вы не угадали ;( Попробуйте еще!";
            }
            index += 1;
            if (index % 10 == 0)
            {
                label3.Text = String.Format("Ваш коэффицент невезучести равен: {0:f1}", count / index);
            }
        }
    }
}
