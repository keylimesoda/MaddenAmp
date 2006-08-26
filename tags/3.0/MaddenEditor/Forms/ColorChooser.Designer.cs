namespace MaddenEditor.Forms
{
	partial class ColorChooser
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.vsBlue = new System.Windows.Forms.VScrollBar();
			this.vsGreen = new System.Windows.Forms.VScrollBar();
			this.vsRed = new System.Windows.Forms.VScrollBar();
			this.gbColorCodes = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.nudHexadecimal = new System.Windows.Forms.NumericUpDown();
			this.nudBlue = new System.Windows.Forms.NumericUpDown();
			this.nudGreen = new System.Windows.Forms.NumericUpDown();
			this.nudRed = new System.Windows.Forms.NumericUpDown();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlPreview = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.gbColorCodes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudHexadecimal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBlue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGreen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRed)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.vsBlue);
			this.groupBox1.Controls.Add(this.vsGreen);
			this.groupBox1.Controls.Add(this.vsRed);
			this.groupBox1.Location = new System.Drawing.Point(2, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(150, 244);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Color Choice";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(105, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Blue";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.label2.Location = new System.Drawing.Point(61, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Green";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(19, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Red";
			// 
			// vsBlue
			// 
			this.vsBlue.LargeChange = 1;
			this.vsBlue.Location = new System.Drawing.Point(106, 54);
			this.vsBlue.Maximum = 255;
			this.vsBlue.Name = "vsBlue";
			this.vsBlue.Size = new System.Drawing.Size(17, 175);
			this.vsBlue.TabIndex = 2;
			this.vsBlue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsBlue_Scroll);
			// 
			// vsGreen
			// 
			this.vsGreen.LargeChange = 1;
			this.vsGreen.Location = new System.Drawing.Point(62, 54);
			this.vsGreen.Maximum = 255;
			this.vsGreen.Name = "vsGreen";
			this.vsGreen.Size = new System.Drawing.Size(17, 175);
			this.vsGreen.TabIndex = 1;
			this.vsGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsGreen_Scroll);
			// 
			// vsRed
			// 
			this.vsRed.LargeChange = 1;
			this.vsRed.Location = new System.Drawing.Point(20, 54);
			this.vsRed.Maximum = 255;
			this.vsRed.Name = "vsRed";
			this.vsRed.Size = new System.Drawing.Size(17, 175);
			this.vsRed.TabIndex = 0;
			this.vsRed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsRed_Scroll);
			// 
			// gbColorCodes
			// 
			this.gbColorCodes.Controls.Add(this.label7);
			this.gbColorCodes.Controls.Add(this.label6);
			this.gbColorCodes.Controls.Add(this.label5);
			this.gbColorCodes.Controls.Add(this.label4);
			this.gbColorCodes.Controls.Add(this.nudHexadecimal);
			this.gbColorCodes.Controls.Add(this.nudBlue);
			this.gbColorCodes.Controls.Add(this.nudGreen);
			this.gbColorCodes.Controls.Add(this.nudRed);
			this.gbColorCodes.Location = new System.Drawing.Point(158, 3);
			this.gbColorCodes.Name = "gbColorCodes";
			this.gbColorCodes.Size = new System.Drawing.Size(220, 139);
			this.gbColorCodes.TabIndex = 1;
			this.gbColorCodes.TabStop = false;
			this.gbColorCodes.Text = "Color Codes";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(33, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Hexadecimal";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Blue;
			this.label6.Location = new System.Drawing.Point(73, 78);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Blue";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.label5.Location = new System.Drawing.Point(65, 52);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Green";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(74, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Red";
			// 
			// nudHexadecimal
			// 
			this.nudHexadecimal.Hexadecimal = true;
			this.nudHexadecimal.Location = new System.Drawing.Point(103, 113);
			this.nudHexadecimal.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
			this.nudHexadecimal.Name = "nudHexadecimal";
			this.nudHexadecimal.Size = new System.Drawing.Size(88, 20);
			this.nudHexadecimal.TabIndex = 3;
			this.nudHexadecimal.ValueChanged += new System.EventHandler(this.nudHexadecimal_ValueChanged);
			// 
			// nudBlue
			// 
			this.nudBlue.Location = new System.Drawing.Point(103, 71);
			this.nudBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudBlue.Name = "nudBlue";
			this.nudBlue.Size = new System.Drawing.Size(59, 20);
			this.nudBlue.TabIndex = 2;
			this.nudBlue.ValueChanged += new System.EventHandler(this.nudBlue_ValueChanged);
			// 
			// nudGreen
			// 
			this.nudGreen.Location = new System.Drawing.Point(103, 45);
			this.nudGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudGreen.Name = "nudGreen";
			this.nudGreen.Size = new System.Drawing.Size(59, 20);
			this.nudGreen.TabIndex = 1;
			this.nudGreen.ValueChanged += new System.EventHandler(this.nudGreen_ValueChanged);
			// 
			// nudRed
			// 
			this.nudRed.Location = new System.Drawing.Point(103, 19);
			this.nudRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudRed.Name = "nudRed";
			this.nudRed.Size = new System.Drawing.Size(59, 20);
			this.nudRed.TabIndex = 0;
			this.nudRed.ValueChanged += new System.EventHandler(this.nudRed_ValueChanged);
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(222, 221);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 3;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(303, 221);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pnlPreview
			// 
			this.pnlPreview.Location = new System.Drawing.Point(158, 148);
			this.pnlPreview.Name = "pnlPreview";
			this.pnlPreview.Size = new System.Drawing.Size(220, 67);
			this.pnlPreview.TabIndex = 4;
			// 
			// ColorChooser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(383, 250);
			this.Controls.Add(this.pnlPreview);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.gbColorCodes);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ColorChooser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ColorChooser";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbColorCodes.ResumeLayout(false);
			this.gbColorCodes.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudHexadecimal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBlue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGreen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRed)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox gbColorCodes;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.VScrollBar vsBlue;
		private System.Windows.Forms.VScrollBar vsGreen;
		private System.Windows.Forms.VScrollBar vsRed;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudHexadecimal;
		private System.Windows.Forms.NumericUpDown nudBlue;
		private System.Windows.Forms.NumericUpDown nudGreen;
		private System.Windows.Forms.NumericUpDown nudRed;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel pnlPreview;
	}
}