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
            this.cancelButton = new System.Windows.Forms.Button();
            this.MainSkillsOnly_Checkbox = new System.Windows.Forms.CheckBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.filterPositionCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCheckbox = new System.Windows.Forms.CheckBox();
            this.filterPositionCheckbox = new System.Windows.Forms.CheckBox();
            this.filterDraftClassCheckbox = new System.Windows.Forms.CheckBox();
            this.Export_Button = new System.Windows.Forms.Button();
            this.AvailTables_ListView = new System.Windows.Forms.ListView();
            this.ExportTables_ListView = new System.Windows.Forms.ListView();
            this.Export_Panel = new System.Windows.Forms.Panel();
            this.RemoveFields_Button = new System.Windows.Forms.Button();
            this.ExportFields_ListView = new System.Windows.Forms.ListView();
            this.AvailFields_ListView = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddFields_Button = new System.Windows.Forms.Button();
            this.RemoveExportTables_Button = new System.Windows.Forms.Button();
            this.AddExportTables_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Replace_Checkbox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.UpdateRecs_Checkbox = new System.Windows.Forms.CheckBox();
            this.ImportCSV_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView3 = new System.Windows.Forms.ListView();
            this.listView4 = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.Export_Panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancelButton);
            this.groupBox1.Controls.Add(this.MainSkillsOnly_Checkbox);
            this.groupBox1.Controls.Add(this.ExportButton);
            this.groupBox1.Controls.Add(this.filterPositionCombo);
            this.groupBox1.Controls.Add(this.filterTeamCombo);
            this.groupBox1.Controls.Add(this.filterTeamCheckbox);
            this.groupBox1.Controls.Add(this.filterPositionCheckbox);
            this.groupBox1.Controls.Add(this.filterDraftClassCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(758, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Madden 04-08 Filter";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(141, 117);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // MainSkillsOnly_Checkbox
            // 
            this.MainSkillsOnly_Checkbox.AutoSize = true;
            this.MainSkillsOnly_Checkbox.Location = new System.Drawing.Point(14, 75);
            this.MainSkillsOnly_Checkbox.Name = "MainSkillsOnly_Checkbox";
            this.MainSkillsOnly_Checkbox.Size = new System.Drawing.Size(133, 17);
            this.MainSkillsOnly_Checkbox.TabIndex = 7;
            this.MainSkillsOnly_Checkbox.Text = "Position Skill Sets Only";
            this.MainSkillsOnly_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(60, 117);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 1;
            this.ExportButton.Text = "Export";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // filterPositionCombo
            // 
            this.filterPositionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPositionCombo.FormattingEnabled = true;
            this.filterPositionCombo.Location = new System.Drawing.Point(80, 49);
            this.filterPositionCombo.Name = "filterPositionCombo";
            this.filterPositionCombo.Size = new System.Drawing.Size(129, 21);
            this.filterPositionCombo.TabIndex = 6;
            this.filterPositionCombo.SelectedIndexChanged += new System.EventHandler(this.filterPositionCombo_SelectedIndexChanged);
            // 
            // filterTeamCombo
            // 
            this.filterTeamCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTeamCombo.FormattingEnabled = true;
            this.filterTeamCombo.Location = new System.Drawing.Point(80, 20);
            this.filterTeamCombo.Name = "filterTeamCombo";
            this.filterTeamCombo.Size = new System.Drawing.Size(129, 21);
            this.filterTeamCombo.TabIndex = 5;
            this.filterTeamCombo.SelectedIndexChanged += new System.EventHandler(this.filterTeamCombo_SelectedIndexChanged);
            // 
            // filterTeamCheckbox
            // 
            this.filterTeamCheckbox.AutoSize = true;
            this.filterTeamCheckbox.Location = new System.Drawing.Point(14, 22);
            this.filterTeamCheckbox.Name = "filterTeamCheckbox";
            this.filterTeamCheckbox.Size = new System.Drawing.Size(53, 17);
            this.filterTeamCheckbox.TabIndex = 4;
            this.filterTeamCheckbox.Text = "Team";
            // 
            // filterPositionCheckbox
            // 
            this.filterPositionCheckbox.AutoSize = true;
            this.filterPositionCheckbox.Location = new System.Drawing.Point(14, 51);
            this.filterPositionCheckbox.Name = "filterPositionCheckbox";
            this.filterPositionCheckbox.Size = new System.Drawing.Size(63, 17);
            this.filterPositionCheckbox.TabIndex = 3;
            this.filterPositionCheckbox.Text = "Position";
            // 
            // filterDraftClassCheckbox
            // 
            this.filterDraftClassCheckbox.AutoSize = true;
            this.filterDraftClassCheckbox.Location = new System.Drawing.Point(14, 98);
            this.filterDraftClassCheckbox.Name = "filterDraftClassCheckbox";
            this.filterDraftClassCheckbox.Size = new System.Drawing.Size(77, 17);
            this.filterDraftClassCheckbox.TabIndex = 2;
            this.filterDraftClassCheckbox.Text = "Draft Class";
            // 
            // Export_Button
            // 
            this.Export_Button.Location = new System.Drawing.Point(758, 238);
            this.Export_Button.Name = "Export_Button";
            this.Export_Button.Size = new System.Drawing.Size(118, 23);
            this.Export_Button.TabIndex = 1;
            this.Export_Button.Text = "Export Selected";
            this.Export_Button.UseVisualStyleBackColor = true;
            this.Export_Button.Click += new System.EventHandler(this.ExportPlay_Button_Click);
            // 
            // AvailTables_ListView
            // 
            this.AvailTables_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AvailTables_ListView.Location = new System.Drawing.Point(5, 29);
            this.AvailTables_ListView.Name = "AvailTables_ListView";
            this.AvailTables_ListView.Size = new System.Drawing.Size(150, 232);
            this.AvailTables_ListView.TabIndex = 3;
            this.AvailTables_ListView.UseCompatibleStateImageBehavior = false;
            this.AvailTables_ListView.View = System.Windows.Forms.View.Details;
            // 
            // ExportTables_ListView
            // 
            this.ExportTables_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ExportTables_ListView.Location = new System.Drawing.Point(217, 29);
            this.ExportTables_ListView.Name = "ExportTables_ListView";
            this.ExportTables_ListView.Size = new System.Drawing.Size(150, 232);
            this.ExportTables_ListView.TabIndex = 4;
            this.ExportTables_ListView.UseCompatibleStateImageBehavior = false;
            this.ExportTables_ListView.View = System.Windows.Forms.View.Details;
            this.ExportTables_ListView.SelectedIndexChanged += new System.EventHandler(this.ExportTables_SelectedIndexChanged);
            // 
            // Export_Panel
            // 
            this.Export_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Export_Panel.Controls.Add(this.RemoveFields_Button);
            this.Export_Panel.Controls.Add(this.ExportFields_ListView);
            this.Export_Panel.Controls.Add(this.AvailFields_ListView);
            this.Export_Panel.Controls.Add(this.label4);
            this.Export_Panel.Controls.Add(this.label3);
            this.Export_Panel.Controls.Add(this.groupBox1);
            this.Export_Panel.Controls.Add(this.Export_Button);
            this.Export_Panel.Controls.Add(this.label2);
            this.Export_Panel.Controls.Add(this.AddFields_Button);
            this.Export_Panel.Controls.Add(this.RemoveExportTables_Button);
            this.Export_Panel.Controls.Add(this.AddExportTables_Button);
            this.Export_Panel.Controls.Add(this.label1);
            this.Export_Panel.Controls.Add(this.AvailTables_ListView);
            this.Export_Panel.Controls.Add(this.ExportTables_ListView);
            this.Export_Panel.Location = new System.Drawing.Point(5, 5);
            this.Export_Panel.Name = "Export_Panel";
            this.Export_Panel.Size = new System.Drawing.Size(995, 270);
            this.Export_Panel.TabIndex = 7;
            // 
            // RemoveFields_Button
            // 
            this.RemoveFields_Button.Location = new System.Drawing.Point(535, 139);
            this.RemoveFields_Button.Name = "RemoveFields_Button";
            this.RemoveFields_Button.Size = new System.Drawing.Size(50, 23);
            this.RemoveFields_Button.TabIndex = 13;
            this.RemoveFields_Button.Text = "DEL";
            this.RemoveFields_Button.UseVisualStyleBackColor = true;
            this.RemoveFields_Button.Click += new System.EventHandler(this.RemoveFields_Button_Click);
            // 
            // ExportFields_ListView
            // 
            this.ExportFields_ListView.Location = new System.Drawing.Point(591, 29);
            this.ExportFields_ListView.Name = "ExportFields_ListView";
            this.ExportFields_ListView.Size = new System.Drawing.Size(146, 232);
            this.ExportFields_ListView.TabIndex = 12;
            this.ExportFields_ListView.UseCompatibleStateImageBehavior = false;
            this.ExportFields_ListView.View = System.Windows.Forms.View.Details;
            // 
            // AvailFields_ListView
            // 
            this.AvailFields_ListView.Location = new System.Drawing.Point(386, 29);
            this.AvailFields_ListView.Name = "AvailFields_ListView";
            this.AvailFields_ListView.Size = new System.Drawing.Size(143, 232);
            this.AvailFields_ListView.TabIndex = 11;
            this.AvailFields_ListView.UseCompatibleStateImageBehavior = false;
            this.AvailFields_ListView.View = System.Windows.Forms.View.Details;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(619, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Export Fields";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(411, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Available Fields";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Export Tables";
            // 
            // AddFields_Button
            // 
            this.AddFields_Button.Location = new System.Drawing.Point(535, 94);
            this.AddFields_Button.Name = "AddFields_Button";
            this.AddFields_Button.Size = new System.Drawing.Size(50, 23);
            this.AddFields_Button.TabIndex = 7;
            this.AddFields_Button.Text = "ADD";
            this.AddFields_Button.UseVisualStyleBackColor = true;
            this.AddFields_Button.Click += new System.EventHandler(this.AddFields_Button_Click);
            // 
            // RemoveExportTables_Button
            // 
            this.RemoveExportTables_Button.Location = new System.Drawing.Point(161, 139);
            this.RemoveExportTables_Button.Name = "RemoveExportTables_Button";
            this.RemoveExportTables_Button.Size = new System.Drawing.Size(50, 23);
            this.RemoveExportTables_Button.TabIndex = 6;
            this.RemoveExportTables_Button.Text = "DEL";
            this.RemoveExportTables_Button.UseVisualStyleBackColor = true;
            this.RemoveExportTables_Button.Click += new System.EventHandler(this.RemoveExportTables_Button_Click);
            // 
            // AddExportTables_Button
            // 
            this.AddExportTables_Button.Location = new System.Drawing.Point(161, 94);
            this.AddExportTables_Button.Name = "AddExportTables_Button";
            this.AddExportTables_Button.Size = new System.Drawing.Size(50, 23);
            this.AddExportTables_Button.TabIndex = 5;
            this.AddExportTables_Button.Text = "ADD";
            this.AddExportTables_Button.UseVisualStyleBackColor = true;
            this.AddExportTables_Button.Click += new System.EventHandler(this.AddExportTables_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tables";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Replace_Checkbox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.UpdateRecs_Checkbox);
            this.panel1.Controls.Add(this.ImportCSV_Button);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.listView3);
            this.panel1.Controls.Add(this.listView4);
            this.panel1.Location = new System.Drawing.Point(5, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 386);
            this.panel1.TabIndex = 8;
            // 
            // Replace_Checkbox
            // 
            this.Replace_Checkbox.AutoSize = true;
            this.Replace_Checkbox.Location = new System.Drawing.Point(570, 204);
            this.Replace_Checkbox.Name = "Replace_Checkbox";
            this.Replace_Checkbox.Size = new System.Drawing.Size(88, 17);
            this.Replace_Checkbox.TabIndex = 2;
            this.Replace_Checkbox.Text = "Replace ALL";
            this.Replace_Checkbox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(318, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Selected Fields";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(212, 76);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 18;
            // 
            // UpdateRecs_Checkbox
            // 
            this.UpdateRecs_Checkbox.AutoSize = true;
            this.UpdateRecs_Checkbox.Location = new System.Drawing.Point(570, 181);
            this.UpdateRecs_Checkbox.Name = "UpdateRecs_Checkbox";
            this.UpdateRecs_Checkbox.Size = new System.Drawing.Size(89, 17);
            this.UpdateRecs_Checkbox.TabIndex = 1;
            this.UpdateRecs_Checkbox.Text = "Update Recs";
            this.UpdateRecs_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ImportCSV_Button
            // 
            this.ImportCSV_Button.Location = new System.Drawing.Point(570, 248);
            this.ImportCSV_Button.Name = "ImportCSV_Button";
            this.ImportCSV_Button.Size = new System.Drawing.Size(89, 23);
            this.ImportCSV_Button.TabIndex = 0;
            this.ImportCSV_Button.Text = "Import CSV";
            this.ImportCSV_Button.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Available Fields";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "DEL";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(227, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "ADD";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(73, 99);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(150, 232);
            this.listView3.TabIndex = 12;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // listView4
            // 
            this.listView4.Location = new System.Drawing.Point(288, 97);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(150, 232);
            this.listView4.TabIndex = 13;
            this.listView4.UseCompatibleStateImageBehavior = false;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Export_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Players";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Export_Panel.ResumeLayout(false);
            this.Export_Panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button Export_Button;
        private System.Windows.Forms.ListView AvailTables_ListView;
        private System.Windows.Forms.ListView ExportTables_ListView;
        private System.Windows.Forms.Panel Export_Panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RemoveExportTables_Button;
        private System.Windows.Forms.Button AddExportTables_Button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Replace_Checkbox;
        private System.Windows.Forms.Button AddFields_Button;
        private System.Windows.Forms.CheckBox UpdateRecs_Checkbox;
        private System.Windows.Forms.Button ImportCSV_Button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ListView ExportFields_ListView;
        private System.Windows.Forms.ListView AvailFields_ListView;
        private System.Windows.Forms.Button RemoveFields_Button;
	}
}