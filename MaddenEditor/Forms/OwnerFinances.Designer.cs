namespace MaddenEditor.Forms
{
    partial class OwnerFinances
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
            this.TeamComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LeagueRevenue_Updown = new System.Windows.Forms.NumericUpDown();
            this.TVIncome_Updown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.LeagueRevenue_Textbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Finances_Label = new System.Windows.Forms.Label();
            this.StadiumUpgrades_Textbox = new System.Windows.Forms.TextBox();
            this.TVIncome_Textbox = new System.Windows.Forms.TextBox();
            this.StadiumUpgrades_Updown = new System.Windows.Forms.NumericUpDown();
            this.TVContract_Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StadiumMaint_Updown = new System.Windows.Forms.NumericUpDown();
            this.TVContract_Updown = new System.Windows.Forms.NumericUpDown();
            this.StadiumMaint_Textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.DataGrid_WeeklyIncome = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.LeagueRevenue_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVIncome_Updown)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StadiumUpgrades_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StadiumMaint_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVContract_Updown)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_WeeklyIncome)).BeginInit();
            this.SuspendLayout();
            // 
            // TeamComboBox
            // 
            this.TeamComboBox.FormattingEnabled = true;
            this.TeamComboBox.Location = new System.Drawing.Point(141, 150);
            this.TeamComboBox.Name = "TeamComboBox";
            this.TeamComboBox.Size = new System.Drawing.Size(121, 21);
            this.TeamComboBox.TabIndex = 0;
            this.TeamComboBox.SelectedIndexChanged += new System.EventHandler(this.TeamComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Team";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "League Revenue Sharing";
            // 
            // LeagueRevenue_Updown
            // 
            this.LeagueRevenue_Updown.Location = new System.Drawing.Point(141, 45);
            this.LeagueRevenue_Updown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LeagueRevenue_Updown.Name = "LeagueRevenue_Updown";
            this.LeagueRevenue_Updown.Size = new System.Drawing.Size(68, 20);
            this.LeagueRevenue_Updown.TabIndex = 21;
            this.LeagueRevenue_Updown.ValueChanged += new System.EventHandler(this.LeagueRevenue_Updown_ValueChanged);
            // 
            // TVIncome_Updown
            // 
            this.TVIncome_Updown.Location = new System.Drawing.Point(141, 193);
            this.TVIncome_Updown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TVIncome_Updown.Name = "TVIncome_Updown";
            this.TVIncome_Updown.Size = new System.Drawing.Size(68, 20);
            this.TVIncome_Updown.TabIndex = 23;
            this.TVIncome_Updown.ValueChanged += new System.EventHandler(this.TVIncome_Updown_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(78, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "TV Income";
            // 
            // LeagueRevenue_Textbox
            // 
            this.LeagueRevenue_Textbox.Enabled = false;
            this.LeagueRevenue_Textbox.Location = new System.Drawing.Point(215, 45);
            this.LeagueRevenue_Textbox.Name = "LeagueRevenue_Textbox";
            this.LeagueRevenue_Textbox.Size = new System.Drawing.Size(73, 20);
            this.LeagueRevenue_Textbox.TabIndex = 24;
            
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Finances_Label);
            this.panel1.Controls.Add(this.StadiumUpgrades_Textbox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TVIncome_Updown);
            this.panel1.Controls.Add(this.TVIncome_Textbox);
            this.panel1.Controls.Add(this.TeamComboBox);
            this.panel1.Controls.Add(this.StadiumUpgrades_Updown);
            this.panel1.Controls.Add(this.LeagueRevenue_Updown);
            this.panel1.Controls.Add(this.TVContract_Label);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.LeagueRevenue_Textbox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.StadiumMaint_Updown);
            this.panel1.Controls.Add(this.TVContract_Updown);
            this.panel1.Controls.Add(this.StadiumMaint_Textbox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 694);
            this.panel1.TabIndex = 25;
            // 
            // Finances_Label
            // 
            this.Finances_Label.AutoSize = true;
            this.Finances_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Finances_Label.Location = new System.Drawing.Point(127, 3);
            this.Finances_Label.Name = "Finances_Label";
            this.Finances_Label.Size = new System.Drawing.Size(82, 20);
            this.Finances_Label.TabIndex = 36;
            this.Finances_Label.Text = "Finances";
            // 
            // StadiumUpgrades_Textbox
            // 
            this.StadiumUpgrades_Textbox.Enabled = false;
            this.StadiumUpgrades_Textbox.Location = new System.Drawing.Point(215, 288);
            this.StadiumUpgrades_Textbox.Name = "StadiumUpgrades_Textbox";
            this.StadiumUpgrades_Textbox.Size = new System.Drawing.Size(73, 20);
            this.StadiumUpgrades_Textbox.TabIndex = 34;
            // 
            // TVIncome_Textbox
            // 
            this.TVIncome_Textbox.Enabled = false;
            this.TVIncome_Textbox.Location = new System.Drawing.Point(215, 193);
            this.TVIncome_Textbox.Name = "TVIncome_Textbox";
            this.TVIncome_Textbox.Size = new System.Drawing.Size(73, 20);
            this.TVIncome_Textbox.TabIndex = 26;
            // 
            // StadiumUpgrades_Updown
            // 
            this.StadiumUpgrades_Updown.Location = new System.Drawing.Point(141, 288);
            this.StadiumUpgrades_Updown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.StadiumUpgrades_Updown.Name = "StadiumUpgrades_Updown";
            this.StadiumUpgrades_Updown.Size = new System.Drawing.Size(68, 20);
            this.StadiumUpgrades_Updown.TabIndex = 33;
            this.StadiumUpgrades_Updown.ValueChanged += new System.EventHandler(this.StadiumUpgrades_Updown_ValueChanged);
            // 
            // TVContract_Label
            // 
            this.TVContract_Label.AutoSize = true;
            this.TVContract_Label.Location = new System.Drawing.Point(73, 226);
            this.TVContract_Label.Name = "TVContract_Label";
            this.TVContract_Label.Size = new System.Drawing.Size(64, 13);
            this.TVContract_Label.TabIndex = 27;
            this.TVContract_Label.Text = "TV Contract";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Stadium Upgrades";
            // 
            // StadiumMaint_Updown
            // 
            this.StadiumMaint_Updown.Location = new System.Drawing.Point(141, 262);
            this.StadiumMaint_Updown.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.StadiumMaint_Updown.Name = "StadiumMaint_Updown";
            this.StadiumMaint_Updown.Size = new System.Drawing.Size(68, 20);
            this.StadiumMaint_Updown.TabIndex = 30;
            this.StadiumMaint_Updown.ValueChanged += new System.EventHandler(this.StadiumMaint_Updown_ValueChanged);
            // 
            // TVContract_Updown
            // 
            this.TVContract_Updown.Location = new System.Drawing.Point(141, 223);
            this.TVContract_Updown.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.TVContract_Updown.Name = "TVContract_Updown";
            this.TVContract_Updown.Size = new System.Drawing.Size(68, 20);
            this.TVContract_Updown.TabIndex = 28;
            this.TVContract_Updown.ValueChanged += new System.EventHandler(this.TVContract_Updown_ValueChanged);
            // 
            // StadiumMaint_Textbox
            // 
            this.StadiumMaint_Textbox.Enabled = false;
            this.StadiumMaint_Textbox.Location = new System.Drawing.Point(215, 262);
            this.StadiumMaint_Textbox.Name = "StadiumMaint_Textbox";
            this.StadiumMaint_Textbox.Size = new System.Drawing.Size(73, 20);
            this.StadiumMaint_Textbox.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Stadium Maintenance";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.DataGrid_WeeklyIncome);
            this.panel2.Location = new System.Drawing.Point(369, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 694);
            this.panel2.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 16);
            this.label4.TabIndex = 36;
            this.label4.Text = "Team Weekly Revenue";
            // 
            // DataGrid_WeeklyIncome
            // 
            this.DataGrid_WeeklyIncome.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGrid_WeeklyIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_WeeklyIncome.Location = new System.Drawing.Point(3, 27);
            this.DataGrid_WeeklyIncome.Name = "DataGrid_WeeklyIncome";
            this.DataGrid_WeeklyIncome.Size = new System.Drawing.Size(307, 550);
            this.DataGrid_WeeklyIncome.TabIndex = 35;
            this.DataGrid_WeeklyIncome.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_WeeklyIncome_CellValueChanged);
            // 
            // OwnerFinances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "OwnerFinances";
            this.Size = new System.Drawing.Size(1004, 700);
            ((System.ComponentModel.ISupportInitialize)(this.LeagueRevenue_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVIncome_Updown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StadiumUpgrades_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StadiumMaint_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVContract_Updown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_WeeklyIncome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TeamComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown LeagueRevenue_Updown;
        private System.Windows.Forms.NumericUpDown TVIncome_Updown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox LeagueRevenue_Textbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TVIncome_Textbox;
        private System.Windows.Forms.NumericUpDown TVContract_Updown;
        private System.Windows.Forms.Label TVContract_Label;
        private System.Windows.Forms.TextBox StadiumMaint_Textbox;
        private System.Windows.Forms.NumericUpDown StadiumMaint_Updown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StadiumUpgrades_Textbox;
        private System.Windows.Forms.NumericUpDown StadiumUpgrades_Updown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DataGrid_WeeklyIncome;
        private System.Windows.Forms.Label Finances_Label;
        private System.Windows.Forms.Label label4;
    }
}