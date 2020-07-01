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
using MaddenEditor.Core.DatEditor;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Manager;
using MaddenEditor.Db;

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
        private bool MadFB = false;        
		private EditorModel model = null;
        private EditorModel stream = null;
		private string filePathToLoad;
		private bool isInitialising = false;

		private SearchForm searchPlayerForm = null;
		private SearchForm searchCoachForm = null;

		private PlayerEditControl playerEditControl = null;
		private CoachEditControl coachEditControl = null;
		private TeamEditControl teamEditControl = null;
        private StadiumEditForm stadiumeditor = null;
        private City cityeditor = null;
        private OptionsForm options = null;
        private UserControlledTeamForm userconeditor = null;
        private OwnerFinances ownerfinances = null;

		private TabPage playerPage = null;
		private TabPage coachPage = null;
		private TabPage teamPage = null;
        private TabPage stadiumPage = null;
        private TabPage optionsPage = null;
        private TabPage citypage = null;
        private TabPage userconpage = null;
        private TabPage financepage = null;

		private delegate void saveMenuDelegate(bool enabled);        
        private MGMT _manager;        
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        
        // setting up classes to deal with frostbyte header and determine if db and bigendian
        public FB fb;
        public BEDB db;
        public bool bigendian = false;
        

        public MainForm()
		{
			InitializeComponent();

			rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);            

			this.Text = TITLE_STRING + " - v" + MaddenEditor.Core.Version.VersionString;

			tabControl.Visible = false;
			Tools.Visible = false;
			franchiseToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;
			exportToolStripMenuItem.Enabled = false;            

			isInitialising = false;

            manager = new MGMT();

            if (!manager.config.Read())
            {
                manager.config = new AmpConfig();
                manager.config.changed = true;
            }
            if (!manager.config.SkipSplash)
            {
                Revisions form = new Revisions();
                form.manager = manager;
                form.InitUI();
                form.Show();
                form.TopMost = true;
            }
		}

        // REDO this, set everything off and turn on as needed
        private void InitialiseUI()
        {   
            this.Text = TITLE_STRING + " - v" + MaddenEditor.Core.Version.VersionString + "  - " + System.IO.Path.GetFileName(filePathToLoad);

            exportToolStripMenuItem.Enabled = true;
            franchiseToolStripMenuItem.Visible = false;
            Tools.Visible = true;
            statusStrip.Visible = false;
            processingTableLabel.Text = "";            
            toolStripProgressBar.Value = 0;

            #region 04-08 Franchise
            if (model.FileType == MaddenFileType.Franchise)
            {
                franchiseToolStripMenuItem.Visible = true;
                setUserControlledTeamsToolStripMenuItem.Enabled = true;
                MaddenManager.Enabled = true;
                manager.LoadDATs();

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                {
                    //2004 version doesn't support Team Captain editing
                    setTeamCaptainsToolStripMenuItem.Enabled = false;
                }
                else setTeamCaptainsToolStripMenuItem.Enabled = true;

                if (model.MadVersion >= MaddenFileVersion.Ver2006)
                    setGameInjuriesToolStripMenuItem.Enabled = true;
                else setGameInjuriesToolStripMenuItem.Enabled = false;

                if (model.FranchiseStage.CurrentStage >= 14 && model.FranchiseStage.CurrentStage <= 23)
                {
                    importDraftClassToolStripMenuItem.Enabled = true;
                    draftMenuItem.Enabled = true;
                }
                else
                {
                    importDraftClassToolStripMenuItem.Enabled = false;
                    draftMenuItem.Enabled = false;
                }

                if (model.FranchiseStage.CurrentStage < 7)
                    editScheduleToolStripMenuItem.Enabled = false;
                else editScheduleToolStripMenuItem.Enabled = true;
            }
            #endregion

            else if (model.FileType == MaddenFileType.Unknown)
            {
                optionsToolStripMenuItem.Visible = false;
                playerEditingToolStripMenuItem.Visible = false;
                coachPlayerToolStripMenuItem.Visible = false;
                teamEditorToolStripMenuItem.Visible = false;
                StadiumToolStripMenuItem.Visible = false;
                cityToolStripMenuItem.Visible = false;
                searchforCoachesToolStripMenuItem.Visible = false;
                searchforPlayerToolStripMenuItem.Visible = false;
                depthChartEditorToolStripMenuItem.Visible = false;
                globalPlayerAttrEditorToolStripMenuItem.Visible = false;
            }
            else
            {
                //optionsToolStripMenuItem.Enabled = false;
                if (model.MadVersion >= MaddenFileVersion.Ver2019)
                {
                    
                    optionsToolStripMenuItem.Visible = false;
                    StadiumToolStripMenuItem.Visible = false;
                    cityToolStripMenuItem.Visible = false;
                    //depthChartEditorToolStripMenuItem.Visible = true;
                    coachPlayerToolStripMenuItem.Visible = false;
                    searchforCoachesToolStripMenuItem.Visible = false;
                    optionsToolStripMenuItem.Visible = false;

                    if (model.FileType == MaddenFileType.DBTeam)
                    {
                        coachPlayerToolStripMenuItem.Visible = true;
                        teamEditorToolStripMenuItem.Visible = true;
                    }
                    else if (model.FileType == MaddenFileType.UserConfig)
                    {
                        playerEditingToolStripMenuItem.Visible = false;
                        searchforPlayerToolStripMenuItem.Visible = false;
                        globalPlayerAttrEditorToolStripMenuItem.Visible = false;
                        teamEditorToolStripMenuItem.Visible = false;
                        optionsToolStripMenuItem.Visible = true;
                    }
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
            Tools.Visible = false;
            processingTableLabel.Text = "";
            franchiseToolStripMenuItem.Visible = false;
        }
        
        private void InitialisePlayerPage()
        {
            playerEditControl = new PlayerEditControl();
            playerPage = new TabPage("Player Editor");
            playerEditControl.manager = manager;
            playerEditControl.playeroverall.InitRatings(manager);
           
            tabControl.Visible = true;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Controls.Add(playerPage);
            playerPage.Controls.Add(playerEditControl);

            playerEditControl.Dock = DockStyle.None;

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
            coachEditControl.Manager = manager;
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

            teamEditControl.Dock = DockStyle.None;
            teamEditControl.Model = model;

            model.TeamModel.InitConfig(manager);            

            teamEditControl.InitialiseUI();
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
                       
        void rosterFileLoaderThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new EditorModel(filePathToLoad, this, bigendian, fb);                               
            }
            catch (ApplicationException err)
            {
                model = null;
                ExceptionDialog.Show(err);
            }

            if (model != null)
            {
                manager.model = model;
                manager.InitDB();                
                model.InitColleges(manager);
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
        		
		private void SetSaveMenuEnabled(bool enabled)
		{
			saveToolStripMenuItem.Enabled = enabled;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox form = new AboutBox();
			form.ShowDialog(this);
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExportForm form = new ExportForm(model);
            if (MadFB && model.FileType == MaddenFileType.Roster)
            {
                form.FB_Draft = new FB(fb, FBType.Draft, model.DraftClassModel.DraftClassVersion);
            }

			form.InitialiseUI();            
			form.ShowDialog(this);
			form.CleanUI();
			form = null;
		}
         
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (manager.config.changed)
                manager.config.Write();
            Close();
            if (CheckSave())
            {
                CloseModel();
                Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (manager.config.changed)
                manager.config.Write();

            if (CheckSave())
            {
                CloseModel();
                // Save the Madden 19 for frostbyte format
                if (MadFB)
                {
                    fb.Save();
                    fb.RemoveDB();
                }

                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }

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
         
        private bool CheckSave()
        {
            if (manager.stream_model != null && manager.stream_model.Dirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save " + manager.stream_model.FileType.ToString() + " changes ?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    manager.stream_model.Save();
            }
            
            if (manager.db_misc_model != null && manager.db_misc_model.Dirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save " + manager.db_misc_model.FileType.ToString() + " changes ?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    manager.db_misc_model.Save();
            }
            
            if (model != null && model.Dirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save " + model.FileType.ToString() + " changes ?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
            manager.stream_model = null;
            manager.db_misc_model = null;
        }
        
        
        #region Tools

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options = new OptionsForm();
            options.Model = model;
            options.manager = manager;
            optionsPage = new TabPage("Options");
            tabControl.Visible = true;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Controls.Add(optionsPage);
            optionsPage.Controls.Add(options);
            options.InitialiseUI();
            tabControl.SelectedTab = optionsPage;
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

        private void StadiumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stadiumeditor == null)
                InitializeStadiumPage();
            tabControl.SelectedTab = stadiumPage;
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cityeditor == null)
            {
                cityeditor = new City();
                citypage = new TabPage("City");
                tabControl.Visible = true;
                tabControl.Dock = DockStyle.Fill;
                tabControl.Controls.Add(citypage);
                citypage.Controls.Add(cityeditor);
                cityeditor.Dock = DockStyle.Fill;
                cityeditor.Model = model;
                cityeditor.InitialiseUI();
            }

            tabControl.SelectedTab = citypage;
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

        private void depthChartEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepthChartEditorForm form = new DepthChartEditorForm(model);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.InitialiseUI();

            form.Show(this);


        }

        private void globalPlayerAttrEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalAttributeForm form = new GlobalAttributeForm(model);
            form.InitialiseUI();
            form.Show();
        }

        private void ErrorCheckMenuItem_Click(object sender, EventArgs e)
        {
            model.CoachModel.CheckCoaches();
        }
        
        #endregion
        
        #region FileIO

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSave())
            {
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Title = "Madden 04-08/19-20 PC Roster or Madden 04-08 PC Franchise";
            //dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "Madden Database Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.ShowDialog();

            if (dialog.FileNames.Length > 0)
            {
                // If they saved above, then reverting does nothing.
                // If not, then reverting is what they wanted.

                CloseModel();

                string filename = dialog.FileNames[0];
                fb = new FB();
                db = new BEDB();

                fb.Extract(filename);
                if (fb.FileType != FBType.NA)
                {                    
                    if (fb.FileType == FBType.Franchise)
                    {
                        MessageBox.Show("Madden 19 Save Files Not Supported !", "Wrong Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (fb.FileType == FBType.Draft)
                    {
                        MessageBox.Show("Please Load a valid Madden 19/20 Roster First !", "Draft Class", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (fb.FileType == FBType.Roster)
                    {
                        MadFB = true;
                        filename = fb.database;
                    }
                    if (fb.FileVersion == FBVersion.Madden19)
                        manager.config.Madden19Serial = fb.Serial;
                    else if (fb.FileVersion == FBVersion.Madden20)
                        manager.config.Madden20Serial = fb.Serial;
                    manager.config.Madden19UserSettingsFilename = manager.UserSettings.ReadUserSettings(manager.config.Madden19UserSettingsFilename, fb.FileVersion);
                }
                else
                {
                    bigendian = db.Read(filename);
                    if (!db.isdb)
                    {                        
                        MessageBox.Show("Not a valid Madden Database !", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                
                filePathToLoad = filename;
                // Insert code here to process the files.
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    CleanUI();
                    statusStrip.Visible = true;
                    rosterFileLoaderThread.RunWorkerAsync();
                    //Now the model is opened.
                }
                catch (ApplicationException err)
                {
                    MessageBox.Show(err.ToString(), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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

            if (MadFB)
                fb.Save();
        }


        #endregion

        #region Franchise Options

        private void Manager_Click(object sender, EventArgs e)
        {            
            manager.model = model;
            ManagerMain form = new ManagerMain();                     
            form.manager = manager;
            form.Init();
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

        private void editFranchiseOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FranchiseOptionsForm form = new FranchiseOptionsForm(model);
            form.ShowDialog();
        }

        private void setTeamCaptainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeamCaptainForm form = new TeamCaptainForm(model);
            form.InitialiseUI();

            form.ShowDialog(this);
        }

        private void setUserControlledTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userconeditor == null)
            {
                userconeditor = new UserControlledTeamForm();
                userconpage = new TabPage("User Con");
                tabControl.Visible = true;
                tabControl.Dock = DockStyle.Fill;
                tabControl.Controls.Add(userconpage);
                userconpage.Controls.Add(userconeditor);
                userconeditor.Dock = DockStyle.None;
                userconeditor.Model = model;
                userconeditor.InitialiseUI();
            }

            tabControl.SelectedTab = userconpage;
        }

        private void setGameInjuriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameInjuryForm form = new GameInjuryForm(model);

            form.InitialiseUI();
            form.ShowDialog();

            form.CleanUI();
        }

        private void enterDraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!model.draftStarted)
            {
                manager.model = this.model;
                DraftConfigForm form = new DraftConfigForm(model);
                form.Show(this);
            }
            else
            {
                MessageBox.Show("You must reopen your file to restart the draft.");
            }
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

        private void clearRookieGamesPlayedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to clear all rookie games played data?  You should only do this during the offseason or at the beginning of the season.", "Confirm", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                DraftModel dm = new DraftModel(model);
                dm.ClearRookieGameRecords();
            }
        }

        private void weeklyMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeeklyMaintenanceForm wm = new WeeklyMaintenanceForm(model);
            wm.Show();
        }

        private void fixProgressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressionForm pf = new ProgressionForm(model);
            pf.Show();
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
        #endregion

        #region Help

        private void trainingCampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampOffSeason tcos = new TrainingCampOffSeason(model);
            tcos.initialiseUI();
            //  form.Show(this);
            tcos.Show();
        }

        private void developerBiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeveloperBioForm form = new DeveloperBioForm();
            form.Show();
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

        private void trainingCampFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingCampFAQ form = new TrainingCampFAQ(model);
            form.Show();

        }

        private void fAQInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAQ form = new FAQ();
            form.InitialiseUI();
            this.Controls.Add(form);
        }


        #endregion

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (optionsPage != null)
            {
                if (e.TabPage == optionsPage)
                {
                    tabControl.SelectedTab = optionsPage;
                    options.InitPlayerProgression();                
                    
                }
            }
        }

        private void FinancesMenu_Click(object sender, EventArgs e)
        {
            if (ownerfinances == null)
                InitializeFinancePage();
            tabControl.SelectedTab = financepage;
        }

        public void InitializeFinancePage()
        {
            ownerfinances = new OwnerFinances();
            financepage = new TabPage("Owner Finances");
            tabControl.Visible = true;
            tabControl.Dock = DockStyle.Fill;
            tabControl.Controls.Add(financepage);
            financepage.Controls.Add(ownerfinances);
            ownerfinances.Dock = DockStyle.Fill;
            ownerfinances.Model = model;
            ownerfinances.InitialiseUI();
        }

        private void exportDraftResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            List<PlayerRecord> playerList = new List<PlayerRecord>();

            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (record.Deleted)
                    continue;
                PlayerRecord playerRecord = (PlayerRecord)record;
                if (playerRecord.YearsPro != 0)
                    continue;
                playerList.Add(playerRecord);
            }

            //Bring up a save dialog
            SaveFileDialog fileDialog = new SaveFileDialog();
            Stream myStream = null;

            fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = fileDialog.OpenFile()) != null)
                    {
                        StreamWriter wText = new StreamWriter(myStream);

                        //Output the headers first
                        StringBuilder hbuilder = new StringBuilder();
                        hbuilder.Append("Pick,");
                        hbuilder.Append("Team,");
                        hbuilder.Append("From,");
                        hbuilder.Append("Position,");
                        hbuilder.Append("First Name,");
                        hbuilder.Append("Last Name,");
                        hbuilder.Append("College");
                        wText.WriteLine(hbuilder.ToString());

                        for (int p = 0; p < 224; p++)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append((p+1).ToString());
                            builder.Append(",");

                            foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                            {
                                DraftPickRecord dpr = (DraftPickRecord)rec;
                                if (dpr.PickNumber == p)
                                {
                                    builder.Append(model.TeamModel.GetTeamNameFromTeamId(dpr.CurrentTeamId));
                                    builder.Append(",");
                                    if (dpr.CurrentTeamId != dpr.OriginalTeamId)
                                    {
                                        builder.Append(model.TeamModel.GetTeamNameFromTeamId(dpr.OriginalTeamId));
                                        builder.Append(",");
                                    }
                                    else
                                    {
                                        builder.Append(" ");
                                        builder.Append(",");
                                    }
                                    
                                    break;
                                }
                            }                          

                            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                            {
                                if (rec.Deleted)
                                    continue;
                                PlayerRecord pr = (PlayerRecord)rec;
                                if (pr.YearsPro != 0 || pr.DraftRoundIndex > 32 || pr.DraftRound > 7)
                                    continue;

                                int pick = (pr.DraftRound - 1) * 32 + pr.DraftRoundIndex - 1;                               

                                if (pick == p)
                                {
                                    string pos = Enum.GetNames(typeof(MaddenPositions))[pr.PositionId].ToString();
                                    builder.Append(pos);
                                    builder.Append(",");
                                    builder.Append(pr.FirstName);
                                    builder.Append(",");
                                    builder.Append(pr.LastName);
                                    builder.Append(",");
                                    string college = model.Colleges[pr.CollegeId].name;
                                    builder.Append(college);
                                    break;
                                }
                            }

                            wText.WriteLine(builder.ToString());
                            wText.Flush();
                        }

                        myStream.Close();
                    }

                }
                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }       
               
        
    }
}