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
            string mySelect = "SELECT ROOMS.ID, ROOM_TYPES.ROOM_TYPE, ROOM_STATUSES.ROOM_STATUS FROM(( ROOMS " +
                "INNER JOIN ROOM_TYPES ON ROOMS.ROOM_TYPE = ROOM_TYPES.ID) " +
                "INNER JOIN ROOM_STATUSES ON ROOMS.ROOM_STATUS = ROOM_STATUSES.ID)" +
                "ORDER BY ROOM_STATUSES.ROOM_STATUS DESC";

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
