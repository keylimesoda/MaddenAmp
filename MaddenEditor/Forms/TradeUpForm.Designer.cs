namespace MaddenEditor.Forms
{
    partial class TradeUpForm
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
            this.myPicks = new System.Windows.Forms.ListBox();
            this.CPUpicks = new System.Windows.Forms.ListBox();
            this.myLabel = new System.Windows.Forms.Label();
            this.CPUlabel = new System.Windows.Forms.Label();
            this.conversation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.approveButton = new System.Windows.Forms.Button();
            this.offerButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.rejectButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeHepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPicks
            // 
            this.myPicks.FormattingEnabled = true;
            this.myPicks.Location = new System.Drawing.Point(12, 55);
            this.myPicks.Name = "myPicks";
            this.myPicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.myPicks.Size = new System.Drawing.Size(173, 199);
            this.myPicks.TabIndex = 0;
            this.myPicks.SelectedIndexChanged += new System.EventHandler(this.myPicks_SelectedIndexChanged);
            // 
            // CPUpicks
            // 
            this.CPUpicks.FormattingEnabled = true;
            this.CPUpicks.Location = new System.Drawing.Point(221, 55);
            this.CPUpicks.Name = "CPUpicks";
            this.CPUpicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CPUpicks.Size = new System.Drawing.Size(173, 199);
            this.CPUpicks.TabIndex = 1;
            this.CPUpicks.SelectedIndexChanged += new System.EventHandler(this.myPicks_SelectedIndexChanged);
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(15, 36);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(31, 13);
            this.myLabel.TabIndex = 2;
            this.myLabel.Text = "label1";
            // 
            // CPUlabel
            // 
            this.CPUlabel.AutoSize = true;
            this.CPUlabel.Location = new System.Drawing.Point(224, 36);
            this.CPUlabel.Name = "CPUlabel";
            this.CPUlabel.Size = new System.Drawing.Size(31, 13);
            this.CPUlabel.TabIndex = 3;
            this.CPUlabel.Text = "label2";
            // 
            // conversation
            // 
            this.conversation.AcceptsReturn = true;
            this.conversation.BackColor = System.Drawing.Color.White;
            this.conversation.Location = new System.Drawing.Point(12, 321);
            this.conversation.Multiline = true;
            this.conversation.Name = "conversation";
            this.conversation.ReadOnly = true;
            this.conversation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conversation.Size = new System.Drawing.Size(382, 96);
            this.conversation.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dialog:";
            // 
            // approveButton
            // 
            this.approveButton.Location = new System.Drawing.Point(22, 264);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(79, 24);
            this.approveButton.TabIndex = 6;
            this.approveButton.Text = "Approve";
            this.approveButton.Click += new System.EventHandler(this.approveButton_Click);
            // 
            // offerButton
            // 
            this.offerButton.Location = new System.Drawing.Point(117, 264);
            this.offerButton.Name = "offerButton";
            this.offerButton.Size = new System.Drawing.Size(79, 24);
            this.offerButton.TabIndex = 7;
            this.offerButton.Text = "Offer";
            this.offerButton.Click += new System.EventHandler(this.offerButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(211, 264);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(79, 24);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // rejectButton
            // 
            this.rejectButton.Location = new System.Drawing.Point(305, 264);
            this.rejectButton.Name = "rejectButton";
            this.rejectButton.Size = new System.Drawing.Size(79, 24);
            this.rejectButton.TabIndex = 9;
            this.rejectButton.Text = "Reject";
            this.rejectButton.Click += new System.EventHandler(this.rejectButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(406, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tradeHepToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // tradeHepToolStripMenuItem
            // 
            this.tradeHepToolStripMenuItem.Name = "tradeHepToolStripMenuItem";
            this.tradeHepToolStripMenuItem.Text = "Trade Help";
            this.tradeHepToolStripMenuItem.Click += new System.EventHandler(this.tradeHepToolStripMenuItem_Click);
            // 
            // TradeUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 429);
            this.Controls.Add(this.rejectButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.offerButton);
            this.Controls.Add(this.approveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.conversation);
            this.Controls.Add(this.CPUlabel);
            this.Controls.Add(this.myLabel);
            this.Controls.Add(this.CPUpicks);
            this.Controls.Add(this.myPicks);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TradeUpForm";
            this.Text = "Trade Dialog";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradeUpForm_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradeUpForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox myPicks;
        private System.Windows.Forms.ListBox CPUpicks;
        private System.Windows.Forms.Label myLabel;
        private System.Windows.Forms.Label CPUlabel;
        private System.Windows.Forms.TextBox conversation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button approveButton;
        private System.Windows.Forms.Button offerButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button rejectButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeHepToolStripMenuItem;
    }
}