using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SystemFinal
{
    public partial class FormGrades : Form
    {
        public FormGrades()
        {
            InitializeComponent();
            this.btnComputeGrades.Click += new System.EventHandler(this.btnComputeGrades_Click);
        }

        private void btnComputeGrades_Click(object sender, EventArgs e)
        {
            int studentId = int.TryParse(txtStudentId.Text, out int id) ? id : 0;
            var student = DataStore.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                MessageBox.Show("Student ID not found in records!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var grades = new List<decimal>();
            foreach (var gradeStr in txtGradesInput.Text.Split(','))
            {
                if (decimal.TryParse(gradeStr.Trim(), out decimal g))
                {
                    grades.Add(g);
                }
            }

            student.Grades = grades;
            decimal average = grades.Count > 0 ? grades.Average() : 0;

            MessageBox.Show($"Grades computed successfully! Average: {average:N2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}