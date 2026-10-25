using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SystemFinal
{
    public partial class Form1 : Form
    {
        private Panel pnlSidebar;
        private Label lblSchool;
        private Button btnRegister, btnGrades, btnAttendance, btnReports, btnProcessing, btnExit;

        private Panel pnlHeader;
        private Label lblTitle;

        private Panel pnlContent;
        private BindingList<Student> studentBindingList;

        public Form1()
        {
            InitializeComponentCustom();
            LoadStudentData();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "SystemFinal - Main Dashboard";
            this.Width = 980;
            this.Height = 580;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 235, 235);

            // 1. Sidebar Panel
            pnlSidebar = new Panel();
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 190;
            pnlSidebar.BackColor = Color.FromArgb(80, 20, 20);
            this.Controls.Add(pnlSidebar);

            lblSchool = new Label();
            lblSchool.Text = "Student Record\nWelcome back!";
            lblSchool.ForeColor = Color.White;
            lblSchool.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSchool.Location = new Point(15, 20);
            lblSchool.AutoSize = true;
            pnlSidebar.Controls.Add(lblSchool);

            btnRegister = CreateSidebarButton("Student Registration", 90, BtnRegister_Click);
            btnGrades = CreateSidebarButton("Grades & Attendance Computation", 150, BtnGradeComputation_Click); // Pwede mo ring paikliin dito para isang linya lang
            btnReports = CreateSidebarButton("Generate Reports", 210, BtnReports_Click);
            btnProcessing = CreateSidebarButton("Concurrent Processing", 270, BtnProcessing_Click);
            btnExit = CreateSidebarButton("Exit", 330, (s, e) => Application.Exit());

            // 2. Header Panel
            pnlHeader = new Panel();
            pnlHeader.Location = new Point(190, 0);
            pnlHeader.Size = new Size(774, 55);
            pnlHeader.BackColor = Color.FromArgb(110, 30, 30);
            this.Controls.Add(pnlHeader);

            lblTitle = new Label();
            lblTitle.Text = "Student Management";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;
            pnlHeader.Controls.Add(lblTitle);

            // 3. Content Panel
            pnlContent = new Panel();
            pnlContent.Location = new Point(195, 60);
            pnlContent.Size = new Size(765, 475);
            pnlContent.BackColor = Color.White;
            this.Controls.Add(pnlContent);

            pnlHeader.SendToBack();
            pnlSidebar.BringToFront();

            // Load Student Registration by default
            BtnRegister_Click(null, null);
        }

        // Bagong helper function para sa Sidebar Buttons na may active highlight
        private Button CreateSidebarButton(string text, int topPosition, EventHandler onClickAction)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(0, topPosition);
            btn.Size = new Size(pnlSidebar.Width, 45);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(80, 20, 20); // Normal dark red color
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);

            // Hover effect
            btn.MouseEnter += (s, e) => {
                if (btn.BackColor != Color.FromArgb(140, 30, 30)) // Kung hindi pa ito ang active button
                    btn.BackColor = Color.FromArgb(110, 25, 25);
            };
            btn.MouseLeave += (s, e) => {
                if (btn.BackColor != Color.FromArgb(140, 30, 30))
                    btn.BackColor = Color.FromArgb(80, 20, 20);
            };

            // Click action na magha-highlight lamang sa piniling button
            btn.Click += (s, e) => {
                // I-reset ang kulay ng LAHAT ng sidebar buttons pabalik sa default
                foreach (Control ctrl in pnlSidebar.Controls)
                {
                    if (ctrl is Button b && b.Text != "Exit") // Kung gustong i-exempt ang Exit button
                    {
                        b.BackColor = Color.FromArgb(80, 20, 20);
                    }
                }

                // I-highlight ang kasalukuyang pinindot
                btn.BackColor = Color.FromArgb(140, 30, 30); // Mas matingkad na pula para sa active state

                // Tawagin ang totoong function ng button (hal. Registration, Grades, atbp.)
                onClickAction?.Invoke(s, e);
            };

            pnlSidebar.Controls.Add(btn);
            return btn;
        }

        private void LoadStudentData()
        {
            studentBindingList = new BindingList<Student>(DataStore.Students);
        }

        // ==========================================
        // STUDENT REGISTRATION MODULE
        // ==========================================
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Student Registration & Management";
            pnlContent.Controls.Clear();

            int selectedStudentId = -1;

            // 1. Row 1 Inputs
            Label lblFN = new Label { Text = "First Name:", Location = new Point(30, 40), AutoSize = true };
            TextBox txtFN = new TextBox { Location = new Point(130, 38), Width = 180 };

            Label lblLN = new Label { Text = "Last Name:", Location = new Point(350, 40), AutoSize = true };
            TextBox txtLN = new TextBox { Location = new Point(450, 38), Width = 180 };

            pnlContent.Controls.AddRange(new Control[] { lblFN, txtFN, lblLN, txtLN });

            // 2. Row 2 Inputs
            Label lblContact = new Label { Text = "Contact Number:", Location = new Point(30, 90), AutoSize = true };
            TextBox txtContact = new TextBox { Location = new Point(130, 88), Width = 180 };

            Label lblDob = new Label { Text = "Date of Birth:", Location = new Point(350, 90), AutoSize = true };
            DateTimePicker dtpDob = new DateTimePicker { Location = new Point(450, 88), Width = 180, Format = DateTimePickerFormat.Short };

            pnlContent.Controls.AddRange(new Control[] { lblContact, txtContact, lblDob, dtpDob });

            // 3. Row 3 Inputs (ID & Gender)
            Label lblSid = new Label { Text = "Student ID:", Location = new Point(30, 140), AutoSize = true };
            TextBox txtSid = new TextBox { Location = new Point(130, 138), Width = 180 };

            Label lblGen = new Label { Text = "Gender:", Location = new Point(350, 140), AutoSize = true };
            RadioButton rbMale = new RadioButton { Text = "Male", Location = new Point(450, 140), AutoSize = true };
            RadioButton rbFemale = new RadioButton { Text = "Female", Location = new Point(520, 140), AutoSize = true };

            pnlContent.Controls.AddRange(new Control[] { lblSid, txtSid, lblGen, rbMale, rbFemale });

            // 4. Address Input
            Label lblAddress = new Label { Text = "Address:", Location = new Point(30, 190), AutoSize = true };
            TextBox txtAddress = new TextBox { Location = new Point(130, 188), Size = new Size(500, 55), Multiline = true };
            pnlContent.Controls.AddRange(new Control[] { lblAddress, txtAddress });

            // 5. Photo Box & Upload Button
            PictureBox picPhoto = new PictureBox { Location = new Point(650, 38), Size = new Size(95, 110), BackColor = Color.LightGray, BorderStyle = BorderStyle.FixedSingle };
            Button btnUpload = new Button { Text = "Upload Photo", Location = new Point(650, 155), Size = new Size(95, 28), BackColor = Color.FromArgb(140, 195, 140), FlatStyle = FlatStyle.Flat };
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.Click += (s, ev) => {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        picPhoto.Image = Image.FromFile(ofd.FileName);
                        picPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            };
            pnlContent.Controls.AddRange(new Control[] { picPhoto, btnUpload });

            // 6. Search Bar Section (Nakatago muna sa Y = 310)
            Label lblSearchFilter = new Label
            {
                Text = "Search Student (ID or Name):",
                Location = new Point(30, 312),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Visible = false
            };

            TextBox txtSearchFilter = new TextBox
            {
                Location = new Point(220, 310),
                Width = 220,
                Font = new Font("Segoe UI", 9F),
                Visible = false
            };

            // 7. DataGridView (Nakatago sa Y = 345)
            DataGridView dgvRegistered = new DataGridView();
            dgvRegistered.Location = new Point(30, 345);
            dgvRegistered.Size = new Size(715, 100);
            dgvRegistered.BackgroundColor = Color.White;
            dgvRegistered.ReadOnly = true;
            dgvRegistered.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRegistered.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRegistered.Visible = false;

            pnlContent.Controls.AddRange(new Control[] { lblSearchFilter, txtSearchFilter, dgvRegistered });

            // Refresh Table Function (Nirerespeto rin ang Search Filter)
            Action refreshTable = () => {
                string query = txtSearchFilter.Text.Trim().ToLower();

                var list = DataStore.Students
                    .Where(st => string.IsNullOrEmpty(query) ||
                                 st.Id.ToString().Contains(query) ||
                                 st.Name.ToLower().Contains(query))
                    .Select(s => new {
                        ID = s.Id,
                        FullName = s.Name,
                        Gender = s.Gender,
                        Contact = s.ContactNumber,
                        BirthDate = s.DateOfBirth,
                        Address = s.Address
                    }).ToList();

                dgvRegistered.DataSource = null;
                dgvRegistered.DataSource = list;
            };

            txtSearchFilter.TextChanged += (s, ev) => refreshTable();

            // 8. Action Buttons (Y = 260)
            Button btnClear = new Button { Text = "Clear", Location = new Point(30, 260), Size = new Size(80, 35), BackColor = Color.FromArgb(235, 215, 160), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, ev) => {
                txtSid.Clear(); txtFN.Clear(); txtLN.Clear(); txtContact.Clear(); txtAddress.Clear();
                rbMale.Checked = false; rbFemale.Checked = false; picPhoto.Image = null;
                txtSid.Enabled = true;
                selectedStudentId = -1;
            };

            Button btnRegister = new Button { Text = "Save New", Location = new Point(120, 260), Size = new Size(100, 35), BackColor = Color.FromArgb(92, 184, 92), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnRegister.FlatAppearance.BorderSize = 0;

            Button btnUpdate = new Button { Text = "Update", Location = new Point(228, 260), Size = new Size(90, 35), BackColor = Color.FromArgb(240, 173, 78), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnUpdate.FlatAppearance.BorderSize = 0;

            Button btnDelete = new Button { Text = "Delete", Location = new Point(326, 260), Size = new Size(90, 35), BackColor = Color.FromArgb(217, 83, 79), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnDelete.FlatAppearance.BorderSize = 0;

            Button btnViewAll = new Button { Text = "View All", Location = new Point(424, 260), Size = new Size(95, 35), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnViewAll.FlatAppearance.BorderSize = 0;

            // Row selection for edit
            dgvRegistered.CellClick += (s, ev) => {
                if (dgvRegistered.SelectedRows.Count > 0)
                {
                    var row = dgvRegistered.SelectedRows[0];
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    var student = DataStore.Students.FirstOrDefault(st => st.Id == id);

                    if (student != null)
                    {
                        selectedStudentId = student.Id;
                        txtSid.Text = student.Id.ToString();
                        txtSid.Enabled = false;

                        string[] nameParts = student.Name.Split(new char[] { ' ' }, 2);
                        txtFN.Text = nameParts.Length > 0 ? nameParts[0] : "";
                        txtLN.Text = nameParts.Length > 1 ? nameParts[1] : "";

                        txtContact.Text = student.ContactNumber;
                        txtAddress.Text = student.Address;

                        if (DateTime.TryParse(student.DateOfBirth, out DateTime dob))
                            dtpDob.Value = dob;

                        if (student.Gender == "Male") rbMale.Checked = true;
                        else if (student.Gender == "Female") rbFemale.Checked = true;
                        else { rbMale.Checked = false; rbFemale.Checked = false; }
                    }
                }
            };

            // Save New Event
            btnRegister.Click += (s, ev) => {
                if (string.IsNullOrWhiteSpace(txtSid.Text) || string.IsNullOrWhiteSpace(txtFN.Text) || string.IsNullOrWhiteSpace(txtLN.Text))
                {
                    MessageBox.Show("Please fill in Student ID, First Name, and Last Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSid.Text.Trim(), out int id))
                {
                    MessageBox.Show("Student ID must be a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DataStore.Students.Exists(st => st.Id == id))
                {
                    MessageBox.Show("This Student ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataStore.Students.Add(new Student
                {
                    Id = id,
                    Name = $"{txtFN.Text.Trim()} {txtLN.Text.Trim()}",
                    ContactNumber = txtContact.Text.Trim(),
                    DateOfBirth = dtpDob.Value.ToShortDateString(),
                    Gender = rbMale.Checked ? "Male" : (rbFemale.Checked ? "Female" : "Not Specified"),
                    Address = txtAddress.Text.Trim(),
                    Grades = new List<decimal>()
                });

                MessageBox.Show("Student successfully registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear.PerformClick();
                if (dgvRegistered.Visible) refreshTable();
            };

            // Update Event
            btnUpdate.Click += (s, ev) => {
                if (selectedStudentId == -1)
                {
                    MessageBox.Show("Please select a student from the table first to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var student = DataStore.Students.FirstOrDefault(st => st.Id == selectedStudentId);
                if (student != null)
                {
                    student.Name = $"{txtFN.Text.Trim()} {txtLN.Text.Trim()}";
                    student.ContactNumber = txtContact.Text.Trim();
                    student.DateOfBirth = dtpDob.Value.ToShortDateString();
                    student.Gender = rbMale.Checked ? "Male" : (rbFemale.Checked ? "Female" : "Not Specified");
                    student.Address = txtAddress.Text.Trim();

                    MessageBox.Show("Student information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear.PerformClick();
                    if (dgvRegistered.Visible) refreshTable();
                }
            };

            // Delete Event
            btnDelete.Click += (s, ev) => {
                if (selectedStudentId == -1)
                {
                    MessageBox.Show("Please select a student from the table first to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Are you sure you want to delete Student ID: {selectedStudentId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    DataStore.Students.RemoveAll(st => st.Id == selectedStudentId);
                    MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear.PerformClick();
                    if (dgvRegistered.Visible) refreshTable();
                }
            };

            // Toggle View All Event
            btnViewAll.Click += (s, ev) => {
                bool showList = !dgvRegistered.Visible;

                dgvRegistered.Visible = showList;
                lblSearchFilter.Visible = showList;
                txtSearchFilter.Visible = showList;

                if (showList)
                {
                    refreshTable();
                    btnViewAll.Text = "Hide List";
                    btnViewAll.BackColor = Color.FromArgb(108, 117, 125);
                }
                else
                {
                    btnViewAll.Text = "View All";
                    btnViewAll.BackColor = Color.FromArgb(0, 120, 215);
                }
            };

            pnlContent.Controls.AddRange(new Control[] { btnClear, btnRegister, btnUpdate, btnDelete, btnViewAll });
        }

        // ==========================================
        // GRADE COMPUTATION MODULE
        // ==========================================
        private void BtnGradeComputation_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Student Grade & Attendance Evaluation Module";
            pnlContent.Controls.Clear();

            // Main Container Panel
            Panel pnlCard = new Panel();
            pnlCard.Location = new Point(20, 10);
            pnlCard.Size = new Size(745, 425);
            pnlCard.BackColor = Color.White;
            pnlCard.BorderStyle = BorderStyle.FixedSingle;
            pnlContent.Controls.Add(pnlCard);

            // 1. Search Section (Student ID)
            Label lblSid = new Label { Text = "Enter Student ID:", Location = new Point(20, 18), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), AutoSize = true };
            TextBox txtSid = new TextBox { Location = new Point(140, 16), Width = 150, Font = new Font("Segoe UI", 9.5F) };
            Button btnSearch = new Button { Text = "Search", Location = new Point(300, 15), Size = new Size(80, 27), BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSearch.FlatAppearance.BorderSize = 0;

            Label lblNameLabel = new Label { Text = "Student Name:", Location = new Point(410, 18), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), AutoSize = true };
            Label lblNameVal = new Label { Text = "[ Not Found ]", Location = new Point(515, 18), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Gray, AutoSize = true };
            pnlCard.Controls.AddRange(new Control[] { lblSid, txtSid, btnSearch, lblNameLabel, lblNameVal });

            // GroupBox para sa 8 Subjects at Attendance
            GroupBox gbContainer = new GroupBox();
            gbContainer.Text = "Academic & Attendance Data Entry";
            gbContainer.Location = new Point(20, 55);
            gbContainer.Size = new Size(705, 305);
            gbContainer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pnlCard.Controls.Add(gbContainer);

            // 8 Subject Textboxes (2 Rows x 4 Columns)
            TextBox[] txtSubjects = new TextBox[8];
            for (int i = 0; i < 8; i++)
            {
                int row = i / 4;
                int col = i % 4;

                int xPos = 20 + (col * 170);
                int yPos = 30 + (row * 60);

                Label lblSub = new Label { Text = $"Subject {i + 1}:", Location = new Point(xPos, yPos), Font = new Font("Segoe UI", 9F), AutoSize = true };
                TextBox txtSub = new TextBox { Location = new Point(xPos, yPos + 20), Width = 150, Font = new Font("Segoe UI", 9F), Enabled = false };
                txtSubjects[i] = txtSub;

                gbContainer.Controls.Add(lblSub);
                gbContainer.Controls.Add(txtSub);
            }

            // Attendance Fields sa ibaba ng Subjects
            Label lblDays = new Label { Text = "Days Present:", Location = new Point(20, 160), Font = new Font("Segoe UI", 9F), AutoSize = true };
            TextBox txtDays = new TextBox { Location = new Point(20, 180), Width = 150, Font = new Font("Segoe UI", 9F), Enabled = false };

            Label lblTotal = new Label { Text = "Total School Days:", Location = new Point(200, 160), Font = new Font("Segoe UI", 9F), AutoSize = true };
            TextBox txtTotal = new TextBox { Text = "200", Location = new Point(200, 180), Width = 150, Font = new Font("Segoe UI", 9F), Enabled = false };

            // Live General Average & Attendance Rate Labels
            Label lblGenAvgTitle = new Label { Text = "General Average:", Location = new Point(380, 160), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), AutoSize = true };
            Label lblGenAvgVal = new Label { Text = "0.00", Location = new Point(500, 158), Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 120, 215), AutoSize = true };

            Label lblAttRateTitle = new Label { Text = "Attendance Rate:", Location = new Point(380, 195), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), AutoSize = true };
            Label lblAttRateVal = new Label { Text = "0.00%", Location = new Point(500, 193), Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(40, 167, 69), AutoSize = true };

            gbContainer.Controls.AddRange(new Control[] { lblDays, txtDays, lblTotal, txtTotal, lblGenAvgTitle, lblGenAvgVal, lblAttRateTitle, lblAttRateVal });

            Student foundStudent = null;

            // Live Calculation Logic
            Action computeAll = () => {
                decimal sum = 0;
                int validCount = 0;
                foreach (var txt in txtSubjects)
                {
                    if (decimal.TryParse(txt.Text, out decimal val))
                    {
                        sum += val;
                        validCount++;
                    }
                }

                decimal genAvg = validCount > 0 ? sum / validCount : 0;
                lblGenAvgVal.Text = genAvg.ToString("F2");

                if (int.TryParse(txtDays.Text, out int present) && int.TryParse(txtTotal.Text, out int total) && total > 0)
                {
                    decimal rate = ((decimal)present / total) * 100m;
                    lblAttRateVal.Text = $"{rate:F2}%";
                }
            };

            foreach (var txt in txtSubjects)
            {
                txt.TextChanged += (s, ev) => computeAll();
            }
            txtDays.TextChanged += (s, ev) => computeAll();
            txtTotal.TextChanged += (s, ev) => computeAll();

            // Search Student Action
            btnSearch.Click += (s, ev) => {
                if (!int.TryParse(txtSid.Text.Trim(), out int searchId))
                {
                    MessageBox.Show("Please enter a valid numeric Student ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foundStudent = DataStore.Students.FirstOrDefault(st => st.Id == searchId);
                if (foundStudent != null)
                {
                    lblNameVal.Text = foundStudent.Name;
                    lblNameVal.ForeColor = Color.FromArgb(40, 167, 69);

                    txtDays.Enabled = true;
                    txtTotal.Enabled = true;
                    foreach (var txt in txtSubjects) txt.Enabled = true;

                    txtDays.Text = foundStudent.DaysPresent.ToString();
                    txtTotal.Text = foundStudent.TotalSchoolDays.ToString();

                    for (int i = 0; i < 8; i++)
                    {
                        if (foundStudent.Grades != null && i < foundStudent.Grades.Count)
                            txtSubjects[i].Text = foundStudent.Grades[i].ToString();
                        else
                            txtSubjects[i].Text = "75";
                    }
                    computeAll();
                }
                else
                {
                    lblNameVal.Text = "[ Not Found ]";
                    lblNameVal.ForeColor = Color.FromArgb(217, 83, 79);
                    txtDays.Enabled = false;
                    txtTotal.Enabled = false;
                    foreach (var txt in txtSubjects) { txt.Enabled = false; txt.Clear(); }
                    txtDays.Clear();
                    foundStudent = null;
                    lblGenAvgVal.Text = "0.00";
                    lblAttRateVal.Text = "0.00%";
                }
            };

            // Save Button
            Button btnSave = new Button { Text = "Compute & Save All", Location = new Point(540, 375), Size = new Size(185, 38), BackColor = Color.FromArgb(92, 184, 92), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (s, ev) => {
                if (foundStudent == null)
                {
                    MessageBox.Show("Please search and select a student first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<decimal> newGrades = new List<decimal>();
                foreach (var txt in txtSubjects)
                {
                    if (decimal.TryParse(txt.Text, out decimal g))
                        newGrades.Add(g);
                    else
                        newGrades.Add(75m); // Default fallback
                }

                if (!int.TryParse(txtDays.Text, out int days) || !int.TryParse(txtTotal.Text, out int total))
                {
                    MessageBox.Show("Please enter valid attendance numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foundStudent.DaysPresent = days;
                foundStudent.TotalSchoolDays = total;
                foundStudent.Grades = newGrades;

                MessageBox.Show($"Grades and Attendance successfully updated for {foundStudent.Name}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            pnlCard.Controls.Add(btnSave);
        }

        

        // ==========================================
        // GENERATE REPORTS MODULE (Integrated in Form1)
        // ==========================================
        // ==========================================
        // GENERATE REPORTS MODULE (Actual vs Mock 100 Option)
        // ==========================================
        private void BtnReports_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Student Performance & Status Reports";
            pnlContent.Controls.Clear();

            // Toggle Button para sa View Mode (Actual vs Mock 100)
            Button btnToggleView = new Button();
            btnToggleView.Text = "Switch to: Mock Records (100)";
            btnToggleView.Location = new Point(15, 12);
            btnToggleView.Size = new Size(210, 32);
            btnToggleView.BackColor = Color.FromArgb(0, 120, 215);
            btnToggleView.ForeColor = Color.White;
            btnToggleView.FlatStyle = FlatStyle.Flat;
            btnToggleView.FlatAppearance.BorderSize = 0;
            btnToggleView.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pnlContent.Controls.Add(btnToggleView);

            Label lblTotalStudents = new Label();
            lblTotalStudents.Location = new Point(240, 18);
            lblTotalStudents.AutoSize = true;
            lblTotalStudents.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlContent.Controls.Add(lblTotalStudents);

            DataGridView dgvReports = new DataGridView();
            dgvReports.Location = new Point(15, 55);
            dgvReports.Size = new Size(735, 335);
            dgvReports.BackgroundColor = Color.White;
            dgvReports.ReadOnly = true;
            dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pnlContent.Controls.Add(dgvReports);

            bool isShowingMock = false;

            Action loadReportData = () => {
                // NGAYON AY DIREKTANG KUKUNIN NA SA DATASTORE ANG MOCK STUDENTS AT ACTUAL STUDENTS
                var sourceList = isShowingMock ? DataStore.MockStudents : DataStore.Students;

                var reportList = sourceList.Select(s => new
                {
                    ID = s.Id,
                    FullName = s.Name,
                    Gender = s.Gender,
                    Contact = s.ContactNumber,
                    GeneralAverage = s.Grades != null && s.Grades.Count > 0 ? s.Grades.Average().ToString("F2") : "No Grades",
                    Attendance = $"{s.DaysPresent} / {s.TotalSchoolDays}",
                    Status = (s.Grades != null && s.Grades.Count > 0 && s.Grades.Average() >= 75 && s.DaysPresent >= (s.TotalSchoolDays * 0.75))
                             ? "Passed"
                             : (s.Grades != null && s.Grades.Count > 0 ? "Failed / Lacking Attendance" : "Pending")
                }).ToList();

                dgvReports.DataSource = reportList;
                lblTotalStudents.Text = isShowingMock ? "Mode: Mock Records (100 Items)" : $"Mode: Actual Records ({DataStore.Students.Count} Items)";
            };

            loadReportData();

            btnToggleView.Click += (s, ev) => {
                isShowingMock = !isShowingMock;
                if (isShowingMock)
                {
                    btnToggleView.Text = "Switch to: Actual Records";
                    btnToggleView.BackColor = Color.FromArgb(92, 184, 92);
                }
                else
                {
                    btnToggleView.Text = "Switch to: Mock Records (100)";
                    btnToggleView.BackColor = Color.FromArgb(0, 120, 215);
                }
                loadReportData();
            };

            Button btnRefreshReport = new Button();
            btnRefreshReport.Text = "Refresh Table";
            btnRefreshReport.Location = new Point(15, 405);
            btnRefreshReport.Size = new Size(130, 35);
            btnRefreshReport.BackColor = Color.FromArgb(108, 117, 125);
            btnRefreshReport.ForeColor = Color.White;
            btnRefreshReport.FlatStyle = FlatStyle.Flat;
            btnRefreshReport.FlatAppearance.BorderSize = 0;
            btnRefreshReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefreshReport.Click += (s, ev) => {
                loadReportData();
                MessageBox.Show("Report table refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            pnlContent.Controls.Add(btnRefreshReport);
        }


        // ==========================================
        // CONCURRENT PROCESSING MODULE (Sync with 100 Mock Records)
        // ==========================================
        private void BtnProcessing_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Concurrent Processing & Queue Manager";
            pnlContent.Controls.Clear();

            Panel pnlProcessing = new Panel();
            pnlProcessing.Dock = DockStyle.Fill;
            pnlProcessing.Padding = new Padding(20);
            pnlContent.Controls.Add(pnlProcessing);

            Label lblDesc = new Label();
            lblDesc.Text = "Choose execution mode: Process real registered students or the 100 Mock Records.";
            lblDesc.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblDesc.Location = new Point(30, 20);
            lblDesc.AutoSize = true;
            pnlProcessing.Controls.Add(lblDesc);

            ProgressBar progressBar = new ProgressBar();
            progressBar.Location = new Point(30, 70);
            progressBar.Size = new Size(520, 25);
            progressBar.Style = ProgressBarStyle.Continuous;
            pnlProcessing.Controls.Add(progressBar);

            Label lblProgressPercent = new Label();
            lblProgressPercent.Text = "0%";
            lblProgressPercent.Location = new Point(560, 73);
            lblProgressPercent.AutoSize = true;
            lblProgressPercent.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            pnlProcessing.Controls.Add(lblProgressPercent);

            TextBox txtLogOutput = new TextBox();
            txtLogOutput.Location = new Point(30, 115);
            txtLogOutput.Size = new Size(700, 235);
            txtLogOutput.Multiline = true;
            txtLogOutput.ScrollBars = ScrollBars.Vertical;
            txtLogOutput.ReadOnly = true;
            txtLogOutput.BackColor = Color.FromArgb(245, 245, 245);
            txtLogOutput.Font = new Font("Consolas", 9F, FontStyle.Regular);
            txtLogOutput.Text = "[System Ready]: Click one of the execution buttons below.\r\n";
            pnlProcessing.Controls.Add(txtLogOutput);

            Button btnRunActual = new Button();
            btnRunActual.Text = "Run Actual Records";
            btnRunActual.Location = new Point(30, 370);
            btnRunActual.Size = new Size(190, 40);
            btnRunActual.BackColor = Color.FromArgb(0, 120, 215);
            btnRunActual.ForeColor = Color.White;
            btnRunActual.FlatStyle = FlatStyle.Flat;
            btnRunActual.FlatAppearance.BorderSize = 0;
            btnRunActual.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // Pinalitan na naging 100 para tugma sa Reports
            Button btnRunMock = new Button();
            btnRunMock.Text = "Run Mock Records (100)";
            btnRunMock.Location = new Point(235, 370);
            btnRunMock.Size = new Size(190, 40);
            btnRunMock.BackColor = Color.FromArgb(92, 184, 92);
            btnRunMock.ForeColor = Color.White;
            btnRunMock.FlatStyle = FlatStyle.Flat;
            btnRunMock.FlatAppearance.BorderSize = 0;
            btnRunMock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            Func<System.Collections.Generic.List<Student>, string, System.Threading.Tasks.Task> executeProcessing = async (records, modeName) => {
                if (records.Count == 0)
                {
                    MessageBox.Show("No records available to process.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnRunActual.Enabled = false;
                btnRunMock.Enabled = false;
                txtLogOutput.Clear();
                txtLogOutput.AppendText($"[Execution Started]: Processing using -> {modeName} ({records.Count} items)\r\n");

                progressBar.Value = 0;
                int totalItems = records.Count;
                progressBar.Maximum = totalItems;
                int completedCount = 0;

                await System.Threading.Tasks.Task.Run(() =>
                {
                    System.Threading.Tasks.Parallel.ForEach(records, student =>
                    {
                        System.Threading.Thread.Sleep(150); // Medyo binilisan para sa 100 items
                        string resultStatus = student.Grades != null && student.Grades.Count > 0 ? "Processed & Synced" : "No Grades Found";

                        this.Invoke(new Action(() => {
                            completedCount++;
                            progressBar.Value = completedCount;
                            int percent = (completedCount * 100) / totalItems;
                            lblProgressPercent.Text = $"{percent}%";

                            txtLogOutput.AppendText($"[Worker Thread #{System.Threading.Thread.CurrentThread.ManagedThreadId}] ID: {student.Id} | {student.Name} -> {resultStatus}\r\n");
                        }));
                    });
                });

                txtLogOutput.AppendText("[Execution Completed]: All parallel tasks finished successfully!\r\n");
                MessageBox.Show("Concurrent processing completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnRunActual.Enabled = true;
                btnRunMock.Enabled = true;
            };

            btnRunActual.Click += async (s, ev) => {
                if (DataStore.Students.Count == 0)
                {
                    MessageBox.Show("No actual registered students found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                await executeProcessing(DataStore.Students, "Actual Records");
            };

            // Gagamitin na rin nito ang DataStore.MockStudents na may 100 items
            btnRunMock.Click += async (s, ev) => {
                await executeProcessing(DataStore.MockStudents, "Mock Records (100)");
            };

            pnlProcessing.Controls.Add(btnRunActual);
            pnlProcessing.Controls.Add(btnRunMock);
        }
    }
}