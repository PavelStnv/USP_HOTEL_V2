using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace USP_HOTEL_V2
{
    public partial class Iliqn_s_Form : Form
    {
        

        public Iliqn_s_Form()
        {
            InitializeComponent();
        }

        private void Iliqn_s_Form_Load(object sender, EventArgs e)
        {
        }

        private DataTable getRSRVList()
        {
            DataTable dtRSRV = new DataTable();
            string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + "\\DB_Tema17.mdb";
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM RESERVATIONS", con))
                {
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    dtRSRV.Load(reader);
                }
            }
                return dtRSRV;
        }

        private DataTable getRSRVMonth(string mnth)
        {
            DataTable dtRSRV = new DataTable();
            string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + "\\DB_Tema17.mdb";
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM RESERVATIONS WHERE MONTH(FROM_DATE)="+mnth, con))
                {
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    dtRSRV.Load(reader);
                }
            }
            return dtRSRV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            dataGridView1.DataSource = getRSRVList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            string month = textBox1.Text;
            dataGridView1.DataSource = getRSRVMonth(month);
        }
    }
}
