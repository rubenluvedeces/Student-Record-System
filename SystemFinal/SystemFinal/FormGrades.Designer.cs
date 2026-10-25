namespace SystemFinal
{
    partial class FormGrades
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGradesInput = new System.Windows.Forms.TextBox();
            this.btnComputeGrades = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(315, 162);
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.Size = new System.Drawing.Size(138, 26);
            this.txtStudentId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Student ID:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grades (comma-seperated):";
            // 
            // txtGradesInput
            // 
            this.txtGradesInput.Location = new System.Drawing.Point(315, 263);
            this.txtGradesInput.Name = "txtGradesInput";
            this.txtGradesInput.Size = new System.Drawing.Size(138, 26);
            this.txtGradesInput.TabIndex = 2;
            // 
            // btnComputeGrades
            // 
            this.btnComputeGrades.Location = new System.Drawing.Point(315, 335);
            this.btnComputeGrades.Name = "btnComputeGrades";
            this.btnComputeGrades.Size = new System.Drawing.Size(150, 41);
            this.btnComputeGrades.TabIndex = 4;
            this.btnComputeGrades.Text = "Compute Grades";
            this.btnComputeGrades.UseVisualStyleBackColor = true;
            // 
            // FormGrades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnComputeGrades);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGradesInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStudentId);
            this.Name = "FormGrades";
            this.Text = "FormGrades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGradesInput;
        private System.Windows.Forms.Button btnComputeGrades;
    }
}