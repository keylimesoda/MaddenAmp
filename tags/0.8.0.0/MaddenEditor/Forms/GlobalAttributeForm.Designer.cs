namespace MaddenEditor.Forms
{
	partial class GlobalAttributeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalAttributeForm));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.attributeCombo = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
			this.filterTeamComboBox = new System.Windows.Forms.ComboBox();
			this.filterPositionCheckBox = new System.Windows.Forms.CheckBox();
			this.filterTeamCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.incrementNumeric = new System.Windows.Forms.NumericUpDown();
			this.decrementNumeric = new System.Windows.Forms.NumericUpDown();
			this.setNumeric = new System.Windows.Forms.NumericUpDown();
			this.incrementCheckBox = new System.Windows.Forms.RadioButton();
			this.decrementCheckBox = new System.Windows.Forms.RadioButton();
			this.setCheckBox = new System.Windows.Forms.RadioButton();
			this.filterDraftClassCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.incrementNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.decrementNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.setNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(491, 33);
			this.textBox1.TabIndex = 7;
			this.textBox1.Text = "Using the available options below, structure your query to edit the attribute you" +
				" want changed on a group of selected players";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 94);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Change Attribute";
			// 
			// attributeCombo
			// 
			this.attributeCombo.FormattingEnabled = true;
			this.attributeCombo.Items.AddRange(new object[] {
            "Age",
            "Years Experience",
            "Speed",
            "Strength",
            "Awareness",
            "Agility",
            "Acceleration",
            "Catching",
            "Carrying",
            "Jumping",
            "Break Tackle",
            "Tackle",
            "Throw Accuracy",
            "Throw Power",
            "Pass Blocking",
            "Run Blocking",
            "Kick Power",
            "Kick Accuracy",
            "Kick Return",
            "Stamina",
            "Injury",
            "Toughness",
            "Experience Points",
            "Importance",
            "Morale"});
			this.attributeCombo.Location = new System.Drawing.Point(93, 91);
			this.attributeCombo.Name = "attributeCombo";
			this.attributeCombo.Size = new System.Drawing.Size(119, 21);
			this.attributeCombo.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.filterDraftClassCheckBox);
			this.groupBox1.Controls.Add(this.filterPositionComboBox);
			this.groupBox1.Controls.Add(this.filterTeamComboBox);
			this.groupBox1.Controls.Add(this.filterPositionCheckBox);
			this.groupBox1.Controls.Add(this.filterTeamCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(268, 52);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(235, 100);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filter Settings";
			// 
			// filterPositionComboBox
			// 
			this.filterPositionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterPositionComboBox.FormattingEnabled = true;
			this.filterPositionComboBox.Location = new System.Drawing.Point(81, 52);
			this.filterPositionComboBox.Name = "filterPositionComboBox";
			this.filterPositionComboBox.Size = new System.Drawing.Size(148, 21);
			this.filterPositionComboBox.TabIndex = 3;
			// 
			// filterTeamComboBox
			// 
			this.filterTeamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterTeamComboBox.FormattingEnabled = true;
			this.filterTeamComboBox.Location = new System.Drawing.Point(81, 22);
			this.filterTeamComboBox.Name = "filterTeamComboBox";
			this.filterTeamComboBox.Size = new System.Drawing.Size(148, 21);
			this.filterTeamComboBox.TabIndex = 1;
			this.filterTeamComboBox.SelectedIndexChanged += new System.EventHandler(this.filterTeamComboBox_SelectedIndexChanged);
			// 
			// filterPositionCheckBox
			// 
			this.filterPositionCheckBox.AutoSize = true;
			this.filterPositionCheckBox.Location = new System.Drawing.Point(16, 54);
			this.filterPositionCheckBox.Name = "filterPositionCheckBox";
			this.filterPositionCheckBox.Size = new System.Drawing.Size(59, 17);
			this.filterPositionCheckBox.TabIndex = 2;
			this.filterPositionCheckBox.Text = "Position";
			// 
			// filterTeamCheckBox
			// 
			this.filterTeamCheckBox.AutoSize = true;
			this.filterTeamCheckBox.Location = new System.Drawing.Point(16, 24);
			this.filterTeamCheckBox.Name = "filterTeamCheckBox";
			this.filterTeamCheckBox.Size = new System.Drawing.Size(49, 17);
			this.filterTeamCheckBox.TabIndex = 0;
			this.filterTeamCheckBox.Text = "Team";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(218, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "with filter";
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(356, 362);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 5;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(437, 362);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.incrementNumeric);
			this.groupBox2.Controls.Add(this.decrementNumeric);
			this.groupBox2.Controls.Add(this.setNumeric);
			this.groupBox2.Controls.Add(this.incrementCheckBox);
			this.groupBox2.Controls.Add(this.decrementCheckBox);
			this.groupBox2.Controls.Add(this.setCheckBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 158);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(491, 198);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Change by";
			// 
			// incrementNumeric
			// 
			this.incrementNumeric.Location = new System.Drawing.Point(124, 90);
			this.incrementNumeric.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.incrementNumeric.Name = "incrementNumeric";
			this.incrementNumeric.Size = new System.Drawing.Size(73, 20);
			this.incrementNumeric.TabIndex = 5;
			// 
			// decrementNumeric
			// 
			this.decrementNumeric.Location = new System.Drawing.Point(124, 57);
			this.decrementNumeric.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.decrementNumeric.Name = "decrementNumeric";
			this.decrementNumeric.Size = new System.Drawing.Size(73, 20);
			this.decrementNumeric.TabIndex = 3;
			// 
			// setNumeric
			// 
			this.setNumeric.Location = new System.Drawing.Point(124, 25);
			this.setNumeric.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.setNumeric.Name = "setNumeric";
			this.setNumeric.Size = new System.Drawing.Size(73, 20);
			this.setNumeric.TabIndex = 1;
			// 
			// incrementCheckBox
			// 
			this.incrementCheckBox.AutoSize = true;
			this.incrementCheckBox.Location = new System.Drawing.Point(18, 93);
			this.incrementCheckBox.Name = "incrementCheckBox";
			this.incrementCheckBox.Size = new System.Drawing.Size(82, 17);
			this.incrementCheckBox.TabIndex = 4;
			this.incrementCheckBox.TabStop = false;
			this.incrementCheckBox.Text = "Increment by";
			// 
			// decrementCheckBox
			// 
			this.decrementCheckBox.AutoSize = true;
			this.decrementCheckBox.Checked = true;
			this.decrementCheckBox.Location = new System.Drawing.Point(18, 60);
			this.decrementCheckBox.Name = "decrementCheckBox";
			this.decrementCheckBox.Size = new System.Drawing.Size(87, 17);
			this.decrementCheckBox.TabIndex = 2;
			this.decrementCheckBox.Text = "Decrement by";
			// 
			// setCheckBox
			// 
			this.setCheckBox.AutoSize = true;
			this.setCheckBox.Location = new System.Drawing.Point(18, 28);
			this.setCheckBox.Name = "setCheckBox";
			this.setCheckBox.Size = new System.Drawing.Size(49, 17);
			this.setCheckBox.TabIndex = 0;
			this.setCheckBox.TabStop = false;
			this.setCheckBox.Text = "Set to";
			// 
			// filterDraftClassCheckBox
			// 
			this.filterDraftClassCheckBox.AutoSize = true;
			this.filterDraftClassCheckBox.Location = new System.Drawing.Point(16, 79);
			this.filterDraftClassCheckBox.Name = "filterDraftClassCheckBox";
			this.filterDraftClassCheckBox.Size = new System.Drawing.Size(73, 17);
			this.filterDraftClassCheckBox.TabIndex = 4;
			this.filterDraftClassCheckBox.Text = "Draft Class";
			// 
			// GlobalAttributeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(515, 397);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.attributeCombo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GlobalAttributeForm";
			this.Text = "Global Player Attribute Editor Form";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.incrementNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.decrementNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.setNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox attributeCombo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox filterPositionCheckBox;
		private System.Windows.Forms.CheckBox filterTeamCheckBox;
		private System.Windows.Forms.ComboBox filterPositionComboBox;
		private System.Windows.Forms.ComboBox filterTeamComboBox;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton setCheckBox;
		private System.Windows.Forms.RadioButton decrementCheckBox;
		private System.Windows.Forms.RadioButton incrementCheckBox;
		private System.Windows.Forms.NumericUpDown incrementNumeric;
		private System.Windows.Forms.NumericUpDown decrementNumeric;
		private System.Windows.Forms.NumericUpDown setNumeric;
		private System.Windows.Forms.CheckBox filterDraftClassCheckBox;
	}
}