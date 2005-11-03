/******************************************************************************
 * Gommo's Madden Editor
 * Copyright (C) 2005 Colin Goudie
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
using System.Text;
// MADDEN DRAFT EDIT
using System.IO;
// MADDEN DRAFT EDIT
using System.Windows.Forms;
//using MaddenEditor.ConSole;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	/// <summary>
	/// This is the main scoutingForm that is displayed to the user. It contains the tab control
	/// which contains most of the other user controls as well as the menu bar enabling
	/// access into special functions.
	/// </summary>
	/// <author>Colin Goudie</author>
	public partial class MainForm : Form
	{
		private const string TITLE_STRING = "Gommo's Madden Editor";
		private EditorModel model = null;
		private string filePathToLoad;
		private bool isInitialising = false;

		private SearchForm searchPlayerForm = null;
		private SearchForm searchCoachForm = null;

		private PlayerEditControl playerEditControl = null;
		private CoachEditControl coachEditControl = null;
		private TeamEditControl teamEditControl = null;

		private delegate void saveMenuDelegate(bool enabled);

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
				setGameInjuriesToolStripMenuItem.Enabled = false;
				if (model.FileVersion == MaddenFileVersion.Ver2004)
				{
					//2004 version don't support Team Captain editing
					setTeamCaptainsToolStripMenuItem.Enabled = false;
					//2004 version has issues at the moment with changing user controlled teams
					setUserControlledTeamsToolStripMenuItem.Enabled = false;
				}
				if (model.FileVersion >= MaddenFileVersion.Ver2005)
				{
					setTeamCaptainsToolStripMenuItem.Enabled = true;
					setUserControlledTeamsToolStripMenuItem.Enabled = true;
				}
				if (model.FileVersion >= MaddenFileVersion.Ver2006)
				{
					setGameInjuriesToolStripMenuItem.Enabled = true;
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
				
				if (InvokeRequired)
				{
					object[] args = { value };
					Invoke(new saveMenuDelegate(SetSaveMenuEnabled), args);
				}
				else
				{
					saveToolStripMenuItem.Enabled = value;
				}
			}
		}

		private void SetSaveMenuEnabled(bool enabled)
		{
			saveToolStripMenuItem.Enabled = enabled;
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

		private void setGameInjuriesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GameInjuryForm form = new GameInjuryForm(model);

			form.InitialiseUI();
			form.ShowDialog();

			form.CleanUI();
		}
		// MADDEN DRAFT EDIT
		private void enterDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!model.draftStarted)
			{
				DraftConfigForm form = new DraftConfigForm(model);
				form.Show(this);
			}
			else
			{
				MessageBox.Show("You must reopen your file to restart the draft.");
			}
		}

		private void depthChartMenuItem_Click(object sender, EventArgs e)
		{
			DepthChartRepairer dcr = new DepthChartRepairer(model, null);
			//DialogResult dr = MessageBox.Show("This will save any changes you made to your file.  Proceed?", "Continue", MessageBoxButtons.YesNo);

//			if (dr == DialogResult.Yes)
//			{
				this.Invalidate(true);
				this.Update();
				Cursor.Current = Cursors.WaitCursor;
				dcr.ReorderDepthCharts(false);
				Cursor.Current = Cursors.Arrow;
//			}
		}

		private void depthChartProgMenuItem_Click(object sender, EventArgs e)
		{
			DepthChartRepairer dcr = new DepthChartRepairer(model, null);
//			DialogResult dr = MessageBox.Show("This will save any changes you made to your file.  Proceed?", "Continue", MessageBoxButtons.YesNo);

//			if (dr == DialogResult.Yes)
//			{
				this.Invalidate(true);
				this.Update();
				Cursor.Current = Cursors.WaitCursor;
				dcr.ReorderDepthCharts(true);
				Cursor.Current = Cursors.Arrow;
//			}
		}

		private void moveTradedDraftPicksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Dictionary<int, Dictionary<int, int>> tradedPicks = new Dictionary<int, Dictionary<int, int>>();

			for (int i = 0; i < 7; i++)
			{
				tradedPicks[i] = new Dictionary<int, int>();
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "dpd";
			dialog.Filter = "Draft Pick Data (*.dpd)|*.dpd";
			dialog.Multiselect = false;
			dialog.ShowDialog();

			if (dialog.FileNames.Length > 0)
			{
				FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
				StreamReader sr = new StreamReader(fs);

				string all = sr.ReadToEnd();

				string[] lines = all.Split('\n');

				for (int i = 0; i < lines.Length; i++)
				{
					if (lines[i].Length == 0)
					{
						break;
					}

					lines[i] = lines[i].Trim();
					string[] nums = lines[i].Split(' ');

					int round;
					int fromTeam;
					int toTeam;

					try
					{
						round = Int32.Parse(nums[0]);
						fromTeam = Int32.Parse(nums[1]);
						toTeam = Int32.Parse(nums[2]);
					}
					catch
					{
						MessageBox.Show("Error processing file.  Incorrect format.");
						fs.Close();
						sr.Close();
						return;
					}

					if (!(round > 0 && round <= 7 && fromTeam >= 0 && fromTeam < 32 && toTeam >= 0 && toTeam < 32))
					{
						MessageBox.Show("Error processing file.  Incorrect ranges.");
					}

					tradedPicks[round - 1][fromTeam] = toTeam;
				}

				fs.Close();
				sr.Close();

				foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
				{
					DraftPickRecord dpr = (DraftPickRecord)rec;

					int round = dpr.PickNumber / 32;
					if (tradedPicks[round].ContainsKey(dpr.OriginalTeamId))
					{
						dpr.CurrentTeamId = tradedPicks[round][dpr.OriginalTeamId];
					}
				}

				CheckSave();
			}
		}

        private void exportDraftClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Count the number of rookies in the class.  Insist on 257 rookies,
            // since that's the number of players in a Madden draft class, and we're
            // just overwriting those players.  Plus, any more might disturb the overall
            // pool of players in the long term.  This is enough for all drafted and the main
            // undrafted rookies anyway.

			DraftModel dmTemp = new DraftModel(model);
			string classStats = dmTemp.AnalyzeDraftClass();

            if (dmTemp.NumRooks > 257)
            {
                MessageBox.Show("Rookie class has more than 257 players.  You must delete some rookies to export this class.");
                return;
            }
            else if (dmTemp.NumRooks < 257)
            {
                MessageBox.Show("Rookie class has less than 257 players.  You must create more rookies to export this class.");
                return;
            }

            DialogResult dr = MessageBox.Show(classStats, "Export This Class?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.OverwritePrompt = true;
                sfd.DefaultExt = "mdc";
                sfd.AddExtension = true;
                sfd.Filter = "Madden Draft Classes (*.mdc)|*.mdc";

                sfd.ShowDialog();

                if (!sfd.FileName.Equals(""))
                {
                    model.PlayerModel.ExportDraftClass(sfd.FileName);
                }
            }
        }

		// MADDEN DRAFT EDIT

	}
}