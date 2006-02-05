namespace MaddenEditor.Forms
{
    partial class TrainingCampTuneGUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingCampTuneGUI));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TuneModifierGrd = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OffSeasonHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simAllCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refetchTunetxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TuneModifierGrd)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TuneModifierGrd);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1195, 684);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tune.txt";
            // 
            // TuneModifierGrd
            // 
            this.TuneModifierGrd.AllowUserToAddRows = false;
            this.TuneModifierGrd.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TuneModifierGrd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.TuneModifierGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TuneModifierGrd.Location = new System.Drawing.Point(6, 19);
            this.TuneModifierGrd.Name = "TuneModifierGrd";
            this.TuneModifierGrd.RowHeadersVisible = false;
            this.TuneModifierGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TuneModifierGrd.Size = new System.Drawing.Size(1183, 659);
            this.TuneModifierGrd.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.cPUSimulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1219, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OffSeasonHelpToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // OffSeasonHelpToolStripMenuItem
            // 
            this.OffSeasonHelpToolStripMenuItem.Name = "OffSeasonHelpToolStripMenuItem";
            this.OffSeasonHelpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OffSeasonHelpToolStripMenuItem.Text = "Tune.txt Help";
            this.OffSeasonHelpToolStripMenuItem.Click += new System.EventHandler(this.OffSeasonHelpToolStripMenuItem_Click);
            // 
            // cPUSimulationToolStripMenuItem
            // 
            this.cPUSimulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simAllCPUToolStripMenuItem,
            this.refetchTunetxtToolStripMenuItem});
            this.cPUSimulationToolStripMenuItem.Name = "cPUSimulationToolStripMenuItem";
            this.cPUSimulationToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.cPUSimulationToolStripMenuItem.Text = "Options";
            // 
            // simAllCPUToolStripMenuItem
            // 
            this.simAllCPUToolStripMenuItem.Name = "simAllCPUToolStripMenuItem";
            this.simAllCPUToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.simAllCPUToolStripMenuItem.Text = "Save Tune.txt...";
            this.simAllCPUToolStripMenuItem.Click += new System.EventHandler(this.simAllCPUToolStripMenuItem_Click);
            // 
            // refetchTunetxtToolStripMenuItem
            // 
            this.refetchTunetxtToolStripMenuItem.Name = "refetchTunetxtToolStripMenuItem";
            this.refetchTunetxtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refetchTunetxtToolStripMenuItem.Text = "Refetch Tune.txt...";
            this.refetchTunetxtToolStripMenuItem.Click += new System.EventHandler(this.refetchTunetxtToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(716, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Do NOT use the semi-colon ( ;) character or the piped character ( | ) in any fiel" +
                "ds!!   These two characters are used as text deliminaters for data access.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TrainingCampTuneGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 746);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrainingCampTuneGUI";
            this.Text = "Tune.txt GUI modifier...";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TuneModifierGrd)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

      //  private System.Windows.Forms.DataGridViewComboBoxColumn TrainingTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView TuneModifierGrd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OffSeasonHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cPUSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simAllCPUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refetchTunetxtToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}