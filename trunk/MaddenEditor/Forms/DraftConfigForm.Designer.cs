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
            this.ResumeRound_Updown = new System.Windows.Forms.NumericUpDown();
            this.ResumePick_Updown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ResumeSelection_Updown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumeRound_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumePick_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumeSelection_Updown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desired Team";
            // 
            // teamChooser
            // 
            this.teamChooser.FormattingEnabled = true;
            this.teamChooser.Location = new System.Drawing.Point(128, 27);
            this.teamChooser.Name = "teamChooser";
            this.teamChooser.Size = new System.Drawing.Size(111, 21);
            this.teamChooser.TabIndex = 1;
            this.teamChooser.SelectedIndexChanged += new System.EventHandler(this.teamChooser_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time Per Pick";
            // 
            // minutes
            // 
            this.minutes.Location = new System.Drawing.Point(131, 58);
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(35, 20);
            this.minutes.TabIndex = 3;
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(191, 58);
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
            this.label3.Location = new System.Drawing.Point(169, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "M";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "S";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(71, 274);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(97, 23);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start Draft";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(202, 274);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(96, 23);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit Draft";
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // autoSave
            // 
            this.autoSave.AutoSize = true;
            this.autoSave.Checked = true;
            this.autoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSave.Location = new System.Drawing.Point(81, 98);
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
            this.overwrite.Location = new System.Drawing.Point(82, 121);
            this.overwrite.Name = "overwrite";
            this.overwrite.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.overwrite.Size = new System.Drawing.Size(155, 17);
            this.overwrite.TabIndex = 11;
            this.overwrite.Text = "Overwrite Previous Backup";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(18, 303);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(326, 17);
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // draftClass
            // 
            this.draftClass.AutoSize = true;
            this.draftClass.Location = new System.Drawing.Point(95, 144);
            this.draftClass.Name = "draftClass";
            this.draftClass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.draftClass.Size = new System.Drawing.Size(142, 17);
            this.draftClass.TabIndex = 13;
            this.draftClass.Text = "Load Custom Draft Class";
            // 
            // ResumeRound_Updown
            // 
            this.ResumeRound_Updown.Location = new System.Drawing.Point(123, 210);
            this.ResumeRound_Updown.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.ResumeRound_Updown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumeRound_Updown.Name = "ResumeRound_Updown";
            this.ResumeRound_Updown.Size = new System.Drawing.Size(40, 20);
            this.ResumeRound_Updown.TabIndex = 14;
            this.ResumeRound_Updown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumeRound_Updown.ValueChanged += new System.EventHandler(this.ResumeRound_Updown_ValueChanged);
            // 
            // ResumePick_Updown
            // 
            this.ResumePick_Updown.Location = new System.Drawing.Point(172, 210);
            this.ResumePick_Updown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.ResumePick_Updown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumePick_Updown.Name = "ResumePick_Updown";
            this.ResumePick_Updown.Size = new System.Drawing.Size(40, 20);
            this.ResumePick_Updown.TabIndex = 15;
            this.ResumePick_Updown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumePick_Updown.ValueChanged += new System.EventHandler(this.ResumePick_Updown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Round";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Pick";
            // 
            // ResumeSelection_Updown
            // 
            this.ResumeSelection_Updown.Location = new System.Drawing.Point(232, 210);
            this.ResumeSelection_Updown.Maximum = new decimal(new int[] {
            224,
            0,
            0,
            0});
            this.ResumeSelection_Updown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumeSelection_Updown.Name = "ResumeSelection_Updown";
            this.ResumeSelection_Updown.Size = new System.Drawing.Size(49, 20);
            this.ResumeSelection_Updown.TabIndex = 19;
            this.ResumeSelection_Updown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResumeSelection_Updown.ValueChanged += new System.EventHandler(this.ResumeSelection_Updown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Selection";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Start Draft at";
            // 
            // DraftConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 343);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ResumeSelection_Updown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ResumePick_Updown);
            this.Controls.Add(this.ResumeRound_Updown);
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
            this.Name = "DraftConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Draft Options";
            this.Load += new System.EventHandler(this.DraftConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumeRound_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumePick_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResumeSelection_Updown)).EndInit();
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
        private System.Windows.Forms.NumericUpDown ResumeRound_Updown;
        private System.Windows.Forms.NumericUpDown ResumePick_Updown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown ResumeSelection_Updown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}