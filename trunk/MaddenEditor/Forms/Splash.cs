/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2016 Stingray68
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using System.IO;

namespace MaddenEditor.Forms
{
    public partial class Revisions : Form
    {
        private MGMT _manager;
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public Revisions()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            textBox1.Text = "This program is distributed in the hopes that it will be useful, but WITHOUT ANY WARRANTY" + Environment.NewLine;
            textBox1.Text += "By using this editor, users are responsible for understanding and adhering to any EULA that they agreed to." + Environment.NewLine;
            textBox1.Text += "ALWAYS backup your files BEFORE attempting to edit them!" + Environment.NewLine;
            textBox1.Text += "CLOSE MADDEN BEFORE EDITING WITH MADDEN AMP";
            
            TreeNode tn = new TreeNode();
            tn.Name = "Revision";
            tn.Text = "Revisions";
            versions.Nodes.Add(tn);
            versions.Nodes[0].Nodes.Add("v4.38");
            versions.Nodes[0].Nodes.Add("v4.37");
            versions.Nodes[0].Nodes.Add("v4.36");
            versions.Nodes[0].Nodes.Add("v4.35");
            versions.Nodes[0].Nodes.Add("v4.34");
            versions.Nodes[0].Nodes.Add("v4.33");
            versions.Nodes[0].Nodes.Add("v4.32");
            versions.Nodes[0].Nodes.Add("v4.31");
            versions.Nodes[0].Nodes.Add("v4.3");
            versions.Nodes[0].Nodes.Add("v4.2 Beta 2");
            versions.Nodes[0].Nodes.Add("v4.2 Beta 1");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 9");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 8");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 7");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 6");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 5");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 4");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 3");
            versions.Nodes[0].Nodes.Add("v4.1 Beta 2");
            versions.Nodes[0].Nodes.Add("v4.0 Beta 5");
            versions.Nodes[0].Nodes.Add("v4.0 Beta 4");

            versions.Nodes[0].Expand();
            versions.NodeMouseClick += versions_NodeMouseClick;
            versions.Visible = true;

            DisplayChangelog("MaddenEditor.Resources.v438.txt");
        }

        public void DisplayChangelog(string filename)
        {
            RevisionText.Clear();
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(filename));
            string result = reader.ReadToEnd();
            reader.Close();
            RevisionText.Text = result;
        }
        
        public void versions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            string file = "";
            if (e.Node.Text == "v4.0 Beta 4")
                file = "MaddenEditor.Resources.v4beta4.txt";
            else if (e.Node.Text == "v4.0 Beta 5")
                file = "MaddenEditor.Resources.v4beta5.txt";
            else if (e.Node.Text == "v4.1 Beta 2")
                file = "MaddenEditor.Resources.v41.txt";
            else if (e.Node.Text == "v4.1 Beta 3")
                file = "MaddenEditor.Resources.v413.txt";
            else if (e.Node.Text == "v4.1 Beta 4")
                file = "MaddenEditor.Resources.v414.txt";
            else if (e.Node.Text == "v4.1 Beta 5")
                file = "MaddenEditor.Resources.v415.txt";
            else if (e.Node.Text == "v4.1 Beta 6")
                file = "MaddenEditor.Resources.v416.txt";
            else if (e.Node.Text == "v4.1 Beta 7")
                file = "MaddenEditor.Resources.v417.txt";
            else if (e.Node.Text == "v4.1 Beta 8")
                file = "MaddenEditor.Resources.v418.txt";
            else if (e.Node.Text == "v4.1 Beta 9")
                file = "MaddenEditor.Resources.v419.txt";
            else if (e.Node.Text == "v4.2 Beta 1")
                file = "MaddenEditor.Resources.v421.txt";
            else if (e.Node.Text == "v4.3")
                file = "MaddenEditor.Resources.v430.txt";
            else if (e.Node.Text == "v4.31")
                file = "MaddenEditor.Resources.v431.txt";
            else if (e.Node.Text == "v4.32")
                file = "MaddenEditor.Resources.v432.txt";
            else if (e.Node.Text == "v4.33")
                file = "MaddenEditor.Resources.v433.txt";
            else if (e.Node.Text == "v4.34")
                file = "MaddenEditor.Resources.v434.txt";
            else if (e.Node.Text == "v4.35")
                file = "MaddenEditor.Resources.v435.txt";
            else if (e.Node.Text == "v4.36")
                file = "MaddenEditor.Resources.v436.txt";
            else if (e.Node.Text == "v4.37")
                file = "MaddenEditor.Resources.v437.txt";
            else if (e.Node.Text == "v4.38")
                file = "MaddenEditor.Resources.v438.txt";
            else return;

            DisplayChangelog(file);
        } 

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.footballidiot.com/forum/viewtopic.php?f=115&t=21075"); 
        }


    }
}
