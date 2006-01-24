namespace MaddenEditor.Forms
{
    partial class TrainingCampSplashScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingCampSplashScreen));
            this.Splashtxt = new System.Windows.Forms.TextBox();
            this.m_fadeInOutTimer = new System.Windows.Forms.Timer(this.components);
            this.m_SplashTextTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Splashtxt
            // 
            this.Splashtxt.BackColor = System.Drawing.Color.Black;
            this.Splashtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Splashtxt.ForeColor = System.Drawing.Color.White;
            this.Splashtxt.Location = new System.Drawing.Point(2, 3);
            this.Splashtxt.Multiline = true;
            this.Splashtxt.Name = "Splashtxt";
            this.Splashtxt.ReadOnly = true;
            this.Splashtxt.Size = new System.Drawing.Size(1034, 667);
            this.Splashtxt.TabIndex = 0;
            // 
            // m_fadeInOutTimer
            // 
            this.m_fadeInOutTimer.Interval = 10;
            this.m_fadeInOutTimer.Tick += new System.EventHandler(this.m_fadeInOutTimer_Tick);
            // 
            // m_SplashTextTimer
            // 
            this.m_SplashTextTimer.Interval = 150;
            this.m_SplashTextTimer.Tick += new System.EventHandler(this.m_SplashTextTimer_Tick);
            // 
            // TrainingCampSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1038, 673);
            this.Controls.Add(this.Splashtxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrainingCampSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Training Camp...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TrainingCampSplashScreen_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Splashtxt;
        private System.Windows.Forms.Timer m_fadeInOutTimer;
        private System.Windows.Forms.Timer m_SplashTextTimer;
    }
}