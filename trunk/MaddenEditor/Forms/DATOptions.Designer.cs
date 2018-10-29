namespace MaddenEditor.Forms
{
    partial class DATOptions
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
            this.PlayerPortLocation_Textbox = new System.Windows.Forms.TextBox();
            this.DefaultPlayerPort_Checkbox = new System.Windows.Forms.CheckBox();
            this.DefaultCoachPort_Checkbox = new System.Windows.Forms.CheckBox();
            this.CoachPortLocation_Textbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CoachMakeDefault_Button = new System.Windows.Forms.Button();
            this.PlayerMakeDefault_Button = new System.Windows.Forms.Button();
            this.AskForCoachSave_Checkbox = new System.Windows.Forms.CheckBox();
            this.AskForPlayerSave_Checkbox = new System.Windows.Forms.CheckBox();
            this.LoadCoachDAT_Button = new System.Windows.Forms.Button();
            this.LoadPlayerDAT_Button = new System.Windows.Forms.Button();
            this.SaveCoachDat_Button = new System.Windows.Forms.Button();
            this.SavePlayerDAT_Button = new System.Windows.Forms.Button();
            this.ExportCoachPortPack_Button = new System.Windows.Forms.Button();
            this.ExportPlayerID_Checkbox = new System.Windows.Forms.CheckBox();
            this.ImportCoachPortPack_Button = new System.Windows.Forms.Button();
            this.ExportPlayerPortPack_Button = new System.Windows.Forms.Button();
            this.ImportPlayerPortPack_Button = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PortFax_Box = new System.Windows.Forms.RichTextBox();
            this.PlayerRepoLoad_Button = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ExportCoachID_Checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.DATProgress = new System.Windows.Forms.ProgressBar();
            this.DATComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentCoachPort_Textbox = new System.Windows.Forms.TextBox();
            this.CurrentPlayerPort_Textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerPortLocation_Textbox
            // 
            this.PlayerPortLocation_Textbox.Location = new System.Drawing.Point(152, 20);
            this.PlayerPortLocation_Textbox.Name = "PlayerPortLocation_Textbox";
            this.PlayerPortLocation_Textbox.ReadOnly = true;
            this.PlayerPortLocation_Textbox.Size = new System.Drawing.Size(270, 20);
            this.PlayerPortLocation_Textbox.TabIndex = 1;
            // 
            // DefaultPlayerPort_Checkbox
            // 
            this.DefaultPlayerPort_Checkbox.AutoSize = true;
            this.DefaultPlayerPort_Checkbox.Location = new System.Drawing.Point(9, 21);
            this.DefaultPlayerPort_Checkbox.Name = "DefaultPlayerPort_Checkbox";
            this.DefaultPlayerPort_Checkbox.Size = new System.Drawing.Size(139, 19);
            this.DefaultPlayerPort_Checkbox.TabIndex = 2;
            this.DefaultPlayerPort_Checkbox.Text = "Load Player Portraits";
            this.DefaultPlayerPort_Checkbox.UseVisualStyleBackColor = true;
            this.DefaultPlayerPort_Checkbox.CheckedChanged += new System.EventHandler(this.DefaultPlayerPort_Checkbox_CheckedChanged);
            // 
            // DefaultCoachPort_Checkbox
            // 
            this.DefaultCoachPort_Checkbox.AutoSize = true;
            this.DefaultCoachPort_Checkbox.Location = new System.Drawing.Point(9, 45);
            this.DefaultCoachPort_Checkbox.Name = "DefaultCoachPort_Checkbox";
            this.DefaultCoachPort_Checkbox.Size = new System.Drawing.Size(140, 19);
            this.DefaultCoachPort_Checkbox.TabIndex = 4;
            this.DefaultCoachPort_Checkbox.Text = "Load Coach Portraits";
            this.DefaultCoachPort_Checkbox.UseVisualStyleBackColor = true;
            this.DefaultCoachPort_Checkbox.CheckedChanged += new System.EventHandler(this.DefaultCoachPort_Checkbox_CheckedChanged);
            // 
            // CoachPortLocation_Textbox
            // 
            this.CoachPortLocation_Textbox.Location = new System.Drawing.Point(152, 45);
            this.CoachPortLocation_Textbox.Name = "CoachPortLocation_Textbox";
            this.CoachPortLocation_Textbox.ReadOnly = true;
            this.CoachPortLocation_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CoachPortLocation_Textbox.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.CoachMakeDefault_Button);
            this.groupBox1.Controls.Add(this.PlayerPortLocation_Textbox);
            this.groupBox1.Controls.Add(this.PlayerMakeDefault_Button);
            this.groupBox1.Controls.Add(this.CoachPortLocation_Textbox);
            this.groupBox1.Controls.Add(this.DefaultPlayerPort_Checkbox);
            this.groupBox1.Controls.Add(this.DefaultCoachPort_Checkbox);
            this.groupBox1.Location = new System.Drawing.Point(5, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(799, 77);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Portrait Default DATs";
            // 
            // CoachMakeDefault_Button
            // 
            this.CoachMakeDefault_Button.Location = new System.Drawing.Point(428, 44);
            this.CoachMakeDefault_Button.Name = "CoachMakeDefault_Button";
            this.CoachMakeDefault_Button.Size = new System.Drawing.Size(96, 23);
            this.CoachMakeDefault_Button.TabIndex = 23;
            this.CoachMakeDefault_Button.Text = "Use Current";
            this.CoachMakeDefault_Button.UseVisualStyleBackColor = true;
            this.CoachMakeDefault_Button.Click += new System.EventHandler(this.CoachMakeDefault_Button_Click);
            // 
            // PlayerMakeDefault_Button
            // 
            this.PlayerMakeDefault_Button.Location = new System.Drawing.Point(428, 20);
            this.PlayerMakeDefault_Button.Name = "PlayerMakeDefault_Button";
            this.PlayerMakeDefault_Button.Size = new System.Drawing.Size(96, 23);
            this.PlayerMakeDefault_Button.TabIndex = 22;
            this.PlayerMakeDefault_Button.Text = "Use Current";
            this.PlayerMakeDefault_Button.UseVisualStyleBackColor = true;
            this.PlayerMakeDefault_Button.Click += new System.EventHandler(this.PlayerMakeDefault_Button_Click);
            // 
            // AskForCoachSave_Checkbox
            // 
            this.AskForCoachSave_Checkbox.AutoSize = true;
            this.AskForCoachSave_Checkbox.Location = new System.Drawing.Point(651, 68);
            this.AskForCoachSave_Checkbox.Name = "AskForCoachSave_Checkbox";
            this.AskForCoachSave_Checkbox.Size = new System.Drawing.Size(129, 19);
            this.AskForCoachSave_Checkbox.TabIndex = 11;
            this.AskForCoachSave_Checkbox.Text = "Ask for Save Name";
            this.AskForCoachSave_Checkbox.UseVisualStyleBackColor = true;
            this.AskForCoachSave_Checkbox.CheckedChanged += new System.EventHandler(this.AskForCoachSave_Checkbox_CheckedChanged);
            // 
            // AskForPlayerSave_Checkbox
            // 
            this.AskForPlayerSave_Checkbox.AutoSize = true;
            this.AskForPlayerSave_Checkbox.Location = new System.Drawing.Point(651, 39);
            this.AskForPlayerSave_Checkbox.Name = "AskForPlayerSave_Checkbox";
            this.AskForPlayerSave_Checkbox.Size = new System.Drawing.Size(129, 19);
            this.AskForPlayerSave_Checkbox.TabIndex = 10;
            this.AskForPlayerSave_Checkbox.Text = "Ask for Save Name";
            this.AskForPlayerSave_Checkbox.UseVisualStyleBackColor = true;
            this.AskForPlayerSave_Checkbox.CheckedChanged += new System.EventHandler(this.AskForPlayerSave_Checkbox_CheckedChanged);
            // 
            // LoadCoachDAT_Button
            // 
            this.LoadCoachDAT_Button.Location = new System.Drawing.Point(415, 66);
            this.LoadCoachDAT_Button.Name = "LoadCoachDAT_Button";
            this.LoadCoachDAT_Button.Size = new System.Drawing.Size(90, 23);
            this.LoadCoachDAT_Button.TabIndex = 9;
            this.LoadCoachDAT_Button.Text = "Load New";
            this.LoadCoachDAT_Button.UseVisualStyleBackColor = true;
            this.LoadCoachDAT_Button.Click += new System.EventHandler(this.LoadCoachDAT_Button_Click);
            // 
            // LoadPlayerDAT_Button
            // 
            this.LoadPlayerDAT_Button.Location = new System.Drawing.Point(415, 37);
            this.LoadPlayerDAT_Button.Name = "LoadPlayerDAT_Button";
            this.LoadPlayerDAT_Button.Size = new System.Drawing.Size(90, 23);
            this.LoadPlayerDAT_Button.TabIndex = 8;
            this.LoadPlayerDAT_Button.Text = "Load New";
            this.LoadPlayerDAT_Button.UseVisualStyleBackColor = true;
            this.LoadPlayerDAT_Button.Click += new System.EventHandler(this.LoadPlayerDAT_Button_Click);
            // 
            // SaveCoachDat_Button
            // 
            this.SaveCoachDat_Button.Location = new System.Drawing.Point(511, 66);
            this.SaveCoachDat_Button.Name = "SaveCoachDat_Button";
            this.SaveCoachDat_Button.Size = new System.Drawing.Size(134, 23);
            this.SaveCoachDat_Button.TabIndex = 7;
            this.SaveCoachDat_Button.Text = "Save Coach Port DAT";
            this.SaveCoachDat_Button.UseVisualStyleBackColor = true;
            this.SaveCoachDat_Button.Click += new System.EventHandler(this.SaveCoachDat_Button_Click);
            // 
            // SavePlayerDAT_Button
            // 
            this.SavePlayerDAT_Button.Location = new System.Drawing.Point(511, 37);
            this.SavePlayerDAT_Button.Name = "SavePlayerDAT_Button";
            this.SavePlayerDAT_Button.Size = new System.Drawing.Size(134, 23);
            this.SavePlayerDAT_Button.TabIndex = 6;
            this.SavePlayerDAT_Button.Text = "Save Player Port DAT";
            this.SavePlayerDAT_Button.UseVisualStyleBackColor = true;
            this.SavePlayerDAT_Button.Click += new System.EventHandler(this.SaveDAT_Button_Click);
            // 
            // ExportCoachPortPack_Button
            // 
            this.ExportCoachPortPack_Button.Location = new System.Drawing.Point(9, 53);
            this.ExportCoachPortPack_Button.Name = "ExportCoachPortPack_Button";
            this.ExportCoachPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ExportCoachPortPack_Button.TabIndex = 10;
            this.ExportCoachPortPack_Button.Text = "Export Coach Port Pack";
            this.ExportCoachPortPack_Button.UseVisualStyleBackColor = true;
            this.ExportCoachPortPack_Button.Click += new System.EventHandler(this.ExportCoachPortPack_Button_Click);
            // 
            // ExportPlayerID_Checkbox
            // 
            this.ExportPlayerID_Checkbox.AutoSize = true;
            this.ExportPlayerID_Checkbox.Location = new System.Drawing.Point(10, 83);
            this.ExportPlayerID_Checkbox.Name = "ExportPlayerID_Checkbox";
            this.ExportPlayerID_Checkbox.Size = new System.Drawing.Size(144, 19);
            this.ExportPlayerID_Checkbox.TabIndex = 5;
            this.ExportPlayerID_Checkbox.Text = "Export with Player IDs";
            this.ExportPlayerID_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ImportCoachPortPack_Button
            // 
            this.ImportCoachPortPack_Button.Location = new System.Drawing.Point(9, 19);
            this.ImportCoachPortPack_Button.Name = "ImportCoachPortPack_Button";
            this.ImportCoachPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ImportCoachPortPack_Button.TabIndex = 9;
            this.ImportCoachPortPack_Button.Text = "Import Coach Port Pack";
            this.ImportCoachPortPack_Button.UseVisualStyleBackColor = true;
            this.ImportCoachPortPack_Button.Click += new System.EventHandler(this.ImportCoachPortPack_Button_Click);
            // 
            // ExportPlayerPortPack_Button
            // 
            this.ExportPlayerPortPack_Button.Location = new System.Drawing.Point(9, 48);
            this.ExportPlayerPortPack_Button.Name = "ExportPlayerPortPack_Button";
            this.ExportPlayerPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ExportPlayerPortPack_Button.TabIndex = 4;
            this.ExportPlayerPortPack_Button.Text = "Export Player Port Pack";
            this.ExportPlayerPortPack_Button.UseVisualStyleBackColor = true;
            this.ExportPlayerPortPack_Button.Click += new System.EventHandler(this.ExportPlayerPack_Button_Click);
            // 
            // ImportPlayerPortPack_Button
            // 
            this.ImportPlayerPortPack_Button.Location = new System.Drawing.Point(9, 19);
            this.ImportPlayerPortPack_Button.Name = "ImportPlayerPortPack_Button";
            this.ImportPlayerPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ImportPlayerPortPack_Button.TabIndex = 8;
            this.ImportPlayerPortPack_Button.Text = "Import Player Port Pack";
            this.ImportPlayerPortPack_Button.UseVisualStyleBackColor = true;
            this.ImportPlayerPortPack_Button.Click += new System.EventHandler(this.ImportPlayerPortPack_Button_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.PortFax_Box);
            this.groupBox3.Controls.Add(this.PlayerRepoLoad_Button);
            this.groupBox3.Location = new System.Drawing.Point(5, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(991, 311);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Portrait Editing Information";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(805, 57);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Portrait Default DATs";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(805, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Portrait Editing Info";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(805, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Portrait Pack Options";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PortFax_Box
            // 
            this.PortFax_Box.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PortFax_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortFax_Box.Location = new System.Drawing.Point(9, 15);
            this.PortFax_Box.Name = "PortFax_Box";
            this.PortFax_Box.ReadOnly = true;
            this.PortFax_Box.Size = new System.Drawing.Size(790, 286);
            this.PortFax_Box.TabIndex = 0;
            this.PortFax_Box.Text = "";
            // 
            // PlayerRepoLoad_Button
            // 
            this.PlayerRepoLoad_Button.Location = new System.Drawing.Point(825, 225);
            this.PlayerRepoLoad_Button.Name = "PlayerRepoLoad_Button";
            this.PlayerRepoLoad_Button.Size = new System.Drawing.Size(118, 23);
            this.PlayerRepoLoad_Button.TabIndex = 14;
            this.PlayerRepoLoad_Button.Text = "Init Player Repo";
            this.PlayerRepoLoad_Button.UseVisualStyleBackColor = true;
            this.PlayerRepoLoad_Button.Visible = false;
            this.PlayerRepoLoad_Button.Click += new System.EventHandler(this.PlayerRepoLoad_Button_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox5.Controls.Add(this.ExportCoachID_Checkbox);
            this.groupBox5.Controls.Add(this.ExportCoachPortPack_Button);
            this.groupBox5.Controls.Add(this.ImportCoachPortPack_Button);
            this.groupBox5.Location = new System.Drawing.Point(820, 460);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 110);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Coach Port Pack Options";
            // 
            // ExportCoachID_Checkbox
            // 
            this.ExportCoachID_Checkbox.AutoSize = true;
            this.ExportCoachID_Checkbox.Location = new System.Drawing.Point(10, 85);
            this.ExportCoachID_Checkbox.Name = "ExportCoachID_Checkbox";
            this.ExportCoachID_Checkbox.Size = new System.Drawing.Size(145, 19);
            this.ExportCoachID_Checkbox.TabIndex = 5;
            this.ExportCoachID_Checkbox.Text = "Export with Coach IDs";
            this.ExportCoachID_Checkbox.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox6.Controls.Add(this.ExportPlayerID_Checkbox);
            this.groupBox6.Controls.Add(this.ImportPlayerPortPack_Button);
            this.groupBox6.Controls.Add(this.ExportPlayerPortPack_Button);
            this.groupBox6.Location = new System.Drawing.Point(820, 342);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(170, 110);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Player Port Pack Options";
            // 
            // DATProgress
            // 
            this.DATProgress.Location = new System.Drawing.Point(9, 38);
            this.DATProgress.Name = "DATProgress";
            this.DATProgress.Size = new System.Drawing.Size(976, 23);
            this.DATProgress.TabIndex = 15;
            // 
            // DATComment
            // 
            this.DATComment.Location = new System.Drawing.Point(137, 12);
            this.DATComment.Name = "DATComment";
            this.DATComment.Size = new System.Drawing.Size(662, 20);
            this.DATComment.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Current Player Port";
            // 
            // CurrentCoachPort_Textbox
            // 
            this.CurrentCoachPort_Textbox.Location = new System.Drawing.Point(137, 67);
            this.CurrentCoachPort_Textbox.Name = "CurrentCoachPort_Textbox";
            this.CurrentCoachPort_Textbox.ReadOnly = true;
            this.CurrentCoachPort_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CurrentCoachPort_Textbox.TabIndex = 18;
            // 
            // CurrentPlayerPort_Textbox
            // 
            this.CurrentPlayerPort_Textbox.Location = new System.Drawing.Point(137, 38);
            this.CurrentPlayerPort_Textbox.Name = "CurrentPlayerPort_Textbox";
            this.CurrentPlayerPort_Textbox.ReadOnly = true;
            this.CurrentPlayerPort_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CurrentPlayerPort_Textbox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Current Coach Port";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.DATComment);
            this.groupBox2.Controls.Add(this.DATProgress);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(5, 605);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(990, 70);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DAT Status";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox4.Controls.Add(this.CurrentPlayerPort_Textbox);
            this.groupBox4.Controls.Add(this.AskForCoachSave_Checkbox);
            this.groupBox4.Controls.Add(this.CurrentCoachPort_Textbox);
            this.groupBox4.Controls.Add(this.SavePlayerDAT_Button);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.LoadPlayerDAT_Button);
            this.groupBox4.Controls.Add(this.LoadCoachDAT_Button);
            this.groupBox4.Controls.Add(this.AskForPlayerSave_Checkbox);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.SaveCoachDat_Button);
            this.groupBox4.Location = new System.Drawing.Point(5, 405);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(799, 182);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DAT Options";
            // 
            // DATOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "DATOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AmpConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox PlayerPortLocation_Textbox;
        private System.Windows.Forms.CheckBox DefaultPlayerPort_Checkbox;
        private System.Windows.Forms.CheckBox DefaultCoachPort_Checkbox;
        private System.Windows.Forms.TextBox CoachPortLocation_Textbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ExportPlayerPortPack_Button;
        private System.Windows.Forms.Button ImportPlayerPortPack_Button;
        private System.Windows.Forms.CheckBox ExportPlayerID_Checkbox;
        private System.Windows.Forms.Button SavePlayerDAT_Button;
        private System.Windows.Forms.Button ExportCoachPortPack_Button;
        private System.Windows.Forms.Button ImportCoachPortPack_Button;
        private System.Windows.Forms.Button SaveCoachDat_Button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox PortFax_Box;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox ExportCoachID_Checkbox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button PlayerRepoLoad_Button;
        private System.Windows.Forms.ProgressBar DATProgress;
        private System.Windows.Forms.TextBox DATComment;
        private System.Windows.Forms.CheckBox AskForCoachSave_Checkbox;
        private System.Windows.Forms.CheckBox AskForPlayerSave_Checkbox;
        private System.Windows.Forms.Button LoadCoachDAT_Button;
        private System.Windows.Forms.Button LoadPlayerDAT_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CurrentCoachPort_Textbox;
        private System.Windows.Forms.TextBox CurrentPlayerPort_Textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CoachMakeDefault_Button;
        private System.Windows.Forms.Button PlayerMakeDefault_Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;

    }
}