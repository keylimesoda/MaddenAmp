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
			this.playerEditingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.coachPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.teamEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.searchforPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchforCoachesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.depthChartEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.globalPlayerAttrEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.franchiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editFranchiseOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.setTeamCaptainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setUserControlledTeamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setGameInjuriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.draftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveTradedDraftPicksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importDraftClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportDraftClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.weeklyMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fixProgressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.depthChartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.depthChartProgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.simulateCPUMinicampsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.offSeasonConditioningTrainingCampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.developerBiosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.developerBiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.processingTableLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.rosterFileLoaderThread = new System.ComponentModel.BackgroundWorker();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.menuStrip1.SuspendLayout();
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
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.openToolStripMenuItem.Text = "&Open ...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.exportToolStripMenuItem.Text = "&Export Players ...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Enabled = false;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerEditingToolStripMenuItem,
            this.coachPlayerToolStripMenuItem,
            this.teamEditorToolStripMenuItem,
            this.toolStripSeparator11,
            this.searchforPlayerToolStripMenuItem,
            this.searchforCoachesToolStripMenuItem,
            this.toolStripSeparator3,
            this.depthChartEditorToolStripMenuItem,
            this.globalPlayerAttrEditorToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// playerEditingToolStripMenuItem
			// 
			this.playerEditingToolStripMenuItem.Name = "playerEditingToolStripMenuItem";
			this.playerEditingToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.playerEditingToolStripMenuItem.Text = "Player Editor ...";
			this.playerEditingToolStripMenuItem.Click += new System.EventHandler(this.playerEditingToolStripMenuItem_Click);
			// 
			// coachPlayerToolStripMenuItem
			// 
			this.coachPlayerToolStripMenuItem.Name = "coachPlayerToolStripMenuItem";
			this.coachPlayerToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.coachPlayerToolStripMenuItem.Text = "Coach Editor ...";
			this.coachPlayerToolStripMenuItem.Click += new System.EventHandler(this.coachPlayerToolStripMenuItem_Click);
			// 
			// teamEditorToolStripMenuItem
			// 
			this.teamEditorToolStripMenuItem.Name = "teamEditorToolStripMenuItem";
			this.teamEditorToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.teamEditorToolStripMenuItem.Text = "Team Editor ...";
			this.teamEditorToolStripMenuItem.Click += new System.EventHandler(this.teamEditorToolStripMenuItem_Click);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(212, 6);
			// 
			// searchforPlayerToolStripMenuItem
			// 
			this.searchforPlayerToolStripMenuItem.Name = "searchforPlayerToolStripMenuItem";
			this.searchforPlayerToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.searchforPlayerToolStripMenuItem.Text = "Search for Player ...";
			this.searchforPlayerToolStripMenuItem.Click += new System.EventHandler(this.searchforPlayerToolStripMenuItem_Click);
			// 
			// searchforCoachesToolStripMenuItem
			// 
			this.searchforCoachesToolStripMenuItem.Name = "searchforCoachesToolStripMenuItem";
			this.searchforCoachesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.searchforCoachesToolStripMenuItem.Text = "Search for Coaches ...";
			this.searchforCoachesToolStripMenuItem.Click += new System.EventHandler(this.searchforCoachesToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
			// 
			// depthChartEditorToolStripMenuItem
			// 
			this.depthChartEditorToolStripMenuItem.Name = "depthChartEditorToolStripMenuItem";
			this.depthChartEditorToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.depthChartEditorToolStripMenuItem.Text = "Depth Chart Editor ...";
			this.depthChartEditorToolStripMenuItem.Click += new System.EventHandler(this.depthChartEditorToolStripMenuItem_Click);
			// 
			// globalPlayerAttrEditorToolStripMenuItem
			// 
			this.globalPlayerAttrEditorToolStripMenuItem.Name = "globalPlayerAttrEditorToolStripMenuItem";
			this.globalPlayerAttrEditorToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.globalPlayerAttrEditorToolStripMenuItem.Text = "Global Player Attr Editor ...";
			this.globalPlayerAttrEditorToolStripMenuItem.Click += new System.EventHandler(this.globalPlayerAttrEditorToolStripMenuItem_Click);
			// 
			// franchiseToolStripMenuItem
			// 
			this.franchiseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editScheduleToolStripMenuItem,
            this.editFranchiseOptionsToolStripMenuItem,
            this.toolStripSeparator5,
            this.setTeamCaptainsToolStripMenuItem,
            this.setUserControlledTeamsToolStripMenuItem,
            this.setGameInjuriesToolStripMenuItem,
            this.toolStripSeparator6,
            this.draftMenuItem,
            this.moveTradedDraftPicksToolStripMenuItem,
            this.importDraftClassToolStripMenuItem,
            this.exportDraftClassToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator7,
            this.weeklyMaintenanceToolStripMenuItem,
            this.fixProgressionToolStripMenuItem,
            this.toolStripSeparator9,
            this.depthChartMenuItem,
            this.depthChartProgMenuItem,
            this.toolStripSeparator8,
            this.simulateCPUMinicampsToolStripMenuItem,
            this.toolStripSeparator10,
            this.offSeasonConditioningTrainingCampToolStripMenuItem,
            this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem});
			this.franchiseToolStripMenuItem.Name = "franchiseToolStripMenuItem";
			this.franchiseToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			this.franchiseToolStripMenuItem.Text = "Franchise";
			// 
			// editScheduleToolStripMenuItem
			// 
			this.editScheduleToolStripMenuItem.Name = "editScheduleToolStripMenuItem";
			this.editScheduleToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.editScheduleToolStripMenuItem.Text = "Edit Schedule ...";
			this.editScheduleToolStripMenuItem.Click += new System.EventHandler(this.editScheduleToolStripMenuItem_Click);
			// 
			// editFranchiseOptionsToolStripMenuItem
			// 
			this.editFranchiseOptionsToolStripMenuItem.Name = "editFranchiseOptionsToolStripMenuItem";
			this.editFranchiseOptionsToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.editFranchiseOptionsToolStripMenuItem.Text = "Edit Franchise Options ...";
			this.editFranchiseOptionsToolStripMenuItem.Click += new System.EventHandler(this.editFranchiseOptionsToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(277, 6);
			// 
			// setTeamCaptainsToolStripMenuItem
			// 
			this.setTeamCaptainsToolStripMenuItem.Name = "setTeamCaptainsToolStripMenuItem";
			this.setTeamCaptainsToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.setTeamCaptainsToolStripMenuItem.Text = "Set Team Captains ...";
			this.setTeamCaptainsToolStripMenuItem.Click += new System.EventHandler(this.setTeamCaptainsToolStripMenuItem_Click);
			// 
			// setUserControlledTeamsToolStripMenuItem
			// 
			this.setUserControlledTeamsToolStripMenuItem.Name = "setUserControlledTeamsToolStripMenuItem";
			this.setUserControlledTeamsToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.setUserControlledTeamsToolStripMenuItem.Text = "Set User Controlled Teams ...";
			this.setUserControlledTeamsToolStripMenuItem.Click += new System.EventHandler(this.setUserControlledTeamsToolStripMenuItem_Click);
			// 
			// setGameInjuriesToolStripMenuItem
			// 
			this.setGameInjuriesToolStripMenuItem.Name = "setGameInjuriesToolStripMenuItem";
			this.setGameInjuriesToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.setGameInjuriesToolStripMenuItem.Text = "Set Game Injuries ...";
			this.setGameInjuriesToolStripMenuItem.Click += new System.EventHandler(this.setGameInjuriesToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(277, 6);
			// 
			// draftMenuItem
			// 
			this.draftMenuItem.Name = "draftMenuItem";
			this.draftMenuItem.Size = new System.Drawing.Size(280, 22);
			this.draftMenuItem.Text = "Enter Draft";
			this.draftMenuItem.Click += new System.EventHandler(this.enterDraftToolStripMenuItem_Click);
			// 
			// moveTradedDraftPicksToolStripMenuItem
			// 
			this.moveTradedDraftPicksToolStripMenuItem.Name = "moveTradedDraftPicksToolStripMenuItem";
			this.moveTradedDraftPicksToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.moveTradedDraftPicksToolStripMenuItem.Text = "Move Traded Draft Picks";
			this.moveTradedDraftPicksToolStripMenuItem.Click += new System.EventHandler(this.moveTradedDraftPicksToolStripMenuItem_Click);
			// 
			// importDraftClassToolStripMenuItem
			// 
			this.importDraftClassToolStripMenuItem.Name = "importDraftClassToolStripMenuItem";
			this.importDraftClassToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.importDraftClassToolStripMenuItem.Text = "Import Draft Class";
			this.importDraftClassToolStripMenuItem.Click += new System.EventHandler(this.importDraftClassToolStripMenuItem_Click);
			// 
			// exportDraftClassToolStripMenuItem
			// 
			this.exportDraftClassToolStripMenuItem.Name = "exportDraftClassToolStripMenuItem";
			this.exportDraftClassToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.exportDraftClassToolStripMenuItem.Text = "Export Draft Class";
			this.exportDraftClassToolStripMenuItem.Click += new System.EventHandler(this.exportDraftClassToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(280, 22);
			this.toolStripMenuItem1.Text = "Clear Rookie Games Played";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.clearRookieGamesPlayedToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(277, 6);
			// 
			// weeklyMaintenanceToolStripMenuItem
			// 
			this.weeklyMaintenanceToolStripMenuItem.Name = "weeklyMaintenanceToolStripMenuItem";
			this.weeklyMaintenanceToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.weeklyMaintenanceToolStripMenuItem.Text = "Weekly Maintenance";
			this.weeklyMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.weeklyMaintenanceToolStripMenuItem_Click);
			// 
			// fixProgressionToolStripMenuItem
			// 
			this.fixProgressionToolStripMenuItem.Name = "fixProgressionToolStripMenuItem";
			this.fixProgressionToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.fixProgressionToolStripMenuItem.Text = "Fix Progression";
			this.fixProgressionToolStripMenuItem.Click += new System.EventHandler(this.fixProgressionToolStripMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(277, 6);
			// 
			// depthChartMenuItem
			// 
			this.depthChartMenuItem.Name = "depthChartMenuItem";
			this.depthChartMenuItem.Size = new System.Drawing.Size(280, 22);
			this.depthChartMenuItem.Text = "Reorder Depth Charts (No Prog)";
			this.depthChartMenuItem.Click += new System.EventHandler(this.depthChartMenuItem_Click);
			// 
			// depthChartProgMenuItem
			// 
			this.depthChartProgMenuItem.Name = "depthChartProgMenuItem";
			this.depthChartProgMenuItem.Size = new System.Drawing.Size(280, 22);
			this.depthChartProgMenuItem.Text = "Reorder Depth Charts (w/ Prog)";
			this.depthChartProgMenuItem.Click += new System.EventHandler(this.depthChartProgMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(277, 6);
			// 
			// simulateCPUMinicampsToolStripMenuItem
			// 
			this.simulateCPUMinicampsToolStripMenuItem.Name = "simulateCPUMinicampsToolStripMenuItem";
			this.simulateCPUMinicampsToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.simulateCPUMinicampsToolStripMenuItem.Text = "Simulate CPU Minicamps";
			this.simulateCPUMinicampsToolStripMenuItem.Click += new System.EventHandler(this.simulateCPUMinicampsToolStripMenuItem_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(277, 6);
			// 
			// offSeasonConditioningTrainingCampToolStripMenuItem
			// 
			this.offSeasonConditioningTrainingCampToolStripMenuItem.Name = "offSeasonConditioningTrainingCampToolStripMenuItem";
			this.offSeasonConditioningTrainingCampToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.offSeasonConditioningTrainingCampToolStripMenuItem.Text = "Off-Season Conditioning / Training Camp";
			this.offSeasonConditioningTrainingCampToolStripMenuItem.Click += new System.EventHandler(this.offSeasonConditioningTrainingCampToolStripMenuItem_Click);
			// 
			// tUNEtxtGUIEditorforTrainingCampToolStripMenuItem
			// 
			this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem.Name = "tUNEtxtGUIEditorforTrainingCampToolStripMenuItem";
			this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
			this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem.Text = "TUNE.txt GUI editor (for Training Camp)";
			this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem.Click += new System.EventHandler(this.tUNEtxtGUIEditorforTrainingCampToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.developerBiosToolStripMenuItem1});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.aboutToolStripMenuItem.Text = "About ...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// developerBiosToolStripMenuItem1
			// 
			this.developerBiosToolStripMenuItem1.Name = "developerBiosToolStripMenuItem1";
			this.developerBiosToolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
			this.developerBiosToolStripMenuItem1.Text = "Developer Bio\'s ...";
			this.developerBiosToolStripMenuItem1.Click += new System.EventHandler(this.developerBiosToolStripMenuItem_Click);
			// 
			// developerBiosToolStripMenuItem
			// 
			this.developerBiosToolStripMenuItem.Name = "developerBiosToolStripMenuItem";
			this.developerBiosToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.developerBiosToolStripMenuItem.Text = "Developer Bio\'s ...";
			this.developerBiosToolStripMenuItem.Click += new System.EventHandler(this.developerBiosToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar,
            this.processingTableLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 544);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(792, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
			this.toolStripStatusLabel1.Text = "Processing   ";
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.AutoSize = false;
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(250, 16);
			// 
			// processingTableLabel
			// 
			this.processingTableLabel.Name = "processingTableLabel";
			this.processingTableLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// rosterFileLoaderThread
			// 
			this.rosterFileLoaderThread.WorkerReportsProgress = true;
			this.rosterFileLoaderThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.rosterFileLoaderThread_RunWorkerCompleted);
			this.rosterFileLoaderThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.rosterFileLoaderThread_ProgressChanged);
			// 
			// tabControl
			// 
			this.tabControl.Location = new System.Drawing.Point(317, 224);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(75, 78);
			this.tabControl.TabIndex = 3;
			this.tabControl.Visible = false;
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
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Madden Amp";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
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
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.ComponentModel.BackgroundWorker rosterFileLoaderThread;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchforPlayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem franchiseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editFranchiseOptionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel processingTableLabel;
		private System.Windows.Forms.ToolStripMenuItem searchforCoachesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem globalPlayerAttrEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setTeamCaptainsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem setUserControlledTeamsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem depthChartEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editScheduleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem draftMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setGameInjuriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthChartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthChartProgMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveTradedDraftPicksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDraftClassToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importDraftClassToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simulateCPUMinicampsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weeklyMaintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixProgressionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem developerBiosToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem developerBiosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem offSeasonConditioningTrainingCampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tUNEtxtGUIEditorforTrainingCampToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem playerEditingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem coachPlayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem teamEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.TabControl tabControl;
    }
}
