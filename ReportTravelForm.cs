using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    public partial class ReportTravelForm : Form
    {
        public ReportTravelForm()
        {
            InitializeComponent();
        }

        private void Report2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'LogInDataDataSet1.TravelInfo' table. You can move, or remove it, as needed.
            this.TravelInfoTableAdapter.Fill(this.LogInDataDataSet1.TravelInfo);

            this.reportViewer1.RefreshReport();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show("Are you want to close the Program?", "Exit", MessageBoxButtons.YesNo);
            if (choice == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (choice == DialogResult.No)
            {
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            new Admin_Travel_Form().Show();
            this.Hide();
        }
    }
}
