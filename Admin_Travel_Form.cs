using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    public partial class Admin_Travel_Form : Form
    {
        public Admin_Travel_Form()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(Program.connParam);
        OleDbCommand cmd = new OleDbCommand();

        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'logInDataDataSet1.TravelInfo' table. You can move, or remove it, as needed.
            this.travelInfoTableAdapter.Fill(this.logInDataDataSet1.TravelInfo);

        }
        #region Queries Code
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var choice = MessageBox.Show("Are you want to Update a Record?", "Warning", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE TravelInfo SET Status='" + this.status.Text + "' WHERE TravelID =" + this.travelID.Text + "";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record Successfully Updated!", "Message");

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Cancelled!", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured!");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var choice = MessageBox.Show("Are you want to Delete a Record?", "Warning", MessageBoxButtons.YesNo);
                if (choice == DialogResult.Yes)
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM TravelInfo WHERE TravelID=" + this.travelID.Text + "";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record Successfully Deleted!", "Message");

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Cancelled!", "Message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured!", "Message");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(Program.connParam);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT TravelID, PlaceOfOrigin, Destination, Purpose, ArrivalDate, ExitingDate, Mode, UserID, Status FROM TravelInfo WHERE TravelID LIKE '%" + search.Text + "%'", con);
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            travelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            pOrigin.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            destination.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            purpose.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            arrivalDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            exitingDate.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            mode.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            userID.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            status.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            travelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            pOrigin.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            destination.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            purpose.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            arrivalDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            exitingDate.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            mode.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            userID.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            status.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            OleDbDataAdapter dAdapter = new OleDbDataAdapter("SELECT TravelID, PlaceOfOrigin, Destination, Purpose, ArrivalDate, ExitingDate, Mode, UserID, Status FROM TravelInfo", Program.connParam);
            OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();

            dAdapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dataTable.Rows[i][0], dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4], dataTable.Rows[i][5], dataTable.Rows[i][6], dataTable.Rows[i][7], dataTable.Rows[i][8]);
            }
        }
        #endregion

        #region Form Controls
        private void BtnClose_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Do you want to log out?", "Log out", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                LogInForm lForm = new LogInForm();
                this.Hide();
                lForm.Show();
            }
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            new Admin_Personal_Form().Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            new ReportTravelForm().Show();
            this.Hide();
        }
        #endregion


    }
}
