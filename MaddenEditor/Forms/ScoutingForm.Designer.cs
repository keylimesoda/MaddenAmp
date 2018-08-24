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
    partial class ScoutingForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.RookiePositionFilter = new System.Windows.Forms.ComboBox();
            this.scoutingHours = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.QBhoursLabel = new System.Windows.Forms.Label();
            this.RookieGrid = new System.Windows.Forms.DataGridView();
            this.RBhoursLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.WRhoursLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.OLhoursLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TEhoursLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.PhoursLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.KhoursLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ShoursLabel = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.CBhoursLabel = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.LBhoursLabel = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.DLhoursLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.doSet = new System.Windows.Forms.Button();
            this.incrementHours = new System.Windows.Forms.ComboBox();
            this.incrementPosition = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoutingHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.wishlistGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stickyDepthCharts = new System.Windows.Forms.CheckBox();
            this.DepthChartGrid = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.depthChartPosition = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.projectionLevel = new System.Windows.Forms.ComboBox();
            this.projectionCondition = new System.Windows.Forms.ComboBox();
            this.projectionFilter = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SetIncrement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picks = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(886, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Positions";
            // 
            // RookiePositionFilter
            // 
            this.RookiePositionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RookiePositionFilter.FormattingEnabled = true;
            this.RookiePositionFilter.Location = new System.Drawing.Point(941, 38);
            this.RookiePositionFilter.Name = "RookiePositionFilter";
            this.RookiePositionFilter.Size = new System.Drawing.Size(55, 21);
            this.RookiePositionFilter.TabIndex = 16;
            this.RookiePositionFilter.SelectedIndexChanged += new System.EventHandler(this.RookiePositionFilter_SelectedIndexChanged);
            // 
            // scoutingHours
            // 
            this.scoutingHours.HeaderText = "Hrs To Scout";
            this.scoutingHours.Name = "scoutingHours";
            this.scoutingHours.Width = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Total Hours Used:";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLabel.Location = new System.Drawing.Point(102, 43);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(42, 13);
            this.TotalLabel.TabIndex = 20;
            this.TotalLabel.Text = "0 / 500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Hours by Position:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(258, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "QB";
            // 
            // QBhoursLabel
            // 
            this.QBhoursLabel.AutoSize = true;
            this.QBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBhoursLabel.Location = new System.Drawing.Point(279, 43);
            this.QBhoursLabel.Name = "QBhoursLabel";
            this.QBhoursLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.QBhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.QBhoursLabel.TabIndex = 23;
            this.QBhoursLabel.Text = "0,";
            // 
            // RookieGrid
            // 
            this.RookieGrid.AllowUserToAddRows = false;
            this.RookieGrid.AllowUserToDeleteRows = false;
            this.RookieGrid.AllowUserToResizeColumns = false;
            this.RookieGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-AU");
            this.RookieGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RookieGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RookieGrid.BackgroundColor = System.Drawing.Color.White;
            this.RookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RookieGrid.GridColor = System.Drawing.Color.White;
            this.RookieGrid.Location = new System.Drawing.Point(5, 65);
            this.RookieGrid.Name = "RookieGrid";
            this.RookieGrid.RowTemplate.Height = 16;
            this.RookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RookieGrid.Size = new System.Drawing.Size(1000, 369);
            this.RookieGrid.TabIndex = 18;
            this.RookieGrid.TabStop = false;
            this.RookieGrid.Text = "dataGridView1";
            this.RookieGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.RookieGrid_CellEndEdit);
            this.RookieGrid.CurrentCellChanged += new System.EventHandler(this.RookieGrid_Click);
            this.RookieGrid.Sorted += new System.EventHandler(this.RookieGrid_Sorted);
            this.RookieGrid.DoubleClick += new System.EventHandler(this.RookieGrid_DoubleClick);
            // 
            // RBhoursLabel
            // 
            this.RBhoursLabel.AutoSize = true;
            this.RBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBhoursLabel.Location = new System.Drawing.Point(318, 43);
            this.RBhoursLabel.Name = "RBhoursLabel";
            this.RBhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.RBhoursLabel.TabIndex = 25;
            this.RBhoursLabel.Text = "0,";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(298, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "RB";
            // 
            // WRhoursLabel
            // 
            this.WRhoursLabel.AutoSize = true;
            this.WRhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WRhoursLabel.Location = new System.Drawing.Point(366, 43);
            this.WRhoursLabel.Name = "WRhoursLabel";
            this.WRhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.WRhoursLabel.TabIndex = 29;
            this.WRhoursLabel.Text = "0,";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(341, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "WR";
            // 
            // OLhoursLabel
            // 
            this.OLhoursLabel.AutoSize = true;
            this.OLhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OLhoursLabel.Location = new System.Drawing.Point(447, 43);
            this.OLhoursLabel.Name = "OLhoursLabel";
            this.OLhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.OLhoursLabel.TabIndex = 33;
            this.OLhoursLabel.Text = "0,";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(428, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "OL";
            // 
            // TEhoursLabel
            // 
            this.TEhoursLabel.AutoSize = true;
            this.TEhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TEhoursLabel.Location = new System.Drawing.Point(409, 43);
            this.TEhoursLabel.Name = "TEhoursLabel";
            this.TEhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.TEhoursLabel.TabIndex = 31;
            this.TEhoursLabel.Text = "0,";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(390, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "TE";
            // 
            // PhoursLabel
            // 
            this.PhoursLabel.AutoSize = true;
            this.PhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoursLabel.Location = new System.Drawing.Point(679, 43);
            this.PhoursLabel.Name = "PhoursLabel";
            this.PhoursLabel.Size = new System.Drawing.Size(13, 13);
            this.PhoursLabel.TabIndex = 45;
            this.PhoursLabel.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(668, 43);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 44;
            this.label20.Text = "P";
            // 
            // KhoursLabel
            // 
            this.KhoursLabel.AutoSize = true;
            this.KhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KhoursLabel.Location = new System.Drawing.Point(647, 43);
            this.KhoursLabel.Name = "KhoursLabel";
            this.KhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.KhoursLabel.TabIndex = 43;
            this.KhoursLabel.Text = "0,";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(636, 43);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(14, 13);
            this.label22.TabIndex = 42;
            this.label22.Text = "K";
            // 
            // ShoursLabel
            // 
            this.ShoursLabel.AutoSize = true;
            this.ShoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShoursLabel.Location = new System.Drawing.Point(613, 43);
            this.ShoursLabel.Name = "ShoursLabel";
            this.ShoursLabel.Size = new System.Drawing.Size(16, 13);
            this.ShoursLabel.TabIndex = 41;
            this.ShoursLabel.Text = "0,";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(601, 43);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(14, 13);
            this.label24.TabIndex = 40;
            this.label24.Text = "S";
            // 
            // CBhoursLabel
            // 
            this.CBhoursLabel.AutoSize = true;
            this.CBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBhoursLabel.Location = new System.Drawing.Point(576, 43);
            this.CBhoursLabel.Name = "CBhoursLabel";
            this.CBhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.CBhoursLabel.TabIndex = 39;
            this.CBhoursLabel.Text = "0,";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(557, 43);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(21, 13);
            this.label26.TabIndex = 38;
            this.label26.Text = "CB";
            // 
            // LBhoursLabel
            // 
            this.LBhoursLabel.AutoSize = true;
            this.LBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBhoursLabel.Location = new System.Drawing.Point(533, 43);
            this.LBhoursLabel.Name = "LBhoursLabel";
            this.LBhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.LBhoursLabel.TabIndex = 37;
            this.LBhoursLabel.Text = "0,";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(516, 43);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(20, 13);
            this.label28.TabIndex = 36;
            this.label28.Text = "LB";
            // 
            // DLhoursLabel
            // 
            this.DLhoursLabel.AutoSize = true;
            this.DLhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DLhoursLabel.Location = new System.Drawing.Point(490, 43);
            this.DLhoursLabel.Name = "DLhoursLabel";
            this.DLhoursLabel.Size = new System.Drawing.Size(16, 13);
            this.DLhoursLabel.TabIndex = 35;
            this.DLhoursLabel.Text = "0,";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(472, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "DL";
            // 
            // doSet
            // 
            this.doSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.doSet.Location = new System.Drawing.Point(106, 82);
            this.doSet.Name = "doSet";
            this.doSet.Size = new System.Drawing.Size(128, 22);
            this.doSet.TabIndex = 50;
            this.doSet.Text = "Make Adjustments";
            this.doSet.Click += new System.EventHandler(this.doSet_Click);
            // 
            // incrementHours
            // 
            this.incrementHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.incrementHours.FormattingEnabled = true;
            this.incrementHours.Location = new System.Drawing.Point(133, 24);
            this.incrementHours.Name = "incrementHours";
            this.incrementHours.Size = new System.Drawing.Size(41, 21);
            this.incrementHours.TabIndex = 54;
            // 
            // incrementPosition
            // 
            this.incrementPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.incrementPosition.FormattingEnabled = true;
            this.incrementPosition.Location = new System.Drawing.Point(252, 24);
            this.incrementPosition.Name = "incrementPosition";
            this.incrementPosition.Size = new System.Drawing.Size(63, 21);
            this.incrementPosition.TabIndex = 52;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(386, 659);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 31);
            this.button1.TabIndex = 56;
            this.button1.Text = "Advance to Next Scouting Stage";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 57;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoutingHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // scoutingHelpToolStripMenuItem
            // 
            this.scoutingHelpToolStripMenuItem.Name = "scoutingHelpToolStripMenuItem";
            this.scoutingHelpToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.scoutingHelpToolStripMenuItem.Text = "Scouting Help";
            this.scoutingHelpToolStripMenuItem.Click += new System.EventHandler(this.scoutingHelpToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.removeButton);
            this.groupBox4.Controls.Add(this.downButton);
            this.groupBox4.Controls.Add(this.upButton);
            this.groupBox4.Controls.Add(this.wishlistGrid);
            this.groupBox4.Location = new System.Drawing.Point(16, 462);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 227);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Draft Board";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(166, 192);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(57, 25);
            this.removeButton.TabIndex = 27;
            this.removeButton.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.remove_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(76, 192);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(44, 23);
            this.downButton.TabIndex = 26;
            this.downButton.Text = "Down";
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(42, 192);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(30, 23);
            this.upButton.TabIndex = 25;
            this.upButton.Text = "Up";
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
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
            this.wishlistGrid.Size = new System.Drawing.Size(241, 157);
            this.wishlistGrid.TabIndex = 23;
            this.wishlistGrid.Text = "dataGridView1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stickyDepthCharts);
            this.groupBox1.Controls.Add(this.DepthChartGrid);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.depthChartPosition);
            this.groupBox1.Location = new System.Drawing.Point(327, 462);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 181);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Depth Charts";
            // 
            // stickyDepthCharts
            // 
            this.stickyDepthCharts.AutoSize = true;
            this.stickyDepthCharts.Checked = true;
            this.stickyDepthCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stickyDepthCharts.Location = new System.Drawing.Point(144, 33);
            this.stickyDepthCharts.Name = "stickyDepthCharts";
            this.stickyDepthCharts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stickyDepthCharts.Size = new System.Drawing.Size(140, 17);
            this.stickyDepthCharts.TabIndex = 14;
            this.stickyDepthCharts.Text = "Auto-Load Depth Charts";
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
            this.DepthChartGrid.Location = new System.Drawing.Point(18, 56);
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Positions";
            // 
            // depthChartPosition
            // 
            this.depthChartPosition.FormattingEnabled = true;
            this.depthChartPosition.Location = new System.Drawing.Point(75, 29);
            this.depthChartPosition.Name = "depthChartPosition";
            this.depthChartPosition.Size = new System.Drawing.Size(63, 21);
            this.depthChartPosition.TabIndex = 8;
            this.depthChartPosition.SelectedValueChanged += new System.EventHandler(this.depthChartFilterChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.projectionLevel);
            this.groupBox2.Controls.Add(this.projectionCondition);
            this.groupBox2.Controls.Add(this.projectionFilter);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.SetIncrement);
            this.groupBox2.Controls.Add(this.incrementHours);
            this.groupBox2.Controls.Add(this.doSet);
            this.groupBox2.Controls.Add(this.incrementPosition);
            this.groupBox2.Location = new System.Drawing.Point(668, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 113);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hour Adjustments";
            // 
            // projectionLevel
            // 
            this.projectionLevel.FormattingEnabled = true;
            this.projectionLevel.Location = new System.Drawing.Point(243, 53);
            this.projectionLevel.Name = "projectionLevel";
            this.projectionLevel.Size = new System.Drawing.Size(72, 21);
            this.projectionLevel.TabIndex = 60;
            // 
            // projectionCondition
            // 
            this.projectionCondition.FormattingEnabled = true;
            this.projectionCondition.Items.AddRange(new object[] {
            "at least",
            "exactly",
            "at most"});
            this.projectionCondition.Location = new System.Drawing.Point(160, 53);
            this.projectionCondition.Name = "projectionCondition";
            this.projectionCondition.Size = new System.Drawing.Size(74, 21);
            this.projectionCondition.TabIndex = 59;
            // 
            // projectionFilter
            // 
            this.projectionFilter.FormattingEnabled = true;
            this.projectionFilter.Items.AddRange(new object[] {
            "Any Projection",
            "All Projections",
            "Initial Projection",
            "Middle Projection",
            "Our Current Grade"});
            this.projectionFilter.Location = new System.Drawing.Point(37, 53);
            this.projectionFilter.Name = "projectionFilter";
            this.projectionFilter.Size = new System.Drawing.Size(116, 21);
            this.projectionFilter.TabIndex = 57;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "with";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "at position(s)";
            // 
            // SetIncrement
            // 
            this.SetIncrement.FormattingEnabled = true;
            this.SetIncrement.Items.AddRange(new object[] {
            "Set to",
            "Increment by"});
            this.SetIncrement.Location = new System.Drawing.Point(13, 24);
            this.SetIncrement.Name = "SetIncrement";
            this.SetIncrement.Size = new System.Drawing.Size(103, 21);
            this.SetIncrement.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(732, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Picks";
            // 
            // picks
            // 
            this.picks.FormattingEnabled = true;
            this.picks.Location = new System.Drawing.Point(766, 39);
            this.picks.Name = "picks";
            this.picks.Size = new System.Drawing.Size(114, 21);
            this.picks.TabIndex = 62;
            // 
            // ScoutingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 702);
            this.Controls.Add(this.picks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PhoursLabel);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.KhoursLabel);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.ShoursLabel);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.CBhoursLabel);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.LBhoursLabel);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.DLhoursLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.OLhoursLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.TEhoursLabel);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.WRhoursLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.RBhoursLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.QBhoursLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RookieGrid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RookiePositionFilter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1022, 736);
            this.Name = "ScoutingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rookie Scouting";
            this.Load += new System.EventHandler(this.ScoutingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wishlistGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DepthChartGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox RookiePositionFilter;
        private System.Windows.Forms.DataGridViewComboBoxColumn scoutingHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label QBhoursLabel;
        private System.Windows.Forms.DataGridView RookieGrid;
        private System.Windows.Forms.Label RBhoursLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label WRhoursLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label OLhoursLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label TEhoursLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label PhoursLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label KhoursLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label ShoursLabel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label CBhoursLabel;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label LBhoursLabel;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label DLhoursLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button doSet;
        private System.Windows.Forms.ComboBox incrementHours;
        private System.Windows.Forms.ComboBox incrementPosition;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scoutingHelpToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.DataGridView wishlistGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox stickyDepthCharts;
        private System.Windows.Forms.DataGridView DepthChartGrid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox depthChartPosition;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox SetIncrement;
        private System.Windows.Forms.ComboBox projectionFilter;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox projectionLevel;
        private System.Windows.Forms.ComboBox projectionCondition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox picks;
    }
}