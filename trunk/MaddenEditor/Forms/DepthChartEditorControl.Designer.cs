namespace MaddenEditor.Forms
{
	partial class DepthChartEditorControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepthChartEditorControl));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.positionCombo = new System.Windows.Forms.ComboBox();
			this.teamCombo = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.applyButton = new System.Windows.Forms.Button();
			this.eraseButton = new System.Windows.Forms.Button();
			this.transferButton = new System.Windows.Forms.Button();
			this.depthOrderDownButton = new System.Windows.Forms.Button();
			this.depthOrderUpButton = new System.Windows.Forms.Button();
			this.availablePlayerDatagrid = new System.Windows.Forms.DataGridView();
			this.TeamPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TeamPlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TeamOverall = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TeamPlayerObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.teamDepthChartLabel = new System.Windows.Forms.Label();
			this.depthChartDataGrid = new System.Windows.Forms.DataGridView();
			this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Overall = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayerObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DepthChartObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.availablePlayerDatagrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).BeginInit();
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
			this.splitContainer.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.splitContainer.Panel1.Controls.Add(this.positionCombo);
			this.splitContainer.Panel1.Controls.Add(this.teamCombo);
			this.splitContainer.Panel1.Controls.Add(this.label2);
			this.splitContainer.Panel1.Controls.Add(this.label1);
			this.splitContainer.Panel1MinSize = 100;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.applyButton);
			this.splitContainer.Panel2.Controls.Add(this.eraseButton);
			this.splitContainer.Panel2.Controls.Add(this.transferButton);
			this.splitContainer.Panel2.Controls.Add(this.depthOrderDownButton);
			this.splitContainer.Panel2.Controls.Add(this.depthOrderUpButton);
			this.splitContainer.Panel2.Controls.Add(this.availablePlayerDatagrid);
			this.splitContainer.Panel2.Controls.Add(this.teamDepthChartLabel);
			this.splitContainer.Panel2.Controls.Add(this.depthChartDataGrid);
			this.splitContainer.Size = new System.Drawing.Size(662, 482);
			this.splitContainer.SplitterDistance = 142;
			this.splitContainer.TabIndex = 2;
			this.splitContainer.Text = "splitContainer1";
			// 
			// positionCombo
			// 
			this.positionCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.positionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.positionCombo.FormattingEnabled = true;
			this.positionCombo.Location = new System.Drawing.Point(4, 90);
			this.positionCombo.Name = "positionCombo";
			this.positionCombo.Size = new System.Drawing.Size(133, 21);
			this.positionCombo.TabIndex = 3;
			this.positionCombo.SelectedIndexChanged += new System.EventHandler(this.positionCombo_SelectedIndexChanged);
			// 
			// teamCombo
			// 
			this.teamCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.teamCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.teamCombo.FormattingEnabled = true;
			this.teamCombo.Location = new System.Drawing.Point(4, 37);
			this.teamCombo.Name = "teamCombo";
			this.teamCombo.Size = new System.Drawing.Size(133, 21);
			this.teamCombo.TabIndex = 2;
			this.teamCombo.SelectedIndexChanged += new System.EventHandler(this.teamCombo_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Position";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Team";
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(436, 454);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 9;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// eraseButton
			// 
			this.eraseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("eraseButton.BackgroundImage")));
			this.eraseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.eraseButton.Location = new System.Drawing.Point(3, 145);
			this.eraseButton.Name = "eraseButton";
			this.eraseButton.Size = new System.Drawing.Size(32, 33);
			this.eraseButton.TabIndex = 8;
			this.eraseButton.Click += new System.EventHandler(this.eraseButton_Click);
			// 
			// transferButton
			// 
			this.transferButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.transferButton.Location = new System.Drawing.Point(411, 185);
			this.transferButton.Name = "transferButton";
			this.transferButton.Size = new System.Drawing.Size(83, 30);
			this.transferButton.TabIndex = 7;
			this.transferButton.Text = "Transfer";
			this.transferButton.Click += new System.EventHandler(this.transferButton_Click);
			// 
			// depthOrderDownButton
			// 
			this.depthOrderDownButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("depthOrderDownButton.BackgroundImage")));
			this.depthOrderDownButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.depthOrderDownButton.Location = new System.Drawing.Point(3, 99);
			this.depthOrderDownButton.Name = "depthOrderDownButton";
			this.depthOrderDownButton.Size = new System.Drawing.Size(32, 33);
			this.depthOrderDownButton.TabIndex = 6;
			this.depthOrderDownButton.Click += new System.EventHandler(this.depthOrderDownButton_Click);
			// 
			// depthOrderUpButton
			// 
			this.depthOrderUpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("depthOrderUpButton.BackgroundImage")));
			this.depthOrderUpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.depthOrderUpButton.Location = new System.Drawing.Point(3, 53);
			this.depthOrderUpButton.Name = "depthOrderUpButton";
			this.depthOrderUpButton.Size = new System.Drawing.Size(32, 33);
			this.depthOrderUpButton.TabIndex = 5;
			this.depthOrderUpButton.Click += new System.EventHandler(this.depthOrderUpButton_Click);
			// 
			// availablePlayerDatagrid
			// 
			this.availablePlayerDatagrid.AllowDrop = true;
			this.availablePlayerDatagrid.AllowUserToAddRows = false;
			this.availablePlayerDatagrid.AllowUserToDeleteRows = false;
			this.availablePlayerDatagrid.AllowUserToResizeColumns = false;
			this.availablePlayerDatagrid.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			this.availablePlayerDatagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.availablePlayerDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.availablePlayerDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.availablePlayerDatagrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.availablePlayerDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.availablePlayerDatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TeamPosition,
            this.TeamPlayerName,
            this.TeamOverall,
            this.TeamPlayerObject});
			this.availablePlayerDatagrid.Location = new System.Drawing.Point(3, 221);
			this.availablePlayerDatagrid.MultiSelect = false;
			this.availablePlayerDatagrid.Name = "availablePlayerDatagrid";
			this.availablePlayerDatagrid.ReadOnly = true;
			this.availablePlayerDatagrid.RowHeadersVisible = false;
			this.availablePlayerDatagrid.Size = new System.Drawing.Size(508, 227);
			this.availablePlayerDatagrid.TabIndex = 2;
			this.availablePlayerDatagrid.Text = "dataGridView2";
			this.availablePlayerDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.availablePlayerDatagrid_CellClick);
			this.availablePlayerDatagrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.availablePlayerDatagrid_CellMouseUp);
			// 
			// TeamPosition
			// 
			this.TeamPosition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.TeamPosition.Frozen = true;
			this.TeamPosition.HeaderText = "Position";
			this.TeamPosition.Name = "TeamPosition";
			this.TeamPosition.ReadOnly = true;
			this.TeamPosition.Width = 50;
			// 
			// TeamPlayerName
			// 
			this.TeamPlayerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TeamPlayerName.HeaderText = "Name";
			this.TeamPlayerName.Name = "TeamPlayerName";
			this.TeamPlayerName.ReadOnly = true;
			// 
			// TeamOverall
			// 
			this.TeamOverall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.TeamOverall.HeaderText = "OVR";
			this.TeamOverall.Name = "TeamOverall";
			this.TeamOverall.ReadOnly = true;
			// 
			// TeamPlayerObject
			// 
			this.TeamPlayerObject.HeaderText = "PlayerObject";
			this.TeamPlayerObject.Name = "TeamPlayerObject";
			this.TeamPlayerObject.ReadOnly = true;
			this.TeamPlayerObject.Visible = false;
			// 
			// teamDepthChartLabel
			// 
			this.teamDepthChartLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.teamDepthChartLabel.AutoSize = true;
			this.teamDepthChartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.teamDepthChartLabel.Location = new System.Drawing.Point(174, 8);
			this.teamDepthChartLabel.Name = "teamDepthChartLabel";
			this.teamDepthChartLabel.Size = new System.Drawing.Size(171, 24);
			this.teamDepthChartLabel.TabIndex = 1;
			this.teamDepthChartLabel.Text = "team Depth Chart";
			this.teamDepthChartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// depthChartDataGrid
			// 
			this.depthChartDataGrid.AllowDrop = true;
			this.depthChartDataGrid.AllowUserToAddRows = false;
			this.depthChartDataGrid.AllowUserToDeleteRows = false;
			this.depthChartDataGrid.AllowUserToResizeColumns = false;
			this.depthChartDataGrid.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
			dataGridViewCellStyle3.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			this.depthChartDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.depthChartDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.depthChartDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.depthChartDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.depthChartDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.depthChartDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Position,
            this.PlayerName,
            this.Overall,
            this.PlayerObject,
            this.DepthChartObject});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(1);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.depthChartDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
			this.depthChartDataGrid.Location = new System.Drawing.Point(41, 37);
			this.depthChartDataGrid.MultiSelect = false;
			this.depthChartDataGrid.Name = "depthChartDataGrid";
			this.depthChartDataGrid.ReadOnly = true;
			this.depthChartDataGrid.RowHeadersVisible = false;
			this.depthChartDataGrid.Size = new System.Drawing.Size(470, 142);
			this.depthChartDataGrid.TabIndex = 0;
			this.depthChartDataGrid.Text = "dataGridView1";
			this.depthChartDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.depthChartDataGrid_CellClick);
			this.depthChartDataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.depthChartDataGrid_CellMouseUp);
			// 
			// Position
			// 
			this.Position.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Position.FillWeight = 76.14214F;
			this.Position.Frozen = true;
			this.Position.HeaderText = "Position";
			this.Position.Name = "Position";
			this.Position.ReadOnly = true;
			this.Position.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Position.Width = 50;
			// 
			// PlayerName
			// 
			this.PlayerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.PlayerName.FillWeight = 111.9289F;
			this.PlayerName.HeaderText = "Name";
			this.PlayerName.Name = "PlayerName";
			this.PlayerName.ReadOnly = true;
			this.PlayerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// Overall
			// 
			this.Overall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Overall.FillWeight = 111.9289F;
			this.Overall.HeaderText = "OVR";
			this.Overall.Name = "Overall";
			this.Overall.ReadOnly = true;
			this.Overall.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// PlayerObject
			// 
			this.PlayerObject.HeaderText = "PlayerObject";
			this.PlayerObject.Name = "PlayerObject";
			this.PlayerObject.ReadOnly = true;
			this.PlayerObject.Visible = false;
			// 
			// DepthChartObject
			// 
			this.DepthChartObject.HeaderText = "DepthChartObject";
			this.DepthChartObject.Name = "DepthChartObject";
			this.DepthChartObject.ReadOnly = true;
			this.DepthChartObject.Visible = false;
			// 
			// DepthChartEditorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "DepthChartEditorControl";
			this.Size = new System.Drawing.Size(662, 482);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.availablePlayerDatagrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ComboBox positionCombo;
		private System.Windows.Forms.ComboBox teamCombo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView availablePlayerDatagrid;
		private System.Windows.Forms.Label teamDepthChartLabel;
		private System.Windows.Forms.DataGridView depthChartDataGrid;
		private System.Windows.Forms.Button depthOrderDownButton;
		private System.Windows.Forms.Button depthOrderUpButton;
		private System.Windows.Forms.Button transferButton;
		private System.Windows.Forms.Button eraseButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn TeamPosition;
		private System.Windows.Forms.DataGridViewTextBoxColumn TeamPlayerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn TeamOverall;
		private System.Windows.Forms.DataGridViewTextBoxColumn TeamPlayerObject;
		private System.Windows.Forms.DataGridViewTextBoxColumn Position;
		private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Overall;
		private System.Windows.Forms.DataGridViewTextBoxColumn PlayerObject;
		private System.Windows.Forms.DataGridViewTextBoxColumn DepthChartObject;
	}
}
