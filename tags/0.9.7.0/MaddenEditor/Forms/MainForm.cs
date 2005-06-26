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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	/// <summary>
	/// This is the main form that is displayed to the user. It contains the tab control
	/// which contains most of the other user controls as well as the menu bar enabling
	/// access into special functions.
	/// </summary>
	/// <author>Colin Goudie</author>
    public partial class MainForm : Form
    {
		private const string TITLE_STRING = "Madden Editor 2005";
		private EditorModel model = null;	
		private string filePathToLoad;
		private bool isInitialising = false;
		
		private SearchForm searchPlayerForm = null;
		private SearchForm searchCoachForm = null;

		private PlayerEditControl playerEditControl = null;
		private CoachEditControl coachEditControl = null;
		private TeamEditControl teamEditControl = null;
		
		/// <summary>
		/// Constructor for the MainForm
		/// </summary>
        public MainForm()
        {
            InitializeComponent();

			rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);


			this.Text = TITLE_STRING + " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			playerEditControl = new PlayerEditControl();
			coachEditControl = new CoachEditControl();
			teamEditControl = new TeamEditControl();
			
			playerPage.Controls.Add(playerEditControl);
			coachPage.Controls.Add(coachEditControl);
			teamPage.Controls.Add(teamEditControl);
			
			playerEditControl.Dock = DockStyle.Fill;
			coachEditControl.Dock = DockStyle.Fill;
			teamEditControl.Dock = DockStyle.Fill;
									
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

		/// <summary>
		/// Checks to see if our model is dirty and if so prompts the user to save the file.
		/// </summary>
		/// <returns>true - If user selected to save or not to save the file
		///          false - If the user cancels the close/save request</returns>
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
					filePathToLoad = filename;
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
			model = new EditorModel(filePathToLoad, this);
			//once the model is initialised set tell the custom edit
			//controls about it
			playerEditControl.Model = model;
			coachEditControl.Model = model;
			teamEditControl.Model = model;
			
		}

		public void updateProgress(int percentage, string tablename)
		{
			rosterFileLoaderThread.ReportProgress(percentage, tablename);
		}

		private void InitialiseUI()
		{
			this.Text = TITLE_STRING + " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "  - " + System.IO.Path.GetFileName(filePathToLoad);
						
			playerEditControl.InitialiseUI();
			coachEditControl.InitialiseUI();
			teamEditControl.InitialiseUI();
			
			exportToolStripMenuItem.Enabled = true;
			tabControl.Visible = true;
			toolsToolStripMenuItem.Visible = true;
			processingTableLabel.Text = "";
			statusStrip.Visible = false;
			toolStripProgressBar.Value = 0;

			if (model.FileType == MaddenFileType.FranchiseFile)
			{
				franchiseToolStripMenuItem.Visible = true;
				if (model.FileVersion == MaddenFileVersion.Ver2004)
				{
					//2004 version don't support Team Captain editing
					setTeamCaptainsToolStripMenuItem.Enabled = false;
					//2004 version has issues at the moment with changing user controlled teams
					setUserControlledTeamsToolStripMenuItem.Enabled = false;
				}
				else
				{
					setTeamCaptainsToolStripMenuItem.Enabled = true;
					setUserControlledTeamsToolStripMenuItem.Enabled = true;
				}
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
			this.Text = TITLE_STRING + " - v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			//Now clean up ready for reloading
			searchPlayerForm = null;

			playerEditControl.CleanUI();
			coachEditControl.CleanUI();
			teamEditControl.CleanUI();
			
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
				this.Cursor = Cursors.WaitCursor;
				model.Save();
				MessageBox.Show("File saved successfully!", "Save success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.ToString(), "Exception thrown while Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.Cursor = Cursors.Default;
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

		private void setUserControlledTeamsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UserControlledTeamForm form = new UserControlledTeamForm(model);

			form.InitialiseUI();

			form.ShowDialog(this);

			form.CleanUI();
			form = null;
		}

		private void depthChartEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DepthChartEditorForm form = new DepthChartEditorForm(model);

			form.InitialiseUI();

			form.Show(this);


		}

		private void editScheduleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ScheduleEditingForm form = new ScheduleEditingForm(model);

				form.InitialiseUI();
				form.Show(this);

				form.CleanUI();
			}
			catch (ArgumentException err)
			{
				MessageBox.Show("The Schedule in this franchise file cannot be loaded for editing\r\nReport this to " + EditorModel.SUPPORT_EMAIL, "Error loading schedule", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}			
		}

    }
}