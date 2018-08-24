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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlledTeamForm));
            this.PlayDIVGames_Checkbox = new System.Windows.Forms.CheckBox();
            this.PlayAwayGames_Checkbox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.ApplyChanges_Button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PlayHomeGames_Checkbox = new System.Windows.Forms.CheckBox();
            this.PlayALLGames_Checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayDIVGames_Checkbox
            // 
            this.PlayDIVGames_Checkbox.AutoSize = true;
            this.PlayDIVGames_Checkbox.Location = new System.Drawing.Point(56, 133);
            this.PlayDIVGames_Checkbox.Name = "PlayDIVGames_Checkbox";
            this.PlayDIVGames_Checkbox.Size = new System.Drawing.Size(136, 17);
            this.PlayDIVGames_Checkbox.TabIndex = 10;
            this.PlayDIVGames_Checkbox.Text = "Play All Division Games";
            this.PlayDIVGames_Checkbox.UseVisualStyleBackColor = true;
            // 
            // PlayAwayGames_Checkbox
            // 
            this.PlayAwayGames_Checkbox.AutoSize = true;
            this.PlayAwayGames_Checkbox.Location = new System.Drawing.Point(56, 110);
            this.PlayAwayGames_Checkbox.Name = "PlayAwayGames_Checkbox";
            this.PlayAwayGames_Checkbox.Size = new System.Drawing.Size(111, 17);
            this.PlayAwayGames_Checkbox.TabIndex = 9;
            this.PlayAwayGames_Checkbox.Text = "Play Away Games";
            this.PlayAwayGames_Checkbox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 670);
            this.panel1.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.ApplyChanges_Button);
            this.panel4.Location = new System.Drawing.Point(3, 450);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(292, 214);
            this.panel4.TabIndex = 13;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Info;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(3, 13);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(284, 47);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "Note : Changes are not saved until Apply Changes button is clicked";
            // 
            // ApplyChanges_Button
            // 
            this.ApplyChanges_Button.BackColor = System.Drawing.Color.Gainsboro;
            this.ApplyChanges_Button.Location = new System.Drawing.Point(56, 175);
            this.ApplyChanges_Button.Name = "ApplyChanges_Button";
            this.ApplyChanges_Button.Size = new System.Drawing.Size(176, 23);
            this.ApplyChanges_Button.TabIndex = 0;
            this.ApplyChanges_Button.Text = "Apply Changes";
            this.ApplyChanges_Button.UseVisualStyleBackColor = false;
            this.ApplyChanges_Button.Click += new System.EventHandler(this.ApplyChanges_Button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.PlayHomeGames_Checkbox);
            this.panel3.Controls.Add(this.PlayALLGames_Checkbox);
            this.panel3.Controls.Add(this.PlayDIVGames_Checkbox);
            this.panel3.Controls.Add(this.PlayAwayGames_Checkbox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 441);
            this.panel3.TabIndex = 12;
            // 
            // PlayHomeGames_Checkbox
            // 
            this.PlayHomeGames_Checkbox.AutoSize = true;
            this.PlayHomeGames_Checkbox.Location = new System.Drawing.Point(56, 87);
            this.PlayHomeGames_Checkbox.Name = "PlayHomeGames_Checkbox";
            this.PlayHomeGames_Checkbox.Size = new System.Drawing.Size(113, 17);
            this.PlayHomeGames_Checkbox.TabIndex = 0;
            this.PlayHomeGames_Checkbox.Text = "Play Home Games";
            this.PlayHomeGames_Checkbox.UseVisualStyleBackColor = true;
            // 
            // PlayALLGames_Checkbox
            // 
            this.PlayALLGames_Checkbox.AutoSize = true;
            this.PlayALLGames_Checkbox.Location = new System.Drawing.Point(56, 64);
            this.PlayALLGames_Checkbox.Name = "PlayALLGames_Checkbox";
            this.PlayALLGames_Checkbox.Size = new System.Drawing.Size(104, 17);
            this.PlayALLGames_Checkbox.TabIndex = 0;
            this.PlayALLGames_Checkbox.Text = "Play ALL Games";
            this.PlayALLGames_Checkbox.UseVisualStyleBackColor = true;
            this.PlayALLGames_Checkbox.CheckedChanged += new System.EventHandler(this.PlayALLGames_Checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "USER Controlled Team Options";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(309, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 670);
            this.panel2.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 367);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(284, 69);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // UserControlledTeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UserControlledTeamForm";
            this.Size = new System.Drawing.Size(998, 668);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.CheckBox PlayDIVGames_Checkbox;
        private System.Windows.Forms.CheckBox PlayAwayGames_Checkbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ApplyChanges_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox PlayHomeGames_Checkbox;
        private System.Windows.Forms.CheckBox PlayALLGames_Checkbox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
	}
}