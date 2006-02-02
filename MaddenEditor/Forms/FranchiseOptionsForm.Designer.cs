namespace MaddenEditor.Forms
{
	partial class FranchiseOptionsForm
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
			this.cbOwnerMode = new System.Windows.Forms.CheckBox();
			this.cbSalaryCap = new System.Windows.Forms.CheckBox();
			this.cbCapPenalties = new System.Windows.Forms.CheckBox();
			this.cbTradeDeadline = new System.Windows.Forms.CheckBox();
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
			this.label1.Location = new System.Drawing.Point(23, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "1st Year RFA Min";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "2nd Year RFA Min";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 137);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "4th Year RFA Min";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(55, 177);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 8;
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
			this.year1RFA.TabIndex = 1;
			this.year1RFA.ThousandsSeparator = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(22, 97);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 13);
			this.label6.TabIndex = 4;
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
			this.year2RFA.TabIndex = 3;
			this.year2RFA.ThousandsSeparator = true;
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
			this.year3RFA.TabIndex = 5;
			this.year3RFA.ThousandsSeparator = true;
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
			this.year4RFA.TabIndex = 7;
			this.year4RFA.ThousandsSeparator = true;
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
			this.salaryCap.TabIndex = 9;
			this.salaryCap.ThousandsSeparator = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(227, 198);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 14;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(308, 198);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 15;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// cbOwnerMode
			// 
			this.cbOwnerMode.AutoSize = true;
			this.cbOwnerMode.Location = new System.Drawing.Point(218, 13);
			this.cbOwnerMode.Name = "cbOwnerMode";
			this.cbOwnerMode.Size = new System.Drawing.Size(123, 17);
			this.cbOwnerMode.TabIndex = 10;
			this.cbOwnerMode.Text = "Owner Mode Toggle";
			this.cbOwnerMode.UseVisualStyleBackColor = true;
			// 
			// cbSalaryCap
			// 
			this.cbSalaryCap.AutoSize = true;
			this.cbSalaryCap.Location = new System.Drawing.Point(218, 55);
			this.cbSalaryCap.Name = "cbSalaryCap";
			this.cbSalaryCap.Size = new System.Drawing.Size(130, 17);
			this.cbSalaryCap.TabIndex = 11;
			this.cbSalaryCap.Text = "Salary Cap On Toggle";
			this.cbSalaryCap.UseVisualStyleBackColor = true;
			// 
			// cbCapPenalties
			// 
			this.cbCapPenalties.AutoSize = true;
			this.cbCapPenalties.Location = new System.Drawing.Point(218, 96);
			this.cbCapPenalties.Name = "cbCapPenalties";
			this.cbCapPenalties.Size = new System.Drawing.Size(176, 17);
			this.cbCapPenalties.TabIndex = 12;
			this.cbCapPenalties.Text = "Salary Cap Penalties On Toggle";
			this.cbCapPenalties.UseVisualStyleBackColor = true;
			// 
			// cbTradeDeadline
			// 
			this.cbTradeDeadline.AutoSize = true;
			this.cbTradeDeadline.Location = new System.Drawing.Point(218, 136);
			this.cbTradeDeadline.Name = "cbTradeDeadline";
			this.cbTradeDeadline.Size = new System.Drawing.Size(152, 17);
			this.cbTradeDeadline.TabIndex = 13;
			this.cbTradeDeadline.Text = "Trade Deadline On Toggle";
			this.cbTradeDeadline.UseVisualStyleBackColor = true;
			// 
			// FranchiseOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(395, 224);
			this.Controls.Add(this.cbTradeDeadline);
			this.Controls.Add(this.cbCapPenalties);
			this.Controls.Add(this.cbSalaryCap);
			this.Controls.Add(this.cbOwnerMode);
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
			this.Name = "FranchiseOptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Franchise Options";
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
		private System.Windows.Forms.CheckBox cbOwnerMode;
		private System.Windows.Forms.CheckBox cbSalaryCap;
		private System.Windows.Forms.CheckBox cbCapPenalties;
		private System.Windows.Forms.CheckBox cbTradeDeadline;
	}
}