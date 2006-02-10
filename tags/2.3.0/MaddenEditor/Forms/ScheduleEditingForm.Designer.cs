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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnApply = new System.Windows.Forms.Button();
			this.dgScheduleView = new System.Windows.Forms.DataGridView();
			this.GameState = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.HomeTeam = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.HomeTeamScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AwayTeam = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.AwayTeamScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.OverTime = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.GameDayType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Weighting = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.lblTitle = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.gbWeekNav.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgScheduleView)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
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
			this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
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
			this.gbWeekNav.Size = new System.Drawing.Size(161, 77);
			this.gbWeekNav.TabIndex = 3;
			this.gbWeekNav.TabStop = false;
			this.gbWeekNav.Text = "Week Navigator";
			// 
			// btnPreviousWeek
			// 
			this.btnPreviousWeek.Location = new System.Drawing.Point(7, 19);
			this.btnPreviousWeek.Name = "btnPreviousWeek";
			this.btnPreviousWeek.Size = new System.Drawing.Size(73, 23);
			this.btnPreviousWeek.TabIndex = 0;
			this.btnPreviousWeek.Text = "<<";
			this.btnPreviousWeek.Click += new System.EventHandler(this.btnPreviousWeek_Click);
			// 
			// btnNextWeek
			// 
			this.btnNextWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextWeek.Location = new System.Drawing.Point(81, 19);
			this.btnNextWeek.Name = "btnNextWeek";
			this.btnNextWeek.Size = new System.Drawing.Size(73, 23);
			this.btnNextWeek.TabIndex = 1;
			this.btnNextWeek.Text = ">>";
			this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
			// 
			// cbWeekSelector
			// 
			this.cbWeekSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbWeekSelector.FormattingEnabled = true;
			this.cbWeekSelector.Location = new System.Drawing.Point(7, 48);
			this.cbWeekSelector.Name = "cbWeekSelector";
			this.cbWeekSelector.Size = new System.Drawing.Size(146, 21);
			this.cbWeekSelector.TabIndex = 2;
			this.cbWeekSelector.SelectedIndexChanged += new System.EventHandler(this.cbWeekSelector_SelectedIndexChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.dgScheduleView, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 538);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(451, 3);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 26);
			this.btnApply.TabIndex = 0;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// dgScheduleView
			// 
			this.dgScheduleView.AllowUserToAddRows = false;
			this.dgScheduleView.AllowUserToDeleteRows = false;
			this.dgScheduleView.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			this.dgScheduleView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgScheduleView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgScheduleView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GameState,
            this.HomeTeam,
            this.HomeTeamScore,
            this.AwayTeam,
            this.AwayTeamScore,
            this.OverTime,
            this.GameDayType,
            this.Weighting});
			this.dgScheduleView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgScheduleView.Location = new System.Drawing.Point(3, 38);
			this.dgScheduleView.Name = "dgScheduleView";
			this.dgScheduleView.RowHeadersVisible = false;
			this.dgScheduleView.Size = new System.Drawing.Size(610, 462);
			this.dgScheduleView.TabIndex = 1;
			this.dgScheduleView.Text = "dataGridView1";
			this.dgScheduleView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgScheduleView_CellEndEdit);
			// 
			// GameState
			// 
			this.GameState.HeaderText = "State";
			this.GameState.Name = "GameState";
			// 
			// HomeTeam
			// 
			this.HomeTeam.HeaderText = "Home Team";
			this.HomeTeam.Name = "HomeTeam";
			// 
			// HomeTeamScore
			// 
			this.HomeTeamScore.HeaderText = "Score";
			this.HomeTeamScore.Name = "HomeTeamScore";
			// 
			// AwayTeam
			// 
			this.AwayTeam.HeaderText = "Away Team";
			this.AwayTeam.Name = "AwayTeam";
			// 
			// AwayTeamScore
			// 
			this.AwayTeamScore.HeaderText = "Score";
			this.AwayTeamScore.Name = "AwayTeamScore";
			// 
			// OverTime
			// 
			this.OverTime.HeaderText = "Overtime";
			this.OverTime.Name = "OverTime";
			// 
			// GameDayType
			// 
			this.GameDayType.HeaderText = "Day";
			this.GameDayType.Name = "GameDayType";
			// 
			// Weighting
			// 
			this.Weighting.HeaderText = "Weighting";
			this.Weighting.Name = "Weighting";
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.Location = new System.Drawing.Point(3, 5);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(610, 24);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Week 1";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Controls.Add(this.btnApply);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 506);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(610, 29);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(532, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ScheduleEditingForm
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(792, 540);
			this.Controls.Add(this.splitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScheduleEditingForm";
			this.Text = "Schedule Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScheduleEditingForm_FormClosing);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.gbWeekNav.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgScheduleView)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
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
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.DataGridViewComboBoxColumn GameState;
		private System.Windows.Forms.DataGridViewComboBoxColumn HomeTeam;
		private System.Windows.Forms.DataGridViewTextBoxColumn HomeTeamScore;
		private System.Windows.Forms.DataGridViewComboBoxColumn AwayTeam;
		private System.Windows.Forms.DataGridViewTextBoxColumn AwayTeamScore;
		private System.Windows.Forms.DataGridViewCheckBoxColumn OverTime;
		private System.Windows.Forms.DataGridViewComboBoxColumn GameDayType;
		private System.Windows.Forms.DataGridViewComboBoxColumn Weighting;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnCancel;
	}
}