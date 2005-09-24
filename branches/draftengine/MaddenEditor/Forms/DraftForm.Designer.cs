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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
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
            this.label7 = new System.Windows.Forms.Label();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DraftResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DraftBoardGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicksToSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).BeginInit();
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
            this.DraftResults.Location = new System.Drawing.Point(15, 70);
            this.DraftResults.MultiSelect = false;
            this.DraftResults.Name = "DraftResults";
            this.DraftResults.ReadOnly = true;
            this.DraftResults.RowTemplate.Height = 16;
            this.DraftResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DraftResults.ShowEditingIcon = false;
            this.DraftResults.Size = new System.Drawing.Size(285, 265);
            this.DraftResults.TabIndex = 0;
            this.DraftResults.Text = "dataGridView1";
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
            this.RookieGrid.Location = new System.Drawing.Point(15, 380);
            this.RookieGrid.Name = "RookieGrid";
            this.RookieGrid.ReadOnly = true;
            this.RookieGrid.RowTemplate.Height = 16;
            this.RookieGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RookieGrid.ShowEditingIcon = false;
            this.RookieGrid.Size = new System.Drawing.Size(975, 167);
            this.RookieGrid.TabIndex = 1;
            this.RookieGrid.Text = "dataGridView1";
            this.RookieGrid.Sorted += new System.EventHandler(this.fixSort);
            this.RookieGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RookieGrid_CellClick);
            this.RookieGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RookieGrid_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Teams";
            // 
            // draftedTeamsFilter
            // 
            this.draftedTeamsFilter.FormattingEnabled = true;
            this.draftedTeamsFilter.Location = new System.Drawing.Point(72, 38);
            this.draftedTeamsFilter.Name = "draftedTeamsFilter";
            this.draftedTeamsFilter.Size = new System.Drawing.Size(108, 21);
            this.draftedTeamsFilter.TabIndex = 3;
            this.draftedTeamsFilter.SelectedValueChanged += new System.EventHandler(this.draftedTeamsFilterChanged);
            // 
            // draftedPositionsFilter
            // 
            this.draftedPositionsFilter.FormattingEnabled = true;
            this.draftedPositionsFilter.Location = new System.Drawing.Point(237, 38);
            this.draftedPositionsFilter.Name = "draftedPositionsFilter";
            this.draftedPositionsFilter.Size = new System.Drawing.Size(63, 21);
            this.draftedPositionsFilter.TabIndex = 4;
            this.draftedPositionsFilter.SelectedValueChanged += new System.EventHandler(this.draftedPositionsFilterChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Positions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(877, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Positions";
            // 
            // depthChartPosition
            // 
            this.depthChartPosition.FormattingEnabled = true;
            this.depthChartPosition.Location = new System.Drawing.Point(928, 194);
            this.depthChartPosition.Name = "depthChartPosition";
            this.depthChartPosition.Size = new System.Drawing.Size(63, 21);
            this.depthChartPosition.TabIndex = 8;
            this.depthChartPosition.SelectedValueChanged += new System.EventHandler(this.depthChartFilterChanged);
            // 
            // depthChartTeam
            // 
            this.depthChartTeam.FormattingEnabled = true;
            this.depthChartTeam.Location = new System.Drawing.Point(763, 194);
            this.depthChartTeam.Name = "depthChartTeam";
            this.depthChartTeam.Size = new System.Drawing.Size(108, 21);
            this.depthChartTeam.TabIndex = 7;
            this.depthChartTeam.SelectedValueChanged += new System.EventHandler(this.depthChartFilterChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(722, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
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
            this.DepthChartGrid.Location = new System.Drawing.Point(718, 225);
            this.DepthChartGrid.Name = "DepthChartGrid";
            this.DepthChartGrid.ReadOnly = true;
            this.DepthChartGrid.RowTemplate.Height = 16;
            this.DepthChartGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DepthChartGrid.Size = new System.Drawing.Size(272, 110);
            this.DepthChartGrid.TabIndex = 10;
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
            this.DraftBoardGrid.Location = new System.Drawing.Point(719, 70);
            this.DraftBoardGrid.Name = "DraftBoardGrid";
            this.DraftBoardGrid.ReadOnly = true;
            this.DraftBoardGrid.RowTemplate.Height = 16;
            this.DraftBoardGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DraftBoardGrid.Size = new System.Drawing.Size(272, 106);
            this.DraftBoardGrid.TabIndex = 11;
            this.DraftBoardGrid.Text = "dataGridView2";
            this.DraftBoardGrid.Sorted += new System.EventHandler(this.fixSort);
            // 
            // draftBoardTeam
            // 
            this.draftBoardTeam.FormattingEnabled = true;
            this.draftBoardTeam.Location = new System.Drawing.Point(763, 39);
            this.draftBoardTeam.Name = "draftBoardTeam";
            this.draftBoardTeam.Size = new System.Drawing.Size(108, 21);
            this.draftBoardTeam.TabIndex = 13;
            this.draftBoardTeam.SelectedValueChanged += new System.EventHandler(this.draftBoardTeamChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(722, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Teams";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(877, 357);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Positions";
            // 
            // RookiePositionFilter
            // 
            this.RookiePositionFilter.FormattingEnabled = true;
            this.RookiePositionFilter.Location = new System.Drawing.Point(928, 353);
            this.RookiePositionFilter.Name = "RookiePositionFilter";
            this.RookiePositionFilter.Size = new System.Drawing.Size(63, 21);
            this.RookiePositionFilter.TabIndex = 14;
            this.RookiePositionFilter.SelectedValueChanged += new System.EventHandler(this.rookieFilterChanged);
            // 
            // PlayerToDraft
            // 
            this.PlayerToDraft.BackColor = System.Drawing.Color.White;
            this.PlayerToDraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerToDraft.Location = new System.Drawing.Point(15, 345);
            this.PlayerToDraft.Name = "PlayerToDraft";
            this.PlayerToDraft.ReadOnly = true;
            this.PlayerToDraft.Size = new System.Drawing.Size(216, 26);
            this.PlayerToDraft.TabIndex = 16;
            // 
            // draftButton
            // 
            this.draftButton.Enabled = false;
            this.draftButton.Location = new System.Drawing.Point(238, 345);
            this.draftButton.Name = "draftButton";
            this.draftButton.Size = new System.Drawing.Size(62, 26);
            this.draftButton.TabIndex = 17;
            this.draftButton.Text = "Draft";
            this.draftButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.draftButton_MouseClick);
            // 
            // clock
            // 
            this.clock.BackColor = System.Drawing.Color.White;
            this.clock.Cursor = System.Windows.Forms.Cursors.Default;
            this.clock.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clock.ForeColor = System.Drawing.Color.Red;
            this.clock.Location = new System.Drawing.Point(449, 70);
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
            this.PicksToSkip.Location = new System.Drawing.Point(517, 144);
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
            this.SkipButton.Location = new System.Drawing.Point(462, 143);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new System.Drawing.Size(49, 28);
            this.SkipButton.TabIndex = 20;
            this.SkipButton.Text = "Skip";
            this.SkipButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SkipButton_MouseClick);
            // 
            // showDraftedPlayers
            // 
            this.showDraftedPlayers.AutoSize = true;
            this.showDraftedPlayers.Checked = true;
            this.showDraftedPlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDraftedPlayers.Location = new System.Drawing.Point(718, 356);
            this.showDraftedPlayers.Name = "showDraftedPlayers";
            this.showDraftedPlayers.Size = new System.Drawing.Size(124, 17);
            this.showDraftedPlayers.TabIndex = 21;
            this.showDraftedPlayers.Text = "Show Drafted Players";
            this.showDraftedPlayers.CheckedChanged += new System.EventHandler(this.showDraftedPlayers_CheckedChanged);
            // 
            // tradeButton
            // 
            this.tradeButton.Location = new System.Drawing.Point(449, 177);
            this.tradeButton.Name = "tradeButton";
            this.tradeButton.Size = new System.Drawing.Size(128, 21);
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
            this.wishlistGrid.Location = new System.Drawing.Point(449, 245);
            this.wishlistGrid.Name = "wishlistGrid";
            this.wishlistGrid.ReadOnly = true;
            this.wishlistGrid.RowTemplate.Height = 16;
            this.wishlistGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wishlistGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.wishlistGrid.ShowEditingIcon = false;
            this.wishlistGrid.Size = new System.Drawing.Size(129, 90);
            this.wishlistGrid.TabIndex = 23;
            this.wishlistGrid.Text = "dataGridView1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Draft Targets";
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(452, 341);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(26, 19);
            this.upButton.TabIndex = 25;
            this.upButton.Text = "Up";
            this.upButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.upButton_MouseClick);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(482, 341);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(26, 19);
            this.downButton.TabIndex = 26;
            this.downButton.Text = "Down";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(517, 341);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(57, 19);
            this.button3.TabIndex = 27;
            this.button3.Text = "Remove";
            // 
            // DraftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 561);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.wishlistGrid);
            this.Controls.Add(this.tradeButton);
            this.Controls.Add(this.showDraftedPlayers);
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.PicksToSkip);
            this.Controls.Add(this.clock);
            this.Controls.Add(this.draftButton);
            this.Controls.Add(this.PlayerToDraft);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RookiePositionFilter);
            this.Controls.Add(this.draftBoardTeam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DraftBoardGrid);
            this.Controls.Add(this.DepthChartGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.depthChartPosition);
            this.Controls.Add(this.depthChartTeam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.draftedPositionsFilter);
            this.Controls.Add(this.draftedTeamsFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RookieGrid);
            this.Controls.Add(this.DraftResults);
            this.Name = "DraftForm";
            this.Text = "DraftForm";
            this.Load += new System.EventHandler(this.DraftForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DraftResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DraftBoardGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicksToSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).EndInit();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button button3;

    }
}