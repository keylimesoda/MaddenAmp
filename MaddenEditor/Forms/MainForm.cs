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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * colin.goudie@gmail.com
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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class MainForm : Form
    {
		private EditorModel model = null;
		private string fileToLoad;
		private bool isInitialising = false;
		
		private SearchForm searchPlayerForm = null;
		private SearchForm searchCoachForm = null;

		private PlayerEditControl playerEditControl = null;
		private CoachEditControl coachEditControl = null;
		private DepthChartEditorControl depthChartEditorControl = null;

        public MainForm()
        {
            InitializeComponent();

			rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);

			this.Text = this.Text + " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			playerEditControl = new PlayerEditControl();
			coachEditControl = new CoachEditControl();
			depthChartEditorControl = new DepthChartEditorControl();

			playerPage.Controls.Add(playerEditControl);
			coachPage.Controls.Add(coachEditControl);
			depthChartPage.Controls.Add(depthChartEditorControl);
			playerEditControl.Dock = DockStyle.Fill;
			coachEditControl.Dock = DockStyle.Fill;
			depthChartEditorControl.Dock = DockStyle.Fill;
						
			tabControl.Visible = false;
			toolsToolStripMenuItem.Visible = false;
			franchiseToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;
			exportToolStripMenuItem.Enabled = false;

			isInitialising = false;
        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (CheckSave())
			{
				Application.Exit();
			}
        }

		private bool CheckSave()
		{
			if (model != null && model.Dirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to currently opened file?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					model.Save();
				}
				else if (result == DialogResult.Cancel)
				{
					return false;
				}
			}

			if (model != null)
			{
				model.Shutdown();
			}	
			model = null;

			return true;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!CheckSave())
			{
				return;
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "ros";
			dialog.Filter = "Madden files (*.ros;*.fra)|*.ros;*.fra";
			dialog.Multiselect = false;
			dialog.ShowDialog();
			if (dialog.FileNames.Length > 0)
			{
				foreach (string filename in dialog.FileNames)
				{
					fileToLoad = filename;
					// Insert code here to process the files.
					try
					{
						this.Cursor = Cursors.WaitCursor;

						CleanUI();
						statusStrip.Visible = true;

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
			model = new EditorModel(fileToLoad, this);
			//once the model is initialised set tell the custom edit
			//controls about it
			playerEditControl.Model = model;
			coachEditControl.Model = model;
			depthChartEditorControl.Model = model;
		}

		public void updateProgress(int percentage, string tablename)
		{
			rosterFileLoaderThread.ReportProgress(percentage, tablename);
		}

		private void InitialiseUI()
		{
			playerEditControl.InitialiseUI();
			coachEditControl.InitialiseUI();
			depthChartEditorControl.InitialiseUI();

			exportToolStripMenuItem.Enabled = true;
			tabControl.Visible = true;
			toolsToolStripMenuItem.Visible = true;
			processingTableLabel.Text = "";
			statusStrip.Visible = false;
			toolStripProgressBar.Value = 0;

			if (model.FileType == MaddenFileType.FranchiseFile)
			{
				franchiseToolStripMenuItem.Visible = true;
			}

			this.Cursor = Cursors.Default;
		}
		
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			isInitialising = true;
			rosterFileLoaderThread.Dispose();
			rosterFileLoaderThread = null;
			rosterFileLoaderThread = new BackgroundWorker();
			rosterFileLoaderThread.WorkerReportsProgress = true;
			rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);
			rosterFileLoaderThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.rosterFileLoaderThread_RunWorkerCompleted);
			rosterFileLoaderThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.rosterFileLoaderThread_ProgressChanged);
			

			if (CheckSave())
			{
				CleanUI();
			}
		}

		private void CleanUI()
		{
			//Now clean up ready for reloading
			searchPlayerForm = null;

			playerEditControl.CleanUI();
			coachEditControl.CleanUI();
			depthChartEditorControl.CleanUI();

			exportToolStripMenuItem.Enabled = false;
			tabControl.Visible = false;
			toolsToolStripMenuItem.Visible = false;
			processingTableLabel.Text = "";
			franchiseToolStripMenuItem.Visible = false;
		}

		private void rosterFileLoaderThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			isInitialising = true;

			try
			{
				InitialiseUI();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}
			
			isInitialising = false;
		}

		private void rosterFileLoaderThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			toolStripProgressBar.Value = e.ProgressPercentage;
			processingTableLabel.Text = "Loading Table: " + e.UserState.ToString();
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CheckSave();

			Application.Exit();
		}
		
		public bool Dirty
		{
			set
			{
				saveToolStripMenuItem.Enabled = value;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				model.Save();
				MessageBox.Show("File saved successfully!", "Save success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.ToString(), "Exception thrown while Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
						
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox form = new AboutBox();
			form.ShowDialog(this);
		}

		private void searchforCoachesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (searchCoachForm == null)
			{
				searchCoachForm = new SearchForm(model, SearchType.COACH);
			}
			searchCoachForm.ShowDialog(this);

			if (searchCoachForm.DialogResult == DialogResult.OK)
			{
				//Found a new coach to switch to
				//need to set current coach to this coach
				model.CoachModel.CurrentCoachRecord = (CoachRecord)searchCoachForm.SelectedSearchTarget;
				coachEditControl.LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
				//Switch tab page to correct page
				tabControl.SelectedIndex = 1;
			}
		}
		
		private void searchforPlayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (searchPlayerForm == null)
			{
				searchPlayerForm = new SearchForm(model, SearchType.PLAYER);
			}

			searchPlayerForm.ShowDialog(this);

			if (searchPlayerForm.DialogResult == DialogResult.OK)
			{
				//Found a new player to switch to
				//need to set current player to this player
				model.PlayerModel.CurrentPlayerRecord = (PlayerRecord)searchPlayerForm.SelectedSearchTarget;
				playerEditControl.LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
				//Switch tab page to correct page
				tabControl.SelectedIndex = 0;
			}
		}
		
		private void testButton_Click(object sender, EventArgs e)
		{
			if (testerWorkerThread.IsBusy)
			{
				testerWorkerThread.CancelAsync();
				
			}
			else
			{
				testerWorkerThread.DoWork += new DoWorkEventHandler(testerWorkerThread_DoWork);
				testerWorkerThread.RunWorkerAsync();
			}
		}

		private void testerWorkerThread_DoWork(object sender, DoWorkEventArgs e)
		{
            // This method will run on a thread other than the UI thread.
            // Be sure not to manipulate any Windows Forms controls created
            // on the UI thread from this method.

		}

		private void testerWorkerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			model.PlayerModel.GetNextPlayerRecord();
			playerEditControl.LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
		}

		private void editSalaryCapsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SalaryCapForm form = new SalaryCapForm(model);
			form.ShowDialog();
		}

		private void globalPlayerAttrEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GlobalAttributeForm form = new GlobalAttributeForm(model);
			form.InitialiseUI();
			form.Show();
		}

		private void setTeamCaptainsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TeamCaptainForm form = new TeamCaptainForm(model);
			form.InitialiseUI();

			form.ShowDialog(this);
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExportForm form = new ExportForm(model);
			form.InitialiseUI();

			form.ShowDialog(this);

			form.CleanUI();
			form = null;
		}

		


    }
}