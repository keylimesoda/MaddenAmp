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
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchforPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchforCoachesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.globalPlayerAttrEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.franchiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editSalaryCapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setTeamCaptainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.playerPage = new System.Windows.Forms.TabPage();
			this.testButton = new System.Windows.Forms.Button();
			this.coachPage = new System.Windows.Forms.TabPage();
			this.depthChartPage = new System.Windows.Forms.TabPage();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.processingTableLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.rosterFileLoaderThread = new System.ComponentModel.BackgroundWorker();
			this.testerWorkerThread = new System.ComponentModel.BackgroundWorker();
			this.setUserControlledTeamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.playerPage.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.franchiseToolStripMenuItem,
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
            this.exportToolStripMenuItem,
            this.toolStripSeparator4,
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
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Text = "&Export Players ...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
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
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchforPlayerToolStripMenuItem,
            this.searchforCoachesToolStripMenuItem,
            this.toolStripSeparator3,
            this.globalPlayerAttrEditorToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// searchforPlayerToolStripMenuItem
			// 
			this.searchforPlayerToolStripMenuItem.Name = "searchforPlayerToolStripMenuItem";
			this.searchforPlayerToolStripMenuItem.Text = "Search for Player ...";
			this.searchforPlayerToolStripMenuItem.Click += new System.EventHandler(this.searchforPlayerToolStripMenuItem_Click);
			// 
			// searchforCoachesToolStripMenuItem
			// 
			this.searchforCoachesToolStripMenuItem.Name = "searchforCoachesToolStripMenuItem";
			this.searchforCoachesToolStripMenuItem.Text = "Search for Coaches ...";
			this.searchforCoachesToolStripMenuItem.Click += new System.EventHandler(this.searchforCoachesToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// globalPlayerAttrEditorToolStripMenuItem
			// 
			this.globalPlayerAttrEditorToolStripMenuItem.Name = "globalPlayerAttrEditorToolStripMenuItem";
			this.globalPlayerAttrEditorToolStripMenuItem.Text = "Global Player Attr Editor ...";
			this.globalPlayerAttrEditorToolStripMenuItem.Click += new System.EventHandler(this.globalPlayerAttrEditorToolStripMenuItem_Click);
			// 
			// franchiseToolStripMenuItem
			// 
			this.franchiseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSalaryCapsToolStripMenuItem,
            this.setTeamCaptainsToolStripMenuItem,
            this.setUserControlledTeamsToolStripMenuItem});
			this.franchiseToolStripMenuItem.Name = "franchiseToolStripMenuItem";
			this.franchiseToolStripMenuItem.Text = "Franchise";
			// 
			// editSalaryCapsToolStripMenuItem
			// 
			this.editSalaryCapsToolStripMenuItem.Name = "editSalaryCapsToolStripMenuItem";
			this.editSalaryCapsToolStripMenuItem.Text = "Edit Salary Caps";
			this.editSalaryCapsToolStripMenuItem.Click += new System.EventHandler(this.editSalaryCapsToolStripMenuItem_Click);
			// 
			// setTeamCaptainsToolStripMenuItem
			// 
			this.setTeamCaptainsToolStripMenuItem.Name = "setTeamCaptainsToolStripMenuItem";
			this.setTeamCaptainsToolStripMenuItem.Text = "Set Team Captains ...";
			this.setTeamCaptainsToolStripMenuItem.Click += new System.EventHandler(this.setTeamCaptainsToolStripMenuItem_Click);
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
			this.tabControl.Controls.Add(this.depthChartPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(792, 519);
			this.tabControl.TabIndex = 1;
			// 
			// playerPage
			// 
			this.playerPage.Controls.Add(this.testButton);
			this.playerPage.Location = new System.Drawing.Point(4, 22);
			this.playerPage.Name = "playerPage";
			this.playerPage.Padding = new System.Windows.Forms.Padding(3);
			this.playerPage.Size = new System.Drawing.Size(784, 493);
			this.playerPage.TabIndex = 0;
			this.playerPage.Text = "Player Editor";
			// 
			// testButton
			// 
			this.testButton.Enabled = false;
			this.testButton.Location = new System.Drawing.Point(142, 107);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(75, 23);
			this.testButton.TabIndex = 37;
			this.testButton.Text = "Test";
			this.testButton.Visible = false;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// coachPage
			// 
			this.coachPage.Location = new System.Drawing.Point(4, 22);
			this.coachPage.Name = "coachPage";
			this.coachPage.Padding = new System.Windows.Forms.Padding(3);
			this.coachPage.Size = new System.Drawing.Size(784, 493);
			this.coachPage.TabIndex = 1;
			this.coachPage.Text = "Coach Editor";
			// 
			// depthChartPage
			// 
			this.depthChartPage.Location = new System.Drawing.Point(4, 22);
			this.depthChartPage.Name = "depthChartPage";
			this.depthChartPage.Size = new System.Drawing.Size(784, 493);
			this.depthChartPage.TabIndex = 2;
			this.depthChartPage.Text = "Depth Chart Editor";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar,
            this.processingTableLabel});
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
			// processingTableLabel
			// 
			this.processingTableLabel.Name = "processingTableLabel";
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
			// setUserControlledTeamsToolStripMenuItem
			// 
			this.setUserControlledTeamsToolStripMenuItem.Name = "setUserControlledTeamsToolStripMenuItem";
			this.setUserControlledTeamsToolStripMenuItem.Text = "Set User Controlled Teams ...";
			this.setUserControlledTeamsToolStripMenuItem.Click += new System.EventHandler(this.setUserControlledTeamsToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.ComponentModel.BackgroundWorker rosterFileLoaderThread;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchforPlayerToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker testerWorkerThread;
		private System.Windows.Forms.ToolStripMenuItem franchiseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editSalaryCapsToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel processingTableLabel;
		private System.Windows.Forms.Button testButton;
		private System.Windows.Forms.TabPage depthChartPage;
		private System.Windows.Forms.ToolStripMenuItem searchforCoachesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem globalPlayerAttrEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setTeamCaptainsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem setUserControlledTeamsToolStripMenuItem;
    }
}