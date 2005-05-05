namespace MaddenEditor.Forms
{
	partial class SalaryCapForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.year1RFA = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.year2RFA = new System.Windows.Forms.NumericUpDown();
			this.year3RFA = new System.Windows.Forms.NumericUpDown();
			this.year4RFA = new System.Windows.Forms.NumericUpDown();
			this.salaryCap = new System.Windows.Forms.NumericUpDown();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.year1RFA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.year2RFA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.year3RFA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.year4RFA)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.salaryCap)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "1st Year RFA Min";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "2nd Year RFA Min";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 142);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "4th Year RFA Min";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(55, 182);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Salary Cap";
			// 
			// year1RFA
			// 
			this.year1RFA.Location = new System.Drawing.Point(115, 12);
			this.year1RFA.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
			this.year1RFA.Name = "year1RFA";
			this.year1RFA.Size = new System.Drawing.Size(85, 20);
			this.year1RFA.TabIndex = 5;
			this.year1RFA.ThousandsSeparator = true;
			this.year1RFA.ValueChanged += new System.EventHandler(this.year1RFA_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(22, 102);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "3rd Year RFA Min";
			// 
			// year2RFA
			// 
			this.year2RFA.Location = new System.Drawing.Point(115, 54);
			this.year2RFA.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
			this.year2RFA.Name = "year2RFA";
			this.year2RFA.Size = new System.Drawing.Size(85, 20);
			this.year2RFA.TabIndex = 8;
			this.year2RFA.ThousandsSeparator = true;
			this.year2RFA.ValueChanged += new System.EventHandler(this.year2RFA_ValueChanged);
			// 
			// year3RFA
			// 
			this.year3RFA.Location = new System.Drawing.Point(115, 95);
			this.year3RFA.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
			this.year3RFA.Name = "year3RFA";
			this.year3RFA.Size = new System.Drawing.Size(85, 20);
			this.year3RFA.TabIndex = 9;
			this.year3RFA.ThousandsSeparator = true;
			this.year3RFA.ValueChanged += new System.EventHandler(this.year3RFA_ValueChanged);
			// 
			// year4RFA
			// 
			this.year4RFA.Location = new System.Drawing.Point(115, 135);
			this.year4RFA.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
			this.year4RFA.Name = "year4RFA";
			this.year4RFA.Size = new System.Drawing.Size(85, 20);
			this.year4RFA.TabIndex = 10;
			this.year4RFA.ThousandsSeparator = true;
			this.year4RFA.ValueChanged += new System.EventHandler(this.year4RFA_ValueChanged);
			// 
			// salaryCap
			// 
			this.salaryCap.Location = new System.Drawing.Point(115, 175);
			this.salaryCap.Maximum = new decimal(new int[] {
            0,
            1,
            0,
            0});
			this.salaryCap.Name = "salaryCap";
			this.salaryCap.Size = new System.Drawing.Size(85, 20);
			this.salaryCap.TabIndex = 11;
			this.salaryCap.ThousandsSeparator = true;
			this.salaryCap.ValueChanged += new System.EventHandler(this.salaryCap_ValueChanged);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(34, 212);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 12;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(125, 212);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 13;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// SalaryCapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(232, 252);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.salaryCap);
			this.Controls.Add(this.year4RFA);
			this.Controls.Add(this.year3RFA);
			this.Controls.Add(this.year2RFA);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.year1RFA);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SalaryCapForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Salary Cap";
			((System.ComponentModel.ISupportInitialize)(this.year1RFA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.year2RFA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.year3RFA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.year4RFA)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.salaryCap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown year1RFA;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown year2RFA;
		private System.Windows.Forms.NumericUpDown year3RFA;
		private System.Windows.Forms.NumericUpDown year4RFA;
		private System.Windows.Forms.NumericUpDown salaryCap;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}