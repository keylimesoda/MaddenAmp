namespace MaddenEditor.Forms
{
	partial class ScheduleEditingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleEditingForm));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.gbWeekNav = new System.Windows.Forms.GroupBox();
			this.btnPreviousWeek = new System.Windows.Forms.Button();
			this.btnNextWeek = new System.Windows.Forms.Button();
			this.cbWeekSelector = new System.Windows.Forms.ComboBox();
			this.dgScheduleView = new System.Windows.Forms.DataGridView();
			this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.lblTitle = new System.Windows.Forms.Label();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.gbWeekNav.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgScheduleView)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.gbWeekNav);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.dgScheduleView);
			this.splitContainer.Panel2.Controls.Add(this.lblTitle);
			this.splitContainer.Size = new System.Drawing.Size(792, 540);
			this.splitContainer.SplitterDistance = 170;
			this.splitContainer.TabIndex = 0;
			this.splitContainer.Text = "splitContainer1";
			// 
			// gbWeekNav
			// 
			this.gbWeekNav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbWeekNav.Controls.Add(this.btnPreviousWeek);
			this.gbWeekNav.Controls.Add(this.btnNextWeek);
			this.gbWeekNav.Controls.Add(this.cbWeekSelector);
			this.gbWeekNav.Location = new System.Drawing.Point(4, 11);
			this.gbWeekNav.Name = "gbWeekNav";
			this.gbWeekNav.Size = new System.Drawing.Size(161, 52);
			this.gbWeekNav.TabIndex = 3;
			this.gbWeekNav.TabStop = false;
			this.gbWeekNav.Text = "Week Navigator";
			// 
			// btnPreviousWeek
			// 
			this.btnPreviousWeek.Location = new System.Drawing.Point(6, 19);
			this.btnPreviousWeek.Name = "btnPreviousWeek";
			this.btnPreviousWeek.Size = new System.Drawing.Size(28, 23);
			this.btnPreviousWeek.TabIndex = 0;
			this.btnPreviousWeek.Text = "<<";
			this.btnPreviousWeek.Click += new System.EventHandler(this.btnPreviousWeek_Click);
			// 
			// btnNextWeek
			// 
			this.btnNextWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextWeek.Location = new System.Drawing.Point(120, 19);
			this.btnNextWeek.Name = "btnNextWeek";
			this.btnNextWeek.Size = new System.Drawing.Size(33, 23);
			this.btnNextWeek.TabIndex = 1;
			this.btnNextWeek.Text = ">>";
			this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
			// 
			// cbWeekSelector
			// 
			this.cbWeekSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbWeekSelector.FormattingEnabled = true;
			this.cbWeekSelector.Location = new System.Drawing.Point(40, 19);
			this.cbWeekSelector.Name = "cbWeekSelector";
			this.cbWeekSelector.Size = new System.Drawing.Size(74, 21);
			this.cbWeekSelector.TabIndex = 2;
			this.cbWeekSelector.SelectedIndexChanged += new System.EventHandler(this.cbWeekSelector_SelectedIndexChanged);
			// 
			// dgScheduleView
			// 
			this.dgScheduleView.AllowUserToAddRows = false;
			this.dgScheduleView.AllowUserToDeleteRows = false;
			this.dgScheduleView.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			this.dgScheduleView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgScheduleView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dgScheduleView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgScheduleView.Columns.Add(this.dataGridViewComboBoxColumn1);
			this.dgScheduleView.Columns.Add(this.dataGridViewComboBoxColumn2);
			this.dgScheduleView.Columns.Add(this.dataGridViewTextBoxColumn1);
			this.dgScheduleView.Columns.Add(this.dataGridViewComboBoxColumn3);
			this.dgScheduleView.Columns.Add(this.dataGridViewTextBoxColumn2);
			this.dgScheduleView.Columns.Add(this.dataGridViewCheckBoxColumn1);
			this.dgScheduleView.Columns.Add(this.dataGridViewComboBoxColumn4);
			this.dgScheduleView.Columns.Add(this.dataGridViewComboBoxColumn5);
			this.dgScheduleView.Location = new System.Drawing.Point(3, 44);
			this.dgScheduleView.Name = "dgScheduleView";
			this.dgScheduleView.RowHeadersVisible = false;
			this.dgScheduleView.Size = new System.Drawing.Size(610, 491);
			this.dgScheduleView.TabIndex = 1;
			this.dgScheduleView.Text = "dataGridView1";
			this.dgScheduleView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgScheduleView_CellEndEdit);
			// 
			// dataGridViewComboBoxColumn1
			// 
			this.dataGridViewComboBoxColumn1.HeaderText = "State";
			this.dataGridViewComboBoxColumn1.Name = "GameState";
			this.dataGridViewComboBoxColumn1.Width = 76;
			// 
			// dataGridViewComboBoxColumn2
			// 
			this.dataGridViewComboBoxColumn2.HeaderText = "Home Team";
			this.dataGridViewComboBoxColumn2.Name = "HomeTeam";
			this.dataGridViewComboBoxColumn2.Width = 76;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Score";
			this.dataGridViewTextBoxColumn1.Name = "HomeTeamScore";
			// 
			// dataGridViewComboBoxColumn3
			// 
			this.dataGridViewComboBoxColumn3.HeaderText = "Away Team";
			this.dataGridViewComboBoxColumn3.Name = "AwayTeam";
			this.dataGridViewComboBoxColumn3.Width = 76;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "Score";
			this.dataGridViewTextBoxColumn2.Name = "AwayTeamScore";
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.HeaderText = "Overtime";
			this.dataGridViewCheckBoxColumn1.Name = "OverTime";
			// 
			// dataGridViewComboBoxColumn4
			// 
			this.dataGridViewComboBoxColumn4.HeaderText = "Day";
			this.dataGridViewComboBoxColumn4.Name = "GameDayType";
			this.dataGridViewComboBoxColumn4.Width = 76;
			// 
			// dataGridViewComboBoxColumn5
			// 
			this.dataGridViewComboBoxColumn5.HeaderText = "Weighting";
			this.dataGridViewComboBoxColumn5.Name = "Weighting";
			this.dataGridViewComboBoxColumn5.Width = 76;
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(271, 11);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(75, 24);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Week 1";
			// 
			// ScheduleEditingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 540);
			this.Controls.Add(this.splitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScheduleEditingForm";
			this.Text = "Schedule Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScheduleEditingForm_FormClosing);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			this.gbWeekNav.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgScheduleView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.GroupBox gbWeekNav;
		private System.Windows.Forms.Button btnPreviousWeek;
		private System.Windows.Forms.Button btnNextWeek;
		private System.Windows.Forms.ComboBox cbWeekSelector;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.DataGridView dgScheduleView;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
	}
}