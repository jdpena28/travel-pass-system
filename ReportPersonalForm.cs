using System;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    public partial class ReportPersonalForm : Form
    {
        public ReportPersonalForm()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'LogInDataDataSet2.PersonalInfo' table. You can move, or remove it, as needed.
            this.PersonalInfoTableAdapter.Fill(this.LogInDataDataSet2.PersonalInfo);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Do you want to go back?", "Report", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Admin_Personal_Form adminForm2 = new Admin_Personal_Form();
                this.Hide();
                adminForm2.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Admin_Personal_Form().Show();
            this.Hide();
        }
    }
}
