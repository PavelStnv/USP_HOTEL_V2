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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                "ORDER BY ROOMS.ID";

            dbConnect.ConnectionString = ConnectionString;
            dbConnect.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(mySelect, dbConnect);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dbConnect.Close();
        }

        private void btnCreateReservation_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            switch(selectedRowCount)
            {
                case 0:
                    MessageBox.Show("Моля изберете помещение.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1:
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            string strRoomStatus = row.Cells[2].Value.ToString();

                            if(strRoomStatus == "Заето")
                            {
                                MessageBox.Show("Помещението е заето. Моля изберете свободно такова.", "Грешка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            DateTime today = DateTime.Now.Date;
                            DateTime from = dateTimePickerFrom.Value.Date;
                            DateTime to = dateTimePickerTo.Value.Date;

                            if(DateTime.Compare(today, from) > 0)
                            {
                                MessageBox.Show("Най-ранната дата за 'От' може да е днешната.", "Грешка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                            if (DateTime.Compare(from, to) > 0)
                            {
                                MessageBox.Show("Дата 'От' не може да е по-голяма от дата 'До'.", "Грешка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }

                            AddRecordInReservations(row.Cells[0].Value.ToString(), from, to);
                            MarkSelectedAsTaken(row.Cells[0].Value.ToString());
                            InsertData();
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Моля изберете само едно помещение.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        void AddRecordInReservations(string roomID, DateTime from, DateTime to)
        {
            string myInsert = "INSERT INTO RESERVATIONS VALUES('" + roomID + "', '" + from.ToString() +
                "', '" + to.ToString() + "')";

            dbConnect.ConnectionString = ConnectionString;

            OleDbCommand dbCommand = new OleDbCommand(myInsert, dbConnect);
            dbConnect.Open();

            dbCommand.CommandText = myInsert;
            dbCommand.Connection = dbConnect;
            dbCommand.ExecuteNonQuery();

            MessageBox.Show("Успешно добавяне на резервация.", "Успех",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            dbConnect.Close();
        }

        void MarkSelectedAsTaken(string roomID)
        {
            string myUpdate = "UPDATE ROOMS SET ROOM_STATUS = 1 WHERE ID = " + roomID;

            dbConnect.ConnectionString = ConnectionString;

            OleDbCommand dbCommand = new OleDbCommand(myUpdate, dbConnect);
            dbConnect.Open();

            dbCommand.CommandText = myUpdate;
            dbCommand.Connection = dbConnect;
            dbCommand.ExecuteNonQuery();

            dbConnect.Close();
        }

        private void UpdateRoomStatuses_Click(object sender, EventArgs e)
        {
            dbConnect.ConnectionString = ConnectionString;
            dbConnect.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string strRoomID = row.Cells[0].Value.ToString();

                string mySelect = "SELECT ROOM_ID, FROM_DATE, TO_DATE FROM RESERVATIONS"
                + " WHERE ROOM_ID = " + strRoomID
                + " ORDER BY TO_DATE DESC";


                OleDbDataAdapter adapter = new OleDbDataAdapter(mySelect, dbConnect);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow newrow in dt.Rows)
                {
                    DateTime reservationDate = Convert.ToDateTime(newrow[2].ToString());

                    // Резервацията е изтекла и стаята е за освобождаване
                    if(DateTime.Compare(DateTime.Now.Date, reservationDate) > 0)
                    {
                        string myUpdate = "UPDATE ROOMS SET ROOM_STATUS = 0 WHERE ID = " + strRoomID;

                        OleDbCommand dbCommand = new OleDbCommand(myUpdate, dbConnect);

                        dbCommand.CommandText = myUpdate;
                        dbCommand.Connection = dbConnect;
                        dbCommand.ExecuteNonQuery();
                    }
                    
                    break;
                }
            }

            dbConnect.Close();
            InsertData();

            MessageBox.Show("Успешно обновяване на статусите.", "Успех",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
