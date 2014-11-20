namespace MaddenEditor.Forms
{
	partial class ExportForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterPositionCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCheckbox = new System.Windows.Forms.CheckBox();
            this.filterPositionCheckbox = new System.Windows.Forms.CheckBox();
            this.filterDraftClassCheckbox = new System.Windows.Forms.CheckBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.MainSkillsOnly_Checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MainSkillsOnly_Checkbox);
            this.groupBox1.Controls.Add(this.filterPositionCombo);
            this.groupBox1.Controls.Add(this.filterTeamCombo);
            this.groupBox1.Controls.Add(this.filterTeamCheckbox);
            this.groupBox1.Controls.Add(this.filterPositionCheckbox);
            this.groupBox1.Controls.Add(this.filterDraftClassCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // filterPositionCombo
            // 
            this.filterPositionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPositionCombo.FormattingEnabled = true;
            this.filterPositionCombo.Location = new System.Drawing.Point(93, 70);
            this.filterPositionCombo.Name = "filterPositionCombo";
            this.filterPositionCombo.Size = new System.Drawing.Size(169, 21);
            this.filterPositionCombo.TabIndex = 6;
            this.filterPositionCombo.SelectedIndexChanged += new System.EventHandler(this.filterPositionCombo_SelectedIndexChanged);
            // 
            // filterTeamCombo
            // 
            this.filterTeamCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTeamCombo.FormattingEnabled = true;
            this.filterTeamCombo.Location = new System.Drawing.Point(93, 28);
            this.filterTeamCombo.Name = "filterTeamCombo";
            this.filterTeamCombo.Size = new System.Drawing.Size(169, 21);
            this.filterTeamCombo.TabIndex = 5;
            this.filterTeamCombo.SelectedIndexChanged += new System.EventHandler(this.filterTeamCombo_SelectedIndexChanged);
            // 
            // filterTeamCheckbox
            // 
            this.filterTeamCheckbox.AutoSize = true;
            this.filterTeamCheckbox.Location = new System.Drawing.Point(27, 30);
            this.filterTeamCheckbox.Name = "filterTeamCheckbox";
            this.filterTeamCheckbox.Size = new System.Drawing.Size(53, 17);
            this.filterTeamCheckbox.TabIndex = 4;
            this.filterTeamCheckbox.Text = "Team";
            // 
            // filterPositionCheckbox
            // 
            this.filterPositionCheckbox.AutoSize = true;
            this.filterPositionCheckbox.Location = new System.Drawing.Point(27, 72);
            this.filterPositionCheckbox.Name = "filterPositionCheckbox";
            this.filterPositionCheckbox.Size = new System.Drawing.Size(63, 17);
            this.filterPositionCheckbox.TabIndex = 3;
            this.filterPositionCheckbox.Text = "Position";
            // 
            // filterDraftClassCheckbox
            // 
            this.filterDraftClassCheckbox.AutoSize = true;
            this.filterDraftClassCheckbox.Location = new System.Drawing.Point(27, 166);
            this.filterDraftClassCheckbox.Name = "filterDraftClassCheckbox";
            this.filterDraftClassCheckbox.Size = new System.Drawing.Size(77, 17);
            this.filterDraftClassCheckbox.TabIndex = 2;
            this.filterDraftClassCheckbox.Text = "Draft Class";
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(124, 240);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 1;
            this.ExportButton.Text = "Export";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(205, 240);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // MainSkillsOnly_Checkbox
            // 
            this.MainSkillsOnly_Checkbox.AutoSize = true;
            this.MainSkillsOnly_Checkbox.Location = new System.Drawing.Point(27, 115);
            this.MainSkillsOnly_Checkbox.Name = "MainSkillsOnly_Checkbox";
            this.MainSkillsOnly_Checkbox.Size = new System.Drawing.Size(133, 17);
            this.MainSkillsOnly_Checkbox.TabIndex = 7;
            this.MainSkillsOnly_Checkbox.Text = "Position Skill Sets Only";
            this.MainSkillsOnly_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Players";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button ExportButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ComboBox filterPositionCombo;
		private System.Windows.Forms.ComboBox filterTeamCombo;
		private System.Windows.Forms.CheckBox filterTeamCheckbox;
		private System.Windows.Forms.CheckBox filterPositionCheckbox;
		private System.Windows.Forms.CheckBox filterDraftClassCheckbox;
        private System.Windows.Forms.CheckBox MainSkillsOnly_Checkbox;
	}
}