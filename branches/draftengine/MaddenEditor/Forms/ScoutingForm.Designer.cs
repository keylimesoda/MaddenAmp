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
			this.label5 = new System.Windows.Forms.Label();
			this.setPosition = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.setHours = new System.Windows.Forms.ComboBox();
			this.doSet = new System.Windows.Forms.Button();
			this.doIncrement = new System.Windows.Forms.Button();
			this.incrementHours = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.incrementPosition = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scoutingHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(872, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 16);
			this.label6.TabIndex = 17;
			this.label6.Text = "Positions";
			// 
			// RookiePositionFilter
			// 
			this.RookiePositionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RookiePositionFilter.FormattingEnabled = true;
			this.RookiePositionFilter.Location = new System.Drawing.Point(937, 40);
			this.RookiePositionFilter.Name = "RookiePositionFilter";
			this.RookiePositionFilter.Size = new System.Drawing.Size(63, 21);
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
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 16);
			this.label1.TabIndex = 19;
			this.label1.Text = "Total Hours Used:";
			// 
			// TotalLabel
			// 
			this.TotalLabel.AutoSize = true;
			this.TotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TotalLabel.Location = new System.Drawing.Point(127, 43);
			this.TotalLabel.Name = "TotalLabel";
			this.TotalLabel.Size = new System.Drawing.Size(42, 16);
			this.TotalLabel.TabIndex = 20;
			this.TotalLabel.Text = "0 / 500";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(190, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 16);
			this.label3.TabIndex = 21;
			this.label3.Text = "Breakdown by Position:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(340, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 16);
			this.label4.TabIndex = 22;
			this.label4.Text = "QB";
			// 
			// QBhoursLabel
			// 
			this.QBhoursLabel.AutoSize = true;
			this.QBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.QBhoursLabel.Location = new System.Drawing.Point(363, 43);
			this.QBhoursLabel.Name = "QBhoursLabel";
			this.QBhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.QBhoursLabel.TabIndex = 23;
			this.QBhoursLabel.Text = "0,";
			// 
			// RookieGrid
			// 
			this.RookieGrid.AllowUserToAddRows = false;
			this.RookieGrid.AllowUserToDeleteRows = false;
			this.RookieGrid.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(234)))));
			dataGridViewCellStyle1.FormatProvider = new System.Globalization.CultureInfo("en-AU");
			this.RookieGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.RookieGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.RookieGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.RookieGrid.BackgroundColor = System.Drawing.Color.White;
			this.RookieGrid.ColumnHeadersHeight = 18;
			this.RookieGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.RookieGrid.GridColor = System.Drawing.Color.White;
			this.RookieGrid.Location = new System.Drawing.Point(13, 67);
			this.RookieGrid.Name = "RookieGrid";
			this.RookieGrid.RowTemplate.Height = 16;
			this.RookieGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.RookieGrid.Size = new System.Drawing.Size(991, 369);
			this.RookieGrid.TabIndex = 18;
			this.RookieGrid.TabStop = false;
			this.RookieGrid.Text = "dataGridView1";
			this.RookieGrid.Sorted += new System.EventHandler(this.RookieGrid_Sorted);
			this.RookieGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.RookieGrid_CellEndEdit);
			// 
			// RBhoursLabel
			// 
			this.RBhoursLabel.AutoSize = true;
			this.RBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RBhoursLabel.Location = new System.Drawing.Point(407, 43);
			this.RBhoursLabel.Name = "RBhoursLabel";
			this.RBhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.RBhoursLabel.TabIndex = 25;
			this.RBhoursLabel.Text = "0,";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(383, 43);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(23, 16);
			this.label8.TabIndex = 24;
			this.label8.Text = "RB";
			// 
			// WRhoursLabel
			// 
			this.WRhoursLabel.AutoSize = true;
			this.WRhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WRhoursLabel.Location = new System.Drawing.Point(458, 43);
			this.WRhoursLabel.Name = "WRhoursLabel";
			this.WRhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.WRhoursLabel.TabIndex = 29;
			this.WRhoursLabel.Text = "0,";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(431, 43);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(27, 16);
			this.label12.TabIndex = 28;
			this.label12.Text = "WR";
			// 
			// OLhoursLabel
			// 
			this.OLhoursLabel.AutoSize = true;
			this.OLhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OLhoursLabel.Location = new System.Drawing.Point(551, 43);
			this.OLhoursLabel.Name = "OLhoursLabel";
			this.OLhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.OLhoursLabel.TabIndex = 33;
			this.OLhoursLabel.Text = "0,";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(530, 43);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(21, 16);
			this.label14.TabIndex = 32;
			this.label14.Text = "OL";
			// 
			// TEhoursLabel
			// 
			this.TEhoursLabel.AutoSize = true;
			this.TEhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TEhoursLabel.Location = new System.Drawing.Point(504, 43);
			this.TEhoursLabel.Name = "TEhoursLabel";
			this.TEhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.TEhoursLabel.TabIndex = 31;
			this.TEhoursLabel.Text = "0,";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(483, 43);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(22, 16);
			this.label16.TabIndex = 30;
			this.label16.Text = "TE";
			// 
			// PhoursLabel
			// 
			this.PhoursLabel.AutoSize = true;
			this.PhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PhoursLabel.Location = new System.Drawing.Point(811, 43);
			this.PhoursLabel.Name = "PhoursLabel";
			this.PhoursLabel.Size = new System.Drawing.Size(11, 16);
			this.PhoursLabel.TabIndex = 45;
			this.PhoursLabel.Text = "0";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.Location = new System.Drawing.Point(798, 43);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(13, 16);
			this.label20.TabIndex = 44;
			this.label20.Text = "P";
			// 
			// KhoursLabel
			// 
			this.KhoursLabel.AutoSize = true;
			this.KhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KhoursLabel.Location = new System.Drawing.Point(777, 43);
			this.KhoursLabel.Name = "KhoursLabel";
			this.KhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.KhoursLabel.TabIndex = 43;
			this.KhoursLabel.Text = "0,";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.Location = new System.Drawing.Point(764, 43);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(12, 16);
			this.label22.TabIndex = 42;
			this.label22.Text = "K";
			// 
			// ShoursLabel
			// 
			this.ShoursLabel.AutoSize = true;
			this.ShoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ShoursLabel.Location = new System.Drawing.Point(739, 43);
			this.ShoursLabel.Name = "ShoursLabel";
			this.ShoursLabel.Size = new System.Drawing.Size(14, 16);
			this.ShoursLabel.TabIndex = 41;
			this.ShoursLabel.Text = "0,";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(725, 43);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(13, 16);
			this.label24.TabIndex = 40;
			this.label24.Text = "S";
			// 
			// CBhoursLabel
			// 
			this.CBhoursLabel.AutoSize = true;
			this.CBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CBhoursLabel.Location = new System.Drawing.Point(700, 43);
			this.CBhoursLabel.Name = "CBhoursLabel";
			this.CBhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.CBhoursLabel.TabIndex = 39;
			this.CBhoursLabel.Text = "0,";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.Location = new System.Drawing.Point(672, 43);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(22, 16);
			this.label26.TabIndex = 38;
			this.label26.Text = "CB";
			// 
			// LBhoursLabel
			// 
			this.LBhoursLabel.AutoSize = true;
			this.LBhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LBhoursLabel.Location = new System.Drawing.Point(646, 43);
			this.LBhoursLabel.Name = "LBhoursLabel";
			this.LBhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.LBhoursLabel.TabIndex = 37;
			this.LBhoursLabel.Text = "0,";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label28.Location = new System.Drawing.Point(625, 43);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(20, 16);
			this.label28.TabIndex = 36;
			this.label28.Text = "LB";
			// 
			// DLhoursLabel
			// 
			this.DLhoursLabel.AutoSize = true;
			this.DLhoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DLhoursLabel.Location = new System.Drawing.Point(599, 43);
			this.DLhoursLabel.Name = "DLhoursLabel";
			this.DLhoursLabel.Size = new System.Drawing.Size(14, 16);
			this.DLhoursLabel.TabIndex = 35;
			this.DLhoursLabel.Text = "0,";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(577, 43);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(21, 16);
			this.label10.TabIndex = 34;
			this.label10.Text = "DL";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(38, 446);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 16);
			this.label5.TabIndex = 46;
			this.label5.Text = "Set all";
			// 
			// setPosition
			// 
			this.setPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.setPosition.FormattingEnabled = true;
			this.setPosition.Location = new System.Drawing.Point(85, 444);
			this.setPosition.Name = "setPosition";
			this.setPosition.Size = new System.Drawing.Size(63, 21);
			this.setPosition.TabIndex = 47;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(158, 446);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(51, 16);
			this.label7.TabIndex = 48;
			this.label7.Text = "hours to";
			// 
			// setHours
			// 
			this.setHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.setHours.FormattingEnabled = true;
			this.setHours.Location = new System.Drawing.Point(215, 444);
			this.setHours.Name = "setHours";
			this.setHours.Size = new System.Drawing.Size(41, 21);
			this.setHours.TabIndex = 49;
			// 
			// doSet
			// 
			this.doSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.doSet.Location = new System.Drawing.Point(277, 444);
			this.doSet.Name = "doSet";
			this.doSet.Size = new System.Drawing.Size(31, 21);
			this.doSet.TabIndex = 50;
			this.doSet.Text = "Go";
			this.doSet.Click += new System.EventHandler(this.doSet_Click);
			// 
			// doIncrement
			// 
			this.doIncrement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.doIncrement.Location = new System.Drawing.Point(652, 444);
			this.doIncrement.Name = "doIncrement";
			this.doIncrement.Size = new System.Drawing.Size(31, 21);
			this.doIncrement.TabIndex = 55;
			this.doIncrement.Text = "Go";
			this.doIncrement.Click += new System.EventHandler(this.doIncrement_Click);
			// 
			// incrementHours
			// 
			this.incrementHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.incrementHours.FormattingEnabled = true;
			this.incrementHours.Location = new System.Drawing.Point(590, 444);
			this.incrementHours.Name = "incrementHours";
			this.incrementHours.Size = new System.Drawing.Size(41, 21);
			this.incrementHours.TabIndex = 54;
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(533, 446);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(55, 16);
			this.label9.TabIndex = 53;
			this.label9.Text = "hours by";
			// 
			// incrementPosition
			// 
			this.incrementPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.incrementPosition.FormattingEnabled = true;
			this.incrementPosition.Location = new System.Drawing.Point(460, 444);
			this.incrementPosition.Name = "incrementPosition";
			this.incrementPosition.Size = new System.Drawing.Size(63, 21);
			this.incrementPosition.TabIndex = 52;
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(372, 446);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(82, 16);
			this.label11.TabIndex = 51;
			this.label11.Text = "Increment  all";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(779, 442);
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
			this.menuStrip1.Size = new System.Drawing.Size(1016, 24);
			this.menuStrip1.TabIndex = 57;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoutingHelpToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// scoutingHelpToolStripMenuItem
			// 
			this.scoutingHelpToolStripMenuItem.Name = "scoutingHelpToolStripMenuItem";
			this.scoutingHelpToolStripMenuItem.Text = "Scouting Help";
			this.scoutingHelpToolStripMenuItem.Click += new System.EventHandler(this.scoutingHelpToolStripMenuItem_Click);
			// 
			// ScoutingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1016, 486);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.doIncrement);
			this.Controls.Add(this.incrementHours);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.incrementPosition);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.doSet);
			this.Controls.Add(this.setHours);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.setPosition);
			this.Controls.Add(this.label5);
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
			this.MinimumSize = new System.Drawing.Size(1024, 520);
			this.Name = "ScoutingForm";
			this.Text = "Rookie Scouting";
			this.Load += new System.EventHandler(this.ScoutingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.RookieGrid)).EndInit();
			this.menuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox setPosition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox setHours;
        private System.Windows.Forms.Button doSet;
        private System.Windows.Forms.Button doIncrement;
        private System.Windows.Forms.ComboBox incrementHours;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox incrementPosition;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scoutingHelpToolStripMenuItem;
    }
}