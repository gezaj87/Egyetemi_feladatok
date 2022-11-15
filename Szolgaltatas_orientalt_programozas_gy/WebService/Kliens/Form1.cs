using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kliens
{
    public partial class Form1 : Form
    {
        ServiceReference1.WebService1SoapClient client;

        public Form1()
        {
            InitializeComponent();
            client = new ServiceReference1.WebService1SoapClient(); //most már ez a client objektum létezik
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = client.Add(int.Parse(textBox1.Text), int.Parse(textBox2.Text)).ToString();
        }
    }
}
