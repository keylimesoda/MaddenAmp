namespace MaddenEditor.Forms
{
    partial class DatConfigForm
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
            this.AutoLoadPlayerPorts = new System.Windows.Forms.CheckBox();
            this.DefaultCoachPort_Checkbox = new System.Windows.Forms.CheckBox();
            this.CoachPortLocation_Textbox = new System.Windows.Forms.TextBox();
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
            this.ExportCoachID_Checkbox = new System.Windows.Forms.CheckBox();
            this.DATProgress = new System.Windows.Forms.ProgressBar();
            this.DATComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentCoachPort_Textbox = new System.Windows.Forms.TextBox();
            this.CurrentPlayerPort_Textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CoachPack_Panel = new System.Windows.Forms.Panel();
            this.PlayerPack_Panel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.CoachPack_Panel.SuspendLayout();
            this.PlayerPack_Panel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayerPortLocation_Textbox
            // 
            this.PlayerPortLocation_Textbox.Enabled = false;
            this.PlayerPortLocation_Textbox.Location = new System.Drawing.Point(231, 32);
            this.PlayerPortLocation_Textbox.Name = "PlayerPortLocation_Textbox";
            this.PlayerPortLocation_Textbox.ReadOnly = true;
            this.PlayerPortLocation_Textbox.Size = new System.Drawing.Size(270, 20);
            this.PlayerPortLocation_Textbox.TabIndex = 1;
            this.PlayerPortLocation_Textbox.TextChanged += new System.EventHandler(this.PlayerPortLocation_Textbox_TextChanged);
            // 
            // AutoLoadPlayerPorts
            // 
            this.AutoLoadPlayerPorts.AutoSize = true;
            this.AutoLoadPlayerPorts.Location = new System.Drawing.Point(43, 33);
            this.AutoLoadPlayerPorts.Name = "AutoLoadPlayerPorts";
            this.AutoLoadPlayerPorts.Size = new System.Drawing.Size(166, 17);
            this.AutoLoadPlayerPorts.TabIndex = 2;
            this.AutoLoadPlayerPorts.Text = "Auto Load Default Player Port";
            this.AutoLoadPlayerPorts.UseVisualStyleBackColor = true;
            this.AutoLoadPlayerPorts.CheckedChanged += new System.EventHandler(this.DefaultPlayerPort_Checkbox_CheckedChanged);
            // 
            // DefaultCoachPort_Checkbox
            // 
            this.DefaultCoachPort_Checkbox.AutoSize = true;
            this.DefaultCoachPort_Checkbox.Location = new System.Drawing.Point(43, 58);
            this.DefaultCoachPort_Checkbox.Name = "DefaultCoachPort_Checkbox";
            this.DefaultCoachPort_Checkbox.Size = new System.Drawing.Size(168, 17);
            this.DefaultCoachPort_Checkbox.TabIndex = 4;
            this.DefaultCoachPort_Checkbox.Text = "Auto Load Default Coach Port";
            this.DefaultCoachPort_Checkbox.UseVisualStyleBackColor = true;
            this.DefaultCoachPort_Checkbox.CheckedChanged += new System.EventHandler(this.DefaultCoachPort_Checkbox_CheckedChanged);
            // 
            // CoachPortLocation_Textbox
            // 
            this.CoachPortLocation_Textbox.Enabled = false;
            this.CoachPortLocation_Textbox.Location = new System.Drawing.Point(231, 57);
            this.CoachPortLocation_Textbox.Name = "CoachPortLocation_Textbox";
            this.CoachPortLocation_Textbox.ReadOnly = true;
            this.CoachPortLocation_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CoachPortLocation_Textbox.TabIndex = 5;
            // 
            // CoachMakeDefault_Button
            // 
            this.CoachMakeDefault_Button.Location = new System.Drawing.Point(507, 56);
            this.CoachMakeDefault_Button.Name = "CoachMakeDefault_Button";
            this.CoachMakeDefault_Button.Size = new System.Drawing.Size(96, 23);
            this.CoachMakeDefault_Button.TabIndex = 23;
            this.CoachMakeDefault_Button.Text = "Use Current";
            this.CoachMakeDefault_Button.UseVisualStyleBackColor = true;
            this.CoachMakeDefault_Button.Click += new System.EventHandler(this.CoachMakeDefault_Button_Click);
            // 
            // PlayerMakeDefault_Button
            // 
            this.PlayerMakeDefault_Button.Location = new System.Drawing.Point(507, 31);
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
            this.AskForCoachSave_Checkbox.Location = new System.Drawing.Point(647, 174);
            this.AskForCoachSave_Checkbox.Name = "AskForCoachSave_Checkbox";
            this.AskForCoachSave_Checkbox.Size = new System.Drawing.Size(118, 17);
            this.AskForCoachSave_Checkbox.TabIndex = 11;
            this.AskForCoachSave_Checkbox.Text = "Ask for Save Name";
            this.AskForCoachSave_Checkbox.UseVisualStyleBackColor = true;
            this.AskForCoachSave_Checkbox.CheckedChanged += new System.EventHandler(this.AskForCoachSave_Checkbox_CheckedChanged);
            // 
            // AskForPlayerSave_Checkbox
            // 
            this.AskForPlayerSave_Checkbox.AutoSize = true;
            this.AskForPlayerSave_Checkbox.Location = new System.Drawing.Point(647, 145);
            this.AskForPlayerSave_Checkbox.Name = "AskForPlayerSave_Checkbox";
            this.AskForPlayerSave_Checkbox.Size = new System.Drawing.Size(118, 17);
            this.AskForPlayerSave_Checkbox.TabIndex = 10;
            this.AskForPlayerSave_Checkbox.Text = "Ask for Save Name";
            this.AskForPlayerSave_Checkbox.UseVisualStyleBackColor = true;
            this.AskForPlayerSave_Checkbox.CheckedChanged += new System.EventHandler(this.AskForPlayerSave_Checkbox_CheckedChanged);
            // 
            // LoadCoachDAT_Button
            // 
            this.LoadCoachDAT_Button.Location = new System.Drawing.Point(411, 172);
            this.LoadCoachDAT_Button.Name = "LoadCoachDAT_Button";
            this.LoadCoachDAT_Button.Size = new System.Drawing.Size(90, 23);
            this.LoadCoachDAT_Button.TabIndex = 9;
            this.LoadCoachDAT_Button.Text = "Load New";
            this.LoadCoachDAT_Button.UseVisualStyleBackColor = true;
            this.LoadCoachDAT_Button.Click += new System.EventHandler(this.LoadCoachDAT_Button_Click);
            // 
            // LoadPlayerDAT_Button
            // 
            this.LoadPlayerDAT_Button.Location = new System.Drawing.Point(411, 143);
            this.LoadPlayerDAT_Button.Name = "LoadPlayerDAT_Button";
            this.LoadPlayerDAT_Button.Size = new System.Drawing.Size(90, 23);
            this.LoadPlayerDAT_Button.TabIndex = 8;
            this.LoadPlayerDAT_Button.Text = "Load New";
            this.LoadPlayerDAT_Button.UseVisualStyleBackColor = true;
            this.LoadPlayerDAT_Button.Click += new System.EventHandler(this.LoadPlayerDAT_Button_Click);
            // 
            // SaveCoachDat_Button
            // 
            this.SaveCoachDat_Button.Location = new System.Drawing.Point(507, 172);
            this.SaveCoachDat_Button.Name = "SaveCoachDat_Button";
            this.SaveCoachDat_Button.Size = new System.Drawing.Size(134, 23);
            this.SaveCoachDat_Button.TabIndex = 7;
            this.SaveCoachDat_Button.Text = "Save Coach Port DAT";
            this.SaveCoachDat_Button.UseVisualStyleBackColor = true;
            this.SaveCoachDat_Button.Click += new System.EventHandler(this.SaveCoachDat_Button_Click);
            // 
            // SavePlayerDAT_Button
            // 
            this.SavePlayerDAT_Button.Location = new System.Drawing.Point(507, 143);
            this.SavePlayerDAT_Button.Name = "SavePlayerDAT_Button";
            this.SavePlayerDAT_Button.Size = new System.Drawing.Size(134, 23);
            this.SavePlayerDAT_Button.TabIndex = 6;
            this.SavePlayerDAT_Button.Text = "Save Player Port DAT";
            this.SavePlayerDAT_Button.UseVisualStyleBackColor = true;
            this.SavePlayerDAT_Button.Click += new System.EventHandler(this.SaveDAT_Button_Click);
            // 
            // ExportCoachPortPack_Button
            // 
            this.ExportCoachPortPack_Button.Location = new System.Drawing.Point(14, 49);
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
            this.ExportPlayerID_Checkbox.Location = new System.Drawing.Point(15, 78);
            this.ExportPlayerID_Checkbox.Name = "ExportPlayerID_Checkbox";
            this.ExportPlayerID_Checkbox.Size = new System.Drawing.Size(129, 17);
            this.ExportPlayerID_Checkbox.TabIndex = 5;
            this.ExportPlayerID_Checkbox.Text = "Export with Player IDs";
            this.ExportPlayerID_Checkbox.UseVisualStyleBackColor = true;
            // 
            // ImportCoachPortPack_Button
            // 
            this.ImportCoachPortPack_Button.Location = new System.Drawing.Point(14, 15);
            this.ImportCoachPortPack_Button.Name = "ImportCoachPortPack_Button";
            this.ImportCoachPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ImportCoachPortPack_Button.TabIndex = 9;
            this.ImportCoachPortPack_Button.Text = "Import Coach Port Pack";
            this.ImportCoachPortPack_Button.UseVisualStyleBackColor = true;
            this.ImportCoachPortPack_Button.Click += new System.EventHandler(this.ImportCoachPortPack_Button_Click);
            // 
            // ExportPlayerPortPack_Button
            // 
            this.ExportPlayerPortPack_Button.Location = new System.Drawing.Point(14, 43);
            this.ExportPlayerPortPack_Button.Name = "ExportPlayerPortPack_Button";
            this.ExportPlayerPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ExportPlayerPortPack_Button.TabIndex = 4;
            this.ExportPlayerPortPack_Button.Text = "Export Player Port Pack";
            this.ExportPlayerPortPack_Button.UseVisualStyleBackColor = true;
            this.ExportPlayerPortPack_Button.Click += new System.EventHandler(this.ExportPlayerPack_Button_Click);
            // 
            // ImportPlayerPortPack_Button
            // 
            this.ImportPlayerPortPack_Button.Location = new System.Drawing.Point(14, 14);
            this.ImportPlayerPortPack_Button.Name = "ImportPlayerPortPack_Button";
            this.ImportPlayerPortPack_Button.Size = new System.Drawing.Size(146, 23);
            this.ImportPlayerPortPack_Button.TabIndex = 8;
            this.ImportPlayerPortPack_Button.Text = "Import Player Port Pack";
            this.ImportPlayerPortPack_Button.UseVisualStyleBackColor = true;
            this.ImportPlayerPortPack_Button.Click += new System.EventHandler(this.ImportPlayerPortPack_Button_Click);
            // 
            // ExportCoachID_Checkbox
            // 
            this.ExportCoachID_Checkbox.AutoSize = true;
            this.ExportCoachID_Checkbox.Location = new System.Drawing.Point(15, 81);
            this.ExportCoachID_Checkbox.Name = "ExportCoachID_Checkbox";
            this.ExportCoachID_Checkbox.Size = new System.Drawing.Size(131, 17);
            this.ExportCoachID_Checkbox.TabIndex = 5;
            this.ExportCoachID_Checkbox.Text = "Export with Coach IDs";
            this.ExportCoachID_Checkbox.UseVisualStyleBackColor = true;
            // 
            // DATProgress
            // 
            this.DATProgress.Location = new System.Drawing.Point(9, 32);
            this.DATProgress.Name = "DATProgress";
            this.DATProgress.Size = new System.Drawing.Size(976, 23);
            this.DATProgress.TabIndex = 15;
            // 
            // DATComment
            // 
            this.DATComment.Location = new System.Drawing.Point(137, 6);
            this.DATComment.Name = "DATComment";
            this.DATComment.Size = new System.Drawing.Size(662, 20);
            this.DATComment.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Current Player Port";
            // 
            // CurrentCoachPort_Textbox
            // 
            this.CurrentCoachPort_Textbox.Location = new System.Drawing.Point(133, 173);
            this.CurrentCoachPort_Textbox.Name = "CurrentCoachPort_Textbox";
            this.CurrentCoachPort_Textbox.ReadOnly = true;
            this.CurrentCoachPort_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CurrentCoachPort_Textbox.TabIndex = 18;
            // 
            // CurrentPlayerPort_Textbox
            // 
            this.CurrentPlayerPort_Textbox.Location = new System.Drawing.Point(133, 144);
            this.CurrentPlayerPort_Textbox.Name = "CurrentPlayerPort_Textbox";
            this.CurrentPlayerPort_Textbox.ReadOnly = true;
            this.CurrentPlayerPort_Textbox.Size = new System.Drawing.Size(270, 20);
            this.CurrentPlayerPort_Textbox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Current Coach Port";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CoachPack_Panel);
            this.panel1.Controls.Add(this.PlayerPack_Panel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CoachMakeDefault_Button);
            this.panel1.Controls.Add(this.PlayerPortLocation_Textbox);
            this.panel1.Controls.Add(this.CurrentPlayerPort_Textbox);
            this.panel1.Controls.Add(this.SaveCoachDat_Button);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PlayerMakeDefault_Button);
            this.panel1.Controls.Add(this.AskForPlayerSave_Checkbox);
            this.panel1.Controls.Add(this.AskForCoachSave_Checkbox);
            this.panel1.Controls.Add(this.LoadCoachDAT_Button);
            this.panel1.Controls.Add(this.CoachPortLocation_Textbox);
            this.panel1.Controls.Add(this.LoadPlayerDAT_Button);
            this.panel1.Controls.Add(this.CurrentCoachPort_Textbox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.AutoLoadPlayerPorts);
            this.panel1.Controls.Add(this.DefaultCoachPort_Checkbox);
            this.panel1.Controls.Add(this.SavePlayerDAT_Button);
            this.panel1.Location = new System.Drawing.Point(5, 363);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 238);
            this.panel1.TabIndex = 23;
            // 
            // CoachPack_Panel
            // 
            this.CoachPack_Panel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CoachPack_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CoachPack_Panel.Controls.Add(this.ExportCoachID_Checkbox);
            this.CoachPack_Panel.Controls.Add(this.ImportCoachPortPack_Button);
            this.CoachPack_Panel.Controls.Add(this.ExportCoachPortPack_Button);
            this.CoachPack_Panel.Location = new System.Drawing.Point(815, 120);
            this.CoachPack_Panel.Name = "CoachPack_Panel";
            this.CoachPack_Panel.Size = new System.Drawing.Size(170, 110);
            this.CoachPack_Panel.TabIndex = 26;
            // 
            // PlayerPack_Panel
            // 
            this.PlayerPack_Panel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PlayerPack_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerPack_Panel.Controls.Add(this.ExportPlayerID_Checkbox);
            this.PlayerPack_Panel.Controls.Add(this.ImportPlayerPortPack_Button);
            this.PlayerPack_Panel.Controls.Add(this.ExportPlayerPortPack_Button);
            this.PlayerPack_Panel.Location = new System.Drawing.Point(815, 3);
            this.PlayerPack_Panel.Name = "PlayerPack_Panel";
            this.PlayerPack_Panel.Size = new System.Drawing.Size(170, 110);
            this.PlayerPack_Panel.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "DAT Options";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.DATComment);
            this.panel2.Controls.Add(this.DATProgress);
            this.panel2.Location = new System.Drawing.Point(5, 607);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 61);
            this.panel2.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "DAT status";
            // 
            // DatConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1004, 680);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DatConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AmpConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CoachPack_Panel.ResumeLayout(false);
            this.CoachPack_Panel.PerformLayout();
            this.PlayerPack_Panel.ResumeLayout(false);
            this.PlayerPack_Panel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox PlayerPortLocation_Textbox;
        private System.Windows.Forms.CheckBox AutoLoadPlayerPorts;
        private System.Windows.Forms.CheckBox DefaultCoachPort_Checkbox;
        private System.Windows.Forms.TextBox CoachPortLocation_Textbox;
        private System.Windows.Forms.Button ExportPlayerPortPack_Button;
        private System.Windows.Forms.Button ImportPlayerPortPack_Button;
        private System.Windows.Forms.CheckBox ExportPlayerID_Checkbox;
        private System.Windows.Forms.Button SavePlayerDAT_Button;
        private System.Windows.Forms.Button ExportCoachPortPack_Button;
        private System.Windows.Forms.Button ImportCoachPortPack_Button;
        private System.Windows.Forms.Button SaveCoachDat_Button;
        private System.Windows.Forms.CheckBox ExportCoachID_Checkbox;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel CoachPack_Panel;
        private System.Windows.Forms.Panel PlayerPack_Panel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;

    }
}