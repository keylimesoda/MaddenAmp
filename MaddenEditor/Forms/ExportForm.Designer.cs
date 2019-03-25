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
            this.ExportFilter_Panel = new System.Windows.Forms.GroupBox();
            this.MainSkillsOnly_Checkbox = new System.Windows.Forms.CheckBox();
            this.ExportButton = new System.Windows.Forms.Button();
            this.filterDraftClassCheckbox = new System.Windows.Forms.CheckBox();
            this.filterPositionCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCombo = new System.Windows.Forms.ComboBox();
            this.filterTeamCheckbox = new System.Windows.Forms.CheckBox();
            this.filterPositionCheckbox = new System.Windows.Forms.CheckBox();
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
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExtractByTableName = new System.Windows.Forms.CheckBox();
            this.Descriptions_Checkbox = new System.Windows.Forms.CheckBox();
            this.Import_Panel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.ImportErrors_Listview = new System.Windows.Forms.ListView();
            this.DeleteCurrentRecs_Checkbox = new System.Windows.Forms.CheckBox();
            this.ProcessRecords_Button = new System.Windows.Forms.Button();
            this.NotImportableCount_Textbox = new System.Windows.Forms.TextBox();
            this.ImportFieldsCount_Textbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.WrongFields_ListView = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ImportTableName_Textbox = new System.Windows.Forms.TextBox();
            this.UpdateRecs_Checkbox = new System.Windows.Forms.CheckBox();
            this.ImportCSV_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ImportAvailFields_ListView = new System.Windows.Forms.ListView();
            this.ImportSelected_ListView = new System.Windows.Forms.ListView();
            this.DraftClass_Panel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.DraftClassDescriptions_Checkbox = new System.Windows.Forms.CheckBox();
            this.CreateDraftClass_Button = new System.Windows.Forms.Button();
            this.ExportDraftClass_Button = new System.Windows.Forms.Button();
            this.LoadDraftClass_Button = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ExportFilter_Panel.SuspendLayout();
            this.Export_Panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Import_Panel.SuspendLayout();
            this.DraftClass_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportFilter_Panel
            // 
            this.ExportFilter_Panel.Controls.Add(this.MainSkillsOnly_Checkbox);
            this.ExportFilter_Panel.Controls.Add(this.ExportButton);
            this.ExportFilter_Panel.Controls.Add(this.filterDraftClassCheckbox);
            this.ExportFilter_Panel.Location = new System.Drawing.Point(379, 5);
            this.ExportFilter_Panel.Name = "ExportFilter_Panel";
            this.ExportFilter_Panel.Size = new System.Drawing.Size(287, 117);
            this.ExportFilter_Panel.TabIndex = 0;
            this.ExportFilter_Panel.TabStop = false;
            this.ExportFilter_Panel.Text = "Madden 04-08 Player Exports";
            // 
            // MainSkillsOnly_Checkbox
            // 
            this.MainSkillsOnly_Checkbox.AutoSize = true;
            this.MainSkillsOnly_Checkbox.Location = new System.Drawing.Point(13, 29);
            this.MainSkillsOnly_Checkbox.Name = "MainSkillsOnly_Checkbox";
            this.MainSkillsOnly_Checkbox.Size = new System.Drawing.Size(205, 17);
            this.MainSkillsOnly_Checkbox.TabIndex = 7;
            this.MainSkillsOnly_Checkbox.Text = "Madden 04-08 Position Skill Sets Only";
            this.MainSkillsOnly_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(13, 81);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(60, 23);
            this.ExportButton.TabIndex = 1;
            this.ExportButton.Text = "Export";
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // filterDraftClassCheckbox
            // 
            this.filterDraftClassCheckbox.AutoSize = true;
            this.filterDraftClassCheckbox.Location = new System.Drawing.Point(13, 52);
            this.filterDraftClassCheckbox.Name = "filterDraftClassCheckbox";
            this.filterDraftClassCheckbox.Size = new System.Drawing.Size(149, 17);
            this.filterDraftClassCheckbox.TabIndex = 2;
            this.filterDraftClassCheckbox.Text = "Madden 04-08 Draft Class";
            // 
            // filterPositionCombo
            // 
            this.filterPositionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPositionCombo.FormattingEnabled = true;
            this.filterPositionCombo.Location = new System.Drawing.Point(77, 141);
            this.filterPositionCombo.Name = "filterPositionCombo";
            this.filterPositionCombo.Size = new System.Drawing.Size(129, 21);
            this.filterPositionCombo.TabIndex = 6;
            this.filterPositionCombo.SelectedIndexChanged += new System.EventHandler(this.filterPositionCombo_SelectedIndexChanged);
            // 
            // filterTeamCombo
            // 
            this.filterTeamCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTeamCombo.FormattingEnabled = true;
            this.filterTeamCombo.Location = new System.Drawing.Point(77, 115);
            this.filterTeamCombo.Name = "filterTeamCombo";
            this.filterTeamCombo.Size = new System.Drawing.Size(129, 21);
            this.filterTeamCombo.TabIndex = 5;
            this.filterTeamCombo.SelectedIndexChanged += new System.EventHandler(this.filterTeamCombo_SelectedIndexChanged);
            // 
            // filterTeamCheckbox
            // 
            this.filterTeamCheckbox.AutoSize = true;
            this.filterTeamCheckbox.Location = new System.Drawing.Point(11, 117);
            this.filterTeamCheckbox.Name = "filterTeamCheckbox";
            this.filterTeamCheckbox.Size = new System.Drawing.Size(53, 17);
            this.filterTeamCheckbox.TabIndex = 4;
            this.filterTeamCheckbox.Text = "Team";
            // 
            // filterPositionCheckbox
            // 
            this.filterPositionCheckbox.AutoSize = true;
            this.filterPositionCheckbox.Location = new System.Drawing.Point(11, 143);
            this.filterPositionCheckbox.Name = "filterPositionCheckbox";
            this.filterPositionCheckbox.Size = new System.Drawing.Size(63, 17);
            this.filterPositionCheckbox.TabIndex = 3;
            this.filterPositionCheckbox.Text = "Position";
            // 
            // Export_Button
            // 
            this.Export_Button.Location = new System.Drawing.Point(58, 354);
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
            this.Export_Panel.Controls.Add(this.label2);
            this.Export_Panel.Controls.Add(this.AddFields_Button);
            this.Export_Panel.Controls.Add(this.RemoveExportTables_Button);
            this.Export_Panel.Controls.Add(this.AddExportTables_Button);
            this.Export_Panel.Controls.Add(this.label1);
            this.Export_Panel.Controls.Add(this.AvailTables_ListView);
            this.Export_Panel.Controls.Add(this.ExportTables_ListView);
            this.Export_Panel.Location = new System.Drawing.Point(5, 128);
            this.Export_Panel.Name = "Export_Panel";
            this.Export_Panel.Size = new System.Drawing.Size(745, 277);
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
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Tables";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(55, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 15);
            this.label11.TabIndex = 15;
            this.label11.Text = "Export Options";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExtractByTableName);
            this.panel1.Controls.Add(this.Descriptions_Checkbox);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.Export_Button);
            this.panel1.Controls.Add(this.filterPositionCombo);
            this.panel1.Controls.Add(this.filterPositionCheckbox);
            this.panel1.Controls.Add(this.filterTeamCombo);
            this.panel1.Controls.Add(this.filterTeamCheckbox);
            this.panel1.Location = new System.Drawing.Point(773, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 393);
            this.panel1.TabIndex = 14;
            // 
            // ExtractByTableName
            // 
            this.ExtractByTableName.AutoSize = true;
            this.ExtractByTableName.Location = new System.Drawing.Point(11, 319);
            this.ExtractByTableName.Name = "ExtractByTableName";
            this.ExtractByTableName.Size = new System.Drawing.Size(162, 17);
            this.ExtractByTableName.TabIndex = 16;
            this.ExtractByTableName.Text = "Extract to DIR by table name";
            this.ExtractByTableName.UseVisualStyleBackColor = true;
            // 
            // Descriptions_Checkbox
            // 
            this.Descriptions_Checkbox.AutoSize = true;
            this.Descriptions_Checkbox.Location = new System.Drawing.Point(11, 79);
            this.Descriptions_Checkbox.Name = "Descriptions_Checkbox";
            this.Descriptions_Checkbox.Size = new System.Drawing.Size(147, 17);
            this.Descriptions_Checkbox.TabIndex = 14;
            this.Descriptions_Checkbox.Text = "Include Field Descriptions";
            this.Descriptions_Checkbox.UseVisualStyleBackColor = true;
            // 
            // Import_Panel
            // 
            this.Import_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Import_Panel.Controls.Add(this.label9);
            this.Import_Panel.Controls.Add(this.ImportErrors_Listview);
            this.Import_Panel.Controls.Add(this.DeleteCurrentRecs_Checkbox);
            this.Import_Panel.Controls.Add(this.ProcessRecords_Button);
            this.Import_Panel.Controls.Add(this.NotImportableCount_Textbox);
            this.Import_Panel.Controls.Add(this.ImportFieldsCount_Textbox);
            this.Import_Panel.Controls.Add(this.label8);
            this.Import_Panel.Controls.Add(this.WrongFields_ListView);
            this.Import_Panel.Controls.Add(this.label7);
            this.Import_Panel.Controls.Add(this.label5);
            this.Import_Panel.Controls.Add(this.ImportTableName_Textbox);
            this.Import_Panel.Controls.Add(this.UpdateRecs_Checkbox);
            this.Import_Panel.Controls.Add(this.ImportCSV_Button);
            this.Import_Panel.Controls.Add(this.label6);
            this.Import_Panel.Controls.Add(this.ImportAvailFields_ListView);
            this.Import_Panel.Controls.Add(this.ImportSelected_ListView);
            this.Import_Panel.Location = new System.Drawing.Point(5, 411);
            this.Import_Panel.Name = "Import_Panel";
            this.Import_Panel.Size = new System.Drawing.Size(995, 309);
            this.Import_Panel.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(737, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 15);
            this.label9.TabIndex = 27;
            this.label9.Text = "Import Errors";
            // 
            // ImportErrors_Listview
            // 
            this.ImportErrors_Listview.Location = new System.Drawing.Point(591, 73);
            this.ImportErrors_Listview.Name = "ImportErrors_Listview";
            this.ImportErrors_Listview.Size = new System.Drawing.Size(394, 220);
            this.ImportErrors_Listview.TabIndex = 26;
            this.ImportErrors_Listview.UseCompatibleStateImageBehavior = false;
            this.ImportErrors_Listview.View = System.Windows.Forms.View.Details;
            // 
            // DeleteCurrentRecs_Checkbox
            // 
            this.DeleteCurrentRecs_Checkbox.AutoSize = true;
            this.DeleteCurrentRecs_Checkbox.Location = new System.Drawing.Point(806, 7);
            this.DeleteCurrentRecs_Checkbox.Name = "DeleteCurrentRecs_Checkbox";
            this.DeleteCurrentRecs_Checkbox.Size = new System.Drawing.Size(144, 17);
            this.DeleteCurrentRecs_Checkbox.TabIndex = 25;
            this.DeleteCurrentRecs_Checkbox.Text = "Delete ALL Current Recs";
            this.DeleteCurrentRecs_Checkbox.UseVisualStyleBackColor = true;
            this.DeleteCurrentRecs_Checkbox.CheckedChanged += new System.EventHandler(this.DeleteCurrentRecs_Checkbox_CheckedChanged);
            // 
            // ProcessRecords_Button
            // 
            this.ProcessRecords_Button.Location = new System.Drawing.Point(398, 4);
            this.ProcessRecords_Button.Name = "ProcessRecords_Button";
            this.ProcessRecords_Button.Size = new System.Drawing.Size(150, 23);
            this.ProcessRecords_Button.TabIndex = 24;
            this.ProcessRecords_Button.Text = "Process Records";
            this.ProcessRecords_Button.UseVisualStyleBackColor = true;
            this.ProcessRecords_Button.Click += new System.EventHandler(this.ProcessRecords_Button_Click);
            // 
            // NotImportableCount_Textbox
            // 
            this.NotImportableCount_Textbox.Location = new System.Drawing.Point(422, 50);
            this.NotImportableCount_Textbox.Name = "NotImportableCount_Textbox";
            this.NotImportableCount_Textbox.Size = new System.Drawing.Size(100, 20);
            this.NotImportableCount_Textbox.TabIndex = 23;
            // 
            // ImportFieldsCount_Textbox
            // 
            this.ImportFieldsCount_Textbox.Location = new System.Drawing.Point(38, 50);
            this.ImportFieldsCount_Textbox.Name = "ImportFieldsCount_Textbox";
            this.ImportFieldsCount_Textbox.Size = new System.Drawing.Size(100, 20);
            this.ImportFieldsCount_Textbox.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(417, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Not Importable";
            // 
            // WrongFields_ListView
            // 
            this.WrongFields_ListView.Location = new System.Drawing.Point(398, 73);
            this.WrongFields_ListView.Name = "WrongFields_ListView";
            this.WrongFields_ListView.Size = new System.Drawing.Size(150, 220);
            this.WrongFields_ListView.TabIndex = 20;
            this.WrongFields_ListView.UseCompatibleStateImageBehavior = false;
            this.WrongFields_ListView.View = System.Windows.Forms.View.Details;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Loaded Table";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(245, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Selected Fields";
            // 
            // ImportTableName_Textbox
            // 
            this.ImportTableName_Textbox.Location = new System.Drawing.Point(256, 5);
            this.ImportTableName_Textbox.Name = "ImportTableName_Textbox";
            this.ImportTableName_Textbox.Size = new System.Drawing.Size(100, 20);
            this.ImportTableName_Textbox.TabIndex = 18;
            // 
            // UpdateRecs_Checkbox
            // 
            this.UpdateRecs_Checkbox.AutoSize = true;
            this.UpdateRecs_Checkbox.Location = new System.Drawing.Point(591, 8);
            this.UpdateRecs_Checkbox.Name = "UpdateRecs_Checkbox";
            this.UpdateRecs_Checkbox.Size = new System.Drawing.Size(195, 17);
            this.UpdateRecs_Checkbox.TabIndex = 1;
            this.UpdateRecs_Checkbox.Text = "Update Player Recs When Possible";
            this.UpdateRecs_Checkbox.UseVisualStyleBackColor = true;
            this.UpdateRecs_Checkbox.CheckedChanged += new System.EventHandler(this.UpdateRecs_Checkbox_CheckedChanged);
            // 
            // ImportCSV_Button
            // 
            this.ImportCSV_Button.Location = new System.Drawing.Point(30, 4);
            this.ImportCSV_Button.Name = "ImportCSV_Button";
            this.ImportCSV_Button.Size = new System.Drawing.Size(89, 23);
            this.ImportCSV_Button.TabIndex = 0;
            this.ImportCSV_Button.Text = "Load CSV";
            this.ImportCSV_Button.UseVisualStyleBackColor = true;
            this.ImportCSV_Button.Click += new System.EventHandler(this.ImportCSV_Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Importable Fields";
            // 
            // ImportAvailFields_ListView
            // 
            this.ImportAvailFields_ListView.Location = new System.Drawing.Point(17, 73);
            this.ImportAvailFields_ListView.Name = "ImportAvailFields_ListView";
            this.ImportAvailFields_ListView.Size = new System.Drawing.Size(150, 220);
            this.ImportAvailFields_ListView.TabIndex = 12;
            this.ImportAvailFields_ListView.UseCompatibleStateImageBehavior = false;
            this.ImportAvailFields_ListView.View = System.Windows.Forms.View.Details;
            // 
            // ImportSelected_ListView
            // 
            this.ImportSelected_ListView.Location = new System.Drawing.Point(229, 73);
            this.ImportSelected_ListView.Name = "ImportSelected_ListView";
            this.ImportSelected_ListView.Size = new System.Drawing.Size(150, 220);
            this.ImportSelected_ListView.TabIndex = 13;
            this.ImportSelected_ListView.UseCompatibleStateImageBehavior = false;
            this.ImportSelected_ListView.View = System.Windows.Forms.View.Details;
            // 
            // DraftClass_Panel
            // 
            this.DraftClass_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DraftClass_Panel.Controls.Add(this.label12);
            this.DraftClass_Panel.Controls.Add(this.DraftClassDescriptions_Checkbox);
            this.DraftClass_Panel.Controls.Add(this.CreateDraftClass_Button);
            this.DraftClass_Panel.Controls.Add(this.ExportDraftClass_Button);
            this.DraftClass_Panel.Controls.Add(this.LoadDraftClass_Button);
            this.DraftClass_Panel.Controls.Add(this.label10);
            this.DraftClass_Panel.Location = new System.Drawing.Point(5, 5);
            this.DraftClass_Panel.Name = "DraftClass_Panel";
            this.DraftClass_Panel.Size = new System.Drawing.Size(368, 117);
            this.DraftClass_Panel.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(188, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "^^ THIS TAKES TIME ^^";
            // 
            // DraftClassDescriptions_Checkbox
            // 
            this.DraftClassDescriptions_Checkbox.AutoSize = true;
            this.DraftClassDescriptions_Checkbox.Location = new System.Drawing.Point(9, 86);
            this.DraftClassDescriptions_Checkbox.Name = "DraftClassDescriptions_Checkbox";
            this.DraftClassDescriptions_Checkbox.Size = new System.Drawing.Size(147, 17);
            this.DraftClassDescriptions_Checkbox.TabIndex = 15;
            this.DraftClassDescriptions_Checkbox.Text = "Include Field Descriptions";
            this.DraftClassDescriptions_Checkbox.UseVisualStyleBackColor = true;
            // 
            // CreateDraftClass_Button
            // 
            this.CreateDraftClass_Button.Location = new System.Drawing.Point(173, 28);
            this.CreateDraftClass_Button.Name = "CreateDraftClass_Button";
            this.CreateDraftClass_Button.Size = new System.Drawing.Size(158, 23);
            this.CreateDraftClass_Button.TabIndex = 3;
            this.CreateDraftClass_Button.Text = "Create Draft Class from CSV";
            this.CreateDraftClass_Button.UseVisualStyleBackColor = true;
            this.CreateDraftClass_Button.Click += new System.EventHandler(this.CreateDraftClass_Button_Click);
            // 
            // ExportDraftClass_Button
            // 
            this.ExportDraftClass_Button.Location = new System.Drawing.Point(9, 57);
            this.ExportDraftClass_Button.Name = "ExportDraftClass_Button";
            this.ExportDraftClass_Button.Size = new System.Drawing.Size(158, 23);
            this.ExportDraftClass_Button.TabIndex = 2;
            this.ExportDraftClass_Button.Text = "Export to CSV";
            this.ExportDraftClass_Button.UseVisualStyleBackColor = true;
            this.ExportDraftClass_Button.Click += new System.EventHandler(this.ExportDraftClass_Button_Click);
            // 
            // LoadDraftClass_Button
            // 
            this.LoadDraftClass_Button.Location = new System.Drawing.Point(9, 28);
            this.LoadDraftClass_Button.Name = "LoadDraftClass_Button";
            this.LoadDraftClass_Button.Size = new System.Drawing.Size(158, 23);
            this.LoadDraftClass_Button.TabIndex = 1;
            this.LoadDraftClass_Button.Text = "Load Draft Class";
            this.LoadDraftClass_Button.UseVisualStyleBackColor = true;
            this.LoadDraftClass_Button.Click += new System.EventHandler(this.LoadDraftClass_Button_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Madden v2019 Draft Class";
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DraftClass_Panel);
            this.Controls.Add(this.Import_Panel);
            this.Controls.Add(this.Export_Panel);
            this.Controls.Add(this.ExportFilter_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import/Export CSV";
            this.ExportFilter_Panel.ResumeLayout(false);
            this.ExportFilter_Panel.PerformLayout();
            this.Export_Panel.ResumeLayout(false);
            this.Export_Panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Import_Panel.ResumeLayout(false);
            this.Import_Panel.PerformLayout();
            this.DraftClass_Panel.ResumeLayout(false);
            this.DraftClass_Panel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox ExportFilter_Panel;
        private System.Windows.Forms.Button ExportButton;
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
        private System.Windows.Forms.Panel Import_Panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddFields_Button;
        private System.Windows.Forms.CheckBox UpdateRecs_Checkbox;
        private System.Windows.Forms.Button ImportCSV_Button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ImportTableName_Textbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView ImportAvailFields_ListView;
        private System.Windows.Forms.ListView ImportSelected_ListView;
        private System.Windows.Forms.ListView ExportFields_ListView;
        private System.Windows.Forms.ListView AvailFields_ListView;
        private System.Windows.Forms.Button RemoveFields_Button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView WrongFields_ListView;
        private System.Windows.Forms.TextBox ImportFieldsCount_Textbox;
        private System.Windows.Forms.TextBox NotImportableCount_Textbox;
        private System.Windows.Forms.Button ProcessRecords_Button;
        private System.Windows.Forms.CheckBox DeleteCurrentRecs_Checkbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView ImportErrors_Listview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox Descriptions_Checkbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel DraftClass_Panel;
        private System.Windows.Forms.Button CreateDraftClass_Button;
        private System.Windows.Forms.Button ExportDraftClass_Button;
        private System.Windows.Forms.Button LoadDraftClass_Button;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox DraftClassDescriptions_Checkbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox ExtractByTableName;
	}
}