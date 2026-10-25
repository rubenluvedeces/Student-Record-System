using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SystemFinal
{
    public partial class FormRegister : Form
    {
        private DataGridView dgvStudents;
        private BindingList<Student> studentBindingList;

        private Label lblFirstName, lblLastName, lblContact, lblDob, lblStudentId, lblGender, lblAddress;
        private TextBox txtFirstName, txtLastName, txtContact, txtStudentId, txtAddress;
        private DateTimePicker dtpDob;
        private RadioButton rbMale, rbFemale;
        private PictureBox picPhoto;
        private Button btnUploadPhoto, btnClear, btnDelete, btnUpdate, btnRegister;

        public FormRegister()
        {
            InitializeComponentCustom();
            LoadStudentData();
        }

        private void InitializeComponentCustom()
        {
            // Form Settings
            this.Text = "Student Registration Management";
            this.Width = 920;
            this.Height = 560;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 235, 235);

            // 1. Top Header Label / Title
            Label lblHeaderTitle = new Label();
            lblHeaderTitle.Text = "Student Registration & Management";
            lblHeaderTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblHeaderTitle.Location = new Point(20, 15);
            lblHeaderTitle.AutoSize = true;
            this.Controls.Add(lblHeaderTitle);

            // 2. DataGridView (Table at the top showing registered students)
            dgvStudents = new DataGridView();
            dgvStudents.Location = new Point(20, 50);
            dgvStudents.Size = new Size(865, 160);
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dgvStudents);

            // 3. Input Fields Section (Bottom part)
            // First Name
            lblFirstName = CreateLabel("First Name:", 20, 230);
            txtFirstName = CreateTextBox(95, 228, 140);

            // Last Name
            lblLastName = CreateLabel("Last Name:", 255, 230);
            txtLastName = CreateTextBox(325, 228, 160);

            // Contact Number
            lblContact = CreateLabel("Contact Number:", 510, 230);
            txtContact = CreateTextBox(615, 228, 270);

            // Date of Birth
            lblDob = CreateLabel("Date of Birth:", 20, 275);
            dtpDob = new DateTimePicker();
            dtpDob.Location = new Point(105, 273);
            dtpDob.Width = 130;
            dtpDob.Format = DateTimePickerFormat.Short;
            this.Controls.Add(dtpDob);

            // Student ID
            lblStudentId = CreateLabel("Student ID:", 255, 275);
            txtStudentId = CreateTextBox(325, 273, 100);

            // Gender
            lblGender = CreateLabel("Gender:", 450, 275);
            rbMale = new RadioButton();
            rbMale.Text = "Male";
            rbMale.Location = new Point(510, 274);
            rbMale.AutoSize = true;
            this.Controls.Add(rbMale);

            rbFemale = new RadioButton();
            rbFemale.Text = "Female";
            rbFemale.Location = new Point(570, 274);
            rbFemale.AutoSize = true;
            this.Controls.Add(rbFemale);

            // Photo Box & Upload Button
            picPhoto = new PictureBox();
            picPhoto.Location = new Point(20, 315);
            picPhoto.Size = new Size(95, 115);
            picPhoto.BackColor = Color.LightGray;
            picPhoto.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(picPhoto);

            btnUploadPhoto = new Button();
            btnUploadPhoto.Text = "Upload Photo";
            btnUploadPhoto.Location = new Point(20, 435);
            btnUploadPhoto.Size = new Size(95, 28);
            btnUploadPhoto.BackColor = Color.FromArgb(140, 195, 140);
            btnUploadPhoto.FlatStyle = FlatStyle.Flat;
            btnUploadPhoto.Click += BtnUploadPhoto_Click;
            this.Controls.Add(btnUploadPhoto);

            // Address (Multiline)
            lblAddress = CreateLabel("Address:", 135, 315);
            txtAddress = new TextBox();
            txtAddress.Location = new Point(195, 315);
            txtAddress.Size = new Size(480, 115);
            txtAddress.Multiline = true;
            this.Controls.Add(txtAddress);

            // Action Buttons (Clear, Delete, Update, Register)
            btnClear = CreateActionButton("Clear", 695, 315, Color.FromArgb(235, 215, 160));
            btnClear.Click += BtnClear_Click;

            btnDelete = CreateActionButton("Delete", 695, 435, Color.FromArgb(217, 83, 79));
            btnDelete.ForeColor = Color.White;
            btnDelete.Click += BtnDelete_Click;

            btnUpdate = CreateActionButton("Update", 775, 435, Color.FromArgb(130, 200, 100));
            btnUpdate.ForeColor = Color.White;

            btnRegister = CreateActionButton("Register", 695, 395, Color.FromArgb(92, 184, 92));
            btnRegister.Size = new Size(155, 32);
            btnRegister.ForeColor = Color.White;
            btnRegister.Click += BtnRegister_Click;
        }

        private Label CreateLabel(string text, int left, int top)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(left, top);
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox CreateTextBox(int left, int top, int width)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(left, top);
            txt.Width = width;
            this.Controls.Add(txt);
            return txt;
        }

        private Button CreateActionButton(string text, int left, int top, Color bgColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(left, top);
            btn.Size = new Size(75, 32);
            btn.BackColor = bgColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btn);
            return btn;
        }

        private void LoadStudentData()
        {
            studentBindingList = new BindingList<Student>(DataStore.Students);
            dgvStudents.DataSource = studentBindingList;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter both Student ID and First Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStudentId.Text.Trim(), out int studentId))
            {
                MessageBox.Show("Student ID must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check duplicate ID
            if (DataStore.Students.Exists(s => s.Id == studentId))
            {
                MessageBox.Show("This Student ID already exists in the records.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string gender = rbMale.Checked ? "Male" : (rbFemale.Checked ? "Female" : "Not Specified");

            // Add to shared DataStore list
            DataStore.Students.Add(new Student
            {
                Id = studentId,
                Name = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
                Grades = new System.Collections.Generic.List<decimal>(),
                DaysPresent = 0,
                TotalSchoolDays = 200
            });

            dgvStudents.DataSource = null;
            dgvStudents.DataSource = DataStore.Students;

            MessageBox.Show($"Successfully registered student {txtFirstName.Text}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtStudentId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            picPhoto.Image = null;
        }

        private void BtnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picPhoto.Image = Image.FromFile(ofd.FileName);
                    picPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var selectedStudent = dgvStudents.SelectedRows[0].DataBoundItem as Student;
                if (selectedStudent != null)
                {
                    DataStore.Students.Remove(selectedStudent);
                    dgvStudents.DataSource = null;
                    dgvStudents.DataSource = DataStore.Students;
                    MessageBox.Show("Student record deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}