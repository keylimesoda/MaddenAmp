namespace MaddenEditor.Forms
{
    partial class ProgressionForm
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
            this.mainButton = new System.Windows.Forms.Button();
            this.startedUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.startedSlider = new System.Windows.Forms.TrackBar();
            this.playedUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.playedSlider = new System.Windows.Forms.TrackBar();
            this.ageUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ageSlider = new System.Windows.Forms.TrackBar();
            this.youthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.youthSlider = new System.Windows.Forms.TrackBar();
            this.recommended = new System.Windows.Forms.Button();
            this.maxUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.maxSlider = new System.Windows.Forms.TrackBar();
            this.randomUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.randomSlider = new System.Windows.Forms.TrackBar();
            this.rooks = new System.Windows.Forms.CheckBox();
            this.freeAgents = new System.Windows.Forms.CheckBox();
            this.output = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.startedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startedSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.youthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.youthSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // mainButton
            // 
            this.mainButton.Location = new System.Drawing.Point(32, 354);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(117, 23);
            this.mainButton.TabIndex = 0;
            this.mainButton.Text = "Fix Progression";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.mainButton_Click);
            // 
            // startedUpDown
            // 
            this.startedUpDown.Location = new System.Drawing.Point(277, 167);
            this.startedUpDown.Name = "startedUpDown";
            this.startedUpDown.Size = new System.Drawing.Size(40, 20);
            this.startedUpDown.TabIndex = 30;
            this.startedUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.startedUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Games Started";
            // 
            // startedSlider
            // 
            this.startedSlider.Location = new System.Drawing.Point(99, 166);
            this.startedSlider.Maximum = 100;
            this.startedSlider.Name = "startedSlider";
            this.startedSlider.Size = new System.Drawing.Size(168, 45);
            this.startedSlider.TabIndex = 28;
            this.startedSlider.TickFrequency = 5;
            this.startedSlider.Value = 40;
            this.startedSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // playedUpDown
            // 
            this.playedUpDown.Location = new System.Drawing.Point(277, 120);
            this.playedUpDown.Name = "playedUpDown";
            this.playedUpDown.Size = new System.Drawing.Size(40, 20);
            this.playedUpDown.TabIndex = 27;
            this.playedUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.playedUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Games Played";
            // 
            // playedSlider
            // 
            this.playedSlider.Location = new System.Drawing.Point(99, 119);
            this.playedSlider.Maximum = 100;
            this.playedSlider.Name = "playedSlider";
            this.playedSlider.Size = new System.Drawing.Size(168, 45);
            this.playedSlider.TabIndex = 25;
            this.playedSlider.TickFrequency = 5;
            this.playedSlider.Value = 30;
            this.playedSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // ageUpDown
            // 
            this.ageUpDown.Location = new System.Drawing.Point(277, 73);
            this.ageUpDown.Name = "ageUpDown";
            this.ageUpDown.Size = new System.Drawing.Size(40, 20);
            this.ageUpDown.TabIndex = 24;
            this.ageUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.ageUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Subtract for Age";
            // 
            // ageSlider
            // 
            this.ageSlider.Location = new System.Drawing.Point(99, 72);
            this.ageSlider.Maximum = 100;
            this.ageSlider.Name = "ageSlider";
            this.ageSlider.Size = new System.Drawing.Size(168, 45);
            this.ageSlider.TabIndex = 22;
            this.ageSlider.TickFrequency = 5;
            this.ageSlider.Value = 60;
            this.ageSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // youthUpDown
            // 
            this.youthUpDown.Location = new System.Drawing.Point(277, 26);
            this.youthUpDown.Name = "youthUpDown";
            this.youthUpDown.Size = new System.Drawing.Size(40, 20);
            this.youthUpDown.TabIndex = 21;
            this.youthUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.youthUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Add for Youth";
            // 
            // youthSlider
            // 
            this.youthSlider.Location = new System.Drawing.Point(99, 25);
            this.youthSlider.Maximum = 100;
            this.youthSlider.Name = "youthSlider";
            this.youthSlider.Size = new System.Drawing.Size(168, 45);
            this.youthSlider.TabIndex = 19;
            this.youthSlider.TickFrequency = 5;
            this.youthSlider.Value = 20;
            this.youthSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // recommended
            // 
            this.recommended.Location = new System.Drawing.Point(188, 354);
            this.recommended.Name = "recommended";
            this.recommended.Size = new System.Drawing.Size(117, 23);
            this.recommended.TabIndex = 31;
            this.recommended.Text = "Load Defaults";
            this.recommended.UseVisualStyleBackColor = true;
            this.recommended.Click += new System.EventHandler(this.recommended_Click);
            // 
            // maxUpDown
            // 
            this.maxUpDown.Location = new System.Drawing.Point(277, 274);
            this.maxUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxUpDown.Name = "maxUpDown";
            this.maxUpDown.Size = new System.Drawing.Size(40, 20);
            this.maxUpDown.TabIndex = 34;
            this.maxUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Max Adjustment";
            // 
            // maxSlider
            // 
            this.maxSlider.Location = new System.Drawing.Point(99, 273);
            this.maxSlider.Maximum = 20;
            this.maxSlider.Name = "maxSlider";
            this.maxSlider.Size = new System.Drawing.Size(168, 45);
            this.maxSlider.TabIndex = 32;
            this.maxSlider.Value = 10;
            this.maxSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // randomUpDown
            // 
            this.randomUpDown.Location = new System.Drawing.Point(277, 214);
            this.randomUpDown.Name = "randomUpDown";
            this.randomUpDown.Size = new System.Drawing.Size(40, 20);
            this.randomUpDown.TabIndex = 37;
            this.randomUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.randomUpDown.ValueChanged += new System.EventHandler(this.youthUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Randomness";
            // 
            // randomSlider
            // 
            this.randomSlider.Location = new System.Drawing.Point(99, 213);
            this.randomSlider.Maximum = 100;
            this.randomSlider.Name = "randomSlider";
            this.randomSlider.Size = new System.Drawing.Size(168, 45);
            this.randomSlider.TabIndex = 35;
            this.randomSlider.TickFrequency = 5;
            this.randomSlider.Value = 50;
            this.randomSlider.ValueChanged += new System.EventHandler(this.youthSlider_ValueChanged);
            // 
            // rooks
            // 
            this.rooks.AutoSize = true;
            this.rooks.Location = new System.Drawing.Point(11, 322);
            this.rooks.Name = "rooks";
            this.rooks.Size = new System.Drawing.Size(97, 17);
            this.rooks.TabIndex = 38;
            this.rooks.Text = "Adjust Rookies";
            this.rooks.UseVisualStyleBackColor = true;
            // 
            // freeAgents
            // 
            this.freeAgents.AutoSize = true;
            this.freeAgents.Location = new System.Drawing.Point(113, 322);
            this.freeAgents.Name = "freeAgents";
            this.freeAgents.Size = new System.Drawing.Size(115, 17);
            this.freeAgents.TabIndex = 39;
            this.freeAgents.Text = "Adjust Free Agents";
            this.freeAgents.UseVisualStyleBackColor = true;
            // 
            // output
            // 
            this.output.AutoSize = true;
            this.output.Location = new System.Drawing.Point(233, 322);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(96, 17);
            this.output.TabIndex = 40;
            this.output.Text = "Output Results";
            this.output.UseVisualStyleBackColor = true;
            // 
            // ProgressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 389);
            this.Controls.Add(this.output);
            this.Controls.Add(this.freeAgents);
            this.Controls.Add(this.rooks);
            this.Controls.Add(this.randomUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.randomSlider);
            this.Controls.Add(this.maxUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maxSlider);
            this.Controls.Add(this.recommended);
            this.Controls.Add(this.startedUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startedSlider);
            this.Controls.Add(this.playedUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playedSlider);
            this.Controls.Add(this.ageUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ageSlider);
            this.Controls.Add(this.youthUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.youthSlider);
            this.Controls.Add(this.mainButton);
            this.Name = "ProgressionForm";
            this.Text = "Fix Progression";
            ((System.ComponentModel.ISupportInitialize)(this.startedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startedSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playedSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.youthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.youthSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.NumericUpDown startedUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar startedSlider;
        private System.Windows.Forms.NumericUpDown playedUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar playedSlider;
        private System.Windows.Forms.NumericUpDown ageUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar ageSlider;
        private System.Windows.Forms.NumericUpDown youthUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar youthSlider;
        private System.Windows.Forms.Button recommended;
        private System.Windows.Forms.NumericUpDown maxUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar maxSlider;
        private System.Windows.Forms.NumericUpDown randomUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar randomSlider;
        private System.Windows.Forms.CheckBox rooks;
        private System.Windows.Forms.CheckBox freeAgents;
        private System.Windows.Forms.CheckBox output;
    }
}