namespace MaddenEditor.Forms
{
    partial class WeeklyMaintenanceForm
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
            this.fumbleSlider = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.fumbleUpDown = new System.Windows.Forms.NumericUpDown();
            this.accuracyUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.accuracySlider = new System.Windows.Forms.TrackBar();
            this.qbInjuryUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.qbInjurySlider = new System.Windows.Forms.TrackBar();
            this.reSacksUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.reSacksSlider = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useSliders = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fixedWeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.fixedWeightSlider = new System.Windows.Forms.TrackBar();
            this.weightSpreadUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.weightSpreadSlider = new System.Windows.Forms.TrackBar();
            this.usePhysicalSliders = new System.Windows.Forms.CheckBox();
            this.fixedHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.fixedHeightSlider = new System.Windows.Forms.TrackBar();
            this.heightSpreadUpDown = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.heightSpreadSlider = new System.Windows.Forms.TrackBar();
            this.fixedSpeedUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.fixedSpeedSlider = new System.Windows.Forms.TrackBar();
            this.speedSpreadUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.speedSpreadSlider = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRecommendedSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.revertRatingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeAdjustmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.reorderDepthCharts = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fumbleSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fumbleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qbInjuryUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qbInjurySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSacksUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSacksSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedWeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedWeightSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightSpreadUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightSpreadSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedHeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedHeightSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSpreadUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSpreadSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedSpeedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedSpeedSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpreadUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpreadSlider)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // fumbleSlider
            // 
            this.fumbleSlider.Location = new System.Drawing.Point(83, 52);
            this.fumbleSlider.Maximum = 100;
            this.fumbleSlider.Name = "fumbleSlider";
            this.fumbleSlider.Size = new System.Drawing.Size(168, 45);
            this.fumbleSlider.TabIndex = 0;
            this.fumbleSlider.TickFrequency = 5;
            this.fumbleSlider.Value = 50;
            this.fumbleSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fumbles";
            // 
            // fumbleUpDown
            // 
            this.fumbleUpDown.Location = new System.Drawing.Point(261, 53);
            this.fumbleUpDown.Name = "fumbleUpDown";
            this.fumbleUpDown.Size = new System.Drawing.Size(40, 20);
            this.fumbleUpDown.TabIndex = 3;
            this.fumbleUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.fumbleUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // accuracyUpDown
            // 
            this.accuracyUpDown.Location = new System.Drawing.Point(261, 100);
            this.accuracyUpDown.Name = "accuracyUpDown";
            this.accuracyUpDown.Size = new System.Drawing.Size(40, 20);
            this.accuracyUpDown.TabIndex = 12;
            this.accuracyUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.accuracyUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "QB Accuracy";
            // 
            // accuracySlider
            // 
            this.accuracySlider.Location = new System.Drawing.Point(83, 99);
            this.accuracySlider.Maximum = 100;
            this.accuracySlider.Name = "accuracySlider";
            this.accuracySlider.Size = new System.Drawing.Size(168, 45);
            this.accuracySlider.TabIndex = 10;
            this.accuracySlider.TickFrequency = 5;
            this.accuracySlider.Value = 50;
            this.accuracySlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // qbInjuryUpDown
            // 
            this.qbInjuryUpDown.Location = new System.Drawing.Point(261, 147);
            this.qbInjuryUpDown.Name = "qbInjuryUpDown";
            this.qbInjuryUpDown.Size = new System.Drawing.Size(40, 20);
            this.qbInjuryUpDown.TabIndex = 15;
            this.qbInjuryUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.qbInjuryUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "QB Injuries";
            // 
            // qbInjurySlider
            // 
            this.qbInjurySlider.Location = new System.Drawing.Point(83, 146);
            this.qbInjurySlider.Maximum = 100;
            this.qbInjurySlider.Name = "qbInjurySlider";
            this.qbInjurySlider.Size = new System.Drawing.Size(168, 45);
            this.qbInjurySlider.TabIndex = 13;
            this.qbInjurySlider.TickFrequency = 5;
            this.qbInjurySlider.Value = 50;
            this.qbInjurySlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // reSacksUpDown
            // 
            this.reSacksUpDown.Location = new System.Drawing.Point(261, 194);
            this.reSacksUpDown.Name = "reSacksUpDown";
            this.reSacksUpDown.Size = new System.Drawing.Size(40, 20);
            this.reSacksUpDown.TabIndex = 18;
            this.reSacksUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.reSacksUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "RE Sacks";
            // 
            // reSacksSlider
            // 
            this.reSacksSlider.Location = new System.Drawing.Point(83, 193);
            this.reSacksSlider.Maximum = 100;
            this.reSacksSlider.Name = "reSacksSlider";
            this.reSacksSlider.Size = new System.Drawing.Size(168, 45);
            this.reSacksSlider.TabIndex = 16;
            this.reSacksSlider.TickFrequency = 5;
            this.reSacksSlider.Value = 50;
            this.reSacksSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.useSliders);
            this.groupBox1.Controls.Add(this.reSacksUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.reSacksSlider);
            this.groupBox1.Controls.Add(this.qbInjuryUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.qbInjurySlider);
            this.groupBox1.Controls.Add(this.accuracyUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.accuracySlider);
            this.groupBox1.Controls.Add(this.fumbleUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fumbleSlider);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 249);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extra Sliders";
            // 
            // useSliders
            // 
            this.useSliders.AutoSize = true;
            this.useSliders.Checked = true;
            this.useSliders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useSliders.Location = new System.Drawing.Point(29, 25);
            this.useSliders.Name = "useSliders";
            this.useSliders.Size = new System.Drawing.Size(149, 17);
            this.useSliders.TabIndex = 19;
            this.useSliders.Text = "Use Extra In-Game Sliders";
            this.useSliders.UseVisualStyleBackColor = true;
            this.useSliders.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fixedWeightUpDown);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.fixedWeightSlider);
            this.groupBox2.Controls.Add(this.weightSpreadUpDown);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.weightSpreadSlider);
            this.groupBox2.Controls.Add(this.usePhysicalSliders);
            this.groupBox2.Controls.Add(this.fixedHeightUpDown);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.fixedHeightSlider);
            this.groupBox2.Controls.Add(this.heightSpreadUpDown);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.heightSpreadSlider);
            this.groupBox2.Controls.Add(this.fixedSpeedUpDown);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.fixedSpeedSlider);
            this.groupBox2.Controls.Add(this.speedSpreadUpDown);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.speedSpreadSlider);
            this.groupBox2.Location = new System.Drawing.Point(348, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 383);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speed, Height, and Weight Differentials";
            // 
            // fixedWeightUpDown
            // 
            this.fixedWeightUpDown.Location = new System.Drawing.Point(283, 336);
            this.fixedWeightUpDown.Maximum = new decimal(new int[] {
            440,
            0,
            0,
            0});
            this.fixedWeightUpDown.Minimum = new decimal(new int[] {
            140,
            0,
            0,
            0});
            this.fixedWeightUpDown.Name = "fixedWeightUpDown";
            this.fixedWeightUpDown.Size = new System.Drawing.Size(40, 20);
            this.fixedWeightUpDown.TabIndex = 25;
            this.fixedWeightUpDown.Value = new decimal(new int[] {
            290,
            0,
            0,
            0});
            this.fixedWeightUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Fixed Weight";
            // 
            // fixedWeightSlider
            // 
            this.fixedWeightSlider.LargeChange = 10;
            this.fixedWeightSlider.Location = new System.Drawing.Point(105, 335);
            this.fixedWeightSlider.Maximum = 440;
            this.fixedWeightSlider.Minimum = 140;
            this.fixedWeightSlider.Name = "fixedWeightSlider";
            this.fixedWeightSlider.Size = new System.Drawing.Size(168, 45);
            this.fixedWeightSlider.TabIndex = 23;
            this.fixedWeightSlider.TickFrequency = 15;
            this.fixedWeightSlider.Value = 290;
            this.fixedWeightSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // weightSpreadUpDown
            // 
            this.weightSpreadUpDown.Location = new System.Drawing.Point(283, 289);
            this.weightSpreadUpDown.Name = "weightSpreadUpDown";
            this.weightSpreadUpDown.Size = new System.Drawing.Size(40, 20);
            this.weightSpreadUpDown.TabIndex = 22;
            this.weightSpreadUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.weightSpreadUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Weight Spread";
            // 
            // weightSpreadSlider
            // 
            this.weightSpreadSlider.Location = new System.Drawing.Point(105, 288);
            this.weightSpreadSlider.Maximum = 100;
            this.weightSpreadSlider.Name = "weightSpreadSlider";
            this.weightSpreadSlider.Size = new System.Drawing.Size(168, 45);
            this.weightSpreadSlider.TabIndex = 20;
            this.weightSpreadSlider.TickFrequency = 5;
            this.weightSpreadSlider.Value = 50;
            this.weightSpreadSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // usePhysicalSliders
            // 
            this.usePhysicalSliders.AutoSize = true;
            this.usePhysicalSliders.Checked = true;
            this.usePhysicalSliders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usePhysicalSliders.Location = new System.Drawing.Point(29, 25);
            this.usePhysicalSliders.Name = "usePhysicalSliders";
            this.usePhysicalSliders.Size = new System.Drawing.Size(164, 17);
            this.usePhysicalSliders.TabIndex = 19;
            this.usePhysicalSliders.Text = "Use Physical In-Game Sliders";
            this.usePhysicalSliders.UseVisualStyleBackColor = true;
            this.usePhysicalSliders.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // fixedHeightUpDown
            // 
            this.fixedHeightUpDown.Location = new System.Drawing.Point(283, 218);
            this.fixedHeightUpDown.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.fixedHeightUpDown.Minimum = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.fixedHeightUpDown.Name = "fixedHeightUpDown";
            this.fixedHeightUpDown.Size = new System.Drawing.Size(40, 20);
            this.fixedHeightUpDown.TabIndex = 18;
            this.fixedHeightUpDown.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.fixedHeightUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Fixed WR Height";
            // 
            // fixedHeightSlider
            // 
            this.fixedHeightSlider.LargeChange = 2;
            this.fixedHeightSlider.Location = new System.Drawing.Point(105, 217);
            this.fixedHeightSlider.Maximum = 95;
            this.fixedHeightSlider.Minimum = 55;
            this.fixedHeightSlider.Name = "fixedHeightSlider";
            this.fixedHeightSlider.Size = new System.Drawing.Size(168, 45);
            this.fixedHeightSlider.TabIndex = 16;
            this.fixedHeightSlider.TickFrequency = 2;
            this.fixedHeightSlider.Value = 75;
            this.fixedHeightSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // heightSpreadUpDown
            // 
            this.heightSpreadUpDown.Location = new System.Drawing.Point(283, 171);
            this.heightSpreadUpDown.Name = "heightSpreadUpDown";
            this.heightSpreadUpDown.Size = new System.Drawing.Size(40, 20);
            this.heightSpreadUpDown.TabIndex = 15;
            this.heightSpreadUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.heightSpreadUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "WR Height Spread";
            // 
            // heightSpreadSlider
            // 
            this.heightSpreadSlider.Location = new System.Drawing.Point(105, 170);
            this.heightSpreadSlider.Maximum = 100;
            this.heightSpreadSlider.Name = "heightSpreadSlider";
            this.heightSpreadSlider.Size = new System.Drawing.Size(168, 45);
            this.heightSpreadSlider.TabIndex = 13;
            this.heightSpreadSlider.TickFrequency = 5;
            this.heightSpreadSlider.Value = 50;
            this.heightSpreadSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // fixedSpeedUpDown
            // 
            this.fixedSpeedUpDown.Location = new System.Drawing.Point(283, 100);
            this.fixedSpeedUpDown.Name = "fixedSpeedUpDown";
            this.fixedSpeedUpDown.Size = new System.Drawing.Size(40, 20);
            this.fixedSpeedUpDown.TabIndex = 12;
            this.fixedSpeedUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.fixedSpeedUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Fixed Speed";
            // 
            // fixedSpeedSlider
            // 
            this.fixedSpeedSlider.Location = new System.Drawing.Point(105, 99);
            this.fixedSpeedSlider.Maximum = 100;
            this.fixedSpeedSlider.Name = "fixedSpeedSlider";
            this.fixedSpeedSlider.Size = new System.Drawing.Size(168, 45);
            this.fixedSpeedSlider.TabIndex = 10;
            this.fixedSpeedSlider.TickFrequency = 5;
            this.fixedSpeedSlider.Value = 50;
            this.fixedSpeedSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // speedSpreadUpDown
            // 
            this.speedSpreadUpDown.Location = new System.Drawing.Point(283, 53);
            this.speedSpreadUpDown.Name = "speedSpreadUpDown";
            this.speedSpreadUpDown.Size = new System.Drawing.Size(40, 20);
            this.speedSpreadUpDown.TabIndex = 3;
            this.speedSpreadUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.speedSpreadUpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Speed Spread";
            // 
            // speedSpreadSlider
            // 
            this.speedSpreadSlider.Location = new System.Drawing.Point(105, 52);
            this.speedSpreadSlider.Maximum = 100;
            this.speedSpreadSlider.Name = "speedSpreadSlider";
            this.speedSpreadSlider.Size = new System.Drawing.Size(168, 45);
            this.speedSpreadSlider.TabIndex = 0;
            this.speedSpreadSlider.TickFrequency = 5;
            this.speedSpreadSlider.Value = 50;
            this.speedSpreadSlider.ValueChanged += new System.EventHandler(this.SliderValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(697, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.loadRecommendedSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.revertRatingsToolStripMenuItem,
            this.makeAdjustmentsToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem2.Text = "Save Settings";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
            this.toolStripMenuItem1.Text = "Load Last Saved Settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // loadRecommendedSettingsToolStripMenuItem
            // 
            this.loadRecommendedSettingsToolStripMenuItem.Name = "loadRecommendedSettingsToolStripMenuItem";
            this.loadRecommendedSettingsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.loadRecommendedSettingsToolStripMenuItem.Text = "Load Recommended Settings";
            this.loadRecommendedSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadRecommendedSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // revertRatingsToolStripMenuItem
            // 
            this.revertRatingsToolStripMenuItem.Name = "revertRatingsToolStripMenuItem";
            this.revertRatingsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.revertRatingsToolStripMenuItem.Text = "Revert Ratings";
            this.revertRatingsToolStripMenuItem.Click += new System.EventHandler(this.revertRatingsToolStripMenuItem_Click);
            // 
            // makeAdjustmentsToolStripMenuItem
            // 
            this.makeAdjustmentsToolStripMenuItem.Name = "makeAdjustmentsToolStripMenuItem";
            this.makeAdjustmentsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.makeAdjustmentsToolStripMenuItem.Text = "Make Adjustments";
            this.makeAdjustmentsToolStripMenuItem.Click += new System.EventHandler(this.makeAdjustmentsToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.reorderDepthCharts);
            this.groupBox3.Location = new System.Drawing.Point(12, 298);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 122);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other Maintenance";
            // 
            // reorderDepthCharts
            // 
            this.reorderDepthCharts.AutoSize = true;
            this.reorderDepthCharts.Checked = true;
            this.reorderDepthCharts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reorderDepthCharts.Location = new System.Drawing.Point(16, 27);
            this.reorderDepthCharts.Name = "reorderDepthCharts";
            this.reorderDepthCharts.Size = new System.Drawing.Size(179, 17);
            this.reorderDepthCharts.TabIndex = 0;
            this.reorderDepthCharts.Text = "Auto-Reorder CPU Depth Charts";
            this.reorderDepthCharts.UseVisualStyleBackColor = true;
            this.reorderDepthCharts.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // WeeklyMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 432);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WeeklyMaintenanceForm";
            this.Text = "Weekly Maintenance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeeklyMaintenanceForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.fumbleSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fumbleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qbInjuryUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qbInjurySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSacksUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reSacksSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedWeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedWeightSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightSpreadUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightSpreadSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedHeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedHeightSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSpreadUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSpreadSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedSpeedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fixedSpeedSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpreadUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpreadSlider)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar fumbleSlider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown fumbleUpDown;
        private System.Windows.Forms.NumericUpDown accuracyUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar accuracySlider;
        private System.Windows.Forms.NumericUpDown qbInjuryUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar qbInjurySlider;
        private System.Windows.Forms.NumericUpDown reSacksUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar reSacksSlider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox useSliders;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown fixedWeightUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar fixedWeightSlider;
        private System.Windows.Forms.NumericUpDown weightSpreadUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar weightSpreadSlider;
        private System.Windows.Forms.NumericUpDown fixedHeightUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar fixedHeightSlider;
        private System.Windows.Forms.NumericUpDown heightSpreadUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar heightSpreadSlider;
        private System.Windows.Forms.NumericUpDown fixedSpeedUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar fixedSpeedSlider;
        private System.Windows.Forms.NumericUpDown speedSpreadUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar speedSpreadSlider;
        private System.Windows.Forms.CheckBox usePhysicalSliders;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRecommendedSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revertRatingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeAdjustmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox reorderDepthCharts;
    }
}