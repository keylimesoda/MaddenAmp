namespace MaddenEditor.Forms
{
	partial class TeamCaptainForm
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
			this.teamComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.specialTeamsCaptainCombo = new System.Windows.Forms.ComboBox();
			this.defensiveCaptainCombo = new System.Windows.Forms.ComboBox();
			this.offensiveCaptainCombo = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(61, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Team";
			// 
			// teamComboBox
			// 
			this.teamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.teamComboBox.FormattingEnabled = true;
			this.teamComboBox.Location = new System.Drawing.Point(97, 12);
			this.teamComboBox.Name = "teamComboBox";
			this.teamComboBox.Size = new System.Drawing.Size(147, 21);
			this.teamComboBox.TabIndex = 1;
			this.teamComboBox.SelectedIndexChanged += new System.EventHandler(this.teamComboBox_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.cancelButton);
			this.groupBox1.Controls.Add(this.okButton);
			this.groupBox1.Controls.Add(this.specialTeamsCaptainCombo);
			this.groupBox1.Controls.Add(this.defensiveCaptainCombo);
			this.groupBox1.Controls.Add(this.offensiveCaptainCombo);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 47);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(303, 255);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Captains";
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(222, 226);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Close";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(141, 226);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Set";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// specialTeamsCaptainCombo
			// 
			this.specialTeamsCaptainCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.specialTeamsCaptainCombo.FormattingEnabled = true;
			this.specialTeamsCaptainCombo.Location = new System.Drawing.Point(124, 163);
			this.specialTeamsCaptainCombo.Name = "specialTeamsCaptainCombo";
			this.specialTeamsCaptainCombo.Size = new System.Drawing.Size(173, 21);
			this.specialTeamsCaptainCombo.TabIndex = 5;
			// 
			// defensiveCaptainCombo
			// 
			this.defensiveCaptainCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.defensiveCaptainCombo.FormattingEnabled = true;
			this.defensiveCaptainCombo.Location = new System.Drawing.Point(124, 111);
			this.defensiveCaptainCombo.Name = "defensiveCaptainCombo";
			this.defensiveCaptainCombo.Size = new System.Drawing.Size(173, 21);
			this.defensiveCaptainCombo.TabIndex = 4;
			// 
			// offensiveCaptainCombo
			// 
			this.offensiveCaptainCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.offensiveCaptainCombo.FormattingEnabled = true;
			this.offensiveCaptainCombo.Location = new System.Drawing.Point(124, 54);
			this.offensiveCaptainCombo.Name = "offensiveCaptainCombo";
			this.offensiveCaptainCombo.Size = new System.Drawing.Size(173, 21);
			this.offensiveCaptainCombo.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 166);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Special Teams Captain";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(28, 114);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Defensive Captain";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(31, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Offensive Captain";
			// 
			// TeamCaptainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 314);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.teamComboBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "TeamCaptainForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Team Captain Select";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox teamComboBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ComboBox specialTeamsCaptainCombo;
		private System.Windows.Forms.ComboBox defensiveCaptainCombo;
		private System.Windows.Forms.ComboBox offensiveCaptainCombo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}