using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USP_HOTEL_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form oForm = new Kristiqn_s_Form();
            oForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form oForm = new Pavel_s_Form();
            oForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form oForm = new Iliqn_s_Form();
            oForm.Show();
        }
    }
}
