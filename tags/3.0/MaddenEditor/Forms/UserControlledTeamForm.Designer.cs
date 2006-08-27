namespace MaddenEditor.Forms
{
	partial class UserControlledTeamForm
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
			this.teamCombo = new System.Windows.Forms.ComboBox();
			this.addTeamButton = new System.Windows.Forms.Button();
			this.teamsList = new System.Windows.Forms.ListBox();
			this.removeSelectedButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.addAllButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Teams";
			// 
			// teamCombo
			// 
			this.teamCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.teamCombo.FormattingEnabled = true;
			this.teamCombo.Location = new System.Drawing.Point(53, 6);
			this.teamCombo.Name = "teamCombo";
			this.teamCombo.Size = new System.Drawing.Size(163, 21);
			this.teamCombo.TabIndex = 1;
			this.teamCombo.SelectedIndexChanged += new System.EventHandler(this.teamCombo_SelectedIndexChanged);
			// 
			// addTeamButton
			// 
			this.addTeamButton.Location = new System.Drawing.Point(222, 4);
			this.addTeamButton.Name = "addTeamButton";
			this.addTeamButton.Size = new System.Drawing.Size(75, 23);
			this.addTeamButton.TabIndex = 2;
			this.addTeamButton.Text = "Add";
			this.addTeamButton.Click += new System.EventHandler(this.addTeamButton_Click);
			// 
			// teamsList
			// 
			this.teamsList.FormattingEnabled = true;
			this.teamsList.Location = new System.Drawing.Point(12, 33);
			this.teamsList.Name = "teamsList";
			this.teamsList.ScrollAlwaysVisible = true;
			this.teamsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.teamsList.Size = new System.Drawing.Size(361, 199);
			this.teamsList.TabIndex = 3;
			// 
			// removeSelectedButton
			// 
			this.removeSelectedButton.Location = new System.Drawing.Point(13, 238);
			this.removeSelectedButton.Name = "removeSelectedButton";
			this.removeSelectedButton.Size = new System.Drawing.Size(105, 23);
			this.removeSelectedButton.TabIndex = 4;
			this.removeSelectedButton.Text = "Remove Selected";
			this.removeSelectedButton.Click += new System.EventHandler(this.removeSelectedButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.Location = new System.Drawing.Point(222, 238);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 5;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(303, 238);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// addAllButton
			// 
			this.addAllButton.Location = new System.Drawing.Point(303, 4);
			this.addAllButton.Name = "addAllButton";
			this.addAllButton.Size = new System.Drawing.Size(75, 23);
			this.addAllButton.TabIndex = 7;
			this.addAllButton.Text = "Add All";
			this.addAllButton.Click += new System.EventHandler(this.addAllButton_Click);
			// 
			// UserControlledTeamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 266);
			this.Controls.Add(this.addAllButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.removeSelectedButton);
			this.Controls.Add(this.teamsList);
			this.Controls.Add(this.addTeamButton);
			this.Controls.Add(this.teamCombo);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "UserControlledTeamForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select User Controlled Teams";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox teamCombo;
		private System.Windows.Forms.Button addTeamButton;
		private System.Windows.Forms.ListBox teamsList;
		private System.Windows.Forms.Button removeSelectedButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button addAllButton;
	}
}