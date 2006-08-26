/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
namespace MaddenEditor.Forms
{
    partial class DraftForm
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
            this.DraftResults = new System.Windows.Forms.DataGridView();
            this.RookieGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.draftedTeamsFilter = new System.Windows.Forms.ComboBox();
            this.draftedPositionsFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.depthChartPosition = new System.Windows.Forms.ComboBox();
            this.depthChartTeam = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DepthChartGrid = new System.Windows.Forms.DataGridView();
            this.DraftBoardGrid = new System.Windows.Forms.DataGridView();
            this.draftBoardTeam = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RookiePositionFilter = new System.Windows.Forms.ComboBox();
            this.PlayerToDraft = new System.Windows.Forms.TextBox();
            this.draftButton = new System.Windows.Forms.Button();
            this.clock = new System.Windows.Forms.TextBox();
            this.draftTimer = new System.Windows.Forms.Timer(this.components);
            this.PicksToSkip = new System.Windows.Forms.NumericUpDown();
            this.SkipButton = new System.Windows.Forms.Button();
            this.showDraftedPlayers = new System.Windows.Forms.CheckBox();
            this.tradeButton = new System.Windows.Forms.Button();
            this.wishlistGrid = new System.Windows.Forms.DataGridView();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.autoPickBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectingLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.draftHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stickyDraftBoards = new System.Windows.Forms.CheckBox();
            this.stickyDepthCharts = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listScoutedOnly = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pickLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DraftResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DraftBoardGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicksToSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // DraftResults
            // 
            this.DraftResults.AllowUserToAddRows = false;
            this.DraftResults.AllowUserToDeleteRows = false;
            this.DraftResults.AllowUserToResizeColumns = false;
            this.DraftResults.AllowUserToResizeRows = false;
            this.DraftResults.BackgroundColor = System.Drawing.Color.White;
            this.DraftResults.GridColor = System.Drawing.Color.White;
            this.DraftResults.Location = new System.Drawing.Point(12, 53);
            this.DraftResults.MultiSelect = false;
            this.DraftResults.Name = "DraftResults";
            this.DraftResults.ReadOnly = true;
            this.DraftResults.RowTemplate.Height = 16;
            this.DraftResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DraftResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DraftResults.ShowEditingIcon = false;
            this.DraftResults.Size = new System.Drawing.Size(285, 308);
            this.DraftResults.TabIndex = 0;
            this.DraftResults.TabStop = false;
            this.DraftResults.Text = "dataGridView1";
            this.DraftResults.DoubleClick += new System.EventHandler(this.DraftResults_DoubleClick);
            // 
            // RookieGrid
            // 
            this.RookieGrid.AllowUserToAddRows = false;
            this.RookieGrid.AllowUserToDeleteRows = false;
            this.RookieGrid.AllowUserToResizeColumns = false;
            this.RookieGrid.AllowUserToResizeRows = false;
            this.RookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.RookieGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.RookieGrid.GridColor = System.Drawing.Color.White;
            this.RookieGrid.Location = new System.Drawing.Point(11, 56);
            this.RookieGrid.Name = "RookieGrid";
            this.RookieGrid.ReadOnly = true;
            this.RookieGrid.RowTemplate.Height = 16;
            this.RookieGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RookieGrid.ShowEditingIcon = false;
            this.RookieGrid.Size = new System.Drawing.Size(965, 167);
            this.RookieGrid.TabIndex = 1;
            this.RookieGrid.TabStop = false;
            this.RookieGrid.Text = "dataGridView1";
            this.RookieGrid.Sorted += new System.EventHandler(this.fixSort);
            this.RookieGrid.DoubleClick += new System.EventHandler(this.RookieGrid_DoubleClick);
            this.RookieGrid.Click += new System.EventHandler(this.RookieGrid_Click);
            this.RookieGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RookieGrid_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Teams";
            // 
            // draftedTeamsFilter
            // 
            this.draftedTeamsFilter.FormattingEnabled = true;
            this.draftedTeamsFilter.Location = new System.Drawing.Point(69, 21);
            this.draftedTeamsFilter.Name = "draftedTeamsFilter";
            this.draftedTeamsFilter.Size = new System.Drawing.Size(108, 21);
            this.draftedTeamsFilter.TabIndex = 3;
            this.draftedTeamsFilter.SelectedValueChanged += new System.EventHandler(this.draftedTeamsFilterChanged);
            // 
            // draftedPositionsFilter
            // 
            this.draftedPositionsFilter.FormattingEnabled = true;
            this.draftedPositionsFilter.Location = new System.Drawing.Point(234, 21);
            this.draftedPositionsFilter.Name = "draftedPositionsFilter";
            this.draftedPositionsFilter.Size = new System.Drawing.Size(63, 21);
            this.draftedPositionsFilter.TabIndex = 4;
            this.draftedPositionsFilter.SelectedValueChanged += new System.EventHandler(this.draftedPositionsFilterChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Positions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Positions";
            // 
            // depthChartPosition
            // 
            this.depthChartPosition.FormattingEnabled = true;
            this.depthChartPosition.Location = new System.Drawing.Point(229, 198);
            this.depthChartPosition.Name = "depthChartPosition";
            this.depthChartPosition.Size = new System.Drawing.Size(63, 21);
            this.depthChartPosition.TabIndex = 8;
            this.depthChartPosition.SelectedValueChanged += new System.EventHandler(this.depthChartFilterChanged);
            // 
            // depthChartTeam
            // 
            this.depthChartTeam.FormattingEnabled = true;
            this.depthChartTeam.Location = new System.Drawing.Point(64, 198);
            this.depthChartTeam.Name = "depthChartTeam";
            this.depthChartTeam.Size = new System.Drawing.Size(108, 21);
            this.depthChartTeam.TabIndex = 7;
            this.depthChartTeam.SelectedValueChanged += new System.EventHandler(this.depthChartFilterChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Teams";
            // 
            // DepthChartGrid
            // 
            this.DepthChartGrid.AllowUserToAddRows = false;
            this.DepthChartGrid.AllowUserToDeleteRows = false;
            this.DepthChartGrid.AllowUserToResizeColumns = false;
            this.DepthChartGrid.AllowUserToResizeRows = false;
            this.DepthChartGrid.BackgroundColor = System.Drawing.Color.White;
            this.DepthChartGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DepthChartGrid.GridColor = System.Drawing.Color.White;
            this.DepthChartGrid.Location = new System.Drawing.Point(19, 229);
            this.DepthChartGrid.Name = "DepthChartGrid";
            this.DepthChartGrid.ReadOnly = true;
            this.DepthChartGrid.RowTemplate.Height = 16;
            this.DepthChartGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DepthChartGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DepthChartGrid.Size = new System.Drawing.Size(272, 110);
            this.DepthChartGrid.TabIndex = 10;
            this.DepthChartGrid.TabStop = false;
            this.DepthChartGrid.Text = "dataGridView2";
            // 
            // DraftBoardGrid
            // 
            this.DraftBoardGrid.AllowUserToAddRows = false;
            this.DraftBoardGrid.AllowUserToDeleteRows = false;
            this.DraftBoardGrid.AllowUserToResizeColumns = false;
            this.DraftBoardGrid.AllowUserToResizeRows = false;
            this.DraftBoardGrid.BackgroundColor = System.Drawing.Color.White;
            this.DraftBoardGrid.GridColor = System.Drawing.Color.White;
            this.DraftBoardGrid.Location = new System.Drawing.Point(20, 54);
            this.DraftBoardGrid.Name = "DraftBoardGrid";
            this.DraftBoardGrid.ReadOnly = true;
            this.DraftBoardGrid.RowTemplate.Height = 16;
            this.DraftBoardGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DraftBoardGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DraftBoardGrid.Size = new System.Drawing.Size(272, 106);
            this.DraftBoardGrid.TabIndex = 11;
            this.DraftBoardGrid.TabStop = false;
            this.DraftBoardGrid.Text = "dataGridView2";
            this.DraftBoardGrid.Sorted += new System.EventHandler(this.fixSort);
            this.DraftBoardGrid.DoubleClick += new System.EventHandler(this.DraftBoardGrid_DoubleClick);
            // 
            // draftBoardTeam
            // 
            this.draftBoardTeam.FormattingEnabled = true;
            this.draftBoardTeam.Location = new System.Drawing.Point(64, 23);
            this.draftBoardTeam.Name = "draftBoardTeam";
            this.draftBoardTeam.Size = new System.Drawing.Size(108, 21);
            this.draftBoardTeam.TabIndex = 13;
            this.draftBoardTeam.SelectedValueChanged += new System.EventHandler(this.draftBoardTeamChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Teams";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(862, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Positions";
            // 
            // RookiePositionFilter
            // 
            this.RookiePositionFilter.FormattingEnabled = true;
            this.RookiePositionFilter.Location = new System.Drawing.Point(913, 24);
            this.RookiePositionFilter.Name = "RookiePositionFilter";
            this.RookiePositionFilter.Size = new System.Drawing.Size(63, 21);
            this.RookiePositionFilter.TabIndex = 14;
            this.RookiePositionFilter.SelectedValueChanged += new System.EventHandler(this.rookieFilterChanged);
            // 
            // PlayerToDraft
            // 
            this.PlayerToDraft.BackColor = System.Drawing.Color.White;
            this.PlayerToDraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerToDraft.Location = new System.Drawing.Point(11, 19);
            this.PlayerToDraft.Name = "PlayerToDraft";
            this.PlayerToDraft.ReadOnly = true;
            this.PlayerToDraft.Size = new System.Drawing.Size(216, 26);
            this.PlayerToDraft.TabIndex = 16;
            this.PlayerToDraft.TabStop = false;
            // 
            // draftButton
            // 
            this.draftButton.Enabled = false;
            this.draftButton.Location = new System.Drawing.Point(234, 19);
            this.draftButton.Name = "draftButton";
            this.draftButton.Size = new System.Drawing.Size(62, 26);
            this.draftButton.TabIndex = 17;
            this.draftButton.Text = "Draft";
            this.draftButton.Click += new System.EventHandler(this.draftButton_Click);
            // 
            // clock
            // 
            this.clock.BackColor = System.Drawing.Color.White;
            this.clock.Cursor = System.Windows.Forms.Cursors.Default;
            this.clock.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clock.ForeColor = System.Drawing.Color.Red;
            this.clock.Location = new System.Drawing.Point(67, 19);
            this.clock.Name = "clock";
            this.clock.ReadOnly = true;
            this.clock.Size = new System.Drawing.Size(129, 68);
            this.clock.TabIndex = 18;
            this.clock.TabStop = false;
            this.clock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // draftTimer
            // 
            this.draftTimer.Interval = 1000;
            this.draftTimer.Tick += new System.EventHandler(this.timerOnTick);
            // 
            // PicksToSkip
            // 
            this.PicksToSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PicksToSkip.Location = new System.Drawing.Point(146, 93);
            this.PicksToSkip.Maximum = new decimal(new int[] {
            224,
            0,
            0,
            0});
            this.PicksToSkip.Name = "PicksToSkip";
            this.PicksToSkip.Size = new System.Drawing.Size(49, 26);
            this.PicksToSkip.TabIndex = 19;
            this.PicksToSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SkipButton
            // 
            this.SkipButton.Location = new System.Drawing.Point(67, 92);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new System.Drawing.Size(69, 28);
            this.SkipButton.TabIndex = 20;
            this.SkipButton.Text = "Advance";
            this.SkipButton.Click += new System.EventHandler(this.SkipButton_Click);
            // 
            // showDraftedPlayers
            // 
            this.showDraftedPlayers.AutoSize = true;
            this.showDraftedPlayers.Checked = true;
            this.showDraftedPlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDraftedPlayers.Location = new System.Drawing.Point(703, 27);
            this.showDraftedPlayers.Name = "showDraftedPlayers";
            this.showDraftedPlayers.Size = new System.Drawing.Size(128, 17);
            this.showDraftedPlayers.TabIndex = 21;
            this.showDraftedPlayers.Text = "Show Drafted Players";
            this.showDraftedPlayers.CheckedChanged += new System.EventHandler(this.showDraftedPlayers_CheckedChanged);
            // 
            // tradeButton
            // 
            this.tradeButton.Location = new System.Drawing.Point(67, 127);
            this.tradeButton.Name = "tradeButton";
            this.tradeButton.Size = new System.Drawing.Size(128, 22);
            this.tradeButton.TabIndex = 22;
            this.tradeButton.Text = "Make Trade Offer";
            this.tradeButton.Click += new System.EventHandler(this.tradeButton_Click);
            // 
            // wishlistGrid
            // 
            this.wishlistGrid.AllowUserToAddRows = false;
            this.wishlistGrid.AllowUserToDeleteRows = false;
            this.wishlistGrid.AllowUserToResizeColumns = false;
            this.wishlistGrid.AllowUserToResizeRows = false;
            this.wishlistGrid.BackgroundColor = System.Drawing.Color.White;
            this.wishlistGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.wishlistGrid.GridColor = System.Drawing.Color.White;
            this.wishlistGrid.Location = new System.Drawing.Point(12, 24);
            this.wishlistGrid.Name = "wishlistGrid";
            this.wishlistGrid.ReadOnly = true;
            this.wishlistGrid.RowTemplate.Height = 16;
            this.wishlistGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wishlistGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.wishlistGrid.ShowEditingIcon = false;
            this.wishlistGrid.Size = new System.Drawing.Size(241, 90);
            this.wishlistGrid.TabIndex = 23;
            this.wishlistGrid.Text = "dataGridView1";
            this.wishlistGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wishlistGrid_CellClick);
            this.wishlistGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wishlistGrid_CellDoubleClick);
            this.wishlistGrid.Sorted += new System.EventHandler(this.wishlistFixSort);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(40, 120);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(30, 23);
            this.upButton.TabIndex = 25;
            this.upButton.Text = "Up";
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(74, 120);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(44, 23);
            this.downButton.TabIndex = 26;
            this.downButton.Text = "Down";
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(164, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 25);
            this.button3.TabIndex = 27;
            this.button3.Text = "Remove";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 674);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip.TabIndex = 28;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(42, 17);
            this.statusLabel.Text = "Ready.";
            // 
            // selectingLabel
            // 
            this.selectingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectingLabel.ForeColor = System.Drawing.Color.Black;
            this.selectingLabel.Location = new System.Drawing.Point(322, 32);
            this.selectingLabel.Name = "selectingLabel";
            this.selectingLabel.Size = new System.Drawing.Size(375, 31);
            this.selectingLabel.TabIndex = 31;
            this.selectingLabel.Text = "On the Clock:  XXXXXXX";
            this.selectingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip1.TabIndex = 32;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draftHelpToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // draftHelpToolStripMenuItem
            // 
            this.draftHelpToolStripMenuItem.Name = "draftHelpToolStripMenuItem";
            this.draftHelpToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.draftHelpToolStripMenuItem.Text = "Draft Help";
            this.draftHelpToolStripMenuItem.Click += new System.EventHandler(this.draftHelpToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stickyDraftBoards);
            this.groupBox1.Controls.Add(this.stickyDepthCharts);
            this.groupBox1.Controls.Add(this.draftBoardTeam);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.DraftBoardGrid);
            this.groupBox1.Controls.Add(this.DepthChartGrid);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.depthChartPosition);
            this.groupBox1.Controls.Add(this.depthChartTeam);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(696, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 373);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draft Boards and Depth Charts";
            // 
            // stickyDraftBoards
            // 
            this.stickyDraftBoards.AutoSize = true;
            this.stickyDraftBoards.Checked = true;
            this.stickyDraftBoards.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stickyDraftBoards.Location = new System.Drawing.Point(140, 168);
            this.stickyDraftBoards.Name = "stickyDraftBoards";
            this.stickyDraftBoards.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stickyDraftBoards.Size = new System.Drawing.Size(137, 17);
            this.stickyDraftBoards.TabIndex = 15;
            this.stickyDraftBoards.Text = "Auto-Load Draft Boards";
            // 
            // stickyDepthCharts
            // 
            this.stickyDepthCharts.AutoSize = true;
            this.stickyDepthCharts.Checked = true;
            this.stickyDepthCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stickyDepthCharts.Location = new System.Drawing.Point(140, 348);
            this.stickyDepthCharts.Name = "stickyDepthCharts";
            this.stickyDepthCharts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stickyDepthCharts.Size = new System.Drawing.Size(140, 17);
            this.stickyDepthCharts.TabIndex = 14;
            this.stickyDepthCharts.Text = "Auto-Load Depth Charts";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listScoutedOnly);
            this.groupBox2.Controls.Add(this.showDraftedPlayers);
            this.groupBox2.Controls.Add(this.draftButton);
            this.groupBox2.Controls.Add(this.PlayerToDraft);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.RookiePositionFilter);
            this.groupBox2.Controls.Add(this.RookieGrid);
            this.groupBox2.Location = new System.Drawing.Point(13, 430);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(991, 233);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rookies";
            // 
            // listScoutedOnly
            // 
            this.listScoutedOnly.AutoSize = true;
            this.listScoutedOnly.Location = new System.Drawing.Point(529, 27);
            this.listScoutedOnly.Name = "listScoutedOnly";
            this.listScoutedOnly.Size = new System.Drawing.Size(157, 17);
            this.listScoutedOnly.TabIndex = 22;
            this.listScoutedOnly.Text = "Show Only Scouted Players";
            this.listScoutedOnly.UseVisualStyleBackColor = true;
            this.listScoutedOnly.CheckedChanged += new System.EventHandler(this.showDraftedPlayers_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.draftedPositionsFilter);
            this.groupBox3.Controls.Add(this.draftedTeamsFilter);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.DraftResults);
            this.groupBox3.Location = new System.Drawing.Point(12, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 373);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Draft Results";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.downButton);
            this.groupBox4.Controls.Add(this.upButton);
            this.groupBox4.Controls.Add(this.wishlistGrid);
            this.groupBox4.Location = new System.Drawing.Point(378, 264);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 151);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Our Draft Board";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tradeButton);
            this.groupBox5.Controls.Add(this.SkipButton);
            this.groupBox5.Controls.Add(this.PicksToSkip);
            this.groupBox5.Controls.Add(this.clock);
            this.groupBox5.Location = new System.Drawing.Point(378, 95);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(263, 160);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Clock";
            // 
            // pickLabel
            // 
            this.pickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pickLabel.ForeColor = System.Drawing.Color.Black;
            this.pickLabel.Location = new System.Drawing.Point(322, 66);
            this.pickLabel.Name = "pickLabel";
            this.pickLabel.Size = new System.Drawing.Size(375, 19);
            this.pickLabel.TabIndex = 38;
            this.pickLabel.Text = "Round 1, Pick 1";
            this.pickLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DraftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 696);
            this.Controls.Add(this.pickLabel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.selectingLabel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 730);
            this.MinimumSize = new System.Drawing.Size(1022, 730);
            this.Name = "DraftForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Draft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DraftForm_FormClosing);
            this.Load += new System.EventHandler(this.DraftForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DraftResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DraftBoardGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicksToSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DraftResults;
        private System.Windows.Forms.DataGridView RookieGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox draftedTeamsFilter;
        private System.Windows.Forms.ComboBox draftedPositionsFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox depthChartPosition;
        private System.Windows.Forms.ComboBox depthChartTeam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DepthChartGrid;
        private System.Windows.Forms.DataGridView DraftBoardGrid;
        private System.Windows.Forms.ComboBox draftBoardTeam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox RookiePositionFilter;
        private System.Windows.Forms.TextBox PlayerToDraft;
        private System.Windows.Forms.Button draftButton;
        private System.Windows.Forms.TextBox clock;
        private System.Windows.Forms.Timer draftTimer;
        private System.Windows.Forms.NumericUpDown PicksToSkip;
        private System.Windows.Forms.Button SkipButton;
        private System.Windows.Forms.CheckBox showDraftedPlayers;
        private System.Windows.Forms.Button tradeButton;
        private System.Windows.Forms.DataGridView wishlistGrid;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button button3;
        private System.ComponentModel.BackgroundWorker autoPickBackgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label selectingLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem draftHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox stickyDepthCharts;
        private System.Windows.Forms.CheckBox stickyDraftBoards;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox listScoutedOnly;
        private System.Windows.Forms.Label pickLabel;

    }
}