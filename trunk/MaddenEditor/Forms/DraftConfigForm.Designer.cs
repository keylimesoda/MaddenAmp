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
    partial class DraftConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.teamChooser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.minutes = new System.Windows.Forms.NumericUpDown();
            this.seconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.autoSave = new System.Windows.Forms.CheckBox();
            this.overwrite = new System.Windows.Forms.CheckBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.draftClass = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Team";
            // 
            // teamChooser
            // 
            this.teamChooser.FormattingEnabled = true;
            this.teamChooser.Location = new System.Drawing.Point(90, 26);
            this.teamChooser.Name = "teamChooser";
            this.teamChooser.Size = new System.Drawing.Size(111, 21);
            this.teamChooser.TabIndex = 1;
            this.teamChooser.SelectedIndexChanged += new System.EventHandler(this.teamChooser_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time Per Pick";
            // 
            // minutes
            // 
            this.minutes.Location = new System.Drawing.Point(93, 57);
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(35, 20);
            this.minutes.TabIndex = 3;
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(150, 57);
            this.seconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(35, 20);
            this.seconds.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "M";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "S";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(37, 200);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(71, 23);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start Draft";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(133, 200);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(70, 23);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit Draft";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // autoSave
            // 
            this.autoSave.AutoSize = true;
            this.autoSave.Checked = true;
            this.autoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSave.Location = new System.Drawing.Point(43, 97);
            this.autoSave.Name = "autoSave";
            this.autoSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.autoSave.Size = new System.Drawing.Size(156, 17);
            this.autoSave.TabIndex = 10;
            this.autoSave.Text = "Auto-Backup Franchise File";
            // 
            // overwrite
            // 
            this.overwrite.AutoSize = true;
            this.overwrite.Checked = true;
            this.overwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.overwrite.Location = new System.Drawing.Point(43, 120);
            this.overwrite.Name = "overwrite";
            this.overwrite.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.overwrite.Size = new System.Drawing.Size(155, 17);
            this.overwrite.TabIndex = 11;
            this.overwrite.Text = "Overwrite Previous Backup";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(2, 236);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(237, 16);
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // draftClass
            // 
            this.draftClass.AutoSize = true;
            this.draftClass.Location = new System.Drawing.Point(50, 143);
            this.draftClass.Name = "draftClass";
            this.draftClass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.draftClass.Size = new System.Drawing.Size(142, 17);
            this.draftClass.TabIndex = 13;
            this.draftClass.Text = "Load Custom Draft Class";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(68, 166);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Optional Enhanced";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DraftConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 257);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.draftClass);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.overwrite);
            this.Controls.Add(this.autoSave);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.seconds);
            this.Controls.Add(this.minutes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.teamChooser);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(247, 295);
            this.MinimumSize = new System.Drawing.Size(247, 295);
            this.Name = "DraftConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Draft Options";
            this.Load += new System.EventHandler(this.DraftConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox teamChooser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minutes;
        private System.Windows.Forms.NumericUpDown seconds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.CheckBox autoSave;
        private System.Windows.Forms.CheckBox overwrite;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox draftClass;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}