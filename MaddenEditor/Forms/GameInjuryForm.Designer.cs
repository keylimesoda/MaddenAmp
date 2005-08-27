namespace MaddenEditor.Forms
{
	partial class GameInjuryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameInjuryForm));
			this.tbInGameInjury = new System.Windows.Forms.TrackBar();
			this.tbSimInjury = new System.Windows.Forms.TrackBar();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.nudSimInjury = new System.Windows.Forms.NumericUpDown();
			this.nudInGameInjury = new System.Windows.Forms.NumericUpDown();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tbInGameInjury)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSimInjury)).BeginInit();
			this.groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSimInjury)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInGameInjury)).BeginInit();
			this.SuspendLayout();
			// 
			// tbInGameInjury
			// 
			this.tbInGameInjury.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbInGameInjury.Location = new System.Drawing.Point(12, 40);
			this.tbInGameInjury.Maximum = 300;
			this.tbInGameInjury.Name = "tbInGameInjury";
			this.tbInGameInjury.Size = new System.Drawing.Size(304, 45);
			this.tbInGameInjury.TabIndex = 0;
			this.tbInGameInjury.Value = 100;
			this.tbInGameInjury.Scroll += new System.EventHandler(this.tbInGameInjury_Scroll);
			// 
			// tbSimInjury
			// 
			this.tbSimInjury.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbSimInjury.Location = new System.Drawing.Point(12, 134);
			this.tbSimInjury.Maximum = 300;
			this.tbSimInjury.Name = "tbSimInjury";
			this.tbSimInjury.Size = new System.Drawing.Size(304, 45);
			this.tbSimInjury.TabIndex = 1;
			this.tbSimInjury.Value = 100;
			this.tbSimInjury.Scroll += new System.EventHandler(this.tbSimInjury_Scroll);
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.nudSimInjury);
			this.groupBox.Controls.Add(this.nudInGameInjury);
			this.groupBox.Controls.Add(this.applyButton);
			this.groupBox.Controls.Add(this.cancelButton);
			this.groupBox.Controls.Add(this.label2);
			this.groupBox.Controls.Add(this.label1);
			this.groupBox.Controls.Add(this.tbInGameInjury);
			this.groupBox.Controls.Add(this.tbSimInjury);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(382, 214);
			this.groupBox.TabIndex = 2;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Game Injury Settings";
			// 
			// nudSimInjury
			// 
			this.nudSimInjury.Location = new System.Drawing.Point(316, 143);
			this.nudSimInjury.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudSimInjury.Name = "nudSimInjury";
			this.nudSimInjury.Size = new System.Drawing.Size(60, 20);
			this.nudSimInjury.TabIndex = 9;
			this.nudSimInjury.ValueChanged += new System.EventHandler(this.nudSimInjury_ValueChanged);
			// 
			// nudInGameInjury
			// 
			this.nudInGameInjury.Location = new System.Drawing.Point(316, 50);
			this.nudInGameInjury.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudInGameInjury.Name = "nudInGameInjury";
			this.nudInGameInjury.Size = new System.Drawing.Size(60, 20);
			this.nudInGameInjury.TabIndex = 8;
			this.nudInGameInjury.ValueChanged += new System.EventHandler(this.nudInGameInjury_ValueChanged);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(214, 185);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 7;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(295, 185);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(153, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Sim Injury";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(140, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "In Game Injury";
			// 
			// GameInjuryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 214);
			this.Controls.Add(this.groupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GameInjuryForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Game Injury Settings";
			((System.ComponentModel.ISupportInitialize)(this.tbInGameInjury)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSimInjury)).EndInit();
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSimInjury)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInGameInjury)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TrackBar tbInGameInjury;
		private System.Windows.Forms.TrackBar tbSimInjury;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.NumericUpDown nudSimInjury;
		private System.Windows.Forms.NumericUpDown nudInGameInjury;
	}
}