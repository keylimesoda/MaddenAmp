/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net             colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Domain;

namespace MaddenEditor.Forms
{
    public partial class MainForm : Form
    {
		private RosterModel model = null;
		private string fileToLoad;
		private int playerRecNo = 0;

        public MainForm()
        {
            InitializeComponent();

			tabControl.Visible = false;
			searchToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;

        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			CheckSave();

			Application.Exit();
        }

		private void CheckSave()
		{
			if (model != null && model.Dirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to currently opened file?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{

				}
				else
				{

				}
			}

			if (model != null)
			{
				model.Shutdown();
			}	
			model = null;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckSave();

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "ros";
			dialog.Filter = "Madden Roster files (*.ros)|*.ros|Madden Franchise files (*.fra)|*.fra";
			dialog.Multiselect = false;
			dialog.ShowDialog();
			if (dialog.FileNames.Length > 0)
			{
				foreach (string filename in dialog.FileNames)
				{
					if (filename.Contains("fra"))
					{
						MessageBox.Show("This version currently does not support loading of Franchise files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					fileToLoad = filename;
					// Insert code here to process the files.
					try
					{
						statusStrip.Visible = true;
						this.Refresh();

						rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);
						rosterFileLoaderThread.RunWorkerAsync();
						
						break;
						//Now the model is opened.

					}
					catch (ApplicationException err)
					{
						MessageBox.Show(err.ToString(), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		void rosterFileLoaderThread_DoWork(object sender, DoWorkEventArgs e)
		{
			model = new RosterModel(fileToLoad, this);
		}

		public void updateProgress(int percentage)
		{
			rosterFileLoaderThread.ReportProgress(percentage);
		}

		private void InitialiseData()
		{
			foreach(TeamRecordCompact team in model.GetTeamNames())
			{
				teamComboBox.Items.Add(team);
				filterTeamComboBox.Items.Add(team);

				teamComboBox.Text = teamComboBox.Items[0].ToString();
				filterTeamComboBox.Text = filterTeamComboBox.Items[0].ToString();
			}

			foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
			{
				positionComboBox.Items.Add(pos);
			}

			LoadPlayerInfo(0);
		}

		private void LoadPlayerInfo(int recno)
		{
			PlayerRecord rec = model.GetPlayerRecord(recno);

			firstNameTextBox.Text = rec.FirstName;
			lastNameTextBox.Text = rec.LastName;
			foreach (TeamRecordCompact recordCompact in teamComboBox.Items)
			{
				if (rec.TeamId == recordCompact.id)
				{
					teamComboBox.Text = recordCompact.name;
				}
			}
			positionComboBox.Text = positionComboBox.Items[rec.PositionId].ToString();
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void rosterFileLoaderThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				InitialiseData();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}

			tabControl.Visible = true;
			searchToolStripMenuItem.Visible = true;
			statusStrip.Visible = false;
			toolStripProgressBar.Value = 0;
		}

		private void rosterFileLoaderThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			toolStripProgressBar.Value = e.ProgressPercentage;
		}

		private void rightButton_Click(object sender, EventArgs e)
		{
			playerRecNo++;
			LoadPlayerInfo(playerRecNo);
		}

		private void leftButton_Click(object sender, EventArgs e)
		{
			playerRecNo--;
			if (playerRecNo < 0)
				playerRecNo = 0;
			LoadPlayerInfo(playerRecNo);
		}

    }
}