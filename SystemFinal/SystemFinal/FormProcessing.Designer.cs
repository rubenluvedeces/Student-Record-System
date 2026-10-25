namespace SystemFinal
{
    partial class FormProcessing
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
            this.btnRunParallel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRunParallel
            // 
            this.btnRunParallel.Location = new System.Drawing.Point(372, 152);
            this.btnRunParallel.Name = "btnRunParallel";
            this.btnRunParallel.Size = new System.Drawing.Size(186, 101);
            this.btnRunParallel.TabIndex = 0;
            this.btnRunParallel.Text = "Run Multithreaded Processing";
            this.btnRunParallel.UseVisualStyleBackColor = true;
            // 
            // FormProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRunParallel);
            this.Name = "FormProcessing";
            this.Text = "FormProcessing";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRunParallel;
    }
}