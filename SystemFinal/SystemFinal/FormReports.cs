using System;
using System.Windows.Forms;

namespace SystemFinal
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            dgvReports.DataSource = null;
            dgvReports.DataSource = DataStore.Reports;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvReports.DataSource = null;
            dgvReports.DataSource = DataStore.Reports;
        }
    }
}