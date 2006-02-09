namespace MaddenEditor.Forms
{
    partial class TrainingCampForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingCampForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TrainingTime = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SelectTeam = new System.Windows.Forms.GroupBox();
            this.selectHumanTeam = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
            this.depthChartDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ActivityGrd = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ActivityCmb = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GroupAssign = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ActivityLbl = new System.Windows.Forms.Label();
            this.SetTimeGrd = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AdvanceBtn = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.massCmb = new System.Windows.Forms.ComboBox();
            this.enableMassChk = new System.Windows.Forms.CheckBox();
            this.defChk = new System.Windows.Forms.CheckBox();
            this.offChk = new System.Windows.Forms.CheckBox();
            this.teamChk = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cPUSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simCPUCampsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectTeam.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActivityGrd)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetTimeGrd)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrainingTime
            // 
            this.TrainingTime.HeaderText = "Change % To";
            this.TrainingTime.Name = "TrainingTime";
            this.TrainingTime.Width = 72;
            // 
            // SelectTeam
            // 
            this.SelectTeam.Controls.Add(this.selectHumanTeam);
            this.SelectTeam.Location = new System.Drawing.Point(12, 36);
            this.SelectTeam.Name = "SelectTeam";
            this.SelectTeam.Size = new System.Drawing.Size(133, 49);
            this.SelectTeam.TabIndex = 0;
            this.SelectTeam.TabStop = false;
            this.SelectTeam.Text = "Select Human Team...";
            // 
            // selectHumanTeam
            // 
            this.selectHumanTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectHumanTeam.FormattingEnabled = true;
            this.selectHumanTeam.Location = new System.Drawing.Point(6, 19);
            this.selectHumanTeam.Name = "selectHumanTeam";
            this.selectHumanTeam.Size = new System.Drawing.Size(121, 21);
            this.selectHumanTeam.TabIndex = 0;
            this.selectHumanTeam.SelectedIndexChanged += new System.EventHandler(this.selectHumanTeam_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.filterPositionComboBox);
            this.groupBox2.Location = new System.Drawing.Point(151, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(102, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Position...";
            // 
            // filterPositionComboBox
            // 
            this.filterPositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPositionComboBox.Enabled = false;
            this.filterPositionComboBox.FormattingEnabled = true;
            this.filterPositionComboBox.Location = new System.Drawing.Point(6, 19);
            this.filterPositionComboBox.Name = "filterPositionComboBox";
            this.filterPositionComboBox.Size = new System.Drawing.Size(89, 21);
            this.filterPositionComboBox.TabIndex = 0;
            this.filterPositionComboBox.SelectedIndexChanged += new System.EventHandler(this.filterPositionComboBox_SelectedIndexChanged);
            // 
            // depthChartDataGrid
            // 
            this.depthChartDataGrid.AllowUserToAddRows = false;
            this.depthChartDataGrid.AllowUserToDeleteRows = false;
            this.depthChartDataGrid.AllowUserToResizeColumns = false;
            this.depthChartDataGrid.AllowUserToResizeRows = false;
            this.depthChartDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.depthChartDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.depthChartDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.depthChartDataGrid.Location = new System.Drawing.Point(12, 91);
            this.depthChartDataGrid.MultiSelect = false;
            this.depthChartDataGrid.Name = "depthChartDataGrid";
            this.depthChartDataGrid.ReadOnly = true;
            this.depthChartDataGrid.RowHeadersVisible = false;
            this.depthChartDataGrid.Size = new System.Drawing.Size(1001, 133);
            this.depthChartDataGrid.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ActivityGrd);
            this.groupBox1.Location = new System.Drawing.Point(445, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 327);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activity Name...";
            // 
            // ActivityGrd
            // 
            this.ActivityGrd.AllowUserToAddRows = false;
            this.ActivityGrd.AllowUserToDeleteRows = false;
            this.ActivityGrd.AllowUserToResizeColumns = false;
            this.ActivityGrd.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.ActivityGrd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.ActivityGrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivityGrd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ActivityGrd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActivityGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ActivityGrd.Location = new System.Drawing.Point(6, 19);
            this.ActivityGrd.MultiSelect = false;
            this.ActivityGrd.Name = "ActivityGrd";
            this.ActivityGrd.ReadOnly = true;
            this.ActivityGrd.RowHeadersVisible = false;
            this.ActivityGrd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ActivityGrd.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.ActivityGrd.RowTemplate.ReadOnly = true;
            this.ActivityGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ActivityGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ActivityGrd.Size = new System.Drawing.Size(562, 302);
            this.ActivityGrd.TabIndex = 0;
            this.toolTip1.SetToolTip(this.ActivityGrd, resources.GetString("ActivityGrd.ToolTip"));
            this.ActivityGrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ActivityGrd_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ActivityCmb);
            this.groupBox3.Location = new System.Drawing.Point(259, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 48);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Activity Type...    ";
            // 
            // ActivityCmb
            // 
            this.ActivityCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActivityCmb.Enabled = false;
            this.ActivityCmb.FormattingEnabled = true;
            this.ActivityCmb.Location = new System.Drawing.Point(6, 19);
            this.ActivityCmb.Name = "ActivityCmb";
            this.ActivityCmb.Size = new System.Drawing.Size(115, 21);
            this.ActivityCmb.TabIndex = 0;
            this.ActivityCmb.SelectedIndexChanged += new System.EventHandler(this.ActivityCmb_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.GroupAssign);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.ActivityLbl);
            this.groupBox4.Controls.Add(this.SetTimeGrd);
            this.groupBox4.Location = new System.Drawing.Point(12, 230);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(427, 208);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Set Time...";
            // 
            // GroupAssign
            // 
            this.GroupAssign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GroupAssign.FormattingEnabled = true;
            this.GroupAssign.Location = new System.Drawing.Point(320, 87);
            this.GroupAssign.MaxDropDownItems = 11;
            this.GroupAssign.Name = "GroupAssign";
            this.GroupAssign.Size = new System.Drawing.Size(50, 21);
            this.GroupAssign.TabIndex = 3;
            this.GroupAssign.SelectedIndexChanged += new System.EventHandler(this.GroupAssign_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(317, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 34);
            this.label2.TabIndex = 2;
            // 
            // ActivityLbl
            // 
            this.ActivityLbl.AutoSize = true;
            this.ActivityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivityLbl.ForeColor = System.Drawing.Color.Red;
            this.ActivityLbl.Location = new System.Drawing.Point(18, 19);
            this.ActivityLbl.Name = "ActivityLbl";
            this.ActivityLbl.Size = new System.Drawing.Size(0, 24);
            this.ActivityLbl.TabIndex = 1;
            // 
            // SetTimeGrd
            // 
            this.SetTimeGrd.AllowUserToAddRows = false;
            this.SetTimeGrd.AllowUserToDeleteRows = false;
            this.SetTimeGrd.AllowUserToResizeColumns = false;
            this.SetTimeGrd.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SetTimeGrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SetTimeGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SetTimeGrd.DefaultCellStyle = dataGridViewCellStyle5;
            this.SetTimeGrd.Location = new System.Drawing.Point(6, 50);
            this.SetTimeGrd.MultiSelect = false;
            this.SetTimeGrd.Name = "SetTimeGrd";
            this.SetTimeGrd.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SetTimeGrd.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.SetTimeGrd.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SetTimeGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.SetTimeGrd.Size = new System.Drawing.Size(308, 149);
            this.SetTimeGrd.TabIndex = 0;
            this.SetTimeGrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SetTimeGrd_CellClick);
            this.SetTimeGrd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SetTimeGrd_CellEndEdit);
            this.SetTimeGrd.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.SetTimeGrd_CellToolTipTextNeeded);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Location = new System.Drawing.Point(12, 444);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(253, 113);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Player Breakdown...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(241, 86);
            this.textBox1.TabIndex = 0;
            // 
            // AdvanceBtn
            // 
            this.AdvanceBtn.Location = new System.Drawing.Point(6, 16);
            this.AdvanceBtn.Name = "AdvanceBtn";
            this.AdvanceBtn.Size = new System.Drawing.Size(69, 24);
            this.AdvanceBtn.TabIndex = 15;
            this.AdvanceBtn.Text = "Advance...";
            this.toolTip1.SetToolTip(this.AdvanceBtn, resources.GetString("AdvanceBtn.ToolTip"));
            this.AdvanceBtn.UseVisualStyleBackColor = true;
            this.AdvanceBtn.Click += new System.EventHandler(this.AdvanceBtn_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.AdvanceBtn);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(398, 36);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(615, 49);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Advance...";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(81, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 23);
            this.label1.TabIndex = 16;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.massCmb);
            this.groupBox7.Controls.Add(this.enableMassChk);
            this.groupBox7.Controls.Add(this.defChk);
            this.groupBox7.Controls.Add(this.offChk);
            this.groupBox7.Controls.Add(this.teamChk);
            this.groupBox7.Enabled = false;
            this.groupBox7.Location = new System.Drawing.Point(271, 444);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(168, 113);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Mass Conditioning Allocation...";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Set all checked to:";
            // 
            // massCmb
            // 
            this.massCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.massCmb.FormattingEnabled = true;
            this.massCmb.Location = new System.Drawing.Point(103, 59);
            this.massCmb.Name = "massCmb";
            this.massCmb.Size = new System.Drawing.Size(50, 21);
            this.massCmb.TabIndex = 5;
            this.massCmb.SelectedIndexChanged += new System.EventHandler(this.massCmb_SelectedIndexChanged);
            // 
            // enableMassChk
            // 
            this.enableMassChk.AutoSize = true;
            this.enableMassChk.Location = new System.Drawing.Point(6, 18);
            this.enableMassChk.Name = "enableMassChk";
            this.enableMassChk.Size = new System.Drawing.Size(59, 17);
            this.enableMassChk.TabIndex = 3;
            this.enableMassChk.Text = "Enable";
            this.enableMassChk.UseVisualStyleBackColor = true;
            this.enableMassChk.CheckedChanged += new System.EventHandler(this.enableMassChk_CheckedChanged);
            // 
            // defChk
            // 
            this.defChk.AutoSize = true;
            this.defChk.Location = new System.Drawing.Point(6, 86);
            this.defChk.Name = "defChk";
            this.defChk.Size = new System.Drawing.Size(94, 17);
            this.defChk.TabIndex = 2;
            this.defChk.Text = "Entire defense";
            this.defChk.UseVisualStyleBackColor = true;
            this.defChk.CheckedChanged += new System.EventHandler(this.defChk_CheckedChanged);
            // 
            // offChk
            // 
            this.offChk.AutoSize = true;
            this.offChk.Location = new System.Drawing.Point(6, 63);
            this.offChk.Name = "offChk";
            this.offChk.Size = new System.Drawing.Size(91, 17);
            this.offChk.TabIndex = 1;
            this.offChk.Text = "Entire offense";
            this.offChk.UseVisualStyleBackColor = true;
            this.offChk.CheckedChanged += new System.EventHandler(this.offChk_CheckedChanged);
            // 
            // teamChk
            // 
            this.teamChk.AutoSize = true;
            this.teamChk.Location = new System.Drawing.Point(6, 39);
            this.teamChk.Name = "teamChk";
            this.teamChk.Size = new System.Drawing.Size(73, 17);
            this.teamChk.TabIndex = 0;
            this.teamChk.Text = "All players";
            this.teamChk.UseVisualStyleBackColor = true;
            this.teamChk.CheckedChanged += new System.EventHandler(this.teamChk_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cPUSimulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1032, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cPUSimulationToolStripMenuItem
            // 
            this.cPUSimulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simCPUCampsToolStripMenuItem});
            this.cPUSimulationToolStripMenuItem.Name = "cPUSimulationToolStripMenuItem";
            this.cPUSimulationToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.cPUSimulationToolStripMenuItem.Text = "CPU Simulation...";
            // 
            // simCPUCampsToolStripMenuItem
            // 
            this.simCPUCampsToolStripMenuItem.Name = "simCPUCampsToolStripMenuItem";
            this.simCPUCampsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.simCPUCampsToolStripMenuItem.Text = "Sim CPU Camps...";
            this.simCPUCampsToolStripMenuItem.Click += new System.EventHandler(this.simCPUCampsToolStripMenuItem_Click);
            // 
            // TrainingCampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 565);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.depthChartDataGrid);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SelectTeam);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TrainingCampForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Camp Utility...";
            this.Load += new System.EventHandler(this.TrainingCampForm_Load);
            this.SelectTeam.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActivityGrd)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetTimeGrd)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox selectHumanTeam;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrainingTime;
        private System.Windows.Forms.GroupBox SelectTeam;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox filterPositionComboBox;
        private System.Windows.Forms.DataGridView depthChartDataGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox ActivityCmb;
        private System.Windows.Forms.DataGridView ActivityGrd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView SetTimeGrd;
        private System.Windows.Forms.Label ActivityLbl;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button AdvanceBtn;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox GroupAssign;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.CheckBox enableMassChk;
        public System.Windows.Forms.CheckBox defChk;
        public System.Windows.Forms.CheckBox offChk;
        public System.Windows.Forms.CheckBox teamChk;
        public System.Windows.Forms.ComboBox massCmb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cPUSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simCPUCampsToolStripMenuItem;
    }
}