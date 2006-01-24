namespace MaddenEditor.Forms
{
	partial class DeveloperBioForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeveloperBioForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.tbBioText = new System.Windows.Forms.TextBox();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.tbRole = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.btnColin = new System.Windows.Forms.Button();
			this.btnJosh = new System.Windows.Forms.Button();
			this.btnDavid = new System.Windows.Forms.Button();
			this.lblNick = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.btnOK, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbBioText, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlTop, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(599, 359);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(521, 330);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 26);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tbBioText
			// 
			this.tbBioText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbBioText.Location = new System.Drawing.Point(3, 123);
			this.tbBioText.Multiline = true;
			this.tbBioText.Name = "tbBioText";
			this.tbBioText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbBioText.Size = new System.Drawing.Size(593, 201);
			this.tbBioText.TabIndex = 2;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.lblNick);
			this.pnlTop.Controls.Add(this.tbRole);
			this.pnlTop.Controls.Add(this.lblName);
			this.pnlTop.Controls.Add(this.btnColin);
			this.pnlTop.Controls.Add(this.btnJosh);
			this.pnlTop.Controls.Add(this.btnDavid);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTop.Location = new System.Drawing.Point(3, 3);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(593, 114);
			this.pnlTop.TabIndex = 3;
			// 
			// tbRole
			// 
			this.tbRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbRole.BackColor = System.Drawing.SystemColors.Control;
			this.tbRole.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbRole.Location = new System.Drawing.Point(402, 43);
			this.tbRole.Multiline = true;
			this.tbRole.Name = "tbRole";
			this.tbRole.Size = new System.Drawing.Size(182, 68);
			this.tbRole.TabIndex = 8;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(399, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(46, 16);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "name";
			// 
			// btnColin
			// 
			this.btnColin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColin.BackgroundImage")));
			this.btnColin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnColin.Location = new System.Drawing.Point(3, 3);
			this.btnColin.Name = "btnColin";
			this.btnColin.Size = new System.Drawing.Size(126, 108);
			this.btnColin.TabIndex = 3;
			this.btnColin.UseVisualStyleBackColor = true;
			this.btnColin.Click += new System.EventHandler(this.btnColin_Click);
			// 
			// btnJosh
			// 
			this.btnJosh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnJosh.Location = new System.Drawing.Point(135, 3);
			this.btnJosh.Name = "btnJosh";
			this.btnJosh.Size = new System.Drawing.Size(126, 108);
			this.btnJosh.TabIndex = 4;
			this.btnJosh.Text = "Josh";
			this.btnJosh.UseVisualStyleBackColor = true;
			this.btnJosh.Click += new System.EventHandler(this.btnJosh_Click);
			// 
			// btnDavid
			// 
			this.btnDavid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDavid.BackgroundImage")));
			this.btnDavid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnDavid.Location = new System.Drawing.Point(267, 3);
			this.btnDavid.Name = "btnDavid";
			this.btnDavid.Size = new System.Drawing.Size(126, 108);
			this.btnDavid.TabIndex = 5;
			this.btnDavid.UseVisualStyleBackColor = true;
			this.btnDavid.Click += new System.EventHandler(this.btnDavid_Click);
			// 
			// lblNick
			// 
			this.lblNick.AutoSize = true;
			this.lblNick.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNick.Location = new System.Drawing.Point(399, 22);
			this.lblNick.Name = "lblNick";
			this.lblNick.Size = new System.Drawing.Size(29, 15);
			this.lblNick.TabIndex = 9;
			this.lblNick.Text = "nick";
			// 
			// DeveloperBioForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(599, 359);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DeveloperBioForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Developer Bio\'s";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox tbBioText;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Button btnColin;
		private System.Windows.Forms.Button btnJosh;
		private System.Windows.Forms.Button btnDavid;
		private System.Windows.Forms.TextBox tbRole;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblNick;

	}
}