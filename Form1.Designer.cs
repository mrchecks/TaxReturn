namespace ReadPDF
{
    partial class Form1
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
            this.lblInputDir = new System.Windows.Forms.TextBox();
            this.btnInputDirectoryBrowse = new System.Windows.Forms.Button();
            this.tbInputDirectory = new System.Windows.Forms.TextBox();
            this.btnOutputDirectoryBrowse = new System.Windows.Forms.Button();
            this.tbOutputDirectory = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnConfigEdit = new System.Windows.Forms.Button();
            this.tbConfigFilePath = new System.Windows.Forms.TextBox();
            this.RadioSa = new System.Windows.Forms.RadioButton();
            this.RadioUk = new System.Windows.Forms.RadioButton();
            this.BtnProcess = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.lblOutputDirectory = new System.Windows.Forms.TextBox();
            this.BtnRename = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInputDir
            // 
            this.lblInputDir.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblInputDir.Location = new System.Drawing.Point(22, 28);
            this.lblInputDir.Name = "lblInputDir";
            this.lblInputDir.ReadOnly = true;
            this.lblInputDir.Size = new System.Drawing.Size(368, 13);
            this.lblInputDir.TabIndex = 0;
            this.lblInputDir.TabStop = false;
            this.lblInputDir.Text = "Bank statements directory";
            // 
            // btnInputDirectoryBrowse
            // 
            this.btnInputDirectoryBrowse.Location = new System.Drawing.Point(406, 45);
            this.btnInputDirectoryBrowse.Name = "btnInputDirectoryBrowse";
            this.btnInputDirectoryBrowse.Size = new System.Drawing.Size(27, 23);
            this.btnInputDirectoryBrowse.TabIndex = 2;
            this.btnInputDirectoryBrowse.Text = "...";
            this.btnInputDirectoryBrowse.UseVisualStyleBackColor = true;
            this.btnInputDirectoryBrowse.Click += new System.EventHandler(this.btnInputDirectoryBrowse_Click);
            // 
            // tbInputDirectory
            // 
            this.tbInputDirectory.Location = new System.Drawing.Point(22, 47);
            this.tbInputDirectory.Name = "tbInputDirectory";
            this.tbInputDirectory.Size = new System.Drawing.Size(384, 20);
            this.tbInputDirectory.TabIndex = 1;
            // 
            // btnOutputDirectoryBrowse
            // 
            this.btnOutputDirectoryBrowse.Location = new System.Drawing.Point(406, 113);
            this.btnOutputDirectoryBrowse.Name = "btnOutputDirectoryBrowse";
            this.btnOutputDirectoryBrowse.Size = new System.Drawing.Size(27, 23);
            this.btnOutputDirectoryBrowse.TabIndex = 5;
            this.btnOutputDirectoryBrowse.Text = "...";
            this.btnOutputDirectoryBrowse.UseVisualStyleBackColor = true;
            this.btnOutputDirectoryBrowse.Click += new System.EventHandler(this.btnOutputDirectoryBrowse_Click);
            // 
            // tbOutputDirectory
            // 
            this.tbOutputDirectory.Location = new System.Drawing.Point(22, 115);
            this.tbOutputDirectory.Name = "tbOutputDirectory";
            this.tbOutputDirectory.Size = new System.Drawing.Size(384, 20);
            this.tbOutputDirectory.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btnConfigEdit);
            this.groupBox1.Controls.Add(this.tbConfigFilePath);
            this.groupBox1.Controls.Add(this.RadioSa);
            this.groupBox1.Controls.Add(this.RadioUk);
            this.groupBox1.Location = new System.Drawing.Point(22, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 148);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statement type";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(16, 89);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(368, 13);
            this.textBox2.TabIndex = 13;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Config file path";
            // 
            // btnConfigEdit
            // 
            this.btnConfigEdit.Location = new System.Drawing.Point(364, 106);
            this.btnConfigEdit.Name = "btnConfigEdit";
            this.btnConfigEdit.Size = new System.Drawing.Size(41, 23);
            this.btnConfigEdit.TabIndex = 15;
            this.btnConfigEdit.Text = "Edit";
            this.btnConfigEdit.UseVisualStyleBackColor = true;
            this.btnConfigEdit.Click += new System.EventHandler(this.btnConfigEdit_Click);
            // 
            // tbConfigFilePath
            // 
            this.tbConfigFilePath.Location = new System.Drawing.Point(16, 108);
            this.tbConfigFilePath.Name = "tbConfigFilePath";
            this.tbConfigFilePath.Size = new System.Drawing.Size(345, 20);
            this.tbConfigFilePath.TabIndex = 14;
            // 
            // RadioSa
            // 
            this.RadioSa.AutoSize = true;
            this.RadioSa.Location = new System.Drawing.Point(32, 54);
            this.RadioSa.Name = "RadioSa";
            this.RadioSa.Size = new System.Drawing.Size(39, 17);
            this.RadioSa.TabIndex = 1;
            this.RadioSa.TabStop = true;
            this.RadioSa.Text = "SA";
            this.RadioSa.UseVisualStyleBackColor = true;
            this.RadioSa.CheckedChanged += new System.EventHandler(this.RadioSa_CheckedChanged);
            // 
            // RadioUk
            // 
            this.RadioUk.AutoSize = true;
            this.RadioUk.Location = new System.Drawing.Point(32, 31);
            this.RadioUk.Name = "RadioUk";
            this.RadioUk.Size = new System.Drawing.Size(40, 17);
            this.RadioUk.TabIndex = 0;
            this.RadioUk.TabStop = true;
            this.RadioUk.Text = "UK";
            this.RadioUk.UseVisualStyleBackColor = true;
            this.RadioUk.CheckedChanged += new System.EventHandler(this.RadioUk_CheckedChanged);
            // 
            // BtnProcess
            // 
            this.BtnProcess.Location = new System.Drawing.Point(178, 322);
            this.BtnProcess.Name = "BtnProcess";
            this.BtnProcess.Size = new System.Drawing.Size(97, 28);
            this.BtnProcess.TabIndex = 7;
            this.BtnProcess.Text = "Process";
            this.BtnProcess.UseVisualStyleBackColor = true;
            this.BtnProcess.Click += new System.EventHandler(this.BtnProcess_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(336, 322);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(97, 28);
            this.BtnClose.TabIndex = 8;
            this.BtnClose.Text = "Save && Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblOutputDirectory.Location = new System.Drawing.Point(22, 96);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.ReadOnly = true;
            this.lblOutputDirectory.Size = new System.Drawing.Size(368, 13);
            this.lblOutputDirectory.TabIndex = 3;
            this.lblOutputDirectory.TabStop = false;
            this.lblOutputDirectory.Text = "Output directory";
            // 
            // BtnRename
            // 
            this.BtnRename.Location = new System.Drawing.Point(22, 322);
            this.BtnRename.Name = "BtnRename";
            this.BtnRename.Size = new System.Drawing.Size(97, 28);
            this.BtnRename.TabIndex = 9;
            this.BtnRename.Text = "Rename";
            this.BtnRename.UseVisualStyleBackColor = true;
            this.BtnRename.Click += new System.EventHandler(this.BtnRename_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 363);
            this.Controls.Add(this.BtnRename);
            this.Controls.Add(this.lblOutputDirectory);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnProcess);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOutputDirectoryBrowse);
            this.Controls.Add(this.tbOutputDirectory);
            this.Controls.Add(this.lblInputDir);
            this.Controls.Add(this.btnInputDirectoryBrowse);
            this.Controls.Add(this.tbInputDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lblInputDir;
        private System.Windows.Forms.Button btnInputDirectoryBrowse;
        private System.Windows.Forms.TextBox tbInputDirectory;
        private System.Windows.Forms.Button btnOutputDirectoryBrowse;
        private System.Windows.Forms.TextBox tbOutputDirectory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RadioSa;
        private System.Windows.Forms.RadioButton RadioUk;
        private System.Windows.Forms.Button BtnProcess;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TextBox lblOutputDirectory;
        private System.Windows.Forms.Button BtnRename;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnConfigEdit;
        private System.Windows.Forms.TextBox tbConfigFilePath;
    }
}

