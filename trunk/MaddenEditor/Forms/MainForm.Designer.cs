namespace MaddenEditor.Forms
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchforPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.playerPage = new System.Windows.Forms.TabPage();
			this.coachPage = new System.Windows.Forms.TabPage();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.rosterFileLoaderThread = new System.ComponentModel.BackgroundWorker();
			this.testerWorkerThread = new System.ComponentModel.BackgroundWorker();
			this.playerEditControl = new MaddenEditor.Forms.PlayerEditControl();
			this.coachEditControl1 = new MaddenEditor.Forms.CoachEditControl();
			this.menuStrip1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.playerPage.SuspendLayout();
			this.coachPage.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Text = "&Open ...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchforPlayerToolStripMenuItem});
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Text = "Search";
			// 
			// searchforPlayerToolStripMenuItem
			// 
			this.searchforPlayerToolStripMenuItem.Name = "searchforPlayerToolStripMenuItem";
			this.searchforPlayerToolStripMenuItem.Text = "Search for Player ...";
			this.searchforPlayerToolStripMenuItem.Click += new System.EventHandler(this.searchforPlayerToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Text = "About ...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.playerPage);
			this.tabControl.Controls.Add(this.coachPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(792, 519);
			this.tabControl.TabIndex = 1;
			// 
			// playerPage
			// 
			this.playerPage.Controls.Add(this.playerEditControl);
			this.playerPage.Location = new System.Drawing.Point(4, 22);
			this.playerPage.Name = "playerPage";
			this.playerPage.Padding = new System.Windows.Forms.Padding(3);
			this.playerPage.Size = new System.Drawing.Size(784, 493);
			this.playerPage.TabIndex = 0;
			this.playerPage.Text = "Player Editor";
			// 
			// coachPage
			// 
			this.coachPage.Controls.Add(this.coachEditControl1);
			this.coachPage.Location = new System.Drawing.Point(4, 22);
			this.coachPage.Name = "coachPage";
			this.coachPage.Padding = new System.Windows.Forms.Padding(3);
			this.coachPage.Size = new System.Drawing.Size(784, 493);
			this.coachPage.TabIndex = 1;
			this.coachPage.Text = "Coach Editor";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar});
			this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statusStrip.Location = new System.Drawing.Point(0, 543);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(792, 23);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Text = "Processing   ";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.AutoSize = false;
			this.toolStripProgressBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(250, 16);
			this.toolStripProgressBar.Text = "toolStripProgressBar1";
			// 
			// rosterFileLoaderThread
			// 
			this.rosterFileLoaderThread.WorkerReportsProgress = true;
			this.rosterFileLoaderThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.rosterFileLoaderThread_RunWorkerCompleted);
			this.rosterFileLoaderThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.rosterFileLoaderThread_ProgressChanged);
			// 
			// testerWorkerThread
			// 
			this.testerWorkerThread.WorkerReportsProgress = true;
			this.testerWorkerThread.WorkerSupportsCancellation = true;
			this.testerWorkerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.testerWorkerThread_DoWork);
			this.testerWorkerThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.testerWorkerThread_ProgressChanged);
			// 
			// playerEditControl
			// 
			this.playerEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playerEditControl.Location = new System.Drawing.Point(3, 3);
			this.playerEditControl.Name = "playerEditControl";
			this.playerEditControl.Size = new System.Drawing.Size(778, 487);
			this.playerEditControl.TabIndex = 0;
			// 
			// coachEditControl1
			// 
			this.coachEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.coachEditControl1.Location = new System.Drawing.Point(3, 3);
			this.coachEditControl1.Name = "coachEditControl1";
			this.coachEditControl1.Size = new System.Drawing.Size(778, 487);
			this.coachEditControl1.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Madden Editor 2005";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.playerPage.ResumeLayout(false);
			this.coachPage.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage playerPage;
		private System.Windows.Forms.TabPage coachPage;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.ComponentModel.BackgroundWorker rosterFileLoaderThread;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchforPlayerToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker testerWorkerThread;
		private PlayerEditControl playerEditControl;
		private CoachEditControl coachEditControl1;
    }
}