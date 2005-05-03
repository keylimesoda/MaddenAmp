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
			this.playerSplitContainer = new System.Windows.Forms.SplitContainer();
			this.testButton = new System.Windows.Forms.Button();
			this.deletePlayerButton = new System.Windows.Forms.Button();
			this.createPlayerButton = new System.Windows.Forms.Button();
			this.playerDominantHand = new System.Windows.Forms.CheckBox();
			this.playerYearsPro = new System.Windows.Forms.NumericUpDown();
			this.playerJerseyNumber = new System.Windows.Forms.NumericUpDown();
			this.positionComboBox = new System.Windows.Forms.ComboBox();
			this.teamComboBox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.collegeComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.playerAge = new System.Windows.Forms.NumericUpDown();
			this.lastNameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.firstNameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.filterDraftClassCheckBox = new System.Windows.Forms.CheckBox();
			this.leftButton = new System.Windows.Forms.Button();
			this.rightButton = new System.Windows.Forms.Button();
			this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
			this.filterTeamComboBox = new System.Windows.Forms.ComboBox();
			this.teamCheckBox = new System.Windows.Forms.CheckBox();
			this.positionCheckBox = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.playerRatingPage = new System.Windows.Forms.TabPage();
			this.label74 = new System.Windows.Forms.Label();
			this.playerTotalSalary = new System.Windows.Forms.NumericUpDown();
			this.label70 = new System.Windows.Forms.Label();
			this.playerSigningBonus = new System.Windows.Forms.NumericUpDown();
			this.label69 = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.playerContractYearsLeft = new System.Windows.Forms.NumericUpDown();
			this.playerContractLength = new System.Windows.Forms.NumericUpDown();
			this.playerProBowl = new System.Windows.Forms.CheckBox();
			this.calculateOverallButton = new System.Windows.Forms.Button();
			this.playerOverall = new System.Windows.Forms.NumericUpDown();
			this.label37 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.playerExperiencePoints = new System.Windows.Forms.NumericUpDown();
			this.playerNFLIcon = new System.Windows.Forms.CheckBox();
			this.playerMorale = new System.Windows.Forms.NumericUpDown();
			this.playerImportance = new System.Windows.Forms.NumericUpDown();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.playerToughness = new System.Windows.Forms.NumericUpDown();
			this.playerInjury = new System.Windows.Forms.NumericUpDown();
			this.playerStamina = new System.Windows.Forms.NumericUpDown();
			this.playerKickReturn = new System.Windows.Forms.NumericUpDown();
			this.playerKickAccuracy = new System.Windows.Forms.NumericUpDown();
			this.playerKickPower = new System.Windows.Forms.NumericUpDown();
			this.playerRunBlocking = new System.Windows.Forms.NumericUpDown();
			this.playerPassBlocking = new System.Windows.Forms.NumericUpDown();
			this.playerThrowAccuracy = new System.Windows.Forms.NumericUpDown();
			this.playerThrowPower = new System.Windows.Forms.NumericUpDown();
			this.playerTackle = new System.Windows.Forms.NumericUpDown();
			this.playerBreakTackle = new System.Windows.Forms.NumericUpDown();
			this.playerJumping = new System.Windows.Forms.NumericUpDown();
			this.playerCarrying = new System.Windows.Forms.NumericUpDown();
			this.playerCatching = new System.Windows.Forms.NumericUpDown();
			this.playerAcceleration = new System.Windows.Forms.NumericUpDown();
			this.playerAgility = new System.Windows.Forms.NumericUpDown();
			this.playerAwareness = new System.Windows.Forms.NumericUpDown();
			this.playerStrength = new System.Windows.Forms.NumericUpDown();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.playerSpeed = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.playerAppearancePage = new System.Windows.Forms.TabPage();
			this.label71 = new System.Windows.Forms.Label();
			this.playerThrowingStyle = new System.Windows.Forms.ComboBox();
			this.label67 = new System.Windows.Forms.Label();
			this.label66 = new System.Windows.Forms.Label();
			this.playerRightTatooCombo = new System.Windows.Forms.ComboBox();
			this.playerLeftTatooCombo = new System.Windows.Forms.ComboBox();
			this.label65 = new System.Windows.Forms.Label();
			this.playerFaceShape = new System.Windows.Forms.NumericUpDown();
			this.label64 = new System.Windows.Forms.Label();
			this.playerFace = new System.Windows.Forms.NumericUpDown();
			this.label63 = new System.Windows.Forms.Label();
			this.playerHairStyleCombo = new System.Windows.Forms.ComboBox();
			this.playerSkinColorCombo = new System.Windows.Forms.ComboBox();
			this.label62 = new System.Windows.Forms.Label();
			this.playerHairColorCombo = new System.Windows.Forms.ComboBox();
			this.label61 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.playerWeight = new System.Windows.Forms.NumericUpDown();
			this.playerHeightComboBox = new System.Windows.Forms.ComboBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label60 = new System.Windows.Forms.Label();
			this.label59 = new System.Windows.Forms.Label();
			this.label58 = new System.Windows.Forms.Label();
			this.label57 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label55 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.playerRearShape = new System.Windows.Forms.NumericUpDown();
			this.playerRearRearFat = new System.Windows.Forms.NumericUpDown();
			this.playerRearMuscle = new System.Windows.Forms.NumericUpDown();
			this.playerLegsCalfFat = new System.Windows.Forms.NumericUpDown();
			this.playerLegsCalfMuscle = new System.Windows.Forms.NumericUpDown();
			this.playerLegsThighFat = new System.Windows.Forms.NumericUpDown();
			this.playerLegsThighMuscle = new System.Windows.Forms.NumericUpDown();
			this.label53 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.playerArmsFat = new System.Windows.Forms.NumericUpDown();
			this.playerArmsMuscle = new System.Windows.Forms.NumericUpDown();
			this.label51 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.playerEquipmentFlakJacket = new System.Windows.Forms.NumericUpDown();
			this.label49 = new System.Windows.Forms.Label();
			this.label48 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.playerEquipmentPadShelf = new System.Windows.Forms.NumericUpDown();
			this.playerEquipmentPadWidth = new System.Windows.Forms.NumericUpDown();
			this.playerEquipmentPadHeight = new System.Windows.Forms.NumericUpDown();
			this.playerEquipmentThighPads = new System.Windows.Forms.NumericUpDown();
			this.playerEquipmentShoes = new System.Windows.Forms.NumericUpDown();
			this.playerBodyFat = new System.Windows.Forms.NumericUpDown();
			this.playerBodyMuscle = new System.Windows.Forms.NumericUpDown();
			this.playerBodyWeight = new System.Windows.Forms.NumericUpDown();
			this.playerBodyOverall = new System.Windows.Forms.NumericUpDown();
			this.label42 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.playerInjuryPage = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.playerRemoveInjuryButton = new System.Windows.Forms.Button();
			this.playerAddInjuryButton = new System.Windows.Forms.Button();
			this.label78 = new System.Windows.Forms.Label();
			this.playerInjuryLength = new System.Windows.Forms.NumericUpDown();
			this.label77 = new System.Windows.Forms.Label();
			this.playerInjuryCombo = new System.Windows.Forms.ComboBox();
			this.label76 = new System.Windows.Forms.Label();
			this.playerInjuryReserve = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label86 = new System.Windows.Forms.Label();
			this.label87 = new System.Windows.Forms.Label();
			this.label88 = new System.Windows.Forms.Label();
			this.playerRightAnkleCombo = new System.Windows.Forms.ComboBox();
			this.playerLeftAnkleCombo = new System.Windows.Forms.ComboBox();
			this.playerRightKneeCombo = new System.Windows.Forms.ComboBox();
			this.playerSleevesCombo = new System.Windows.Forms.ComboBox();
			this.playerLeftKneeCombo = new System.Windows.Forms.ComboBox();
			this.playerRightHandCombo = new System.Windows.Forms.ComboBox();
			this.playerLeftHandCombo = new System.Windows.Forms.ComboBox();
			this.label89 = new System.Windows.Forms.Label();
			this.label90 = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.label92 = new System.Windows.Forms.Label();
			this.label93 = new System.Windows.Forms.Label();
			this.playerRightWristCombo = new System.Windows.Forms.ComboBox();
			this.label94 = new System.Windows.Forms.Label();
			this.playerLeftWristCombo = new System.Windows.Forms.ComboBox();
			this.label85 = new System.Windows.Forms.Label();
			this.label84 = new System.Windows.Forms.Label();
			this.label83 = new System.Windows.Forms.Label();
			this.playerRightElbowCombo = new System.Windows.Forms.ComboBox();
			this.playerLeftElbowCombo = new System.Windows.Forms.ComboBox();
			this.playerNeckRollCombo = new System.Windows.Forms.ComboBox();
			this.playerNasalStripCombo = new System.Windows.Forms.ComboBox();
			this.playerMouthPieceCombo = new System.Windows.Forms.ComboBox();
			this.playerEyePaintCombo = new System.Windows.Forms.ComboBox();
			this.playerVisorCombo = new System.Windows.Forms.ComboBox();
			this.label82 = new System.Windows.Forms.Label();
			this.label81 = new System.Windows.Forms.Label();
			this.label80 = new System.Windows.Forms.Label();
			this.label79 = new System.Windows.Forms.Label();
			this.label73 = new System.Windows.Forms.Label();
			this.playerFaceMaskCombo = new System.Windows.Forms.ComboBox();
			this.label72 = new System.Windows.Forms.Label();
			this.playerHelmetStyleCombo = new System.Windows.Forms.ComboBox();
			this.teamPage = new System.Windows.Forms.TabPage();
			this.label75 = new System.Windows.Forms.Label();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.rosterFileLoaderThread = new System.ComponentModel.BackgroundWorker();
			this.testerWorkerThread = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.playerPage.SuspendLayout();
			this.playerSplitContainer.Panel1.SuspendLayout();
			this.playerSplitContainer.Panel2.SuspendLayout();
			this.playerSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerYearsPro)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerJerseyNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.playerRatingPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerTotalSalary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerSigningBonus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerContractYearsLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerContractLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerOverall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerExperiencePoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerMorale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerImportance)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerToughness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerInjury)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerStamina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickReturn)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickAccuracy)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickPower)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRunBlocking)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerPassBlocking)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerThrowAccuracy)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerThrowPower)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerTackle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBreakTackle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerJumping)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerCarrying)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerCatching)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAcceleration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAgility)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAwareness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerStrength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerSpeed)).BeginInit();
			this.playerAppearancePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerFaceShape)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerFace)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearShape)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearRearFat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearMuscle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsCalfFat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsCalfMuscle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsThighFat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsThighMuscle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerArmsFat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerArmsMuscle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentFlakJacket)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadShelf)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentThighPads)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentShoes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyFat)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyMuscle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyWeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyOverall)).BeginInit();
			this.playerInjuryPage.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerInjuryLength)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.teamPage.SuspendLayout();
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
			this.tabControl.Controls.Add(this.teamPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(792, 519);
			this.tabControl.TabIndex = 1;
			// 
			// playerPage
			// 
			this.playerPage.Controls.Add(this.playerSplitContainer);
			this.playerPage.Location = new System.Drawing.Point(4, 22);
			this.playerPage.Name = "playerPage";
			this.playerPage.Padding = new System.Windows.Forms.Padding(3);
			this.playerPage.Size = new System.Drawing.Size(784, 493);
			this.playerPage.TabIndex = 0;
			this.playerPage.Text = "Player Editor";
			// 
			// playerSplitContainer
			// 
			this.playerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playerSplitContainer.Location = new System.Drawing.Point(3, 3);
			this.playerSplitContainer.Name = "playerSplitContainer";
			// 
			// playerSplitContainer.Panel1
			// 
			this.playerSplitContainer.Panel1.Controls.Add(this.testButton);
			this.playerSplitContainer.Panel1.Controls.Add(this.deletePlayerButton);
			this.playerSplitContainer.Panel1.Controls.Add(this.createPlayerButton);
			this.playerSplitContainer.Panel1.Controls.Add(this.playerDominantHand);
			this.playerSplitContainer.Panel1.Controls.Add(this.playerYearsPro);
			this.playerSplitContainer.Panel1.Controls.Add(this.playerJerseyNumber);
			this.playerSplitContainer.Panel1.Controls.Add(this.positionComboBox);
			this.playerSplitContainer.Panel1.Controls.Add(this.teamComboBox);
			this.playerSplitContainer.Panel1.Controls.Add(this.label9);
			this.playerSplitContainer.Panel1.Controls.Add(this.label8);
			this.playerSplitContainer.Panel1.Controls.Add(this.label7);
			this.playerSplitContainer.Panel1.Controls.Add(this.label6);
			this.playerSplitContainer.Panel1.Controls.Add(this.collegeComboBox);
			this.playerSplitContainer.Panel1.Controls.Add(this.label5);
			this.playerSplitContainer.Panel1.Controls.Add(this.label4);
			this.playerSplitContainer.Panel1.Controls.Add(this.playerAge);
			this.playerSplitContainer.Panel1.Controls.Add(this.lastNameTextBox);
			this.playerSplitContainer.Panel1.Controls.Add(this.label3);
			this.playerSplitContainer.Panel1.Controls.Add(this.label2);
			this.playerSplitContainer.Panel1.Controls.Add(this.label1);
			this.playerSplitContainer.Panel1.Controls.Add(this.pictureBox1);
			this.playerSplitContainer.Panel1.Controls.Add(this.firstNameTextBox);
			this.playerSplitContainer.Panel1.Controls.Add(this.groupBox1);
			// 
			// playerSplitContainer.Panel2
			// 
			this.playerSplitContainer.Panel2.Controls.Add(this.tabControl1);
			this.playerSplitContainer.Size = new System.Drawing.Size(778, 487);
			this.playerSplitContainer.SplitterDistance = 230;
			this.playerSplitContainer.TabIndex = 0;
			this.playerSplitContainer.Text = "splitContainer1";
			// 
			// testButton
			// 
			this.testButton.Enabled = false;
			this.testButton.Location = new System.Drawing.Point(137, 105);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(75, 23);
			this.testButton.TabIndex = 36;
			this.testButton.Text = "Test";
			this.testButton.Visible = false;
			this.testButton.Click += new System.EventHandler(this.testButton_Click);
			// 
			// deletePlayerButton
			// 
			this.deletePlayerButton.Location = new System.Drawing.Point(137, 56);
			this.deletePlayerButton.Name = "deletePlayerButton";
			this.deletePlayerButton.Size = new System.Drawing.Size(75, 23);
			this.deletePlayerButton.TabIndex = 37;
			this.deletePlayerButton.Text = "Delete";
			this.deletePlayerButton.Click += new System.EventHandler(this.deletePlayerButton_Click);
			// 
			// createPlayerButton
			// 
			this.createPlayerButton.Enabled = false;
			this.createPlayerButton.Location = new System.Drawing.Point(137, 27);
			this.createPlayerButton.Name = "createPlayerButton";
			this.createPlayerButton.Size = new System.Drawing.Size(75, 23);
			this.createPlayerButton.TabIndex = 36;
			this.createPlayerButton.Text = "Create";
			this.createPlayerButton.Click += new System.EventHandler(this.createPlayerButton_Click);
			// 
			// playerDominantHand
			// 
			this.playerDominantHand.AutoSize = true;
			this.playerDominantHand.Location = new System.Drawing.Point(124, 307);
			this.playerDominantHand.Name = "playerDominantHand";
			this.playerDominantHand.Size = new System.Drawing.Size(81, 17);
			this.playerDominantHand.TabIndex = 8;
			this.playerDominantHand.Text = "Left Handed";
			this.playerDominantHand.CheckedChanged += new System.EventHandler(this.playerDominantHand_CheckedChanged);
			// 
			// playerYearsPro
			// 
			this.playerYearsPro.Location = new System.Drawing.Point(63, 331);
			this.playerYearsPro.Name = "playerYearsPro";
			this.playerYearsPro.Size = new System.Drawing.Size(51, 20);
			this.playerYearsPro.TabIndex = 7;
			this.playerYearsPro.ValueChanged += new System.EventHandler(this.playerYearsPro_ValueChanged);
			// 
			// playerJerseyNumber
			// 
			this.playerJerseyNumber.Location = new System.Drawing.Point(63, 305);
			this.playerJerseyNumber.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerJerseyNumber.Name = "playerJerseyNumber";
			this.playerJerseyNumber.Size = new System.Drawing.Size(51, 20);
			this.playerJerseyNumber.TabIndex = 6;
			this.playerJerseyNumber.ValueChanged += new System.EventHandler(this.playerJerseyNumber_ValueChanged);
			// 
			// positionComboBox
			// 
			this.positionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.positionComboBox.FormattingEnabled = true;
			this.positionComboBox.Location = new System.Drawing.Point(63, 278);
			this.positionComboBox.Name = "positionComboBox";
			this.positionComboBox.Size = new System.Drawing.Size(163, 21);
			this.positionComboBox.TabIndex = 5;
			this.positionComboBox.SelectedIndexChanged += new System.EventHandler(this.positionComboBox_SelectedIndexChanged);
			// 
			// teamComboBox
			// 
			this.teamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.teamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.teamComboBox.FormattingEnabled = true;
			this.teamComboBox.Location = new System.Drawing.Point(63, 251);
			this.teamComboBox.Name = "teamComboBox";
			this.teamComboBox.Size = new System.Drawing.Size(163, 21);
			this.teamComboBox.TabIndex = 4;
			this.teamComboBox.SelectedIndexChanged += new System.EventHandler(this.teamComboBox_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 333);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 13);
			this.label9.TabIndex = 14;
			this.label9.Text = "Years Exp";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 308);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(43, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "Jersey #";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 281);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Position";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(27, 254);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Team";
			// 
			// collegeComboBox
			// 
			this.collegeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.collegeComboBox.FormattingEnabled = true;
			this.collegeComboBox.Items.AddRange(new object[] {
            "Abilene Chr.",
            "Air Force",
            "Akron",
            "Alabama    ",
            "Alabama A&M  ",
            "Alabama St.  ",
            "Alcorn St.  ",
            "Appalach. St.    ",
            "Arizona    ",
            "Arizona St.",
            "Arkansas",
            "Arkansas P.B.",
            "Arkansas St.",
            "Army",
            "Auburn",
            "Austin Peay",
            "Ball State",
            "Baylor",
            "Beth Cookman",
            "Boise State",
            "Boston Coll.",
            "Bowl. Green",
            "Brown",
            "Bucknell",
            "Buffalo",
            "Butler",
            "BYU",
            "Cal Poly SLO",
            "California",
            "Cal-Nrthridge",
            "Cal-Sacrmnto",
            "Canisius",
            "Cent Conn St.    ",
            "Central MI   ",
            "Central St Ohio    ",
            "Charleston S.    ",
            "Cincinnati   ",
            "Citadel    ",
            "Clemson   ",
            "Clinch Valley    ",
            "Colgate    ",
            "Colorado   ",
            "Colorado St.   ",
            "Columbia  ",
            "Cornell  ",
            "Culver-Stockton    ",
            "Dartmouth  ",
            "Davidson    ",
            "Dayton    ",
            "Delaware    ",
            "Delaware St.  ",
            "Drake    ",
            "Duke   ",
            "Duquesne    ",
            "E. Carolina   ",
            "E. Illinois    ",
            "E. Kentucky    ",
            "E. Tenn. St.   ",
            "East. Mich.  ",
            "Eastern Wash.   ",
            "Elon College ",
            "Fairfield  ",
            "Florida    ",
            "Florida A&M",
            "Florida State",
            "Fordham  ",
            "Fresno State   ",
            "Furman     ",
            "Ga. Southern ",
            "Georgetown  ",
            "Georgia     ",
            "Georgia Tech    ",
            "Grambling St. ",
            "Grand Valley St.    ",
            "Hampton    ",
            "Harvard     ",
            "Hawaii       ",
            "Henderson St.    ",
            "Hofstra          ",
            "Holy Cross       ",
            "Houston       ",
            "Howard        ",
            "Idaho           ",
            "Idaho State   ",
            "Illinois     ",
            "Illinois St.    ",
            "Indiana       ",
            "Indiana St.    ",
            "Iona            ",
            "Iowa           ",
            "Iowa State    ",
            "J. Madison     ",
            "Jackson St.    ",
            "Jacksonv. St.    ",
            "John Carroll    ",
            "Kansas        ",
            "Kansas State   ",
            "Kent State      ",
            "Kentucky      ",
            "Kutztown        ",
            "La Salle        ",
            "LA. Tech        ",
            "Lambuth           ",
            "Lehigh           ",
            "Liberty         ",
            "Louisville      ",
            "LSU              ",
            "M. Valley St.  ",
            "Maine           ",
            "Marist           ",
            "Marshall         ",
            "Maryland         ",
            "Massachusetts    ",
            "McNeese St.     ",
            "Memphis         ",
            "Miami           ",
            "Miami Univ.      ",
            "Michigan         ",
            "Michigan St.     ",
            "Mid Tenn St.     ",
            "Minnesota        ",
            "Miss. State      ",
            "Missouri         ",
            "Monmouth         ",
            "Montana           ",
            "Montana State   ",
            "Morehead St.   ",
            "Morehouse      ",
            "Morgan St.    ",
            "Morris Brown       ",
            "Mt S. Antonio      ",
            "Murray State       ",
            "N. Alabama        ",
            "N. Arizona       ",
            "N. Car A&T       ",
            "N. Carolina       ",
            "N. Colorado       ",
            "N. Illinois       ",
            "N.C. State      ",
            "Navy             ",
            "NC Central        ",
            "Nebr.-Omaha      ",
            "Nebraska        ",
            "Nevada          ",
            "New Mex. St.      ",
            "New Mexico        ",
            "Nicholls St.     ",
            "Norfolk State    ",
            "North Texas     ",
            "Northeastern      ",
            "Northern Iowa      ",
            "Northwestern    ",
            "Notre Dame        ",
            "NW Oklahoma St.   ",
            "N\'western St.     ",
            "Ohio              ",
            "Ohio State        ",
            "Oklahoma         ",
            "Oklahoma St.     ",
            "Ole Miss        ",
            "Oregon           ",
            "Oregon State     ",
            "P. View A&M      ",
            "Penn             ",
            "Penn State      ",
            "Pittsburg St.   ",
            "Pittsburgh       ",
            "Portland St.     ",
            "Princeton       ",
            "Purdue           ",
            "Rhode Island     ",
            "Rice             ",
            "Richmond         ",
            "Robert Morris      ",
            "Rowan             ",
            "Rutgers         ",
            "S. Carolina     ",
            "S. Dakota St.      ",
            "S. Illinois       ",
            "S.C. State       ",
            "S.D. State      ",
            "S.F. Austin        ",
            "Sacred Heart      ",
            "Sam Houston        ",
            "Samford            ",
            "San Jose St.      ",
            "Savannah St.       ",
            "SE Missouri        ",
            "SE Missouri St.    ",
            "Shippensburg       ",
            "Siena              ",
            "Simon Fraser      ",
            "SMU              ",
            "Southern        ",
            "Southern Miss     ",
            "Southern Utah    ",
            "St. Francis      ",
            "St. John\'s        ",
            "St. Mary\'s        ",
            "St. Peters        ",
            "Stanford         ",
            "Stony Brook        ",
            "SUNY Albany        ",
            "SW Miss St        ",
            "SW Texas St.      ",
            "Syracuse         ",
            "T A&M K\'ville     ",
            "TCU              ",
            "Temple          ",
            "Tenn. Tech        ",
            "Tenn-Chat         ",
            "Tennessee         ",
            "Tennessee St.      ",
            "Tenn-Martin        ",
            "Texas             ",
            "Texas A&M         ",
            "Texas South.     ",
            "Texas Tech        ",
            "Toledo           ",
            "Towson State       ",
            "Troy State       ",
            "Tulane            ",
            "Tulsa             ",
            "Tuskegee           ",
            "UAB               ",
            "UCF               ",
            "UCLA              ",
            "UConn            ",
            "UL Lafayette      ",
            "UL Monroe         ",
            "UNLV             ",
            "USC            ",
            "USF             ",
            "Utah             ",
            "Utah State      ",
            "UTEP            ",
            "Valdosta St.    ",
            "Valparaiso       ",
            "Vanderbilt       ",
            "Villanova          ",
            "Virginia        ",
            "Virginia Tech    ",
            "VMI                ",
            "W. Carolina      ",
            "W. Illinois      ",
            "W. Kentucky       ",
            "W. Michigan      ",
            "W. Texas A&M    ",
            "Wagner           ",
            "Wake Forest      ",
            "Walla Walla      ",
            "Wash. St.       ",
            "Washington       ",
            "Weber State      ",
            "West Virginia  ",
            "Westminster      ",
            "Will. & Mary    ",
            "Winston Salem    ",
            "Wisconsin      ",
            "Wofford         ",
            "Wyoming        ",
            "Yale            ",
            "Youngstwn St.    ",
            "Sonoma St.       ",
            "No College       ",
            "N/A               ",
            "New Hampshire      ",
            "UW Lacrosse       ",
            "Hastings College    ",
            "Midwestern St.     ",
            "North Dakota       ",
            "Wayne State        ",
            "UW Stevens Pt.   ",
            "Indiana(Penn.)    ",
            "Saginaw Valley    ",
            "Central St.(OK)   ",
            "Emporia State     "});
			this.collegeComboBox.Location = new System.Drawing.Point(63, 224);
			this.collegeComboBox.Name = "collegeComboBox";
			this.collegeComboBox.Size = new System.Drawing.Size(163, 21);
			this.collegeComboBox.TabIndex = 3;
			this.collegeComboBox.SelectedIndexChanged += new System.EventHandler(this.collegeComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 227);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "College";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(34, 198);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(22, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Age";
			// 
			// playerAge
			// 
			this.playerAge.Location = new System.Drawing.Point(63, 198);
			this.playerAge.Name = "playerAge";
			this.playerAge.Size = new System.Drawing.Size(51, 20);
			this.playerAge.TabIndex = 2;
			this.playerAge.ValueChanged += new System.EventHandler(this.playerAge_ValueChanged);
			// 
			// lastNameTextBox
			// 
			this.lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lastNameTextBox.Location = new System.Drawing.Point(63, 172);
			this.lastNameTextBox.MaxLength = 13;
			this.lastNameTextBox.Name = "lastNameTextBox";
			this.lastNameTextBox.Size = new System.Drawing.Size(163, 20);
			this.lastNameTextBox.TabIndex = 1;
			this.lastNameTextBox.Leave += new System.EventHandler(this.lastNameTextBox_Leave);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 174);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Last Name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(212, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "PLAYER INFORMATION";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 149);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "First Name";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(35, 27);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(89, 115);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// firstNameTextBox
			// 
			this.firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.firstNameTextBox.Location = new System.Drawing.Point(63, 146);
			this.firstNameTextBox.MaxLength = 11;
			this.firstNameTextBox.Name = "firstNameTextBox";
			this.firstNameTextBox.Size = new System.Drawing.Size(163, 20);
			this.firstNameTextBox.TabIndex = 0;
			this.firstNameTextBox.Leave += new System.EventHandler(this.firstNameTextBox_Leave);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.filterDraftClassCheckBox);
			this.groupBox1.Controls.Add(this.leftButton);
			this.groupBox1.Controls.Add(this.rightButton);
			this.groupBox1.Controls.Add(this.filterPositionComboBox);
			this.groupBox1.Controls.Add(this.filterTeamComboBox);
			this.groupBox1.Controls.Add(this.teamCheckBox);
			this.groupBox1.Controls.Add(this.positionCheckBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 358);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 129);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Player Filter";
			// 
			// filterDraftClassCheckBox
			// 
			this.filterDraftClassCheckBox.AutoSize = true;
			this.filterDraftClassCheckBox.Location = new System.Drawing.Point(7, 67);
			this.filterDraftClassCheckBox.Name = "filterDraftClassCheckBox";
			this.filterDraftClassCheckBox.Size = new System.Drawing.Size(73, 17);
			this.filterDraftClassCheckBox.TabIndex = 8;
			this.filterDraftClassCheckBox.Text = "Draft Class";
			this.filterDraftClassCheckBox.CheckedChanged += new System.EventHandler(this.filterDraftClassCheckBox_CheckedChanged);
			// 
			// leftButton
			// 
			this.leftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.leftButton.Location = new System.Drawing.Point(1, 98);
			this.leftButton.Name = "leftButton";
			this.leftButton.Size = new System.Drawing.Size(75, 26);
			this.leftButton.TabIndex = 7;
			this.leftButton.Text = "<<";
			this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
			// 
			// rightButton
			// 
			this.rightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.rightButton.Location = new System.Drawing.Point(152, 98);
			this.rightButton.Name = "rightButton";
			this.rightButton.Size = new System.Drawing.Size(75, 26);
			this.rightButton.TabIndex = 6;
			this.rightButton.Text = ">>";
			this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
			// 
			// filterPositionComboBox
			// 
			this.filterPositionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterPositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filterPositionComboBox.FormattingEnabled = true;
			this.filterPositionComboBox.Location = new System.Drawing.Point(73, 39);
			this.filterPositionComboBox.Name = "filterPositionComboBox";
			this.filterPositionComboBox.Size = new System.Drawing.Size(153, 21);
			this.filterPositionComboBox.TabIndex = 3;
			this.filterPositionComboBox.SelectedIndexChanged += new System.EventHandler(this.positionCheckBox_CheckedChanged);
			// 
			// filterTeamComboBox
			// 
			this.filterTeamComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.filterTeamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filterTeamComboBox.FormattingEnabled = true;
			this.filterTeamComboBox.Location = new System.Drawing.Point(73, 16);
			this.filterTeamComboBox.Name = "filterTeamComboBox";
			this.filterTeamComboBox.Size = new System.Drawing.Size(153, 21);
			this.filterTeamComboBox.TabIndex = 1;
			this.filterTeamComboBox.SelectedIndexChanged += new System.EventHandler(this.teamCheckBox_CheckedChanged);
			// 
			// teamCheckBox
			// 
			this.teamCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.teamCheckBox.AutoSize = true;
			this.teamCheckBox.Location = new System.Drawing.Point(7, 20);
			this.teamCheckBox.Name = "teamCheckBox";
			this.teamCheckBox.Size = new System.Drawing.Size(49, 17);
			this.teamCheckBox.TabIndex = 0;
			this.teamCheckBox.Text = "Team";
			this.teamCheckBox.CheckedChanged += new System.EventHandler(this.teamCheckBox_CheckedChanged);
			// 
			// positionCheckBox
			// 
			this.positionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.positionCheckBox.AutoSize = true;
			this.positionCheckBox.Location = new System.Drawing.Point(7, 43);
			this.positionCheckBox.Name = "positionCheckBox";
			this.positionCheckBox.Size = new System.Drawing.Size(59, 17);
			this.positionCheckBox.TabIndex = 2;
			this.positionCheckBox.Text = "Position";
			this.positionCheckBox.CheckedChanged += new System.EventHandler(this.positionCheckBox_CheckedChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.playerRatingPage);
			this.tabControl1.Controls.Add(this.playerAppearancePage);
			this.tabControl1.Controls.Add(this.playerInjuryPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(544, 487);
			this.tabControl1.TabIndex = 0;
			// 
			// playerRatingPage
			// 
			this.playerRatingPage.Controls.Add(this.label74);
			this.playerRatingPage.Controls.Add(this.playerTotalSalary);
			this.playerRatingPage.Controls.Add(this.label70);
			this.playerRatingPage.Controls.Add(this.playerSigningBonus);
			this.playerRatingPage.Controls.Add(this.label69);
			this.playerRatingPage.Controls.Add(this.label68);
			this.playerRatingPage.Controls.Add(this.playerContractYearsLeft);
			this.playerRatingPage.Controls.Add(this.playerContractLength);
			this.playerRatingPage.Controls.Add(this.playerProBowl);
			this.playerRatingPage.Controls.Add(this.calculateOverallButton);
			this.playerRatingPage.Controls.Add(this.playerOverall);
			this.playerRatingPage.Controls.Add(this.label37);
			this.playerRatingPage.Controls.Add(this.label35);
			this.playerRatingPage.Controls.Add(this.playerExperiencePoints);
			this.playerRatingPage.Controls.Add(this.playerNFLIcon);
			this.playerRatingPage.Controls.Add(this.playerMorale);
			this.playerRatingPage.Controls.Add(this.playerImportance);
			this.playerRatingPage.Controls.Add(this.label34);
			this.playerRatingPage.Controls.Add(this.label33);
			this.playerRatingPage.Controls.Add(this.label30);
			this.playerRatingPage.Controls.Add(this.label29);
			this.playerRatingPage.Controls.Add(this.label28);
			this.playerRatingPage.Controls.Add(this.label27);
			this.playerRatingPage.Controls.Add(this.label26);
			this.playerRatingPage.Controls.Add(this.label25);
			this.playerRatingPage.Controls.Add(this.label24);
			this.playerRatingPage.Controls.Add(this.label23);
			this.playerRatingPage.Controls.Add(this.label22);
			this.playerRatingPage.Controls.Add(this.label21);
			this.playerRatingPage.Controls.Add(this.playerToughness);
			this.playerRatingPage.Controls.Add(this.playerInjury);
			this.playerRatingPage.Controls.Add(this.playerStamina);
			this.playerRatingPage.Controls.Add(this.playerKickReturn);
			this.playerRatingPage.Controls.Add(this.playerKickAccuracy);
			this.playerRatingPage.Controls.Add(this.playerKickPower);
			this.playerRatingPage.Controls.Add(this.playerRunBlocking);
			this.playerRatingPage.Controls.Add(this.playerPassBlocking);
			this.playerRatingPage.Controls.Add(this.playerThrowAccuracy);
			this.playerRatingPage.Controls.Add(this.playerThrowPower);
			this.playerRatingPage.Controls.Add(this.playerTackle);
			this.playerRatingPage.Controls.Add(this.playerBreakTackle);
			this.playerRatingPage.Controls.Add(this.playerJumping);
			this.playerRatingPage.Controls.Add(this.playerCarrying);
			this.playerRatingPage.Controls.Add(this.playerCatching);
			this.playerRatingPage.Controls.Add(this.playerAcceleration);
			this.playerRatingPage.Controls.Add(this.playerAgility);
			this.playerRatingPage.Controls.Add(this.playerAwareness);
			this.playerRatingPage.Controls.Add(this.playerStrength);
			this.playerRatingPage.Controls.Add(this.label20);
			this.playerRatingPage.Controls.Add(this.label19);
			this.playerRatingPage.Controls.Add(this.label18);
			this.playerRatingPage.Controls.Add(this.label17);
			this.playerRatingPage.Controls.Add(this.label16);
			this.playerRatingPage.Controls.Add(this.label15);
			this.playerRatingPage.Controls.Add(this.label14);
			this.playerRatingPage.Controls.Add(this.label13);
			this.playerRatingPage.Controls.Add(this.label12);
			this.playerRatingPage.Controls.Add(this.playerSpeed);
			this.playerRatingPage.Controls.Add(this.label11);
			this.playerRatingPage.Location = new System.Drawing.Point(4, 22);
			this.playerRatingPage.Name = "playerRatingPage";
			this.playerRatingPage.Padding = new System.Windows.Forms.Padding(3);
			this.playerRatingPage.Size = new System.Drawing.Size(536, 461);
			this.playerRatingPage.TabIndex = 0;
			this.playerRatingPage.Text = "Player Ratings";
			// 
			// label74
			// 
			this.label74.AutoSize = true;
			this.label74.Location = new System.Drawing.Point(382, 166);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(59, 13);
			this.label74.TabIndex = 65;
			this.label74.Text = "Total Salary";
			// 
			// playerTotalSalary
			// 
			this.playerTotalSalary.DecimalPlaces = 2;
			this.playerTotalSalary.Location = new System.Drawing.Point(447, 159);
			this.playerTotalSalary.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            131072});
			this.playerTotalSalary.Name = "playerTotalSalary";
			this.playerTotalSalary.Size = new System.Drawing.Size(74, 20);
			this.playerTotalSalary.TabIndex = 24;
			this.playerTotalSalary.ValueChanged += new System.EventHandler(this.playerTotalSalary_ValueChanged);
			// 
			// label70
			// 
			this.label70.AutoSize = true;
			this.label70.Location = new System.Drawing.Point(370, 133);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(71, 13);
			this.label70.TabIndex = 63;
			this.label70.Text = "Signing Bonus";
			// 
			// playerSigningBonus
			// 
			this.playerSigningBonus.DecimalPlaces = 2;
			this.playerSigningBonus.Location = new System.Drawing.Point(447, 126);
			this.playerSigningBonus.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            131072});
			this.playerSigningBonus.Name = "playerSigningBonus";
			this.playerSigningBonus.Size = new System.Drawing.Size(74, 20);
			this.playerSigningBonus.TabIndex = 23;
			this.playerSigningBonus.ValueChanged += new System.EventHandler(this.playerSigningBonus_ValueChanged);
			// 
			// label69
			// 
			this.label69.AutoSize = true;
			this.label69.Location = new System.Drawing.Point(366, 95);
			this.label69.Name = "label69";
			this.label69.Size = new System.Drawing.Size(94, 13);
			this.label69.TabIndex = 61;
			this.label69.Text = "Contract Years Left";
			// 
			// label68
			// 
			this.label68.AutoSize = true;
			this.label68.Location = new System.Drawing.Point(381, 56);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(79, 13);
			this.label68.TabIndex = 60;
			this.label68.Text = "Contract Length";
			// 
			// playerContractYearsLeft
			// 
			this.playerContractYearsLeft.Location = new System.Drawing.Point(466, 88);
			this.playerContractYearsLeft.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.playerContractYearsLeft.Name = "playerContractYearsLeft";
			this.playerContractYearsLeft.Size = new System.Drawing.Size(55, 20);
			this.playerContractYearsLeft.TabIndex = 22;
			this.playerContractYearsLeft.ValueChanged += new System.EventHandler(this.playerContractYearsLeft_ValueChanged);
			// 
			// playerContractLength
			// 
			this.playerContractLength.Location = new System.Drawing.Point(466, 49);
			this.playerContractLength.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.playerContractLength.Name = "playerContractLength";
			this.playerContractLength.Size = new System.Drawing.Size(55, 20);
			this.playerContractLength.TabIndex = 21;
			this.playerContractLength.ValueChanged += new System.EventHandler(this.playerContractLength_ValueChanged);
			// 
			// playerProBowl
			// 
			this.playerProBowl.AutoSize = true;
			this.playerProBowl.Location = new System.Drawing.Point(455, 263);
			this.playerProBowl.Name = "playerProBowl";
			this.playerProBowl.Size = new System.Drawing.Size(61, 17);
			this.playerProBowl.TabIndex = 25;
			this.playerProBowl.Text = "ProBowl";
			this.playerProBowl.CheckedChanged += new System.EventHandler(this.playerProBowl_CheckedChanged);
			// 
			// calculateOverallButton
			// 
			this.calculateOverallButton.Location = new System.Drawing.Point(151, 10);
			this.calculateOverallButton.Name = "calculateOverallButton";
			this.calculateOverallButton.Size = new System.Drawing.Size(80, 21);
			this.calculateOverallButton.TabIndex = 56;
			this.calculateOverallButton.Text = "Calculate";
			this.calculateOverallButton.Click += new System.EventHandler(this.calculateOverallButton_Click);
			// 
			// playerOverall
			// 
			this.playerOverall.Location = new System.Drawing.Point(90, 10);
			this.playerOverall.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerOverall.Name = "playerOverall";
			this.playerOverall.Size = new System.Drawing.Size(55, 20);
			this.playerOverall.TabIndex = 0;
			this.playerOverall.ValueChanged += new System.EventHandler(this.playerOverall_ValueChanged);
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label37.Location = new System.Drawing.Point(19, 15);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(65, 15);
			this.label37.TabIndex = 54;
			this.label37.Text = "OVERALL";
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(392, 328);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(49, 13);
			this.label35.TabIndex = 51;
			this.label35.Text = "XP Points";
			// 
			// playerExperiencePoints
			// 
			this.playerExperiencePoints.Location = new System.Drawing.Point(447, 321);
			this.playerExperiencePoints.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
			this.playerExperiencePoints.Name = "playerExperiencePoints";
			this.playerExperiencePoints.Size = new System.Drawing.Size(74, 20);
			this.playerExperiencePoints.TabIndex = 27;
			this.playerExperiencePoints.ValueChanged += new System.EventHandler(this.playerExperiencePoints_ValueChanged);
			// 
			// playerNFLIcon
			// 
			this.playerNFLIcon.AutoSize = true;
			this.playerNFLIcon.Location = new System.Drawing.Point(455, 286);
			this.playerNFLIcon.Name = "playerNFLIcon";
			this.playerNFLIcon.Size = new System.Drawing.Size(66, 17);
			this.playerNFLIcon.TabIndex = 26;
			this.playerNFLIcon.Text = "NFL Icon";
			this.playerNFLIcon.CheckedChanged += new System.EventHandler(this.playerNFLIcon_CheckedChanged);
			// 
			// playerMorale
			// 
			this.playerMorale.Location = new System.Drawing.Point(466, 400);
			this.playerMorale.Name = "playerMorale";
			this.playerMorale.Size = new System.Drawing.Size(55, 20);
			this.playerMorale.TabIndex = 29;
			this.playerMorale.ValueChanged += new System.EventHandler(this.playerMorale_ValueChanged);
			// 
			// playerImportance
			// 
			this.playerImportance.Location = new System.Drawing.Point(466, 361);
			this.playerImportance.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerImportance.Name = "playerImportance";
			this.playerImportance.Size = new System.Drawing.Size(55, 20);
			this.playerImportance.TabIndex = 28;
			this.playerImportance.ValueChanged += new System.EventHandler(this.playerImportance_ValueChanged);
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(425, 407);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(35, 13);
			this.label34.TabIndex = 45;
			this.label34.Text = "Morale";
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(404, 368);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(56, 13);
			this.label33.TabIndex = 44;
			this.label33.Text = "Importance";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(232, 407);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(56, 13);
			this.label30.TabIndex = 39;
			this.label30.Text = "Toughness";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(260, 368);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(28, 13);
			this.label29.TabIndex = 38;
			this.label29.Text = "Injury";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(247, 329);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(41, 13);
			this.label28.TabIndex = 37;
			this.label28.Text = "Stamina";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(229, 290);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(59, 13);
			this.label27.TabIndex = 36;
			this.label27.Text = "Kick Return";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(216, 251);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(72, 13);
			this.label26.TabIndex = 35;
			this.label26.Text = "Kick Accuracy";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(231, 211);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(57, 13);
			this.label25.TabIndex = 34;
			this.label25.Text = "Kick Power";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(222, 173);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(67, 13);
			this.label24.TabIndex = 33;
			this.label24.Text = "Run Blocking";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(218, 133);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(70, 13);
			this.label23.TabIndex = 32;
			this.label23.Text = "Pass Blocking";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(207, 95);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(81, 13);
			this.label22.TabIndex = 31;
			this.label22.Text = "Throw Accuracy";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(222, 56);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(66, 13);
			this.label21.TabIndex = 30;
			this.label21.Text = "Throw Power";
			// 
			// playerToughness
			// 
			this.playerToughness.Location = new System.Drawing.Point(294, 400);
			this.playerToughness.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerToughness.Name = "playerToughness";
			this.playerToughness.Size = new System.Drawing.Size(55, 20);
			this.playerToughness.TabIndex = 20;
			this.playerToughness.ValueChanged += new System.EventHandler(this.playerToughness_ValueChanged);
			// 
			// playerInjury
			// 
			this.playerInjury.Location = new System.Drawing.Point(294, 361);
			this.playerInjury.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerInjury.Name = "playerInjury";
			this.playerInjury.Size = new System.Drawing.Size(55, 20);
			this.playerInjury.TabIndex = 19;
			this.playerInjury.ValueChanged += new System.EventHandler(this.playerInjury_ValueChanged);
			// 
			// playerStamina
			// 
			this.playerStamina.Location = new System.Drawing.Point(294, 322);
			this.playerStamina.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerStamina.Name = "playerStamina";
			this.playerStamina.Size = new System.Drawing.Size(55, 20);
			this.playerStamina.TabIndex = 18;
			this.playerStamina.ValueChanged += new System.EventHandler(this.playerStamina_ValueChanged);
			// 
			// playerKickReturn
			// 
			this.playerKickReturn.Location = new System.Drawing.Point(294, 283);
			this.playerKickReturn.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerKickReturn.Name = "playerKickReturn";
			this.playerKickReturn.Size = new System.Drawing.Size(55, 20);
			this.playerKickReturn.TabIndex = 17;
			this.playerKickReturn.ValueChanged += new System.EventHandler(this.playerKickReturn_ValueChanged);
			// 
			// playerKickAccuracy
			// 
			this.playerKickAccuracy.Location = new System.Drawing.Point(294, 244);
			this.playerKickAccuracy.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerKickAccuracy.Name = "playerKickAccuracy";
			this.playerKickAccuracy.Size = new System.Drawing.Size(55, 20);
			this.playerKickAccuracy.TabIndex = 16;
			this.playerKickAccuracy.ValueChanged += new System.EventHandler(this.playerKickAccuracy_ValueChanged);
			// 
			// playerKickPower
			// 
			this.playerKickPower.Location = new System.Drawing.Point(294, 205);
			this.playerKickPower.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerKickPower.Name = "playerKickPower";
			this.playerKickPower.Size = new System.Drawing.Size(55, 20);
			this.playerKickPower.TabIndex = 15;
			this.playerKickPower.ValueChanged += new System.EventHandler(this.playerKickPower_ValueChanged);
			// 
			// playerRunBlocking
			// 
			this.playerRunBlocking.Location = new System.Drawing.Point(294, 166);
			this.playerRunBlocking.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerRunBlocking.Name = "playerRunBlocking";
			this.playerRunBlocking.Size = new System.Drawing.Size(55, 20);
			this.playerRunBlocking.TabIndex = 14;
			this.playerRunBlocking.ValueChanged += new System.EventHandler(this.playerRunBlocking_ValueChanged);
			// 
			// playerPassBlocking
			// 
			this.playerPassBlocking.Location = new System.Drawing.Point(294, 127);
			this.playerPassBlocking.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerPassBlocking.Name = "playerPassBlocking";
			this.playerPassBlocking.Size = new System.Drawing.Size(55, 20);
			this.playerPassBlocking.TabIndex = 13;
			this.playerPassBlocking.ValueChanged += new System.EventHandler(this.playerPassBlocking_ValueChanged);
			// 
			// playerThrowAccuracy
			// 
			this.playerThrowAccuracy.Location = new System.Drawing.Point(294, 88);
			this.playerThrowAccuracy.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerThrowAccuracy.Name = "playerThrowAccuracy";
			this.playerThrowAccuracy.Size = new System.Drawing.Size(55, 20);
			this.playerThrowAccuracy.TabIndex = 12;
			this.playerThrowAccuracy.ValueChanged += new System.EventHandler(this.playerThrowAccuracy_ValueChanged);
			// 
			// playerThrowPower
			// 
			this.playerThrowPower.Location = new System.Drawing.Point(294, 49);
			this.playerThrowPower.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerThrowPower.Name = "playerThrowPower";
			this.playerThrowPower.Size = new System.Drawing.Size(55, 20);
			this.playerThrowPower.TabIndex = 11;
			this.playerThrowPower.ValueChanged += new System.EventHandler(this.playerThrowPower_ValueChanged);
			// 
			// playerTackle
			// 
			this.playerTackle.Location = new System.Drawing.Point(90, 400);
			this.playerTackle.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerTackle.Name = "playerTackle";
			this.playerTackle.Size = new System.Drawing.Size(55, 20);
			this.playerTackle.TabIndex = 10;
			this.playerTackle.ValueChanged += new System.EventHandler(this.playerTackle_ValueChanged);
			// 
			// playerBreakTackle
			// 
			this.playerBreakTackle.Location = new System.Drawing.Point(90, 361);
			this.playerBreakTackle.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerBreakTackle.Name = "playerBreakTackle";
			this.playerBreakTackle.Size = new System.Drawing.Size(55, 20);
			this.playerBreakTackle.TabIndex = 9;
			this.playerBreakTackle.ValueChanged += new System.EventHandler(this.playerBreakTackle_ValueChanged);
			// 
			// playerJumping
			// 
			this.playerJumping.Location = new System.Drawing.Point(90, 322);
			this.playerJumping.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerJumping.Name = "playerJumping";
			this.playerJumping.Size = new System.Drawing.Size(55, 20);
			this.playerJumping.TabIndex = 8;
			this.playerJumping.ValueChanged += new System.EventHandler(this.playerJumping_ValueChanged);
			// 
			// playerCarrying
			// 
			this.playerCarrying.Location = new System.Drawing.Point(90, 283);
			this.playerCarrying.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerCarrying.Name = "playerCarrying";
			this.playerCarrying.Size = new System.Drawing.Size(55, 20);
			this.playerCarrying.TabIndex = 7;
			this.playerCarrying.ValueChanged += new System.EventHandler(this.playerCarrying_ValueChanged);
			// 
			// playerCatching
			// 
			this.playerCatching.Location = new System.Drawing.Point(90, 244);
			this.playerCatching.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerCatching.Name = "playerCatching";
			this.playerCatching.Size = new System.Drawing.Size(55, 20);
			this.playerCatching.TabIndex = 6;
			this.playerCatching.ValueChanged += new System.EventHandler(this.playerCatching_ValueChanged);
			// 
			// playerAcceleration
			// 
			this.playerAcceleration.Location = new System.Drawing.Point(90, 205);
			this.playerAcceleration.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerAcceleration.Name = "playerAcceleration";
			this.playerAcceleration.Size = new System.Drawing.Size(55, 20);
			this.playerAcceleration.TabIndex = 5;
			this.playerAcceleration.ValueChanged += new System.EventHandler(this.playerAcceleration_ValueChanged);
			// 
			// playerAgility
			// 
			this.playerAgility.Location = new System.Drawing.Point(90, 166);
			this.playerAgility.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerAgility.Name = "playerAgility";
			this.playerAgility.Size = new System.Drawing.Size(55, 20);
			this.playerAgility.TabIndex = 4;
			this.playerAgility.ValueChanged += new System.EventHandler(this.playerAgility_ValueChanged);
			// 
			// playerAwareness
			// 
			this.playerAwareness.Location = new System.Drawing.Point(90, 127);
			this.playerAwareness.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerAwareness.Name = "playerAwareness";
			this.playerAwareness.Size = new System.Drawing.Size(55, 20);
			this.playerAwareness.TabIndex = 3;
			this.playerAwareness.ValueChanged += new System.EventHandler(this.playerAwareness_ValueChanged);
			// 
			// playerStrength
			// 
			this.playerStrength.Location = new System.Drawing.Point(90, 88);
			this.playerStrength.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerStrength.Name = "playerStrength";
			this.playerStrength.Size = new System.Drawing.Size(55, 20);
			this.playerStrength.TabIndex = 2;
			this.playerStrength.ValueChanged += new System.EventHandler(this.playerStrength_ValueChanged);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(49, 407);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(36, 13);
			this.label20.TabIndex = 10;
			this.label20.Text = "Tackle";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(17, 368);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(67, 13);
			this.label19.TabIndex = 9;
			this.label19.Text = "Break Tackle";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(22, 213);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(62, 13);
			this.label18.TabIndex = 8;
			this.label18.Text = "Acceleration";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(39, 251);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(45, 13);
			this.label17.TabIndex = 7;
			this.label17.Text = "Catching";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(43, 292);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(41, 13);
			this.label16.TabIndex = 6;
			this.label16.Text = "Carrying";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(43, 329);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(42, 13);
			this.label15.TabIndex = 5;
			this.label15.Text = "Jumping";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(54, 173);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(30, 13);
			this.label14.TabIndex = 4;
			this.label14.Text = "Agility";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(29, 134);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(55, 13);
			this.label13.TabIndex = 3;
			this.label13.Text = "Awareness";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(41, 95);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(43, 13);
			this.label12.TabIndex = 2;
			this.label12.Text = "Strength";
			// 
			// playerSpeed
			// 
			this.playerSpeed.Location = new System.Drawing.Point(90, 49);
			this.playerSpeed.Name = "playerSpeed";
			this.playerSpeed.Size = new System.Drawing.Size(55, 20);
			this.playerSpeed.TabIndex = 1;
			this.playerSpeed.ValueChanged += new System.EventHandler(this.playerSpeed_ValueChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(50, 56);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 13);
			this.label11.TabIndex = 0;
			this.label11.Text = "Speed";
			// 
			// playerAppearancePage
			// 
			this.playerAppearancePage.Controls.Add(this.label71);
			this.playerAppearancePage.Controls.Add(this.playerThrowingStyle);
			this.playerAppearancePage.Controls.Add(this.label67);
			this.playerAppearancePage.Controls.Add(this.label66);
			this.playerAppearancePage.Controls.Add(this.playerRightTatooCombo);
			this.playerAppearancePage.Controls.Add(this.playerLeftTatooCombo);
			this.playerAppearancePage.Controls.Add(this.label65);
			this.playerAppearancePage.Controls.Add(this.playerFaceShape);
			this.playerAppearancePage.Controls.Add(this.label64);
			this.playerAppearancePage.Controls.Add(this.playerFace);
			this.playerAppearancePage.Controls.Add(this.label63);
			this.playerAppearancePage.Controls.Add(this.playerHairStyleCombo);
			this.playerAppearancePage.Controls.Add(this.playerSkinColorCombo);
			this.playerAppearancePage.Controls.Add(this.label62);
			this.playerAppearancePage.Controls.Add(this.playerHairColorCombo);
			this.playerAppearancePage.Controls.Add(this.label61);
			this.playerAppearancePage.Controls.Add(this.label36);
			this.playerAppearancePage.Controls.Add(this.playerWeight);
			this.playerAppearancePage.Controls.Add(this.playerHeightComboBox);
			this.playerAppearancePage.Controls.Add(this.label32);
			this.playerAppearancePage.Controls.Add(this.label31);
			this.playerAppearancePage.Controls.Add(this.label60);
			this.playerAppearancePage.Controls.Add(this.label59);
			this.playerAppearancePage.Controls.Add(this.label58);
			this.playerAppearancePage.Controls.Add(this.label57);
			this.playerAppearancePage.Controls.Add(this.label56);
			this.playerAppearancePage.Controls.Add(this.label55);
			this.playerAppearancePage.Controls.Add(this.label54);
			this.playerAppearancePage.Controls.Add(this.playerRearShape);
			this.playerAppearancePage.Controls.Add(this.playerRearRearFat);
			this.playerAppearancePage.Controls.Add(this.playerRearMuscle);
			this.playerAppearancePage.Controls.Add(this.playerLegsCalfFat);
			this.playerAppearancePage.Controls.Add(this.playerLegsCalfMuscle);
			this.playerAppearancePage.Controls.Add(this.playerLegsThighFat);
			this.playerAppearancePage.Controls.Add(this.playerLegsThighMuscle);
			this.playerAppearancePage.Controls.Add(this.label53);
			this.playerAppearancePage.Controls.Add(this.label52);
			this.playerAppearancePage.Controls.Add(this.playerArmsFat);
			this.playerAppearancePage.Controls.Add(this.playerArmsMuscle);
			this.playerAppearancePage.Controls.Add(this.label51);
			this.playerAppearancePage.Controls.Add(this.label50);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentFlakJacket);
			this.playerAppearancePage.Controls.Add(this.label49);
			this.playerAppearancePage.Controls.Add(this.label48);
			this.playerAppearancePage.Controls.Add(this.label47);
			this.playerAppearancePage.Controls.Add(this.label46);
			this.playerAppearancePage.Controls.Add(this.label45);
			this.playerAppearancePage.Controls.Add(this.label44);
			this.playerAppearancePage.Controls.Add(this.label43);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentPadShelf);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentPadWidth);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentPadHeight);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentThighPads);
			this.playerAppearancePage.Controls.Add(this.playerEquipmentShoes);
			this.playerAppearancePage.Controls.Add(this.playerBodyFat);
			this.playerAppearancePage.Controls.Add(this.playerBodyMuscle);
			this.playerAppearancePage.Controls.Add(this.playerBodyWeight);
			this.playerAppearancePage.Controls.Add(this.playerBodyOverall);
			this.playerAppearancePage.Controls.Add(this.label42);
			this.playerAppearancePage.Controls.Add(this.label41);
			this.playerAppearancePage.Controls.Add(this.label40);
			this.playerAppearancePage.Controls.Add(this.label39);
			this.playerAppearancePage.Controls.Add(this.label38);
			this.playerAppearancePage.Controls.Add(this.label10);
			this.playerAppearancePage.Location = new System.Drawing.Point(4, 22);
			this.playerAppearancePage.Name = "playerAppearancePage";
			this.playerAppearancePage.Padding = new System.Windows.Forms.Padding(3);
			this.playerAppearancePage.Size = new System.Drawing.Size(536, 461);
			this.playerAppearancePage.TabIndex = 1;
			this.playerAppearancePage.Text = "Appearance";
			// 
			// label71
			// 
			this.label71.AutoSize = true;
			this.label71.Location = new System.Drawing.Point(292, 389);
			this.label71.Name = "label71";
			this.label71.Size = new System.Drawing.Size(73, 13);
			this.label71.TabIndex = 63;
			this.label71.Text = "Throwing Style";
			// 
			// playerThrowingStyle
			// 
			this.playerThrowingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerThrowingStyle.FormattingEnabled = true;
			this.playerThrowingStyle.Items.AddRange(new object[] {
            "Over",
            "Side"});
			this.playerThrowingStyle.Location = new System.Drawing.Point(371, 381);
			this.playerThrowingStyle.Name = "playerThrowingStyle";
			this.playerThrowingStyle.Size = new System.Drawing.Size(105, 21);
			this.playerThrowingStyle.TabIndex = 0;
			this.playerThrowingStyle.SelectedIndexChanged += new System.EventHandler(this.playerThrowingStyle_SelectedIndexChanged);
			// 
			// label67
			// 
			this.label67.AutoSize = true;
			this.label67.Location = new System.Drawing.Point(306, 350);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(59, 13);
			this.label67.TabIndex = 61;
			this.label67.Text = "Right Tatoo";
			// 
			// label66
			// 
			this.label66.AutoSize = true;
			this.label66.Location = new System.Drawing.Point(313, 313);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(52, 13);
			this.label66.TabIndex = 59;
			this.label66.Text = "Left Tatoo";
			// 
			// playerRightTatooCombo
			// 
			this.playerRightTatooCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightTatooCombo.Enabled = false;
			this.playerRightTatooCombo.FormattingEnabled = true;
			this.playerRightTatooCombo.Items.AddRange(new object[] {
            "None",
            "Patch 1",
            "Patch 2",
            "Patch 3",
            "Patch 4",
            "Patch 5",
            "Patch 6",
            "Band 1",
            "Band 2",
            "Band 3",
            "Band 4",
            "Band 5",
            "Band 6"});
			this.playerRightTatooCombo.Location = new System.Drawing.Point(371, 342);
			this.playerRightTatooCombo.Name = "playerRightTatooCombo";
			this.playerRightTatooCombo.Size = new System.Drawing.Size(105, 21);
			this.playerRightTatooCombo.TabIndex = 62;
			// 
			// playerLeftTatooCombo
			// 
			this.playerLeftTatooCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftTatooCombo.Enabled = false;
			this.playerLeftTatooCombo.FormattingEnabled = true;
			this.playerLeftTatooCombo.Items.AddRange(new object[] {
            "None",
            "Patch 1",
            "Patch 2",
            "Patch 3",
            "Patch 4",
            "Patch 5",
            "Patch 6",
            "Band 1",
            "Band 2",
            "Band 3",
            "Band 4",
            "Band 5",
            "Band 6"});
			this.playerLeftTatooCombo.Location = new System.Drawing.Point(371, 305);
			this.playerLeftTatooCombo.Name = "playerLeftTatooCombo";
			this.playerLeftTatooCombo.Size = new System.Drawing.Size(105, 21);
			this.playerLeftTatooCombo.TabIndex = 60;
			// 
			// label65
			// 
			this.label65.AutoSize = true;
			this.label65.Location = new System.Drawing.Point(304, 274);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(61, 13);
			this.label65.TabIndex = 57;
			this.label65.Text = "Face Shape";
			// 
			// playerFaceShape
			// 
			this.playerFaceShape.Location = new System.Drawing.Point(371, 267);
			this.playerFaceShape.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.playerFaceShape.Name = "playerFaceShape";
			this.playerFaceShape.Size = new System.Drawing.Size(83, 20);
			this.playerFaceShape.TabIndex = 58;
			this.playerFaceShape.ValueChanged += new System.EventHandler(this.playerFaceShape_ValueChanged);
			// 
			// label64
			// 
			this.label64.AutoSize = true;
			this.label64.Location = new System.Drawing.Point(338, 239);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(27, 13);
			this.label64.TabIndex = 55;
			this.label64.Text = "Face";
			// 
			// playerFace
			// 
			this.playerFace.Enabled = false;
			this.playerFace.Location = new System.Drawing.Point(371, 232);
			this.playerFace.Maximum = new decimal(new int[] {
            299,
            0,
            0,
            0});
			this.playerFace.Name = "playerFace";
			this.playerFace.Size = new System.Drawing.Size(83, 20);
			this.playerFace.TabIndex = 56;
			// 
			// label63
			// 
			this.label63.AutoSize = true;
			this.label63.Location = new System.Drawing.Point(317, 201);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(48, 13);
			this.label63.TabIndex = 53;
			this.label63.Text = "Hair Style";
			// 
			// playerHairStyleCombo
			// 
			this.playerHairStyleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerHairStyleCombo.FormattingEnabled = true;
			this.playerHairStyleCombo.Items.AddRange(new object[] {
            "Bald",
            "Corn Rows",
            "Afro",
            "Flattop",
            "Buzzcut",
            "Fade",
            "Balding",
            "Close Crop",
            "Unknown",
            "Bald(2)",
            "Balding(2)",
            "Buzzcut(2)",
            "Fade(2)",
            "Curly",
            "Dreadlocks",
            "Mullet"});
			this.playerHairStyleCombo.Location = new System.Drawing.Point(371, 193);
			this.playerHairStyleCombo.Name = "playerHairStyleCombo";
			this.playerHairStyleCombo.Size = new System.Drawing.Size(105, 21);
			this.playerHairStyleCombo.TabIndex = 54;
			this.playerHairStyleCombo.SelectedIndexChanged += new System.EventHandler(this.playerHairStyleCombo_SelectedIndexChanged);
			// 
			// playerSkinColorCombo
			// 
			this.playerSkinColorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerSkinColorCombo.FormattingEnabled = true;
			this.playerSkinColorCombo.Items.AddRange(new object[] {
            "Dark",
            "Medium",
            "Light"});
			this.playerSkinColorCombo.Location = new System.Drawing.Point(371, 115);
			this.playerSkinColorCombo.Name = "playerSkinColorCombo";
			this.playerSkinColorCombo.Size = new System.Drawing.Size(105, 21);
			this.playerSkinColorCombo.TabIndex = 50;
			this.playerSkinColorCombo.SelectedIndexChanged += new System.EventHandler(this.playerSkinColorCombo_SelectedIndexChanged);
			// 
			// label62
			// 
			this.label62.AutoSize = true;
			this.label62.Location = new System.Drawing.Point(316, 161);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(49, 13);
			this.label62.TabIndex = 51;
			this.label62.Text = "Hair Color";
			// 
			// playerHairColorCombo
			// 
			this.playerHairColorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerHairColorCombo.FormattingEnabled = true;
			this.playerHairColorCombo.Items.AddRange(new object[] {
            "Black",
            "Blonde",
            "Brown",
            "Red",
            "Light Brown",
            "Gray"});
			this.playerHairColorCombo.Location = new System.Drawing.Point(371, 153);
			this.playerHairColorCombo.Name = "playerHairColorCombo";
			this.playerHairColorCombo.Size = new System.Drawing.Size(105, 21);
			this.playerHairColorCombo.TabIndex = 52;
			// 
			// label61
			// 
			this.label61.AutoSize = true;
			this.label61.Location = new System.Drawing.Point(314, 123);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(51, 13);
			this.label61.TabIndex = 49;
			this.label61.Text = "Skin Color";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(460, 49);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(16, 13);
			this.label36.TabIndex = 46;
			this.label36.Text = "lbs";
			// 
			// playerWeight
			// 
			this.playerWeight.Location = new System.Drawing.Point(371, 42);
			this.playerWeight.Maximum = new decimal(new int[] {
            415,
            0,
            0,
            0});
			this.playerWeight.Minimum = new decimal(new int[] {
            160,
            0,
            0,
            0});
			this.playerWeight.Name = "playerWeight";
			this.playerWeight.Size = new System.Drawing.Size(83, 20);
			this.playerWeight.TabIndex = 45;
			this.playerWeight.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
			// 
			// playerHeightComboBox
			// 
			this.playerHeightComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerHeightComboBox.FormattingEnabled = true;
			this.playerHeightComboBox.Items.AddRange(new object[] {
            "5\' 5\"",
            "5\' 6\"",
            "5\' 7\"",
            "5\' 8\"",
            "5\' 9\"",
            "5\' 10\"",
            "5\' 11\"",
            "6\' 0\"",
            "6\' 1\"",
            "6\' 2\"",
            "6\' 3\"",
            "6\' 4\"",
            "6\' 5\"",
            "6\' 6\"",
            "6\' 7\"",
            "6\' 8\"",
            "6\' 9\"",
            "6\' 10\"",
            "6\' 11\"",
            "7\' 0\""});
			this.playerHeightComboBox.Location = new System.Drawing.Point(371, 78);
			this.playerHeightComboBox.Name = "playerHeightComboBox";
			this.playerHeightComboBox.Size = new System.Drawing.Size(105, 21);
			this.playerHeightComboBox.TabIndex = 48;
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(331, 86);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(34, 13);
			this.label32.TabIndex = 47;
			this.label32.Text = "Height";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(331, 49);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(37, 13);
			this.label31.TabIndex = 44;
			this.label31.Text = "Weight";
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.Location = new System.Drawing.Point(168, 429);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(34, 13);
			this.label60.TabIndex = 41;
			this.label60.Text = "Shape";
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.Location = new System.Drawing.Point(158, 389);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(44, 13);
			this.label59.TabIndex = 39;
			this.label59.Text = "Rear Fat";
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.Location = new System.Drawing.Point(165, 353);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(37, 13);
			this.label58.TabIndex = 33;
			this.label58.Text = "Muscle";
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Location = new System.Drawing.Point(163, 274);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(39, 13);
			this.label57.TabIndex = 31;
			this.label57.Text = "Calf Fat";
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(144, 236);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(58, 13);
			this.label56.TabIndex = 28;
			this.label56.Text = "Calf Muscle";
			// 
			// label55
			// 
			this.label55.AutoSize = true;
			this.label55.Location = new System.Drawing.Point(154, 196);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(48, 13);
			this.label55.TabIndex = 26;
			this.label55.Text = "Thigh Fat";
			// 
			// label54
			// 
			this.label54.AutoSize = true;
			this.label54.Location = new System.Drawing.Point(135, 159);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(67, 13);
			this.label54.TabIndex = 24;
			this.label54.Text = "Thigh Muscle";
			// 
			// playerRearShape
			// 
			this.playerRearShape.Location = new System.Drawing.Point(208, 422);
			this.playerRearShape.Name = "playerRearShape";
			this.playerRearShape.Size = new System.Drawing.Size(58, 20);
			this.playerRearShape.TabIndex = 42;
			this.playerRearShape.ValueChanged += new System.EventHandler(this.playerRearShape_ValueChanged);
			// 
			// playerRearRearFat
			// 
			this.playerRearRearFat.Location = new System.Drawing.Point(208, 384);
			this.playerRearRearFat.Name = "playerRearRearFat";
			this.playerRearRearFat.Size = new System.Drawing.Size(58, 20);
			this.playerRearRearFat.TabIndex = 40;
			this.playerRearRearFat.ValueChanged += new System.EventHandler(this.playerRearRearFat_ValueChanged);
			// 
			// playerRearMuscle
			// 
			this.playerRearMuscle.Enabled = false;
			this.playerRearMuscle.Location = new System.Drawing.Point(208, 346);
			this.playerRearMuscle.Name = "playerRearMuscle";
			this.playerRearMuscle.Size = new System.Drawing.Size(58, 20);
			this.playerRearMuscle.TabIndex = 34;
			// 
			// playerLegsCalfFat
			// 
			this.playerLegsCalfFat.Location = new System.Drawing.Point(208, 267);
			this.playerLegsCalfFat.Name = "playerLegsCalfFat";
			this.playerLegsCalfFat.Size = new System.Drawing.Size(58, 20);
			this.playerLegsCalfFat.TabIndex = 43;
			this.playerLegsCalfFat.ValueChanged += new System.EventHandler(this.playerLegsCalfFat_ValueChanged);
			// 
			// playerLegsCalfMuscle
			// 
			this.playerLegsCalfMuscle.Location = new System.Drawing.Point(208, 229);
			this.playerLegsCalfMuscle.Name = "playerLegsCalfMuscle";
			this.playerLegsCalfMuscle.Size = new System.Drawing.Size(58, 20);
			this.playerLegsCalfMuscle.TabIndex = 29;
			this.playerLegsCalfMuscle.ValueChanged += new System.EventHandler(this.playerLegsCalfMuscle_ValueChanged);
			// 
			// playerLegsThighFat
			// 
			this.playerLegsThighFat.Location = new System.Drawing.Point(208, 191);
			this.playerLegsThighFat.Name = "playerLegsThighFat";
			this.playerLegsThighFat.Size = new System.Drawing.Size(58, 20);
			this.playerLegsThighFat.TabIndex = 27;
			this.playerLegsThighFat.ValueChanged += new System.EventHandler(this.playerLegsThighFat_ValueChanged);
			// 
			// playerLegsThighMuscle
			// 
			this.playerLegsThighMuscle.Location = new System.Drawing.Point(208, 153);
			this.playerLegsThighMuscle.Name = "playerLegsThighMuscle";
			this.playerLegsThighMuscle.Size = new System.Drawing.Size(58, 20);
			this.playerLegsThighMuscle.TabIndex = 25;
			this.playerLegsThighMuscle.ValueChanged += new System.EventHandler(this.playerLegsThighMuscle_ValueChanged);
			// 
			// label53
			// 
			this.label53.AutoSize = true;
			this.label53.Location = new System.Drawing.Point(184, 88);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(18, 13);
			this.label53.TabIndex = 21;
			this.label53.Text = "Fat";
			// 
			// label52
			// 
			this.label52.AutoSize = true;
			this.label52.Location = new System.Drawing.Point(165, 49);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(37, 13);
			this.label52.TabIndex = 19;
			this.label52.Text = "Muscle";
			// 
			// playerArmsFat
			// 
			this.playerArmsFat.Location = new System.Drawing.Point(208, 81);
			this.playerArmsFat.Name = "playerArmsFat";
			this.playerArmsFat.Size = new System.Drawing.Size(58, 20);
			this.playerArmsFat.TabIndex = 22;
			this.playerArmsFat.ValueChanged += new System.EventHandler(this.playerArmsFat_ValueChanged);
			// 
			// playerArmsMuscle
			// 
			this.playerArmsMuscle.Location = new System.Drawing.Point(208, 42);
			this.playerArmsMuscle.Name = "playerArmsMuscle";
			this.playerArmsMuscle.Size = new System.Drawing.Size(58, 20);
			this.playerArmsMuscle.TabIndex = 20;
			this.playerArmsMuscle.ValueChanged += new System.EventHandler(this.playerArmsMuscle_ValueChanged);
			// 
			// label51
			// 
			this.label51.AutoSize = true;
			this.label51.Location = new System.Drawing.Point(2, 429);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(58, 13);
			this.label51.TabIndex = 37;
			this.label51.Text = "Flak Jacket";
			// 
			// label50
			// 
			this.label50.AutoSize = true;
			this.label50.Location = new System.Drawing.Point(11, 391);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(49, 13);
			this.label50.TabIndex = 35;
			this.label50.Text = "Pad Shelf";
			// 
			// playerEquipmentFlakJacket
			// 
			this.playerEquipmentFlakJacket.Location = new System.Drawing.Point(66, 422);
			this.playerEquipmentFlakJacket.Name = "playerEquipmentFlakJacket";
			this.playerEquipmentFlakJacket.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentFlakJacket.TabIndex = 38;
			this.playerEquipmentFlakJacket.ValueChanged += new System.EventHandler(this.playerEquipmentFlakJacket_ValueChanged);
			// 
			// label49
			// 
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(7, 353);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(53, 13);
			this.label49.TabIndex = 16;
			this.label49.Text = "Pad Width";
			// 
			// label48
			// 
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(4, 315);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(56, 13);
			this.label48.TabIndex = 14;
			this.label48.Text = "Pad Height";
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(3, 277);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(57, 13);
			this.label47.TabIndex = 12;
			this.label47.Text = "Thigh Pads";
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(27, 239);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(33, 13);
			this.label46.TabIndex = 10;
			this.label46.Text = "Shoes";
			// 
			// label45
			// 
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(42, 166);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(18, 13);
			this.label45.TabIndex = 7;
			this.label45.Text = "Fat";
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(23, 127);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(37, 13);
			this.label44.TabIndex = 5;
			this.label44.Text = "Muscle";
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(23, 88);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(37, 13);
			this.label43.TabIndex = 3;
			this.label43.Text = "Weight";
			// 
			// playerEquipmentPadShelf
			// 
			this.playerEquipmentPadShelf.Location = new System.Drawing.Point(66, 384);
			this.playerEquipmentPadShelf.Name = "playerEquipmentPadShelf";
			this.playerEquipmentPadShelf.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentPadShelf.TabIndex = 36;
			this.playerEquipmentPadShelf.ValueChanged += new System.EventHandler(this.playerEquipmentPadShelf_ValueChanged);
			// 
			// playerEquipmentPadWidth
			// 
			this.playerEquipmentPadWidth.Location = new System.Drawing.Point(66, 346);
			this.playerEquipmentPadWidth.Name = "playerEquipmentPadWidth";
			this.playerEquipmentPadWidth.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentPadWidth.TabIndex = 17;
			this.playerEquipmentPadWidth.ValueChanged += new System.EventHandler(this.playerEquipmentPadWidth_ValueChanged);
			// 
			// playerEquipmentPadHeight
			// 
			this.playerEquipmentPadHeight.Location = new System.Drawing.Point(66, 308);
			this.playerEquipmentPadHeight.Name = "playerEquipmentPadHeight";
			this.playerEquipmentPadHeight.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentPadHeight.TabIndex = 15;
			this.playerEquipmentPadHeight.ValueChanged += new System.EventHandler(this.playerEquipmentPadHeight_ValueChanged);
			// 
			// playerEquipmentThighPads
			// 
			this.playerEquipmentThighPads.Enabled = false;
			this.playerEquipmentThighPads.Location = new System.Drawing.Point(66, 270);
			this.playerEquipmentThighPads.Name = "playerEquipmentThighPads";
			this.playerEquipmentThighPads.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentThighPads.TabIndex = 13;
			// 
			// playerEquipmentShoes
			// 
			this.playerEquipmentShoes.Location = new System.Drawing.Point(66, 232);
			this.playerEquipmentShoes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.playerEquipmentShoes.Name = "playerEquipmentShoes";
			this.playerEquipmentShoes.Size = new System.Drawing.Size(58, 20);
			this.playerEquipmentShoes.TabIndex = 11;
			this.playerEquipmentShoes.ValueChanged += new System.EventHandler(this.playerEquipmentShoes_ValueChanged);
			// 
			// playerBodyFat
			// 
			this.playerBodyFat.Location = new System.Drawing.Point(66, 159);
			this.playerBodyFat.Name = "playerBodyFat";
			this.playerBodyFat.Size = new System.Drawing.Size(58, 20);
			this.playerBodyFat.TabIndex = 8;
			this.playerBodyFat.ValueChanged += new System.EventHandler(this.playerBodyFat_ValueChanged);
			// 
			// playerBodyMuscle
			// 
			this.playerBodyMuscle.Location = new System.Drawing.Point(66, 120);
			this.playerBodyMuscle.Name = "playerBodyMuscle";
			this.playerBodyMuscle.Size = new System.Drawing.Size(58, 20);
			this.playerBodyMuscle.TabIndex = 6;
			this.playerBodyMuscle.ValueChanged += new System.EventHandler(this.playerBodyMuscle_ValueChanged);
			// 
			// playerBodyWeight
			// 
			this.playerBodyWeight.Location = new System.Drawing.Point(66, 81);
			this.playerBodyWeight.Name = "playerBodyWeight";
			this.playerBodyWeight.Size = new System.Drawing.Size(58, 20);
			this.playerBodyWeight.TabIndex = 4;
			this.playerBodyWeight.ValueChanged += new System.EventHandler(this.playerBodyWeight_ValueChanged);
			// 
			// playerBodyOverall
			// 
			this.playerBodyOverall.Enabled = false;
			this.playerBodyOverall.Location = new System.Drawing.Point(66, 42);
			this.playerBodyOverall.Name = "playerBodyOverall";
			this.playerBodyOverall.Size = new System.Drawing.Size(58, 20);
			this.playerBodyOverall.TabIndex = 2;
			this.playerBodyOverall.ValueChanged += new System.EventHandler(this.playerBodyOverall_ValueChanged);
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(24, 49);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(36, 13);
			this.label42.TabIndex = 1;
			this.label42.Text = "Overall";
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label41.Location = new System.Drawing.Point(195, 308);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(38, 16);
			this.label41.TabIndex = 32;
			this.label41.Text = "Rear";
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label40.Location = new System.Drawing.Point(196, 118);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(38, 16);
			this.label40.TabIndex = 23;
			this.label40.Text = "Legs";
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label39.Location = new System.Drawing.Point(195, 14);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(39, 16);
			this.label39.TabIndex = 18;
			this.label39.Text = "Arms";
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label38.Location = new System.Drawing.Point(50, 198);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(77, 16);
			this.label38.TabIndex = 9;
			this.label38.Text = "Equipment";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(50, 14);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "Body";
			// 
			// playerInjuryPage
			// 
			this.playerInjuryPage.Controls.Add(this.groupBox3);
			this.playerInjuryPage.Controls.Add(this.groupBox2);
			this.playerInjuryPage.Location = new System.Drawing.Point(4, 22);
			this.playerInjuryPage.Name = "playerInjuryPage";
			this.playerInjuryPage.Size = new System.Drawing.Size(536, 461);
			this.playerInjuryPage.TabIndex = 2;
			this.playerInjuryPage.Text = "Equipment/Injury";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.playerRemoveInjuryButton);
			this.groupBox3.Controls.Add(this.playerAddInjuryButton);
			this.groupBox3.Controls.Add(this.label78);
			this.groupBox3.Controls.Add(this.playerInjuryLength);
			this.groupBox3.Controls.Add(this.label77);
			this.groupBox3.Controls.Add(this.playerInjuryCombo);
			this.groupBox3.Controls.Add(this.label76);
			this.groupBox3.Controls.Add(this.playerInjuryReserve);
			this.groupBox3.Location = new System.Drawing.Point(4, 311);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(531, 139);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Injury";
			// 
			// playerRemoveInjuryButton
			// 
			this.playerRemoveInjuryButton.Location = new System.Drawing.Point(137, 86);
			this.playerRemoveInjuryButton.Name = "playerRemoveInjuryButton";
			this.playerRemoveInjuryButton.Size = new System.Drawing.Size(75, 23);
			this.playerRemoveInjuryButton.TabIndex = 7;
			this.playerRemoveInjuryButton.Text = "Remove";
			this.playerRemoveInjuryButton.Click += new System.EventHandler(this.playerRemoveInjuryButton_Click);
			// 
			// playerAddInjuryButton
			// 
			this.playerAddInjuryButton.Location = new System.Drawing.Point(46, 86);
			this.playerAddInjuryButton.Name = "playerAddInjuryButton";
			this.playerAddInjuryButton.Size = new System.Drawing.Size(75, 23);
			this.playerAddInjuryButton.TabIndex = 6;
			this.playerAddInjuryButton.Text = "Add";
			this.playerAddInjuryButton.Click += new System.EventHandler(this.playerAddInjuryButton_Click);
			// 
			// label78
			// 
			this.label78.AutoSize = true;
			this.label78.Location = new System.Drawing.Point(136, 56);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(25, 13);
			this.label78.TabIndex = 5;
			this.label78.Text = "days";
			// 
			// playerInjuryLength
			// 
			this.playerInjuryLength.Location = new System.Drawing.Point(47, 49);
			this.playerInjuryLength.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
			this.playerInjuryLength.Name = "playerInjuryLength";
			this.playerInjuryLength.Size = new System.Drawing.Size(83, 20);
			this.playerInjuryLength.TabIndex = 4;
			this.playerInjuryLength.ValueChanged += new System.EventHandler(this.playerInjuryLength_ValueChanged);
			// 
			// label77
			// 
			this.label77.AutoSize = true;
			this.label77.Location = new System.Drawing.Point(5, 56);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(36, 13);
			this.label77.TabIndex = 3;
			this.label77.Text = "Length";
			// 
			// playerInjuryCombo
			// 
			this.playerInjuryCombo.FormattingEnabled = true;
			this.playerInjuryCombo.Location = new System.Drawing.Point(46, 20);
			this.playerInjuryCombo.Name = "playerInjuryCombo";
			this.playerInjuryCombo.Size = new System.Drawing.Size(121, 21);
			this.playerInjuryCombo.TabIndex = 2;
			this.playerInjuryCombo.SelectedIndexChanged += new System.EventHandler(this.playerInjuryCombo_SelectedIndexChanged);
			// 
			// label76
			// 
			this.label76.AutoSize = true;
			this.label76.Location = new System.Drawing.Point(12, 28);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(28, 13);
			this.label76.TabIndex = 1;
			this.label76.Text = "Injury";
			// 
			// playerInjuryReserve
			// 
			this.playerInjuryReserve.AutoSize = true;
			this.playerInjuryReserve.Location = new System.Drawing.Point(182, 22);
			this.playerInjuryReserve.Name = "playerInjuryReserve";
			this.playerInjuryReserve.Size = new System.Drawing.Size(97, 17);
			this.playerInjuryReserve.TabIndex = 0;
			this.playerInjuryReserve.Text = "Injured Reserve";
			this.playerInjuryReserve.CheckedChanged += new System.EventHandler(this.playerInjuryReserve_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label86);
			this.groupBox2.Controls.Add(this.label87);
			this.groupBox2.Controls.Add(this.label88);
			this.groupBox2.Controls.Add(this.playerRightAnkleCombo);
			this.groupBox2.Controls.Add(this.playerLeftAnkleCombo);
			this.groupBox2.Controls.Add(this.playerRightKneeCombo);
			this.groupBox2.Controls.Add(this.playerSleevesCombo);
			this.groupBox2.Controls.Add(this.playerLeftKneeCombo);
			this.groupBox2.Controls.Add(this.playerRightHandCombo);
			this.groupBox2.Controls.Add(this.playerLeftHandCombo);
			this.groupBox2.Controls.Add(this.label89);
			this.groupBox2.Controls.Add(this.label90);
			this.groupBox2.Controls.Add(this.label91);
			this.groupBox2.Controls.Add(this.label92);
			this.groupBox2.Controls.Add(this.label93);
			this.groupBox2.Controls.Add(this.playerRightWristCombo);
			this.groupBox2.Controls.Add(this.label94);
			this.groupBox2.Controls.Add(this.playerLeftWristCombo);
			this.groupBox2.Controls.Add(this.label85);
			this.groupBox2.Controls.Add(this.label84);
			this.groupBox2.Controls.Add(this.label83);
			this.groupBox2.Controls.Add(this.playerRightElbowCombo);
			this.groupBox2.Controls.Add(this.playerLeftElbowCombo);
			this.groupBox2.Controls.Add(this.playerNeckRollCombo);
			this.groupBox2.Controls.Add(this.playerNasalStripCombo);
			this.groupBox2.Controls.Add(this.playerMouthPieceCombo);
			this.groupBox2.Controls.Add(this.playerEyePaintCombo);
			this.groupBox2.Controls.Add(this.playerVisorCombo);
			this.groupBox2.Controls.Add(this.label82);
			this.groupBox2.Controls.Add(this.label81);
			this.groupBox2.Controls.Add(this.label80);
			this.groupBox2.Controls.Add(this.label79);
			this.groupBox2.Controls.Add(this.label73);
			this.groupBox2.Controls.Add(this.playerFaceMaskCombo);
			this.groupBox2.Controls.Add(this.label72);
			this.groupBox2.Controls.Add(this.playerHelmetStyleCombo);
			this.groupBox2.Location = new System.Drawing.Point(4, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(529, 300);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Equipment";
			// 
			// label86
			// 
			this.label86.AutoSize = true;
			this.label86.Location = new System.Drawing.Point(277, 274);
			this.label86.Name = "label86";
			this.label86.Size = new System.Drawing.Size(58, 13);
			this.label86.TabIndex = 35;
			this.label86.Text = "Right Ankle";
			// 
			// label87
			// 
			this.label87.AutoSize = true;
			this.label87.Location = new System.Drawing.Point(284, 243);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(51, 13);
			this.label87.TabIndex = 34;
			this.label87.Text = "Left Ankle";
			// 
			// label88
			// 
			this.label88.AutoSize = true;
			this.label88.Location = new System.Drawing.Point(279, 212);
			this.label88.Name = "label88";
			this.label88.Size = new System.Drawing.Size(56, 13);
			this.label88.TabIndex = 33;
			this.label88.Text = "Right Knee";
			// 
			// playerRightAnkleCombo
			// 
			this.playerRightAnkleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightAnkleCombo.FormattingEnabled = true;
			this.playerRightAnkleCombo.Items.AddRange(new object[] {
            "Normal",
            "White Taped",
            "Black Taped",
            "TC Taped"});
			this.playerRightAnkleCombo.Location = new System.Drawing.Point(342, 266);
			this.playerRightAnkleCombo.Name = "playerRightAnkleCombo";
			this.playerRightAnkleCombo.Size = new System.Drawing.Size(92, 21);
			this.playerRightAnkleCombo.TabIndex = 32;
			this.playerRightAnkleCombo.SelectedIndexChanged += new System.EventHandler(this.playerRightAnkleCombo_SelectedIndexChanged);
			// 
			// playerLeftAnkleCombo
			// 
			this.playerLeftAnkleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftAnkleCombo.FormattingEnabled = true;
			this.playerLeftAnkleCombo.Items.AddRange(new object[] {
            "Normal",
            "White Taped",
            "Black Taped",
            "TC Taped"});
			this.playerLeftAnkleCombo.Location = new System.Drawing.Point(341, 235);
			this.playerLeftAnkleCombo.Name = "playerLeftAnkleCombo";
			this.playerLeftAnkleCombo.Size = new System.Drawing.Size(92, 21);
			this.playerLeftAnkleCombo.TabIndex = 31;
			this.playerLeftAnkleCombo.SelectedIndexChanged += new System.EventHandler(this.playerLeftAnkleCombo_SelectedIndexChanged);
			// 
			// playerRightKneeCombo
			// 
			this.playerRightKneeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightKneeCombo.FormattingEnabled = true;
			this.playerRightKneeCombo.Items.AddRange(new object[] {
            "Normal",
            "Brace"});
			this.playerRightKneeCombo.Location = new System.Drawing.Point(341, 204);
			this.playerRightKneeCombo.Name = "playerRightKneeCombo";
			this.playerRightKneeCombo.Size = new System.Drawing.Size(92, 21);
			this.playerRightKneeCombo.TabIndex = 30;
			this.playerRightKneeCombo.SelectedIndexChanged += new System.EventHandler(this.playerRightKneeCombo_SelectedIndexChanged);
			// 
			// playerSleevesCombo
			// 
			this.playerSleevesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerSleevesCombo.FormattingEnabled = true;
			this.playerSleevesCombo.Items.AddRange(new object[] {
            "None",
            "Black",
            "White",
            "Team Color",
            "Wt Half",
            "Blk Half",
            "TC Half"});
			this.playerSleevesCombo.Location = new System.Drawing.Point(341, 142);
			this.playerSleevesCombo.Name = "playerSleevesCombo";
			this.playerSleevesCombo.Size = new System.Drawing.Size(92, 21);
			this.playerSleevesCombo.TabIndex = 29;
			this.playerSleevesCombo.SelectedIndexChanged += new System.EventHandler(this.playerSleevesCombo_SelectedIndexChanged);
			// 
			// playerLeftKneeCombo
			// 
			this.playerLeftKneeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftKneeCombo.FormattingEnabled = true;
			this.playerLeftKneeCombo.Items.AddRange(new object[] {
            "Normal",
            "Brace"});
			this.playerLeftKneeCombo.Location = new System.Drawing.Point(341, 173);
			this.playerLeftKneeCombo.Name = "playerLeftKneeCombo";
			this.playerLeftKneeCombo.Size = new System.Drawing.Size(92, 21);
			this.playerLeftKneeCombo.TabIndex = 28;
			this.playerLeftKneeCombo.SelectedIndexChanged += new System.EventHandler(this.playerLeftKneeCombo_SelectedIndexChanged);
			// 
			// playerRightHandCombo
			// 
			this.playerRightHandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightHandCombo.FormattingEnabled = true;
			this.playerRightHandCombo.Items.AddRange(new object[] {
            "Normal",
            "Taped",
            "Blk Gloves",
            "Wt Gloves",
            "TC Gloves",
            "Wt RB Gloves",
            "Blk RB Gloves",
            "TC RB Gloves"});
			this.playerRightHandCombo.Location = new System.Drawing.Point(340, 111);
			this.playerRightHandCombo.Name = "playerRightHandCombo";
			this.playerRightHandCombo.Size = new System.Drawing.Size(92, 21);
			this.playerRightHandCombo.TabIndex = 27;
			this.playerRightHandCombo.SelectedIndexChanged += new System.EventHandler(this.playerRightHandCombo_SelectedIndexChanged);
			// 
			// playerLeftHandCombo
			// 
			this.playerLeftHandCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftHandCombo.FormattingEnabled = true;
			this.playerLeftHandCombo.Items.AddRange(new object[] {
            "Normal",
            "Taped",
            "Blk Gloves",
            "Wt Gloves",
            "TC Gloves",
            "Wt RB Gloves",
            "Blk RB Gloves",
            "TC RB Gloves"});
			this.playerLeftHandCombo.Location = new System.Drawing.Point(340, 80);
			this.playerLeftHandCombo.Name = "playerLeftHandCombo";
			this.playerLeftHandCombo.Size = new System.Drawing.Size(92, 21);
			this.playerLeftHandCombo.TabIndex = 26;
			this.playerLeftHandCombo.SelectedIndexChanged += new System.EventHandler(this.playerLeftHandCombo_SelectedIndexChanged);
			// 
			// label89
			// 
			this.label89.AutoSize = true;
			this.label89.Location = new System.Drawing.Point(286, 181);
			this.label89.Name = "label89";
			this.label89.Size = new System.Drawing.Size(49, 13);
			this.label89.TabIndex = 25;
			this.label89.Text = "Left Knee";
			// 
			// label90
			// 
			this.label90.AutoSize = true;
			this.label90.Location = new System.Drawing.Point(294, 150);
			this.label90.Name = "label90";
			this.label90.Size = new System.Drawing.Size(41, 13);
			this.label90.TabIndex = 24;
			this.label90.Text = "Sleeves";
			// 
			// label91
			// 
			this.label91.AutoSize = true;
			this.label91.Location = new System.Drawing.Point(278, 119);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(57, 13);
			this.label91.TabIndex = 23;
			this.label91.Text = "Right Hand";
			// 
			// label92
			// 
			this.label92.AutoSize = true;
			this.label92.Location = new System.Drawing.Point(285, 88);
			this.label92.Name = "label92";
			this.label92.Size = new System.Drawing.Size(50, 13);
			this.label92.TabIndex = 22;
			this.label92.Text = "Left Hand";
			// 
			// label93
			// 
			this.label93.AutoSize = true;
			this.label93.Location = new System.Drawing.Point(280, 57);
			this.label93.Name = "label93";
			this.label93.Size = new System.Drawing.Size(55, 13);
			this.label93.TabIndex = 21;
			this.label93.Text = "Right Wrist";
			// 
			// playerRightWristCombo
			// 
			this.playerRightWristCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightWristCombo.FormattingEnabled = true;
			this.playerRightWristCombo.Items.AddRange(new object[] {
            "Normal",
            "QB Wrist",
            "Wt Wrist",
            "Blk Wrist",
            "TC Wrist",
            "Wt Double",
            "Blk Double",
            "TC Double"});
			this.playerRightWristCombo.Location = new System.Drawing.Point(340, 49);
			this.playerRightWristCombo.Name = "playerRightWristCombo";
			this.playerRightWristCombo.Size = new System.Drawing.Size(92, 21);
			this.playerRightWristCombo.TabIndex = 20;
			this.playerRightWristCombo.SelectedIndexChanged += new System.EventHandler(this.playerRightWristCombo_SelectedIndexChanged);
			// 
			// label94
			// 
			this.label94.AutoSize = true;
			this.label94.Location = new System.Drawing.Point(287, 26);
			this.label94.Name = "label94";
			this.label94.Size = new System.Drawing.Size(48, 13);
			this.label94.TabIndex = 19;
			this.label94.Text = "Left Wrist";
			// 
			// playerLeftWristCombo
			// 
			this.playerLeftWristCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftWristCombo.FormattingEnabled = true;
			this.playerLeftWristCombo.Items.AddRange(new object[] {
            "Normal",
            "QB Wrist",
            "Wt Wrist",
            "Blk Wrist",
            "TC Wrist",
            "Wt Double",
            "Blk Double",
            "TC Double"});
			this.playerLeftWristCombo.Location = new System.Drawing.Point(340, 18);
			this.playerLeftWristCombo.Name = "playerLeftWristCombo";
			this.playerLeftWristCombo.Size = new System.Drawing.Size(92, 21);
			this.playerLeftWristCombo.TabIndex = 18;
			this.playerLeftWristCombo.SelectedIndexChanged += new System.EventHandler(this.playerLeftWristCombo_SelectedIndexChanged);
			// 
			// label85
			// 
			this.label85.AutoSize = true;
			this.label85.Location = new System.Drawing.Point(54, 275);
			this.label85.Name = "label85";
			this.label85.Size = new System.Drawing.Size(60, 13);
			this.label85.TabIndex = 17;
			this.label85.Text = "Right Elbow";
			// 
			// label84
			// 
			this.label84.AutoSize = true;
			this.label84.Location = new System.Drawing.Point(61, 244);
			this.label84.Name = "label84";
			this.label84.Size = new System.Drawing.Size(53, 13);
			this.label84.TabIndex = 16;
			this.label84.Text = "Left Elbow";
			// 
			// label83
			// 
			this.label83.AutoSize = true;
			this.label83.Location = new System.Drawing.Point(67, 213);
			this.label83.Name = "label83";
			this.label83.Size = new System.Drawing.Size(48, 13);
			this.label83.TabIndex = 15;
			this.label83.Text = "NeckPad";
			// 
			// playerRightElbowCombo
			// 
			this.playerRightElbowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerRightElbowCombo.FormattingEnabled = true;
			this.playerRightElbowCombo.Items.AddRange(new object[] {
            "Normal",
            "Turf Tape",
            "Rubber Pad",
            "Black Pad",
            "White Pad",
            "Blk TC Pad",
            "Wt TC Pad",
            "Black Wrist",
            "White Wrist",
            "TC Wrist"});
			this.playerRightElbowCombo.Location = new System.Drawing.Point(122, 267);
			this.playerRightElbowCombo.Name = "playerRightElbowCombo";
			this.playerRightElbowCombo.Size = new System.Drawing.Size(92, 21);
			this.playerRightElbowCombo.TabIndex = 14;
			this.playerRightElbowCombo.SelectedIndexChanged += new System.EventHandler(this.playerRightElbowCombo_SelectedIndexChanged);
			// 
			// playerLeftElbowCombo
			// 
			this.playerLeftElbowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerLeftElbowCombo.FormattingEnabled = true;
			this.playerLeftElbowCombo.Items.AddRange(new object[] {
            "Normal",
            "Turf Tape",
            "Rubber Pad",
            "Black Pad",
            "White Pad",
            "Blk TC Pad",
            "Wt TC Pad",
            "Black Wrist",
            "White Wrist",
            "TC Wrist"});
			this.playerLeftElbowCombo.Location = new System.Drawing.Point(121, 236);
			this.playerLeftElbowCombo.Name = "playerLeftElbowCombo";
			this.playerLeftElbowCombo.Size = new System.Drawing.Size(92, 21);
			this.playerLeftElbowCombo.TabIndex = 13;
			this.playerLeftElbowCombo.SelectedIndexChanged += new System.EventHandler(this.playerLeftElbowCombo_SelectedIndexChanged);
			// 
			// playerNeckRollCombo
			// 
			this.playerNeckRollCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerNeckRollCombo.FormattingEnabled = true;
			this.playerNeckRollCombo.Items.AddRange(new object[] {
            "None",
            "Normal",
            "Extended"});
			this.playerNeckRollCombo.Location = new System.Drawing.Point(121, 205);
			this.playerNeckRollCombo.Name = "playerNeckRollCombo";
			this.playerNeckRollCombo.Size = new System.Drawing.Size(92, 21);
			this.playerNeckRollCombo.TabIndex = 12;
			this.playerNeckRollCombo.SelectedIndexChanged += new System.EventHandler(this.playerNeckRollCombo_SelectedIndexChanged);
			// 
			// playerNasalStripCombo
			// 
			this.playerNasalStripCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerNasalStripCombo.FormattingEnabled = true;
			this.playerNasalStripCombo.Items.AddRange(new object[] {
            "None",
            "White",
            "Black"});
			this.playerNasalStripCombo.Location = new System.Drawing.Point(121, 143);
			this.playerNasalStripCombo.Name = "playerNasalStripCombo";
			this.playerNasalStripCombo.Size = new System.Drawing.Size(92, 21);
			this.playerNasalStripCombo.TabIndex = 11;
			this.playerNasalStripCombo.SelectedIndexChanged += new System.EventHandler(this.playerNasalStripCombo_SelectedIndexChanged);
			// 
			// playerMouthPieceCombo
			// 
			this.playerMouthPieceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerMouthPieceCombo.FormattingEnabled = true;
			this.playerMouthPieceCombo.Items.AddRange(new object[] {
            "None",
            "White",
            "Black",
            "Team Color"});
			this.playerMouthPieceCombo.Location = new System.Drawing.Point(121, 174);
			this.playerMouthPieceCombo.Name = "playerMouthPieceCombo";
			this.playerMouthPieceCombo.Size = new System.Drawing.Size(92, 21);
			this.playerMouthPieceCombo.TabIndex = 10;
			this.playerMouthPieceCombo.SelectedIndexChanged += new System.EventHandler(this.playerMouthPieceCombo_SelectedIndexChanged);
			// 
			// playerEyePaintCombo
			// 
			this.playerEyePaintCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerEyePaintCombo.FormattingEnabled = true;
			this.playerEyePaintCombo.Items.AddRange(new object[] {
            "None",
            "Black"});
			this.playerEyePaintCombo.Location = new System.Drawing.Point(120, 112);
			this.playerEyePaintCombo.Name = "playerEyePaintCombo";
			this.playerEyePaintCombo.Size = new System.Drawing.Size(92, 21);
			this.playerEyePaintCombo.TabIndex = 9;
			this.playerEyePaintCombo.SelectedIndexChanged += new System.EventHandler(this.playerEyePaintCombo_SelectedIndexChanged);
			// 
			// playerVisorCombo
			// 
			this.playerVisorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerVisorCombo.FormattingEnabled = true;
			this.playerVisorCombo.Items.AddRange(new object[] {
            "None",
            "Clear",
            "Dark",
            "Amber"});
			this.playerVisorCombo.Location = new System.Drawing.Point(120, 81);
			this.playerVisorCombo.Name = "playerVisorCombo";
			this.playerVisorCombo.Size = new System.Drawing.Size(92, 21);
			this.playerVisorCombo.TabIndex = 8;
			this.playerVisorCombo.SelectedIndexChanged += new System.EventHandler(this.playerVisorCombo_SelectedIndexChanged);
			// 
			// label82
			// 
			this.label82.AutoSize = true;
			this.label82.Location = new System.Drawing.Point(56, 182);
			this.label82.Name = "label82";
			this.label82.Size = new System.Drawing.Size(59, 13);
			this.label82.TabIndex = 7;
			this.label82.Text = "Mouthpiece";
			// 
			// label81
			// 
			this.label81.AutoSize = true;
			this.label81.Location = new System.Drawing.Point(61, 151);
			this.label81.Name = "label81";
			this.label81.Size = new System.Drawing.Size(54, 13);
			this.label81.TabIndex = 6;
			this.label81.Text = "Nasal Strip";
			// 
			// label80
			// 
			this.label80.AutoSize = true;
			this.label80.Location = new System.Drawing.Point(66, 119);
			this.label80.Name = "label80";
			this.label80.Size = new System.Drawing.Size(48, 13);
			this.label80.TabIndex = 5;
			this.label80.Text = "Eye Paint";
			// 
			// label79
			// 
			this.label79.AutoSize = true;
			this.label79.Location = new System.Drawing.Point(88, 89);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(26, 13);
			this.label79.TabIndex = 4;
			this.label79.Text = "Visor";
			// 
			// label73
			// 
			this.label73.AutoSize = true;
			this.label73.Location = new System.Drawing.Point(58, 58);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(56, 13);
			this.label73.TabIndex = 3;
			this.label73.Text = "Face Mask";
			// 
			// playerFaceMaskCombo
			// 
			this.playerFaceMaskCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerFaceMaskCombo.FormattingEnabled = true;
			this.playerFaceMaskCombo.Items.AddRange(new object[] {
            "2-Bar",
            "3-Bar",
            "Half Cage",
            "Full Cage",
            "1-Bar",
            "2-Bar-thin",
            "3-Bar-QB",
            "2-Bar-RB",
            "3 Bar RB",
            "RB Robots",
            "RB Bull",
            "RevoG2B",
            "RevoG3BDU",
            "RevoG2EG"});
			this.playerFaceMaskCombo.Location = new System.Drawing.Point(120, 50);
			this.playerFaceMaskCombo.Name = "playerFaceMaskCombo";
			this.playerFaceMaskCombo.Size = new System.Drawing.Size(92, 21);
			this.playerFaceMaskCombo.TabIndex = 2;
			// 
			// label72
			// 
			this.label72.AutoSize = true;
			this.label72.Location = new System.Drawing.Point(52, 26);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(62, 13);
			this.label72.TabIndex = 1;
			this.label72.Text = "Helmet Style";
			// 
			// playerHelmetStyleCombo
			// 
			this.playerHelmetStyleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.playerHelmetStyleCombo.FormattingEnabled = true;
			this.playerHelmetStyleCombo.Items.AddRange(new object[] {
            "Style 1",
            "Style 2",
            "Style 3",
            "Revolution"});
			this.playerHelmetStyleCombo.Location = new System.Drawing.Point(120, 19);
			this.playerHelmetStyleCombo.Name = "playerHelmetStyleCombo";
			this.playerHelmetStyleCombo.Size = new System.Drawing.Size(92, 21);
			this.playerHelmetStyleCombo.TabIndex = 0;
			// 
			// teamPage
			// 
			this.teamPage.Controls.Add(this.label75);
			this.teamPage.Location = new System.Drawing.Point(4, 22);
			this.teamPage.Name = "teamPage";
			this.teamPage.Padding = new System.Windows.Forms.Padding(3);
			this.teamPage.Size = new System.Drawing.Size(784, 493);
			this.teamPage.TabIndex = 1;
			this.teamPage.Text = "Team Editor";
			// 
			// label75
			// 
			this.label75.AutoSize = true;
			this.label75.Location = new System.Drawing.Point(25, 16);
			this.label75.Name = "label75";
			this.label75.Size = new System.Drawing.Size(117, 13);
			this.label75.TabIndex = 0;
			this.label75.Text = "Nothing to See Here yet";
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
			this.toolStripProgressBar.Size = new System.Drawing.Size(250, 15);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
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
			this.playerSplitContainer.Panel1.ResumeLayout(false);
			this.playerSplitContainer.Panel1.PerformLayout();
			this.playerSplitContainer.Panel2.ResumeLayout(false);
			this.playerSplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.playerYearsPro)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerJerseyNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.playerRatingPage.ResumeLayout(false);
			this.playerRatingPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerTotalSalary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerSigningBonus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerContractYearsLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerContractLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerOverall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerExperiencePoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerMorale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerImportance)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerToughness)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerInjury)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerStamina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickReturn)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickAccuracy)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerKickPower)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRunBlocking)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerPassBlocking)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerThrowAccuracy)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerThrowPower)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerTackle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBreakTackle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerJumping)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerCarrying)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerCatching)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAcceleration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAgility)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerAwareness)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerStrength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerSpeed)).EndInit();
			this.playerAppearancePage.ResumeLayout(false);
			this.playerAppearancePage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerFaceShape)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerFace)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearShape)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearRearFat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerRearMuscle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsCalfFat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsCalfMuscle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsThighFat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerLegsThighMuscle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerArmsFat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerArmsMuscle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentFlakJacket)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadShelf)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentPadHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentThighPads)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerEquipmentShoes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyFat)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyMuscle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyWeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.playerBodyOverall)).EndInit();
			this.playerInjuryPage.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.playerInjuryLength)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.teamPage.ResumeLayout(false);
			this.teamPage.PerformLayout();
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
		private System.Windows.Forms.TabPage teamPage;
		private System.Windows.Forms.SplitContainer playerSplitContainer;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage playerRatingPage;
		private System.Windows.Forms.TabPage playerAppearancePage;
		private System.Windows.Forms.TextBox firstNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox lastNameTextBox;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown playerAge;
		private System.Windows.Forms.CheckBox teamCheckBox;
		private System.Windows.Forms.CheckBox positionCheckBox;
		private System.Windows.Forms.ComboBox filterTeamComboBox;
		private System.Windows.Forms.ComboBox filterPositionComboBox;
		private System.Windows.Forms.Button leftButton;
		private System.Windows.Forms.Button rightButton;
		private System.Windows.Forms.ComboBox collegeComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox teamComboBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox positionComboBox;
		private System.Windows.Forms.NumericUpDown playerJerseyNumber;
		private System.Windows.Forms.NumericUpDown playerYearsPro;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.ComponentModel.BackgroundWorker rosterFileLoaderThread;
		private System.Windows.Forms.NumericUpDown playerSpeed;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown playerStrength;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown playerTackle;
		private System.Windows.Forms.NumericUpDown playerBreakTackle;
		private System.Windows.Forms.NumericUpDown playerJumping;
		private System.Windows.Forms.NumericUpDown playerCarrying;
		private System.Windows.Forms.NumericUpDown playerCatching;
		private System.Windows.Forms.NumericUpDown playerAcceleration;
		private System.Windows.Forms.NumericUpDown playerAgility;
		private System.Windows.Forms.NumericUpDown playerAwareness;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.NumericUpDown playerToughness;
		private System.Windows.Forms.NumericUpDown playerInjury;
		private System.Windows.Forms.NumericUpDown playerStamina;
		private System.Windows.Forms.NumericUpDown playerKickReturn;
		private System.Windows.Forms.NumericUpDown playerKickAccuracy;
		private System.Windows.Forms.NumericUpDown playerKickPower;
		private System.Windows.Forms.NumericUpDown playerRunBlocking;
		private System.Windows.Forms.NumericUpDown playerPassBlocking;
		private System.Windows.Forms.NumericUpDown playerThrowAccuracy;
		private System.Windows.Forms.NumericUpDown playerThrowPower;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.NumericUpDown playerMorale;
		private System.Windows.Forms.NumericUpDown playerImportance;
		private System.Windows.Forms.CheckBox playerNFLIcon;
		private System.Windows.Forms.NumericUpDown playerExperiencePoints;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TabPage playerInjuryPage;
		private System.Windows.Forms.Button calculateOverallButton;
		private System.Windows.Forms.NumericUpDown playerOverall;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.CheckBox playerDominantHand;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.NumericUpDown playerEquipmentPadShelf;
		private System.Windows.Forms.NumericUpDown playerEquipmentPadWidth;
		private System.Windows.Forms.NumericUpDown playerEquipmentPadHeight;
		private System.Windows.Forms.NumericUpDown playerEquipmentThighPads;
		private System.Windows.Forms.NumericUpDown playerEquipmentShoes;
		private System.Windows.Forms.NumericUpDown playerBodyFat;
		private System.Windows.Forms.NumericUpDown playerBodyMuscle;
		private System.Windows.Forms.NumericUpDown playerBodyWeight;
		private System.Windows.Forms.NumericUpDown playerBodyOverall;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.NumericUpDown playerEquipmentFlakJacket;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.NumericUpDown playerRearShape;
		private System.Windows.Forms.NumericUpDown playerRearRearFat;
		private System.Windows.Forms.NumericUpDown playerRearMuscle;
		private System.Windows.Forms.NumericUpDown playerLegsCalfFat;
		private System.Windows.Forms.NumericUpDown playerLegsCalfMuscle;
		private System.Windows.Forms.NumericUpDown playerLegsThighFat;
		private System.Windows.Forms.NumericUpDown playerLegsThighMuscle;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.NumericUpDown playerArmsFat;
		private System.Windows.Forms.NumericUpDown playerArmsMuscle;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.NumericUpDown playerWeight;
		private System.Windows.Forms.ComboBox playerHeightComboBox;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox playerHairColorCombo;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.ComboBox playerLeftTatooCombo;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.NumericUpDown playerFaceShape;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.NumericUpDown playerFace;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.ComboBox playerHairStyleCombo;
		private System.Windows.Forms.ComboBox playerSkinColorCombo;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.ComboBox playerRightTatooCombo;
		private System.Windows.Forms.CheckBox playerProBowl;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.NumericUpDown playerContractYearsLeft;
		private System.Windows.Forms.NumericUpDown playerContractLength;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.NumericUpDown playerSigningBonus;
		private System.Windows.Forms.Label label71;
		private System.Windows.Forms.ComboBox playerThrowingStyle;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox playerHelmetStyleCombo;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.ComboBox playerFaceMaskCombo;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.ToolStripMenuItem searchforPlayerToolStripMenuItem;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.NumericUpDown playerTotalSalary;
		private System.Windows.Forms.Label label75;
		private System.Windows.Forms.CheckBox filterDraftClassCheckBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox playerInjuryReserve;
		private System.Windows.Forms.ComboBox playerInjuryCombo;
		private System.Windows.Forms.Label label76;
		private System.Windows.Forms.Button playerRemoveInjuryButton;
		private System.Windows.Forms.Button playerAddInjuryButton;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.NumericUpDown playerInjuryLength;
		private System.Windows.Forms.Label label77;
		private System.Windows.Forms.Label label86;
		private System.Windows.Forms.Label label87;
		private System.Windows.Forms.Label label88;
		private System.Windows.Forms.ComboBox playerRightAnkleCombo;
		private System.Windows.Forms.ComboBox playerLeftAnkleCombo;
		private System.Windows.Forms.ComboBox playerRightKneeCombo;
		private System.Windows.Forms.ComboBox playerSleevesCombo;
		private System.Windows.Forms.ComboBox playerLeftKneeCombo;
		private System.Windows.Forms.ComboBox playerRightHandCombo;
		private System.Windows.Forms.ComboBox playerLeftHandCombo;
		private System.Windows.Forms.Label label89;
		private System.Windows.Forms.Label label90;
		private System.Windows.Forms.Label label91;
		private System.Windows.Forms.Label label92;
		private System.Windows.Forms.Label label93;
		private System.Windows.Forms.ComboBox playerRightWristCombo;
		private System.Windows.Forms.Label label94;
		private System.Windows.Forms.ComboBox playerLeftWristCombo;
		private System.Windows.Forms.Label label85;
		private System.Windows.Forms.Label label84;
		private System.Windows.Forms.Label label83;
		private System.Windows.Forms.ComboBox playerRightElbowCombo;
		private System.Windows.Forms.ComboBox playerLeftElbowCombo;
		private System.Windows.Forms.ComboBox playerNeckRollCombo;
		private System.Windows.Forms.ComboBox playerNasalStripCombo;
		private System.Windows.Forms.ComboBox playerMouthPieceCombo;
		private System.Windows.Forms.ComboBox playerEyePaintCombo;
		private System.Windows.Forms.ComboBox playerVisorCombo;
		private System.Windows.Forms.Label label82;
		private System.Windows.Forms.Label label81;
		private System.Windows.Forms.Label label80;
		private System.Windows.Forms.Label label79;
		private System.Windows.Forms.Button deletePlayerButton;
		private System.Windows.Forms.Button createPlayerButton;
		private System.Windows.Forms.Button testButton;
		private System.ComponentModel.BackgroundWorker testerWorkerThread;
    }
}