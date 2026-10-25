using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SystemFinal
{
    public partial class FormProcessing : Form
    {
        public FormProcessing()
        {
            InitializeComponent();
            this.btnRunParallel.Click += new System.EventHandler(this.btnRunParallel_Click);
        }

        private void btnRunParallel_Click(object sender, EventArgs e)
        {
            if (DataStore.Students.Count == 0)
            {
                MessageBox.Show("No students available to process. Please register students first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            var reports = ProcessingEngine.ProcessRecordsConcurrently(DataStore.Students);
            sw.Stop();

            DataStore.Reports = reports;

            MessageBox.Show($"Successfully processed {reports.Count} records in {sw.ElapsedMilliseconds} ms using Multithreading!", "Processing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}