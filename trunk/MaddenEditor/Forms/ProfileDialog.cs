using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
    public partial class ProfileDialog : Form
    {
        public string profile;

        public ProfileDialog(List<string> profiles)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(profiles.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            profile = (string)comboBox1.Text;
            this.Close();
        }
    }
}