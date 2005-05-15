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
			this.eraseButton = new System.Windows.Forms.Button();
			this.transferButton = new System.Windows.Forms.Button();
			this.depthOrderDownButton = new System.Windows.Forms.Button();
			this.depthOrderUpButton = new System.Windows.Forms.Button();
			this.availablePlayerDatagrid = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.teamDepthChartLabel = new System.Windows.Forms.Label();
			this.depthChartDataGrid = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			this.label2.Size = new System.Drawing.Size(40, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Position";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Team";
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
			this.availablePlayerDatagrid.Columns.Add(this.dataGridViewTextBoxColumn4);
			this.availablePlayerDatagrid.Columns.Add(this.dataGridViewTextBoxColumn5);
			this.availablePlayerDatagrid.Columns.Add(this.dataGridViewTextBoxColumn6);
			this.availablePlayerDatagrid.Columns.Add(this.dataGridViewTextBoxColumn7);
			this.availablePlayerDatagrid.Location = new System.Drawing.Point(3, 221);
			this.availablePlayerDatagrid.MultiSelect = false;
			this.availablePlayerDatagrid.Name = "availablePlayerDatagrid";
			this.availablePlayerDatagrid.ReadOnly = true;
			this.availablePlayerDatagrid.RowHeadersVisible = false;
			this.availablePlayerDatagrid.Size = new System.Drawing.Size(508, 248);
			this.availablePlayerDatagrid.TabIndex = 2;
			this.availablePlayerDatagrid.Text = "dataGridView2";
			this.availablePlayerDatagrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.availablePlayerDatagrid_CellMouseUp);
			this.availablePlayerDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.availablePlayerDatagrid_CellClick);
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn4.Frozen = true;
			this.dataGridViewTextBoxColumn4.HeaderText = "Position";
			this.dataGridViewTextBoxColumn4.Name = "Position";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 50;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn5.HeaderText = "Name";
			this.dataGridViewTextBoxColumn5.Name = "PlayersName";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn6.HeaderText = "OVR";
			this.dataGridViewTextBoxColumn6.Name = "Overall";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.HeaderText = "PlayerObject";
			this.dataGridViewTextBoxColumn7.Name = "PlayerObject";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Visible = false;
			// 
			// teamDepthChartLabel
			// 
			this.teamDepthChartLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.teamDepthChartLabel.AutoSize = true;
			this.teamDepthChartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.teamDepthChartLabel.Location = new System.Drawing.Point(174, 8);
			this.teamDepthChartLabel.Name = "teamDepthChartLabel";
			this.teamDepthChartLabel.Size = new System.Drawing.Size(166, 24);
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
			this.depthChartDataGrid.Columns.Add(this.dataGridViewTextBoxColumn1);
			this.depthChartDataGrid.Columns.Add(this.dataGridViewTextBoxColumn2);
			this.depthChartDataGrid.Columns.Add(this.dataGridViewTextBoxColumn3);
			this.depthChartDataGrid.Columns.Add(this.dataGridViewTextBoxColumn8);
			this.depthChartDataGrid.Columns.Add(this.dataGridViewTextBoxColumn9);
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
			this.depthChartDataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.depthChartDataGrid_CellMouseUp);
			this.depthChartDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.depthChartDataGrid_CellClick);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn1.FillWeight = 76.14214F;
			this.dataGridViewTextBoxColumn1.Frozen = true;
			this.dataGridViewTextBoxColumn1.HeaderText = "Position";
			this.dataGridViewTextBoxColumn1.Name = "Position";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn1.Width = 50;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.FillWeight = 111.9289F;
			this.dataGridViewTextBoxColumn2.HeaderText = "Name";
			this.dataGridViewTextBoxColumn2.Name = "PlayersName";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn3.FillWeight = 111.9289F;
			this.dataGridViewTextBoxColumn3.HeaderText = "OVR";
			this.dataGridViewTextBoxColumn3.Name = "Overall";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.HeaderText = "PlayerObject";
			this.dataGridViewTextBoxColumn8.Name = "PlayerObject";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			this.dataGridViewTextBoxColumn8.Visible = false;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.HeaderText = "DepthChartObject";
			this.dataGridViewTextBoxColumn9.Name = "DepthChartObject";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			this.dataGridViewTextBoxColumn9.Visible = false;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.Button transferButton;
		private System.Windows.Forms.Button eraseButton;
	}
}
