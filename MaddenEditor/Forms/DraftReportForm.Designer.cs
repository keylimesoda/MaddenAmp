namespace MaddenEditor.Forms
{
    partial class DraftReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DraftReportForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDraftReport = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.DraftReportName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DraftReportValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DraftReportName,
            this.DraftReportValue});
            this.dataGridView1.Location = new System.Drawing.Point(16, 102);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(525, 333);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 78);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // buttonDraftReport
            // 
            this.buttonDraftReport.Location = new System.Drawing.Point(451, 23);
            this.buttonDraftReport.Name = "buttonDraftReport";
            this.buttonDraftReport.Size = new System.Drawing.Size(64, 54);
            this.buttonDraftReport.TabIndex = 2;
            this.buttonDraftReport.Text = "Draft Class Report";
            this.buttonDraftReport.UseVisualStyleBackColor = true;
            this.buttonDraftReport.Click += new System.EventHandler(this.buttonDraftReport_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(462, 441);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // DraftReportName
            // 
            this.DraftReportName.HeaderText = "Name";
            this.DraftReportName.Name = "DraftReportName";
            this.DraftReportName.ReadOnly = true;
            this.DraftReportName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DraftReportName.Width = 350;
            // 
            // DraftReportValue
            // 
            this.DraftReportValue.HeaderText = "Value";
            this.DraftReportValue.Name = "DraftReportValue";
            this.DraftReportValue.ReadOnly = true;
            this.DraftReportValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DraftReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 476);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.buttonDraftReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DraftReportForm";
            this.Text = "DraftReport";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDraftReport;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn DraftReportName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DraftReportValue;
    }
}