namespace MaddenEditor.Forms
{
    partial class TrainingCampForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingCampForm));
            this.SelectTeam = new System.Windows.Forms.GroupBox();
            this.selectHumanTeam = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filterPositionComboBox = new System.Windows.Forms.ComboBox();
            this.depthChartDataGrid = new System.Windows.Forms.DataGridView();
            this.SelectTeam.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectTeam
            // 
            this.SelectTeam.Controls.Add(this.selectHumanTeam);
            this.SelectTeam.Location = new System.Drawing.Point(4, 6);
            this.SelectTeam.Name = "SelectTeam";
            this.SelectTeam.Size = new System.Drawing.Size(133, 49);
            this.SelectTeam.TabIndex = 0;
            this.SelectTeam.TabStop = false;
            this.SelectTeam.Text = "Select Human Team...";
            // 
            // selectHumanTeam
            // 
            this.selectHumanTeam.FormattingEnabled = true;
            this.selectHumanTeam.Location = new System.Drawing.Point(6, 19);
            this.selectHumanTeam.Name = "selectHumanTeam";
            this.selectHumanTeam.Size = new System.Drawing.Size(121, 21);
            this.selectHumanTeam.TabIndex = 0;
            this.selectHumanTeam.SelectedIndexChanged += new System.EventHandler(this.selectHumanTeam_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.filterPositionComboBox);
            this.groupBox2.Location = new System.Drawing.Point(143, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(102, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Position...";
            // 
            // filterPositionComboBox
            // 
            this.filterPositionComboBox.FormattingEnabled = true;
            this.filterPositionComboBox.Location = new System.Drawing.Point(6, 19);
            this.filterPositionComboBox.Name = "filterPositionComboBox";
            this.filterPositionComboBox.Size = new System.Drawing.Size(89, 21);
            this.filterPositionComboBox.TabIndex = 0;
            this.filterPositionComboBox.SelectedIndexChanged += new System.EventHandler(this.filterPositionComboBox_SelectedIndexChanged);
            // 
            // depthChartDataGrid
            // 
            this.depthChartDataGrid.AllowUserToAddRows = false;
            this.depthChartDataGrid.AllowUserToDeleteRows = false;
            this.depthChartDataGrid.AllowUserToResizeColumns = false;
            this.depthChartDataGrid.AllowUserToResizeRows = false;
            this.depthChartDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.depthChartDataGrid.Location = new System.Drawing.Point(4, 61);
            this.depthChartDataGrid.Name = "depthChartDataGrid";
            this.depthChartDataGrid.ReadOnly = true;
            this.depthChartDataGrid.Size = new System.Drawing.Size(1106, 193);
            this.depthChartDataGrid.TabIndex = 10;
            this.depthChartDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.depthChartDataGrid_CellContentClick);
            // 
            // TrainingCampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 645);
            this.Controls.Add(this.depthChartDataGrid);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SelectTeam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrainingCampForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Camp Utility...";
            this.Load += new System.EventHandler(this.TrainingCampForm_Load);
            this.SelectTeam.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.depthChartDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox selectHumanTeam;
        private System.Windows.Forms.GroupBox SelectTeam;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox filterPositionComboBox;
        private System.Windows.Forms.DataGridView depthChartDataGrid;
    }
}