namespace MaddenEditor.Forms
{
	partial class CoachEditControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.leftButton = new System.Windows.Forms.Button();
			this.rightButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.attributePage = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.coachesName = new System.Windows.Forms.TextBox();
			this.filterTeamComboBox = new System.Windows.Forms.ComboBox();
			this.filterTeamCheckBox = new System.Windows.Forms.CheckBox();
			this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
			this.filterPositionCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.coachesPositionCombo = new System.Windows.Forms.ComboBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.coachesPositionCombo);
			this.splitContainer1.Panel1.Controls.Add(this.coachesName);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl);
			this.splitContainer1.Size = new System.Drawing.Size(770, 583);
			this.splitContainer1.SplitterDistance = 228;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.filterPositionComboBox);
			this.groupBox1.Controls.Add(this.filterPositionCheckBox);
			this.groupBox1.Controls.Add(this.filterTeamComboBox);
			this.groupBox1.Controls.Add(this.filterTeamCheckBox);
			this.groupBox1.Controls.Add(this.rightButton);
			this.groupBox1.Controls.Add(this.leftButton);
			this.groupBox1.Location = new System.Drawing.Point(0, 454);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(228, 129);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Coach Navigate / Filter";
			// 
			// leftButton
			// 
			this.leftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.leftButton.Location = new System.Drawing.Point(1, 98);
			this.leftButton.Name = "leftButton";
			this.leftButton.Size = new System.Drawing.Size(75, 26);
			this.leftButton.TabIndex = 0;
			this.leftButton.Text = "<<";
			// 
			// rightButton
			// 
			this.rightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.rightButton.Location = new System.Drawing.Point(150, 98);
			this.rightButton.Name = "rightButton";
			this.rightButton.Size = new System.Drawing.Size(75, 26);
			this.rightButton.TabIndex = 1;
			this.rightButton.Text = ">>";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.attributePage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(538, 583);
			this.tabControl.TabIndex = 0;
			// 
			// attributePage
			// 
			this.attributePage.Location = new System.Drawing.Point(4, 22);
			this.attributePage.Name = "attributePage";
			this.attributePage.Padding = new System.Windows.Forms.Padding(3);
			this.attributePage.Size = new System.Drawing.Size(530, 557);
			this.attributePage.TabIndex = 0;
			this.attributePage.Text = "Attributes";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Coach Name";
			// 
			// coachesName
			// 
			this.coachesName.Location = new System.Drawing.Point(78, 33);
			this.coachesName.Name = "coachesName";
			this.coachesName.Size = new System.Drawing.Size(130, 20);
			this.coachesName.TabIndex = 1;
			// 
			// filterTeamComboBox
			// 
			this.filterTeamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterTeamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filterTeamComboBox.FormattingEnabled = true;
			this.filterTeamComboBox.Location = new System.Drawing.Point(67, 32);
			this.filterTeamComboBox.Name = "filterTeamComboBox";
			this.filterTeamComboBox.Size = new System.Drawing.Size(151, 21);
			this.filterTeamComboBox.TabIndex = 3;
			// 
			// filterTeamCheckBox
			// 
			this.filterTeamCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.filterTeamCheckBox.AutoSize = true;
			this.filterTeamCheckBox.Location = new System.Drawing.Point(6, 34);
			this.filterTeamCheckBox.Name = "filterTeamCheckBox";
			this.filterTeamCheckBox.Size = new System.Drawing.Size(49, 17);
			this.filterTeamCheckBox.TabIndex = 2;
			this.filterTeamCheckBox.Text = "Team";
			// 
			// filterPositionComboBox
			// 
			this.filterPositionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterPositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filterPositionComboBox.FormattingEnabled = true;
			this.filterPositionComboBox.Location = new System.Drawing.Point(67, 59);
			this.filterPositionComboBox.Name = "filterPositionComboBox";
			this.filterPositionComboBox.Size = new System.Drawing.Size(151, 21);
			this.filterPositionComboBox.TabIndex = 5;
			// 
			// filterPositionCheckBox
			// 
			this.filterPositionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.filterPositionCheckBox.AutoSize = true;
			this.filterPositionCheckBox.Location = new System.Drawing.Point(6, 61);
			this.filterPositionCheckBox.Name = "filterPositionCheckBox";
			this.filterPositionCheckBox.Size = new System.Drawing.Size(59, 17);
			this.filterPositionCheckBox.TabIndex = 4;
			this.filterPositionCheckBox.Text = "Position";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(36, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Position";
			// 
			// coachesPositionCombo
			// 
			this.coachesPositionCombo.FormattingEnabled = true;
			this.coachesPositionCombo.Items.AddRange(new object[] {
            "Head Coach",
            "Offensive Coordinator",
            "Defensive Coordinator",
            "Special Teams Coach"});
			this.coachesPositionCombo.Location = new System.Drawing.Point(78, 75);
			this.coachesPositionCombo.Name = "coachesPositionCombo";
			this.coachesPositionCombo.Size = new System.Drawing.Size(130, 21);
			this.coachesPositionCombo.TabIndex = 1;
			this.coachesPositionCombo.SelectedIndexChanged += new System.EventHandler(this.coachesPositionCombo_SelectedIndexChanged);
			// 
			// CoachEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "CoachEditControl";
			this.Size = new System.Drawing.Size(770, 583);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button rightButton;
		private System.Windows.Forms.Button leftButton;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage attributePage;
		private System.Windows.Forms.TextBox coachesName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox filterTeamComboBox;
		private System.Windows.Forms.CheckBox filterTeamCheckBox;
		private System.Windows.Forms.ComboBox filterPositionComboBox;
		private System.Windows.Forms.CheckBox filterPositionCheckBox;
		private System.Windows.Forms.ComboBox coachesPositionCombo;
		private System.Windows.Forms.Label label2;


	}
}
