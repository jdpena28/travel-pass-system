using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace LogInWindowsForm
{

    //NOTE!:
    // Connection string or Connection parameter are declared in Global in the Program.cs or the Main method
    // Special thanks and credits also to zxing qrcode generator open source library :)
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        #region Queries Code

        public static string username { get; set; }
        public static string password { get; set; }


        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Empty Fields", "Error");
            }
            else if (textBoxUsername.Text == "admin" && textBoxPassword.Text == "admin")
            {
                Admin_Travel_Form adminForm = new Admin_Travel_Form();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                username = textBoxUsername.Text;
                password = textBoxPassword.Text;
                string cmdText = "select Count(*) from LogIn where Username=? and User_Password=?";
                using (OleDbConnection con = new OleDbConnection(Program.connParam))
                using (OleDbCommand cmd = new OleDbCommand(cmdText, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@p1", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@p2", textBoxPassword.Text);
                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        WelcomeForm wf = new WelcomeForm();
                        wf.Show();
                        this.Hide();
                    }

                    else
                    {
                        MessageBox.Show("Invalid Credentials, Please Re-Enter", "Error");
                    }

                    con.Close();
                }

            }

        }
        #endregion




        #region form control
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShow.Checked == true)
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }


        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm1 f2 = new SignUpForm1();
            this.Hide();
            f2.Show();

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("To access administrator form login Username = admin and Password = admin");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit the program? ", "Exit Application", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }

        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private bool mouseDown;
        private Point lastLocation;
        private void LogInForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void LogInForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void LogInForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion


    }
}
