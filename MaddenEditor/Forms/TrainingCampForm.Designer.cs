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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingCampForm));
            this.TrainingTime = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SelectTeam = new System.Windows.Forms.GroupBox();
            this.selectHumanTeam = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
            this.depthChartDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ActivityGrd = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AllocateByCmb = new System.Windows.Forms.ComboBox();
            this.ActivityCmb = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ActivityLbl = new System.Windows.Forms.Label();
            this.SetTimeGrd = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SelectTeam.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActivityGrd)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetTimeGrd)).BeginInit();
            this.groupBox5.SuspendLayout();
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
            this.SelectTeam.Location = new System.Drawing.Point(4, 6);
            this.SelectTeam.Name = "SelectTeam";
            this.SelectTeam.Size = new System.Drawing.Size(133, 49);
            this.SelectTeam.TabIndex = 0;
            this.SelectTeam.TabStop = false;
            this.SelectTeam.Text = "Select Human Team...";
            // 
            // selectHumanTeam
            // 
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
            this.groupBox2.Location = new System.Drawing.Point(143, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(102, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Position...";
            // 
            // filterPositionComboBox
            // 
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
            this.depthChartDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.depthChartDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.depthChartDataGrid.Location = new System.Drawing.Point(6, 60);
            this.depthChartDataGrid.Name = "depthChartDataGrid";
            this.depthChartDataGrid.ReadOnly = true;
            this.depthChartDataGrid.RowHeadersVisible = false;
            this.depthChartDataGrid.Size = new System.Drawing.Size(1001, 133);
            this.depthChartDataGrid.TabIndex = 10;
            this.depthChartDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.depthChartDataGrid_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ActivityGrd);
            this.groupBox1.Location = new System.Drawing.Point(6, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 422);
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
            this.ActivityGrd.Size = new System.Drawing.Size(572, 397);
            this.ActivityGrd.TabIndex = 0;
            this.ActivityGrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ActivityGrd_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AllocateByCmb);
            this.groupBox3.Controls.Add(this.ActivityCmb);
            this.groupBox3.Location = new System.Drawing.Point(251, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 48);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Activity Type...                        Allocate by...";
            // 
            // AllocateByCmb
            // 
            this.AllocateByCmb.Enabled = false;
            this.AllocateByCmb.FormattingEnabled = true;
            this.AllocateByCmb.Location = new System.Drawing.Point(138, 19);
            this.AllocateByCmb.Name = "AllocateByCmb";
            this.AllocateByCmb.Size = new System.Drawing.Size(97, 21);
            this.AllocateByCmb.TabIndex = 1;
            // 
            // ActivityCmb
            // 
            this.ActivityCmb.FormattingEnabled = true;
            this.ActivityCmb.Location = new System.Drawing.Point(6, 19);
            this.ActivityCmb.Name = "ActivityCmb";
            this.ActivityCmb.Size = new System.Drawing.Size(115, 21);
            this.ActivityCmb.TabIndex = 0;
            this.ActivityCmb.SelectedIndexChanged += new System.EventHandler(this.ActivityCmb_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ActivityLbl);
            this.groupBox4.Controls.Add(this.SetTimeGrd);
            this.groupBox4.Location = new System.Drawing.Point(594, 199);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 208);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Set Time...";
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
            this.SetTimeGrd.Size = new System.Drawing.Size(289, 149);
            this.SetTimeGrd.TabIndex = 0;
            this.SetTimeGrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SetTimeGrd_CellClick);
            this.SetTimeGrd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SetTimeGrd_CellEndEdit);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Location = new System.Drawing.Point(594, 409);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(253, 212);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Player Breakdown...";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(241, 187);
            this.textBox1.TabIndex = 0;
            // 
            // TrainingCampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 629);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.depthChartDataGrid);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SelectTeam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox selectHumanTeam;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrainingTime;
        private System.Windows.Forms.GroupBox SelectTeam;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox filterPositionComboBox;
        private System.Windows.Forms.DataGridView depthChartDataGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox ActivityCmb;
        private System.Windows.Forms.ComboBox AllocateByCmb;
        private System.Windows.Forms.DataGridView ActivityGrd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView SetTimeGrd;
        private System.Windows.Forms.Label ActivityLbl;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox1;
    }
}