namespace MaddenEditor.Forms
{
	partial class ExceptionDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDialog));
			this.tbException = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.lblException = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbException
			// 
			this.tbException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbException.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tbException.Location = new System.Drawing.Point(12, 28);
			this.tbException.Multiline = true;
			this.tbException.Name = "tbException";
			this.tbException.ReadOnly = true;
			this.tbException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbException.Size = new System.Drawing.Size(395, 172);
			this.tbException.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(332, 206);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 31);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// lblException
			// 
			this.lblException.AutoSize = true;
			this.lblException.Location = new System.Drawing.Point(12, 9);
			this.lblException.Name = "lblException";
			this.lblException.Size = new System.Drawing.Size(116, 13);
			this.lblException.TabIndex = 6;
			this.lblException.Tag = "";
			this.lblException.Text = "Exception has occured";
			// 
			// ExceptionDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(410, 240);
			this.Controls.Add(this.lblException);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.tbException);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExceptionDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Exception occured in MaddenAmp";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbException;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label lblException;
	}
}