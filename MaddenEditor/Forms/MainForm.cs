/******************************************************************************
 * MaddenAmp
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
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.IO;
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
		private const string TITLE_STRING = "Madden Amp";
		private EditorModel model = null;
		private string filePathToLoad;
		private bool isInitialising = false;

		private SearchForm searchPlayerForm = null;
		private SearchForm searchCoachForm = null;

		private PlayerEditControl playerEditControl = null;
		private CoachEditControl coachEditControl = null;
		private TeamEditControl teamEditControl = null;
        private StadiumEditForm stadiumeditor = null;

		private TabPage playerPage = null;
		private TabPage coachPage = null;
		private TabPage teamPage = null;
        private TabPage stadiumPage = null;

		private delegate void saveMenuDelegate(bool enabled);
        
		public MainForm()
		{
			InitializeComponent();

			rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);
            

			this.Text = TITLE_STRING + " - v" + MaddenEditor.Core.Version.VersionString;

			tabControl.Visible = false;
			ErrorCheck.Visible = false;
			franchiseToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;
			exportToolStripMenuItem.Enabled = false;            

			isInitialising = false;
		}

        private void InitialiseUI()
        {   
            this.Text = TITLE_STRING + " - v" + MaddenEditor.Core.Version.VersionString + "  - " + System.IO.Path.GetFileName(filePathToLoad);

            exportToolStripMenuItem.Enabled = true;
            ErrorCheck.Visible = true;
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
                    //setUserControlledTeamsToolStripMenuItem.Enabled = false;
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

        private void CleanUI()
        {
            this.Text = TITLE_STRING + " - v" + MaddenEditor.Core.Version.VersionString;
            //Now clean up ready for reloading
            searchPlayerForm = null;

            if (playerEditControl != null)
            {
                playerEditControl.CleanUI();
            }
            if (coachEditControl != null)
            {
                coachEditControl.CleanUI();
            }
            if (teamEditControl != null)
            {
                teamEditControl.CleanUI();
            }

            exportToolStripMenuItem.Enabled = false;
            tabControl.Visible = false;
            ErrorCheck.Visible = false;
            processingTableLabel.Text = "";
            franchiseToolStripMenuItem.Visible = false;
        }

        void rosterFileLoaderThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new EditorModel(filePathToLoad, this);                                
            }
            catch (ApplicationException err)
            {
                model = null;
                ExceptionDialog.Show(err);
            }            
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
			//if (CheckSave())
			//{
		//		CloseModel();
		//		Application.Exit();
		//	}
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

			return true;
		}

		private void CloseModel()
		{
			if (model != null)
			{
				model.Shutdown();
			}
			model = null;
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
                // If they saved above, then reverting does nothing.
                // If not, then reverting is what they wanted.

                CloseModel();

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

		

		public void updateProgress(int percentage, string tablename)
		{
			rosterFileLoaderThread.ReportProgress(percentage, tablename);
		}

		public void updateTableProgress(int percentage, string tablename)
		{
			ProgressChangedEventArgs e = new ProgressChangedEventArgs(percentage, tablename);

			BeginInvoke(new ProgressChangedEventHandler(rosterFileLoaderThread_ProgressChanged), new object[] { null, e });
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
				CloseModel();
				CleanUI();
			}
		}

		
		private void rosterFileLoaderThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (model == null)
			{
				this.Cursor = Cursors.Default;
				return;
			}
			isInitialising = true;

			try
			{
				InitialiseUI();
			}
			catch (Exception err)
			{
				Trace.WriteLine(err.ToString());
			}

			isInitialising = false;
		}

		private void rosterFileLoaderThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == 0)
			{
				statusStrip.Visible = true;
			}

			toolStripProgressBar.Value = e.ProgressPercentage;
			processingTableLabel.Text = "Loading Table: " + e.UserState.ToString();

			if (e.ProgressPercentage == 100)
			{
				statusStrip.Visible = false;
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CheckSave())
			{
				CloseModel();
				Application.Exit();
			}
			else
			{
				e.Cancel = true;
			}
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
			if (coachEditControl == null)
			{
				InitialiseCoachPage();
			}

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
				tabControl.SelectedTab = coachPage;
			}
		}

		private void searchforPlayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (playerEditControl == null)
			{
				InitialisePlayerPage();
			}

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
				tabControl.SelectedTab = playerPage;
			}
		}

		private void editFranchiseOptionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FranchiseOptionsForm form = new FranchiseOptionsForm(model);
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
				err = err;
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

			this.Invalidate(true);
			this.Update();
			Cursor.Current = Cursors.WaitCursor;

			List<int> toSkip = new List<int>();
			foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
			{
				if (team.UserControlled)
				{
					toSkip.Add(team.TeamId);
				}
			}

			dcr.ReorderDepthCharts(false, toSkip);
			Cursor.Current = Cursors.Arrow;
		}

		private void depthChartProgMenuItem_Click(object sender, EventArgs e)
		{
			DepthChartRepairer dcr = new DepthChartRepairer(model, null);

			this.Invalidate(true);
			this.Update();
			Cursor.Current = Cursors.WaitCursor;

			List<int> toSkip = new List<int>();
			foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
			{
				if (team.UserControlled)
				{
					toSkip.Add(team.TeamId);
				}
			}

			dcr.ReorderDepthCharts(true, toSkip);
			Cursor.Current = Cursors.Arrow;
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
			string classStats = dmTemp.AnalyzeDraftClass(false);

			
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

            DialogResult dr = MessageBox.Show(classStats, "Draft Class Diagnostics", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

		private void importDraftClassToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = "mdc";
			ofd.Filter = "Madden Draft Class (*.mdc)|*.mdc";
			ofd.Multiselect = false;
			ofd.ShowDialog();

			if (ofd.FileName.Length > 0)
			{
				DraftModel dmTemp = new DraftModel(model);
				string response = dmTemp.MDCVerify(ofd.FileName);

				if (response == null)
				{
					dmTemp.ImportRookies(ofd.FileName);
				}
				else
				{
					MessageBox.Show("Error reading file.  " + response, "Error");
				}
			}
		}

		private void simulateCPUMinicampsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            DialogResult dr = MessageBox.Show("This tool will simulate minicamps for CPU players with less than three years of experience.  Do you want to skip human controlled teams?", "Simulate CPU Minicamps", MessageBoxButtons.YesNoCancel);

            if (dr != DialogResult.Cancel)
            {
                this.Invalidate(true);
                this.Update();
                Cursor.Current = Cursors.WaitCursor;

                List<int> toSkip = new List<int>();
                foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
                {
                    if (team.UserControlled)
                    {
                        toSkip.Add(team.TeamId);
                    }
                }

                DepthChartRepairer dcr = new DepthChartRepairer(model, null);
                dcr.ReorderDepthCharts(true, toSkip);

                if (dr == DialogResult.No)
                {
                    toSkip = new List<int>();
                }

                foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                {
                    TeamRecord team = (TeamRecord)record;

                    if (team.TeamId < 32 && !toSkip.Contains(team.TeamId))
                    {
                        Trace.WriteLine(team.Name + ":");
                        team.SimulateMinicamp();
                        Trace.WriteLine("");
                    }
                }

                Cursor.Current = Cursors.Arrow;
            }
		}

        private void weeklyMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeeklyMaintenanceForm wm = new WeeklyMaintenanceForm(model);
            wm.Show();
        }

		private void trainingCampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampOffSeason tcos = new TrainingCampOffSeason(model);
            tcos.initialiseUI();
            //  form.Show(this);
            tcos.Show();
        }

        private void fixProgressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressionForm pf = new ProgressionForm(model);
            pf.Show();
        }

		private void developerBiosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeveloperBioForm form = new DeveloperBioForm();
			form.Show();
		}

        private void clearRookieGamesPlayedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to clear all rookie games played data?  You should only do this during the offseason or at the beginning of the season.", "Confirm", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                DraftModel dm = new DraftModel(model);
                dm.ClearRookieGameRecords();
            }
        }

        private void tunetxtModifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampTuneGUI tcos = new TrainingCampTuneGUI(model);
            //  form.Show(this);
            tcos.Show();
        }

        private void tunetxtGUIEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampTuneGUI tcos = new TrainingCampTuneGUI(model);
            tcos.Show();
        }

        private void offSeasonConditioningTrainingCampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampOffSeason tcos = new TrainingCampOffSeason(model);
            tcos.initialiseUI();
            //  form.Show(this);
            tcos.Show();
        }

        private void tUNEtxtGUIEditorforTrainingCampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampTuneGUI tcos = new TrainingCampTuneGUI(model);
            tcos.Show();
        }

		private void playerEditingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (playerEditControl == null)
			{
				InitialisePlayerPage();
			}
			tabControl.SelectedTab = playerPage;
		}

		private void coachPlayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (coachEditControl == null)
			{
				InitialiseCoachPage();

			}
			tabControl.SelectedTab = coachPage;
		}

		private void teamEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (teamEditControl == null)
			{
				InitialiseTeamPage();
			}
			tabControl.SelectedTab = teamPage;
		}

		private void InitialisePlayerPage()
		{
			playerEditControl = new PlayerEditControl();
			playerPage = new TabPage("Player Editor");

			tabControl.Visible = true;
			tabControl.Dock = DockStyle.Fill;
			tabControl.Controls.Add(playerPage);
			playerPage.Controls.Add(playerEditControl);

            playerEditControl.Dock = DockStyle.Fill;

			playerEditControl.Model = model;

			playerEditControl.InitialiseUI();
		}

		private void InitialiseCoachPage()
		{
			coachEditControl = new CoachEditControl();
			coachPage = new TabPage("Coach Editor");

			tabControl.Visible = true;
			tabControl.Dock = DockStyle.Fill;
			tabControl.Controls.Add(coachPage);
			coachPage.Controls.Add(coachEditControl);

			coachEditControl.Dock = DockStyle.Fill;

			coachEditControl.Model = model;

			coachEditControl.InitialiseUI();
		}

		private void InitialiseTeamPage()
		{
			teamEditControl = new TeamEditControl();
			teamPage = new TabPage("Team Editor");

			tabControl.Visible = true;
			tabControl.Dock = DockStyle.Fill;
			tabControl.Controls.Add(teamPage);
			teamPage.Controls.Add(teamEditControl);

			teamEditControl.Dock = DockStyle.Fill;

			teamEditControl.Model = model;

			teamEditControl.InitialiseUI();
		}

        private void trainingCampFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampFAQ form = new TrainingCampFAQ(model);
            form.Show();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
        
        private void Manager_Click(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void StadiumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stadiumeditor == null)
                InitializeStadiumPage();
            tabControl.SelectedTab = stadiumPage;
        }

        private void InitializeStadiumPage()
        {
            stadiumeditor = new StadiumEditForm();
            stadiumPage = new TabPage("Stadium Editor");
            tabControl.Visible = true;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Controls.Add(stadiumPage);
            stadiumPage.Controls.Add(stadiumeditor);
            stadiumeditor.Dock = DockStyle.Fill;
            stadiumeditor.Model = model;
            stadiumeditor.InitialiseUI();
        }

        private void ErrorCheckMenuItem_Click(object sender, EventArgs e)
        {
            model.CoachModel.CheckCoaches();
        }
       
        

       

        
        
    }
}