/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
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
using System.IO;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;

namespace MaddenEditor.Forms
{
    public partial class DraftConfigForm : Form
    {
        EditorModel model;
		ScoutingForm scoutingForm;
		DraftModel draftModel;
		int secs;
		int humanId;
        string customclass = null;

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
			startButton.Enabled = false;
			this.Cursor = Cursors.WaitCursor;

			secs = (int)minutes.Value * 60 + (int)seconds.Value;
			humanId = model.TeamModel.GetTeamIdFromTeamName((string)teamChooser.SelectedItem);
			
			progressBar.Visible = true;

            if (draftClass.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "mdc";
                ofd.Filter = "Madden Draft Class (*.mdc)|*.mdc";
                ofd.Multiselect = false;
                ofd.ShowDialog();

                if (ofd.FileName.Length > 0)
                {
                    customclass = ofd.FileName;
                }
            }

			backgroundWorker.RunWorkerAsync();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void teamChooser_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!teamChooser.SelectedItem.ToString().Equals(""))
			{
				startButton.Enabled = true;
			}
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
            // This method will run on a thread other than the UI thread.
            // Be sure not to manipulate any Windows Forms controls created
            // on the UI thread from this method.
			if (autoSave.Checked)
			{
				string fileName = model.GetFileName();
				string backupFile = fileName.Substring(0, fileName.LastIndexOf('.')) + "-mfebak";
				string newFile;

				if (overwrite.Checked)
				{
					File.Delete(backupFile + "0.fra");
				}

				int index = 0;
				while (true)
				{
					newFile = backupFile + index + ".fra";
					if (!File.Exists(newFile))
					{
						break;
					}
					index++;
				}

				File.Copy(fileName, newFile);
			}

			draftModel = new DraftModel(model);

			ReportProgress(15);
			draftModel.InitializeDraft(humanId, this, customclass);
			ReportProgress(55);
			draftModel.FixDraftOrder();
			ReportProgress(75);
			draftModel.InitializeScouting();

			ReportProgress(100);
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			scoutingForm = new ScoutingForm(model, humanId, secs, draftModel, true);
			scoutingForm.Show();

			this.Cursor = Cursors.Default;
			this.Close();
		}

		public void ReportProgress(int percentage)
		{
			backgroundWorker.ReportProgress(percentage);
		}
    }
}