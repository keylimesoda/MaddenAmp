/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;

namespace MaddenEditor.Forms
{
    public partial class DraftConfigForm : Form
    {
        EditorModel model;

        public DraftConfigForm(EditorModel em)
        {
            model = em;
            InitializeComponent();
        }

        private void DraftConfigForm_Load(object sender, EventArgs e)
        {
            List<string> teamNames = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                teamNames.Add(model.TeamModel.GetTeamRecord(i).Name);
            }
            teamNames.Sort();
            teamChooser.Items.AddRange(teamNames.ToArray());
            seconds.Value = 0;
            minutes.Value = 5;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int secs = (int)minutes.Value * 60 + (int)seconds.Value;
            int humanId = model.TeamModel.GetTeamIdFromTeamName((string)teamChooser.SelectedItem);
            ScoutingForm form = new ScoutingForm(model, humanId, secs);
            form.Show();
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}