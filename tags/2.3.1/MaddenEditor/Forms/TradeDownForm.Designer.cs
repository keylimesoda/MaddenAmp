namespace MaddenEditor.Forms
{
    partial class TradeDownForm
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
            this.rejectButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.offerButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.conversation = new System.Windows.Forms.TextBox();
            this.CPUlabel = new System.Windows.Forms.Label();
            this.myLabel = new System.Windows.Forms.Label();
            this.CPUpicks = new System.Windows.Forms.ListBox();
            this.myPicks = new System.Windows.Forms.ListBox();
            this.LowerPendingBox = new System.Windows.Forms.ListBox();
            this.HigherPendingBox = new System.Windows.Forms.ListBox();
            this.NoOfferBox = new System.Windows.Forms.ListBox();
            this.RejectedBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humanValue = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CPUvalue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rejectButton
            // 
            this.rejectButton.Location = new System.Drawing.Point(479, 300);
            this.rejectButton.Name = "rejectButton";
            this.rejectButton.Size = new System.Drawing.Size(79, 24);
            this.rejectButton.TabIndex = 19;
            this.rejectButton.Text = "Reject";
            this.rejectButton.Click += new System.EventHandler(this.rejectButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(385, 300);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(79, 24);
            this.resetButton.TabIndex = 18;
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // offerButton
            // 
            this.offerButton.Location = new System.Drawing.Point(291, 300);
            this.offerButton.Name = "offerButton";
            this.offerButton.Size = new System.Drawing.Size(79, 24);
            this.offerButton.TabIndex = 17;
            this.offerButton.Text = "Offer";
            this.offerButton.Click += new System.EventHandler(this.offerButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(196, 300);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(79, 24);
            this.acceptButton.TabIndex = 16;
            this.acceptButton.Text = "Accept";
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Dialog:";
            // 
            // conversation
            // 
            this.conversation.AcceptsReturn = true;
            this.conversation.BackColor = System.Drawing.Color.White;
            this.conversation.Location = new System.Drawing.Point(186, 357);
            this.conversation.Multiline = true;
            this.conversation.Name = "conversation";
            this.conversation.ReadOnly = true;
            this.conversation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conversation.Size = new System.Drawing.Size(382, 108);
            this.conversation.TabIndex = 14;
            // 
            // CPUlabel
            // 
            this.CPUlabel.AutoSize = true;
            this.CPUlabel.Location = new System.Drawing.Point(398, 36);
            this.CPUlabel.Name = "CPUlabel";
            this.CPUlabel.Size = new System.Drawing.Size(31, 13);
            this.CPUlabel.TabIndex = 13;
            this.CPUlabel.Text = "label2";
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(189, 36);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(31, 13);
            this.myLabel.TabIndex = 12;
            this.myLabel.Text = "label1";
            // 
            // CPUpicks
            // 
            this.CPUpicks.FormattingEnabled = true;
            this.CPUpicks.Location = new System.Drawing.Point(395, 55);
            this.CPUpicks.Name = "CPUpicks";
            this.CPUpicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CPUpicks.Size = new System.Drawing.Size(173, 199);
            this.CPUpicks.TabIndex = 11;
            this.CPUpicks.SelectedIndexChanged += new System.EventHandler(this.Picks_SelectedIndexChanged);
            // 
            // myPicks
            // 
            this.myPicks.FormattingEnabled = true;
            this.myPicks.Location = new System.Drawing.Point(186, 55);
            this.myPicks.Name = "myPicks";
            this.myPicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.myPicks.Size = new System.Drawing.Size(173, 199);
            this.myPicks.TabIndex = 10;
            this.myPicks.SelectedIndexChanged += new System.EventHandler(this.Picks_SelectedIndexChanged);
            // 
            // LowerPendingBox
            // 
            this.LowerPendingBox.FormattingEnabled = true;
            this.LowerPendingBox.Location = new System.Drawing.Point(12, 55);
            this.LowerPendingBox.Name = "LowerPendingBox";
            this.LowerPendingBox.Size = new System.Drawing.Size(157, 82);
            this.LowerPendingBox.TabIndex = 20;
            this.LowerPendingBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // HigherPendingBox
            // 
            this.HigherPendingBox.FormattingEnabled = true;
            this.HigherPendingBox.Location = new System.Drawing.Point(12, 163);
            this.HigherPendingBox.Name = "HigherPendingBox";
            this.HigherPendingBox.Size = new System.Drawing.Size(157, 82);
            this.HigherPendingBox.TabIndex = 21;
            this.HigherPendingBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // NoOfferBox
            // 
            this.NoOfferBox.FormattingEnabled = true;
            this.NoOfferBox.Location = new System.Drawing.Point(12, 271);
            this.NoOfferBox.Name = "NoOfferBox";
            this.NoOfferBox.Size = new System.Drawing.Size(157, 82);
            this.NoOfferBox.TabIndex = 22;
            this.NoOfferBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // RejectedBox
            // 
            this.RejectedBox.FormattingEnabled = true;
            this.RejectedBox.Location = new System.Drawing.Point(12, 383);
            this.RejectedBox.Name = "RejectedBox";
            this.RejectedBox.Size = new System.Drawing.Size(157, 82);
            this.RejectedBox.TabIndex = 23;
            this.RejectedBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Waiting on Their Response";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Waiting on Your Response";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "No Offer Yet";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Offer Rejected";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(580, 24);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tradeHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // tradeHelpToolStripMenuItem
            // 
            this.tradeHelpToolStripMenuItem.Name = "tradeHelpToolStripMenuItem";
            this.tradeHelpToolStripMenuItem.Text = "Trade Help";
            this.tradeHelpToolStripMenuItem.Click += new System.EventHandler(this.tradeHelpToolStripMenuItem_Click);
            // 
            // humanValue
            // 
            this.humanValue.Location = new System.Drawing.Point(314, 259);
            this.humanValue.Name = "humanValue";
            this.humanValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.humanValue.Size = new System.Drawing.Size(35, 12);
            this.humanValue.TabIndex = 30;
            this.humanValue.Text = "8888";
            this.humanValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(247, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Total Value:";
            // 
            // CPUvalue
            // 
            this.CPUvalue.Location = new System.Drawing.Point(524, 259);
            this.CPUvalue.Name = "CPUvalue";
            this.CPUvalue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CPUvalue.Size = new System.Drawing.Size(35, 12);
            this.CPUvalue.TabIndex = 32;
            this.CPUvalue.Text = "8888";
            this.CPUvalue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(457, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Total Value:";
            // 
            // TradeDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 477);
            this.Controls.Add(this.CPUvalue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.humanValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RejectedBox);
            this.Controls.Add(this.NoOfferBox);
            this.Controls.Add(this.HigherPendingBox);
            this.Controls.Add(this.LowerPendingBox);
            this.Controls.Add(this.rejectButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.offerButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.conversation);
            this.Controls.Add(this.CPUlabel);
            this.Controls.Add(this.myLabel);
            this.Controls.Add(this.CPUpicks);
            this.Controls.Add(this.myPicks);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(588, 511);
            this.MinimumSize = new System.Drawing.Size(588, 511);
            this.Name = "TradeDownForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trade Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradeDownForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rejectButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button offerButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox conversation;
        private System.Windows.Forms.Label CPUlabel;
        private System.Windows.Forms.Label myLabel;
        private System.Windows.Forms.ListBox CPUpicks;
        private System.Windows.Forms.ListBox myPicks;
        private System.Windows.Forms.ListBox LowerPendingBox;
        private System.Windows.Forms.ListBox HigherPendingBox;
        private System.Windows.Forms.ListBox NoOfferBox;
        private System.Windows.Forms.ListBox RejectedBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeHelpToolStripMenuItem;
        private System.Windows.Forms.Label humanValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label CPUvalue;
        private System.Windows.Forms.Label label8;
    }
}