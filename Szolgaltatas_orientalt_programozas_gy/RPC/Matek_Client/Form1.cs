using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matek_Client
{
    public partial class Form1 : Form
    {
        //ismerni kell a DLL-t, jobbegér, Add, Reference => Browse => Matek_Service.DLL
        Matek_Service.IMatek client;

        public Form1()
        {
            InitializeComponent();
            client = (Matek_Service.IMatek) Activator.GetObject(typeof(Matek_Service.IMatek), "tcp://127.0.0.1:8888/Matek");
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            int res = client.Add(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            label1.Text = res.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int res = client.Sub(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            label1.Text = res.ToString();
        }
    }
}
