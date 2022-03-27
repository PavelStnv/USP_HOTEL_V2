using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace USP_HOTEL_V2
{
    public partial class Pavel_s_Form : Form
    {
        string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + "\\DB_Tema17.mdb";
        OleDbConnection dbConnect = new OleDbConnection();

        public Pavel_s_Form()
        {
            InitializeComponent();
        }

        private void Pavel_s_Form_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            InsertData();

        }

        private void InsertData()
        {
            string mySelect = "SELECT * FROM ROOMS WHERE ROOM_STATUS = 1";

            dbConnect.ConnectionString = ConnectionString;
            dbConnect.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(mySelect, dbConnect);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dbConnect.Close();
        }
    }
}
