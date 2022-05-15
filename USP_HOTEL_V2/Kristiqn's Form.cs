using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace USP_HOTEL_V2
{
    public partial class Kristiqn_s_Form : Form
    {
        string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + "\\DB_Tema17.mdb";
        OleDbConnection dbConnect = new OleDbConnection();


        public Kristiqn_s_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type = "";
            string status = "0";

            if (radioButton1.Checked == true)
            {
                type = "1";
            }

            if (radioButton2.Checked == true)
            {
                type = "0";
            }

            dbConnect.ConnectionString = ConnectionString;
            string myInsert = "INSERT INTO ROOMS(ROOM_TYPE, ROOM_STATUS) VALUES(" + type + "," + status + ")";

            OleDbCommand dbCommand = new OleDbCommand(myInsert, dbConnect);
            dbConnect.Open();

            dbCommand.CommandText = myInsert;
            dbCommand.Connection = dbConnect;
            dbCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно добавяне на запис.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dbConnect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id;
            string type = "";
            string status = "";
            int check;

            id = textBox1.Text;

            check = Int32.Parse(id);

            if (check >= 1)
            {

                if (radioButton3.Checked == true)
                {
                    type = "1";
                }

                if (radioButton4.Checked == true)
                {
                    type = "0";
                }

                if (radioButton5.Checked == true)
                {
                    status = "0";
                }

                if (radioButton6.Checked == true)
                {
                    status = "1";
                }


                dbConnect.ConnectionString = ConnectionString;
                string myInsert = "UPDATE ROOMS SET ROOM_TYPE = " + type + ", ROOM_STATUS = " + status + " WHERE ID = " + id + ";";

                OleDbCommand dbCommand = new OleDbCommand(myInsert, dbConnect);
                dbConnect.Open();

                dbCommand.CommandText = myInsert;
                dbCommand.Connection = dbConnect;
                dbCommand.ExecuteNonQuery();

                MessageBox.Show("Успешно актуализиране на запис.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dbConnect.Close();
            }
            else
            {
                MessageBox.Show("Невалидни данни.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
