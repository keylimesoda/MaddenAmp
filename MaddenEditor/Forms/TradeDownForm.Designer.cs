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
            this.SuspendLayout();
            // 
            // rejectButton
            // 
            this.rejectButton.Location = new System.Drawing.Point(479, 246);
            this.rejectButton.Name = "rejectButton";
            this.rejectButton.Size = new System.Drawing.Size(79, 24);
            this.rejectButton.TabIndex = 19;
            this.rejectButton.Text = "Reject";
            this.rejectButton.Click += new System.EventHandler(this.rejectButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(385, 246);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(79, 24);
            this.resetButton.TabIndex = 18;
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // offerButton
            // 
            this.offerButton.Location = new System.Drawing.Point(291, 246);
            this.offerButton.Name = "offerButton";
            this.offerButton.Size = new System.Drawing.Size(79, 24);
            this.offerButton.TabIndex = 17;
            this.offerButton.Text = "Offer";
            this.offerButton.Click += new System.EventHandler(this.offerButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(196, 246);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(79, 24);
            this.acceptButton.TabIndex = 16;
            this.acceptButton.Text = "Accept";
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Dialog:";
            // 
            // conversation
            // 
            this.conversation.AcceptsReturn = true;
            this.conversation.BackColor = System.Drawing.Color.White;
            this.conversation.Location = new System.Drawing.Point(186, 303);
            this.conversation.Multiline = true;
            this.conversation.Name = "conversation";
            this.conversation.ReadOnly = true;
            this.conversation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.conversation.Size = new System.Drawing.Size(382, 96);
            this.conversation.TabIndex = 14;
            // 
            // CPUlabel
            // 
            this.CPUlabel.AutoSize = true;
            this.CPUlabel.Location = new System.Drawing.Point(398, 18);
            this.CPUlabel.Name = "CPUlabel";
            this.CPUlabel.Size = new System.Drawing.Size(31, 13);
            this.CPUlabel.TabIndex = 13;
            this.CPUlabel.Text = "label2";
            // 
            // myLabel
            // 
            this.myLabel.AutoSize = true;
            this.myLabel.Location = new System.Drawing.Point(189, 18);
            this.myLabel.Name = "myLabel";
            this.myLabel.Size = new System.Drawing.Size(31, 13);
            this.myLabel.TabIndex = 12;
            this.myLabel.Text = "label1";
            // 
            // CPUpicks
            // 
            this.CPUpicks.FormattingEnabled = true;
            this.CPUpicks.Location = new System.Drawing.Point(395, 37);
            this.CPUpicks.Name = "CPUpicks";
            this.CPUpicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CPUpicks.Size = new System.Drawing.Size(173, 199);
            this.CPUpicks.TabIndex = 11;
            this.CPUpicks.SelectedIndexChanged += new System.EventHandler(this.Picks_SelectedIndexChanged);
            // 
            // myPicks
            // 
            this.myPicks.FormattingEnabled = true;
            this.myPicks.Location = new System.Drawing.Point(186, 37);
            this.myPicks.Name = "myPicks";
            this.myPicks.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.myPicks.Size = new System.Drawing.Size(173, 199);
            this.myPicks.TabIndex = 10;
            this.myPicks.SelectedIndexChanged += new System.EventHandler(this.Picks_SelectedIndexChanged);
            // 
            // LowerPendingBox
            // 
            this.LowerPendingBox.FormattingEnabled = true;
            this.LowerPendingBox.Location = new System.Drawing.Point(12, 37);
            this.LowerPendingBox.Name = "LowerPendingBox";
            this.LowerPendingBox.Size = new System.Drawing.Size(157, 69);
            this.LowerPendingBox.TabIndex = 20;
            this.LowerPendingBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // HigherPendingBox
            // 
            this.HigherPendingBox.FormattingEnabled = true;
            this.HigherPendingBox.Location = new System.Drawing.Point(12, 135);
            this.HigherPendingBox.Name = "HigherPendingBox";
            this.HigherPendingBox.Size = new System.Drawing.Size(157, 69);
            this.HigherPendingBox.TabIndex = 21;
            this.HigherPendingBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // NoOfferBox
            // 
            this.NoOfferBox.FormattingEnabled = true;
            this.NoOfferBox.Location = new System.Drawing.Point(12, 231);
            this.NoOfferBox.Name = "NoOfferBox";
            this.NoOfferBox.Size = new System.Drawing.Size(157, 69);
            this.NoOfferBox.TabIndex = 22;
            this.NoOfferBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // RejectedBox
            // 
            this.RejectedBox.FormattingEnabled = true;
            this.RejectedBox.Location = new System.Drawing.Point(12, 330);
            this.RejectedBox.Name = "RejectedBox";
            this.RejectedBox.Size = new System.Drawing.Size(157, 69);
            this.RejectedBox.TabIndex = 23;
            this.RejectedBox.SelectedIndexChanged += new System.EventHandler(this.TeamSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Waiting on Their Response";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Waiting on Your Response";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "No Offer Yet";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 312);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Offer Rejected";
            // 
            // TradeDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 411);
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
            this.Name = "TradeDownForm";
            this.Text = "Trade Dialog";
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
    }
}