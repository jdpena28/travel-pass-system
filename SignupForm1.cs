using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;


namespace LogInWindowsForm
{


    public partial class SignUpForm1 : Form
    {
        public SignUpForm1()
        {
            InitializeComponent();
        }

        #region Queries Code
        OleDbConnection con = new OleDbConnection(Program.connParam);
        OleDbCommand cmd = new OleDbCommand();

        public Random iD = new Random();


        private void SignUpForm1_Load(object sender, EventArgs e)
        {
            // this will give random userid that contain 5 digits random number with less prob of having the same id
            // since populated database are equal to 50
            Program.userID = iD.Next(1000, 9999);
            lblUserID.Text = Program.userID.ToString();
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxPass.Text == "" || textBoxConfirmPass.Text == "")
            {
                MessageBox.Show("Empty Fields, Please Fill-up the the Needed Records", "Error");
            }
            else
            {
                if (textBoxPass.Text != textBoxConfirmPass.Text)
                {
                    MessageBox.Show("Password does not match, Please Re-Enter", "Error");
                }
                else
                {
                    // check first the username and password if taken or already in the database 
                    string cmdText = "select Count(*) from LogIn where Username=? and User_Password=?";
                    using (OleDbCommand checkCmd = new OleDbCommand(cmdText, con))
                    {
                        con.Open();
                        checkCmd.Parameters.AddWithValue("@p1", textBoxUsername.Text);
                        checkCmd.Parameters.AddWithValue("@p2", textBoxPass.Text);
                        int result = (int)checkCmd.ExecuteScalar();
                        if (result > 0)
                        {
                            MessageBox.Show("Username and Password are Taken", "Error");
                            con.Close();
                        }
                        else
                        {
                            string signUpInsert = "INSERT INTO LogIn VALUES ('" + lblUserID.Text + "','" + textBoxUsername.Text + "','" +
                                textBoxPass.Text + "');";
                            cmd = new OleDbCommand(signUpInsert, con);
                            int temp = cmd.ExecuteNonQuery();
                            if (temp > 0)
                            {
                                SignUpForm2 signUp = new SignUpForm2();
                                signUp.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Error Occured", "Error");
                            }
                            con.Close();

                        }
                    }



                }

            }

        }
        #endregion

        #region form control
        private bool mouseDown;
        private Point lastLocation;

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogInForm f1 = new LogInForm();
            f1.Show();
            this.Hide();

        }

        private void btnMinimized_Click(object sender, EventArgs e)
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
        private void btnMinimized_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void btnMinimized_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void btnMinimized_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion
    }
}
