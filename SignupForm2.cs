using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    public partial class SignUpForm2 : Form
    {
        public SignUpForm2()
        {
            InitializeComponent();
        }

        #region Queries Code
        OleDbConnection con = new OleDbConnection(Program.connParam);
        OleDbCommand cmd = new OleDbCommand();

        private void SignUpForm2_Load(object sender, EventArgs e)
        {
            /// this line of code use to get the userID generated in the first form
            /// to be set as foreign key for travel information table
            lblUserID.Text = Program.userID.ToString();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (txtBoxLName.Text == "" || txtBoxFName.Text == "" || txtBoxMName.Text == "" || txtBoxDestination.Text == ""
                 || txtBoxPlace.Text == "" || comboBoxMOT.Text == "" || comboBoxPurpose.Text == "" || comboBoxSex.Text == "")
            {
                MessageBox.Show("Please fill up the needed fields", "Error");
            }
            else
            {
                con.Open();
                string addRecord = "INSERT INTO PersonalInfo(LastName, FirstName,Middlename,ContactNumber,Email,Sex,DateOfBirth,UserID)" +
                     "VALUES (@LastName, @FirstName, @MiddleName," +
                     "@ContactNum,@Email,@Sex,@Bday,@UserID);";
                using (OleDbCommand insertPersonalInfo = new OleDbCommand(addRecord, con))
                {
                    insertPersonalInfo.Parameters.AddWithValue("@LastName", txtBoxLName.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@FirstName", txtBoxFName.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@MiddleName", txtBoxMName.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@ContactNum", txtBoxContactNum.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@Email", txtBoxEmail.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@Sex", comboBoxSex.Text);
                    insertPersonalInfo.Parameters.AddWithValue("@Bday", datePickerBDay.Value.ToShortDateString());
                    insertPersonalInfo.Parameters.AddWithValue("@UserID", lblUserID.Text);
                    int pResultt = insertPersonalInfo.ExecuteNonQuery();
                }

                string addToTravelInfo = "INSERT INTO TravelInfo(PlaceOfOrigin,Destination,Purpose,ArrivalDate,ExitingDate,Mode,UserID)" +
                "VALUES (@POO,@Destination,@Purpose,@ArrDate,@ExitDate,@Mode,@UserID);";
                using (OleDbCommand insertTravelInfo = new OleDbCommand(addToTravelInfo, con))
                {
                    insertTravelInfo.Parameters.AddWithValue("@POO", txtBoxPlace.Text);
                    insertTravelInfo.Parameters.AddWithValue("@Destination", txtBoxDestination.Text);
                    insertTravelInfo.Parameters.AddWithValue("@Purpose", comboBoxPurpose.Text);
                    insertTravelInfo.Parameters.AddWithValue("@ArrDate", datePickerArrival.Value.ToShortDateString());
                    insertTravelInfo.Parameters.AddWithValue("@ExitDate", datePickerExit.Value.ToShortDateString());
                    insertTravelInfo.Parameters.AddWithValue("@Mode", comboBoxMOT.Text);
                    insertTravelInfo.Parameters.AddWithValue("@UserID", lblUserID.Text);
                    int tResult = insertTravelInfo.ExecuteNonQuery();
                }
                MessageBox.Show("Records Added Sucessfully, Please Log-in to the Homepage", "Added Successfully");
            }
            con.Close();
            LogInForm f1 = new LogInForm();
            f1.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // textbox contact number accepts only 11 digit number
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Please enter 11 digits phone number only");
            }
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // disabling letters
        }
        #endregion

        #region form control
        private bool mouseDown;
        private Point lastLocation;

        private void txtBoxContactNum_Click(object sender, EventArgs e)
        {
            txtBoxContactNum.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogInForm f1 = new LogInForm();
            f1.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /// minimize the form

            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void SignUpForm2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void SignUpForm2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void SignUpForm2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion


    }
}
