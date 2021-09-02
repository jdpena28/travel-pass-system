using QRCoder;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }
# region Queries Code
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            string userid = "";
            string getID = "SELECT UserID, Username from LogIn where Username = ? and User_Password=?";
            using (OleDbConnection con = new OleDbConnection(Program.connParam))
            using (OleDbCommand cmdGetID = new OleDbCommand(getID, con))
            {
                con.Open();
                cmdGetID.Parameters.AddWithValue("@p1", LogInForm.username);
                cmdGetID.Parameters.AddWithValue("@p2", LogInForm.password);
                OleDbDataReader read;
                read = cmdGetID.ExecuteReader();
                while (read.Read())
                {
                    lblName.Text = read["Username"].ToString();
                    userid = read["UserID"].ToString();
                }

            }
            /// this will print out if the status of user is for approval or approved
            string status = "SELECT Status FROM TravelInfo where UserID = @userid";
            using (OleDbConnection con = new OleDbConnection(Program.connParam))
            using (OleDbCommand cmdStats = new OleDbCommand(status, con))
            {
                con.Open();
                cmdStats.Parameters.AddWithValue("@userid", userid);
                OleDbDataReader read;
                read = cmdStats.ExecuteReader();
                while (read.Read())
                {
                    label4.Text = read["Status"].ToString();
                }

            }

            if (label4.Text.ToUpper() == "APPROVED")
            {
                /// using a open licenced and source code from github -QrGenerator Package
                /// this will give the userID a QR code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(userid, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                qrPictureBox.Image = qrCode.GetGraphic(6);
                qrPictureBox.Visible = true;
                panelQr.Visible = true;
                btnSave.Visible = true;
                panelNote.Visible = true;
            }

        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                // selectin of path to save the generated qrcode
                fbd.ShowDialog();
                string reqPath = fbd.SelectedPath;
                qrPictureBox.Image.Save(reqPath + @"\" + lblName.Text + "QR" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Saved", "QR CODE",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        #region form control
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to log out?", "Log out", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                LogInForm lForm = new LogInForm();
                this.Hide();
                lForm.Show();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to log out?", "Log out", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                LogInForm lForm = new LogInForm();
                this.Hide();
                lForm.Show();
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
        private void WelcomeForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void WelcomeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void WelcomeForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion


    }
}
