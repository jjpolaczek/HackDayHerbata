using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Herbata
{
    public partial class newTea : Form
    {
        public newTea()
        {
            InitializeComponent();
            read = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            try
            {
                temp = Double.Parse(textBox2.Text);
                time = Int32.Parse(textBox3.Text);
                if (time < 0 || temp > 100.0 || temp < 0.0)
                    throw new InvalidConstraintException("Invalid data");
                read = true;
            }
            catch
            {
                MessageBox.Show("Incorrect input value", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                read = false;
                return;
            }
            this.Close();
        }
        public string name;
        public double temp;
        public int time;
        public bool read;
    }
}
