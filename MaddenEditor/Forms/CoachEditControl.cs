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
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.DatEditor;

namespace MaddenEditor.Forms
{
    //  Fix changing coach's team
    
    public partial class CoachEditControl : UserControl, IEditorForm
    {
        private bool isInitialising = false;

        private NumericUpDown[] prioritySliders = null;
        private NumericUpDown[] priorityTypeSliders = null;
        private Label[] priorityDescriptionLabels = null;
        private EditorModel model = null;
        private CoachRecord lastLoadedRecord = null;
        private int currentcoachrow = 0;

        private AmpConfig _config;
        public AmpConfig config
        {
            get { return _config; }
            set { _config = value; }
        }
        private MGMT _manager;
        public MGMT Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        
        public CoachEditControl()
        {
            isInitialising = true;
            InitializeComponent();
            isInitialising = false;
        }

        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        //  fix coach skin color
        public void InitialiseUI()
        {
            isInitialising = true;

            LegacyRatings_Panel.Visible = false;
            coachCurrentSeason_GroupBox.Visible = false;
            coachPostSeason_Groupbox.Visible = false;
            
            //  TO DO:  Coach skin color values change from 04/05 to 06-08
            //  04/05 vary from 0 to 7 and  06-08 vary from 0 to 2
            foreach (GenericRecord rec in model.CoachModel.CoachSkinColor)
                cbSkinColor.Items.Add(rec);

            filterTeamComboBox.Items.Add("ALL");
            if (Manager.stream_model != null)
            {
                model.CoachModel.manager = this.Manager;
                filterTeamComboBox.Items.Add("Unemployed");
            }

            foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                // Don't add NFC/AFC Pro Bowl values here, keep free agents and use the table from streameddata if possible                
                if (rec.Deleted)
                    continue;
                TeamRecord team = (TeamRecord)rec;
                
                if (team.TeamId != 1010 && team.TeamId != 1011)
                {
                    CoachTeamCombo.Items.Add(rec);
                    coachPreviousTeam.Items.Add(rec);
                    filterTeamComboBox.Items.Add(rec);
                }
            }

            filterPositionComboBox.SelectedIndex = 0;
            filterTeamComboBox.SelectedIndex = 0;

            foreach (GenericRecord rec in model.TeamModel.DefensivePlaybookList)
            {
                coachDefensivePlaybook.Items.Add(rec);
            }
            coachDefensivePlaybook.SelectedIndex = 0;

            foreach (GenericRecord rec in model.TeamModel.OffensivePlaybookList)
            {
                coachOffensivePlaybook.Items.Add(rec);
            }
            coachOffensivePlaybook.SelectedIndex = 0;


            //Create priority controls
            int numPositions = Enum.GetNames(typeof(PlayerDraftedPositions)).Length;
            prioritySliders = new NumericUpDown[numPositions];
            priorityTypeSliders = new NumericUpDown[numPositions];
            priorityDescriptionLabels = new Label[numPositions];
            for (int i = 0; i < numPositions; i++)
            {
                prioritySliders[i] = new NumericUpDown();
                prioritySliders[i].Location = new Point(48, 22 + i * 26);
                prioritySliders[i].Size = new Size(86, 20);
                prioritySliders[i].Minimum = 0;
                prioritySliders[i].Maximum = 100;
                prioritySliders[i].Visible = true;
                prioritySliders[i].ValueChanged += new EventHandler(prioritySlider_ValueChanged);

                priorityGroupBox.Controls.Add(prioritySliders[i]);

                priorityTypeSliders[i] = new NumericUpDown();
                priorityTypeSliders[i].Location = new Point(140, 22 + i * 26);
                priorityTypeSliders[i].Size = new Size(56, 20);
                priorityTypeSliders[i].Minimum = 0;
                priorityTypeSliders[i].Maximum = 2;
                priorityTypeSliders[i].Visible = true;
                priorityTypeSliders[i].ValueChanged += new EventHandler(priorityTypeSlider_ValueChanged);

                priorityGroupBox.Controls.Add(priorityTypeSliders[i]);

                priorityDescriptionLabels[i] = new Label();
                priorityDescriptionLabels[i].Location = new Point(205, 25 + i * 26);
                priorityDescriptionLabels[i].Visible = true;

                priorityGroupBox.Controls.Add(priorityDescriptionLabels[i]);
            }

            if (Manager.CoachPortDAT.isterf)
            {
                ImportCoachPic_Button.Visible = true;
                ExportCoachPic_Button.Visible = true;
            }
            else
            {
                ImportCoachPic_Button.Visible = false;
                ExportCoachPic_Button.Visible = false;
            }

            InitCoachList();

            if (model.MadVersion == MaddenFileVersion.Ver2019)
            {
                coachName.MaxLength = 17;
                coachAsset.Enabled = true;
                Approval_Label.Visible = false;
                Approval.Visible = false;
                coachFirstName.Enabled = true;
                coachLastName.Enabled = true;
                CoachFaceID_UpDown.Value = 0;
                CoachFaceID_UpDown.Enabled = false;
                CoachHeadID_UpDown.Value = 0;
                CoachHeadID_UpDown.Enabled = false;
                coachPreviousTeam.Enabled = false;
            }
            else
            {
                coachName.MaxLength = 16;
                coachAsset.Enabled = false;
                coachAsset.Text = "";
                LegacyRatings_Panel.Visible = true;
                coachCurrentSeason_GroupBox.Visible = true;
                coachPostSeason_Groupbox.Visible = true;
                coachFirstName.Enabled = false;
                coachLastName.Enabled = false;
                coachAsset.Text = "";
                coachAsset.Enabled = false;
            }

            isInitialising = false;
        }

        public void CleanUI()
        {
            CoachTeamCombo.Items.Clear();
            filterTeamComboBox.Items.Clear();
        }

        #endregion

        
        
        
        public void LoadCoachInfo(CoachRecord record)
        {
            model.CoachModel.CurrentCoachRecord = record;
            
            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            isInitialising = true;

            try
            {
                //Load Coach General info
                coachName.Text = record.Name;

                // TO FIX not working right for 2007
                // Shows Unemployed coaches and lists Positions as Head Coaches etc...
                // While team field is blank.  1009 = unemployed.
                coachesPositionCombo.Text = coachesPositionCombo.Items[record.Position].ToString(); ;
                

                TeamRecord team = model.TeamModel.GetTeamRecord(record.TeamId);
                TeamRecord prvteam = model.TeamModel.GetTeamRecord(record.LastTeamFranchise);
                if (record.TeamId == 1023)      // have to fix this
                    record.TeamId = 1009;
                if (record.TeamId == 1009)
                {
                    CoachTeamCombo.Text = "Free Agents";
                    coachPreviousTeam.Text = "Free Agents";
                }
                else
                {
                    CoachTeamCombo.SelectedItem = (object)team;
                    coachPreviousTeam.SelectedItem = (object)prvteam;
                }

                coachAge.Value = (int)record.Age;

                // this isnt right for all versions
                foreach (Object obj in model.CoachModel.CoachSkinColor)
                {
                    if (((GenericRecord)obj).Id == record.SkinColor)
                    {
                        cbSkinColor.SelectedItem = obj;
                        break;
                    }
                }

                GetCoachHeight();

                WearsGlassesCheckbox.Checked = record.CoachGlasses;
                FormerPlayer_Checkbox.Checked = record.WasPlayer;
                coachpic.Value = (int)record.Coachpic;
                CoachFaceID_UpDown.Value = (int)record.FaceId;
                CoachHeadID_UpDown.Value = (int)record.HeadHair;
                coachSalary.Value = (decimal)((double)record.Salary / 100.0);
                coachyearsleft.Value = (int)record.ContractLength;
                Approval.Value = (int)record.ApprovalRating;
                BodySize.Value = (int)record.BodySize;

                CoachOff.Value = (int)record.Offense;
                CoachDef.Value = (int)record.DefenseRating;
                CoachSafetyUpDown.Value = (int)record.CoachS;
                coachQB.Value = (int)record.CoachQB;
                coachRB.Value = (int)record.CoachRB;
                coachWR.Value = (int)record.CoachWR;
                coachOL.Value = (int)record.CoachOL;
                coachDL.Value = (int)record.CoachDL;
                coachLB.Value = (int)record.CoachLB;
                coachDB.Value = (int)record.CoachDB;
                coachKS.Value = (int)record.CoachKS;
                coachPS.Value = (int)record.CoachPS;

                //Win-Loss Records
                coachPlayoffWins.Value = (int)record.PlayoffWins;
                coachPlayoffLoses.Value = (int)record.PlayoffLosses;
                coachSuperbowlWins.Value = (int)record.SuperBowlWins;
                coachSuperBowlLoses.Value = (int)record.SuperBowlLoses;
                coachWinningSeasons.Value = (int)record.WinningSeasons;
                coachCareerWins.Value = (int)record.CareerWins;
                coachCareerLoses.Value = (int)record.CareerLosses;
                coachCareerTies.Value = (int)record.CareerTies;
                PlayoffsMade.Value = (int)record.PlayoffsMade;
                coachSeasonWins.Value = (int)record.SeasonWins;
                coachSeasonLosses.Value = (int)record.SeasonLosses;
                coachSeasonTies.Value = (int)record.SeasonTies;

                threeFourButton.Checked = false;
                fourThreeButton.Checked = false;

                if (model.MadVersion != MaddenFileVersion.Ver2019)
                {
                    if (record.DefenseType == 95)
                        fourThreeButton.Checked = true;
                    else if (record.DefenseType == 5)
                        threeFourButton.Checked = true;
                    else
                    {
                        // have seen this get corrupted, change it to 4-3
                        record.DefenseType = 95;
                        fourThreeButton.Checked = true;
                    }
                }
                else
                {
                    threeFourButton.Enabled = false;
                    fourThreeButton.Enabled = false;
                }

                //Attributes
                coachEthics.Value = (int)record.Ethics;
                coachKnowledge.Value = (int)record.Knowledge;
                coachMotivation.Value = (int)record.Motivation;
                coachChemistry.Value = (int)record.Chemistry;
                coachPassOff.Value = (int)record.OffensiveStrategy;
                coachRunOff.Value = (int)(100 - record.OffensiveStrategy);
                coachPassDef.Value = (int)record.DefensiveStrategy;
                coachRunDef.Value = (int)(100 - record.DefensiveStrategy);
                rb2.Value = (int)(100 - record.RBCarryDist);
                rb1.Value = (int)(record.RBCarryDist);
                coachDefAggression.Value = record.DefensiveAggression;
                coachOffAggression.Value = record.OffensiveAggression;
                
                SetCoachTendency(record);

                coachDefensivePlaybook.Text = model.TeamModel.GetDEFPlaybook((int)record.DefensivePlaybook);
                coachOffensivePlaybook.Text = model.TeamModel.GetOFFPlaybook((int)record.OffensivePlaybook);

                // Set slider values to 0
                for (int i = 0; i < Enum.GetNames(typeof(PlayerDraftedPositions)).Length; i++)
                {
                    prioritySliders[i].Value = 0;
                    priorityTypeSliders[i].Value = 0;
                    priorityDescriptionLabels[i].Text = "";
                    prioritySliders[i].Enabled = false;
                    priorityTypeSliders[i].Enabled = false;
                }
                //Priorities (NOTE: Madden 2007-2008 don't have coach sliders)
                if (model.MadVersion >= MaddenFileVersion.Ver2007)
                {
                    if (tabcontrol.TabPages.Contains(tabPage2))
                        tabcontrol.TabPages.Remove(tabPage2);
                }
                else
                {
                    if (!tabcontrol.TabPages.Contains(tabPage2))
                    {
                        tabcontrol.TabPages.Add(tabPage2);
                    }

                    if (!record.wasinstreamed)
                    {
                        bool priorityMatches = false;
                        SortedList<int, CoachPrioritySliderRecord> priorites = null;
                        if (model.MadVersion <= MaddenFileVersion.Ver2006)
                        {
                            priorites = model.CoachModel.GetCurrentCoachSliders();
                            int priorityCount = Enum.GetNames(typeof(PlayerDraftedPositions)).Length;
                            priorityMatches = (priorityCount != priorites.Count);
                        }

                        if (!priorityMatches)
                        {
                            int index = 0;
                            foreach (CoachPrioritySliderRecord priorRecord in priorites.Values)
                            {
                                prioritySliders[index].Value = priorRecord.Priority;
                                priorityTypeSliders[index].Value = priorRecord.PriorityType;
                                priorityDescriptionLabels[index].Text = DecodePriorityType((PlayerDraftedPositions)index, priorRecord.PriorityType);
                                prioritySliders[index].Enabled = true;
                                priorityTypeSliders[index].Enabled = true;
                                index++;
                            }
                        }
                    }
                }

                if (model.MadVersion == MaddenFileVersion.Ver2019)
                {
                    coachAsset.Text = record.Asset;
                    coachFirstName.Text = record.FirstName;
                    coachLastName.Text = record.LastName;
                }
            }

            catch (Exception e)
            {
                MessageBox.Show("Exception Occured loading this Coach:\r\n" + e.ToString(), "Exception Loading Coach", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadCoachInfo(lastLoadedRecord);
                return;
            }
            finally
            {
                isInitialising = false;
            }

            lastLoadedRecord = record;
            DisplayCoachPort();
            isInitialising = false;
        }

        public void SetCoachTendency(CoachRecord record)
        {
            if (coachPassOff.Value <= 50 && coachOffAggression.Value <= 50)
            {
                OffTendency.SelectedIndex = 1;
                DefTendency.SelectedIndex = 1;
            }
            else if (coachPassOff.Value <= 50 && coachOffAggression.Value > 50)
            {
                OffTendency.SelectedIndex = 0;
                DefTendency.SelectedIndex = 0;
            }
            else if (coachPassOff.Value > 50 && coachOffAggression.Value <= 50)
            {
                OffTendency.SelectedIndex = 3;
                DefTendency.SelectedIndex = 3;
            }
            else if (coachPassOff.Value > 50 && coachOffAggression.Value > 50)
            {
                OffTendency.SelectedIndex = 2;
                DefTendency.SelectedIndex = 2;
            }
        }
        
        public void InitCoachList()
        {
            model.CoachModel.InitCoachList();

            CoachGridView.Rows.Clear();
            CoachGridView.Refresh();
            CoachGridView.MultiSelect = false;
            CoachGridView.RowHeadersVisible = false;
            CoachGridView.AutoGenerateColumns = false;
            CoachGridView.AllowUserToAddRows = false;
            CoachGridView.ColumnCount = 2;
            CoachGridView.Columns[0].Name = "ID";
            CoachGridView.Columns[0].Width = 35;
            CoachGridView.Columns[1].Name = "Coach";
            CoachGridView.Columns[1].Width = 100;

            foreach (KeyValuePair<int, string> Coach in model.CoachModel.CoachNames)
            {
                object[] o = { (int)Coach.Key, (string)Coach.Value };
                CoachGridView.Rows.Add(o);
            }
            if (model.CoachModel.CoachNames.Count > 0)
            {
                CoachGridView.Rows[0].Selected = true;
                currentcoachrow = 0;
                LoadCoachInfo(model.CoachModel.GetCoachById((int)CoachGridView.Rows[0].Cells[0].Value));
            }

            else MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
        private void CoachGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (!isInitialising)
            {
                DataGridViewRow row = CoachGridView.Rows[e.RowIndex];
                int r = (int)row.Cells[0].Value;
                //if (r == currentcoachrow)
                //    return;
                //else
                //{
                    CoachGridView.Rows[currentcoachrow].Selected = false;
                    LoadCoachInfo(model.CoachModel.GetCoachById(r));
                    CoachGridView.Rows[e.RowIndex].Selected = true;
                    currentcoachrow = e.RowIndex;
                //}
            }
        }

        private void GetCoachHeight()
        {
            CoachHeight_Feet.SelectedIndex = (int)model.CoachModel.CurrentCoachRecord.height / 12;
            CoachHeight_Inches.SelectedIndex = model.CoachModel.CurrentCoachRecord.height - (int)(CoachHeight_Feet.SelectedIndex * 12);
        }
        
        private void CoachHeight_Feet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                SetCoachHeight();
        }
        private void PlayerHeight_Inches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                SetCoachHeight();
        }

        private void SetCoachHeight()
        {
            int height = (int)CoachHeight_Feet.SelectedIndex * 12 + (int)CoachHeight_Inches.SelectedIndex;
            if (height > 127)
                height = 127;
            else if (height == 0)
                height = 1;
            model.CoachModel.CurrentCoachRecord.height = height;

            GetCoachHeight();
        }
        
        public void DisplayCoachPort()
        {            
            CoachPortBox.BackColor = Color.White;
            if (!Manager.CoachPortDAT.isterf)
                return;

            int portid = model.CoachModel.CurrentCoachRecord.Coachpic + 1;

            if (Manager.CoachPortDAT.ParentTerf.files >= portid)
            {
                if (Manager.CoachPortDAT.ParentTerf.Data.DataFiles[portid].filetype == "MMAP")
                    CoachPortBox.Image = Manager.CoachPortDAT.ParentTerf.Data.DataFiles[portid].mmap_data.GetPortraitDisplay();
                else if (Manager.CoachPortDAT.ParentTerf.Data.DataFiles[portid].filetype == "COMP") CoachPortBox.BackColor = Color.Green;
                else CoachPortBox.BackColor = Color.Red;
            }

        }
        
        private string DecodePriorityType(PlayerDraftedPositions pos, int type)
        {
            if (type == 2)
            {
                return "Balanced";
            }

            switch (pos)
            {
                case PlayerDraftedPositions.QB:
                    return (type == 0 ? "Pocket" : "Scrambling");
                case PlayerDraftedPositions.HB:
                    return (type == 0 ? "Power" : "Speed");
                case PlayerDraftedPositions.FB:
                case PlayerDraftedPositions.TE:
                    return (type == 0 ? "Blocking" : "Receiving");
                case PlayerDraftedPositions.WR:
                    return (type == 0 ? "Possession" : "Speed");
                case PlayerDraftedPositions.T:
                case PlayerDraftedPositions.G:
                case PlayerDraftedPositions.C:
                    return (type == 0 ? "Run Blocking" : "Pass Blocking");
                case PlayerDraftedPositions.DE:
                case PlayerDraftedPositions.DT:
                    return (type == 0 ? "Pass Rushing" : "Run Stopping");
                case PlayerDraftedPositions.OLB:
                case PlayerDraftedPositions.MLB:
                    return (type == 0 ? "Coverage" : "Run Stopping");
                case PlayerDraftedPositions.CB:
                case PlayerDraftedPositions.SS:
                case PlayerDraftedPositions.FS:
                    return (type == 0 ? "Coverage" : "Hard Hitting");
                case PlayerDraftedPositions.K:
                case PlayerDraftedPositions.P:
                    return (type == 0 ? "Power" : "Accurate");
            }

            return "";
        }

        #region Coaches General Settings

        private void coachesName_Leave(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Name = coachName.Text;
            }
        }

        private void coachesPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                bool changed = false;
                changed = model.CoachModel.ChangeCoachPosition((int)coachesPositionCombo.SelectedIndex);

                if (changed)
                    LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
            }
        }
        
        private void CoachTeamCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                bool changed = false;
                if (CoachTeamCombo.Text == "Free Agents")
                    changed = model.CoachModel.ChangeCoachTeam(1009);
                else if (model.FileType == MaddenFileType.Roster)
                    changed = model.CoachModel.ChangeCoachTeam(CoachTeamCombo.SelectedIndex -1);
                else if (model.FileType == MaddenFileType.Franchise)
                    changed = model.CoachModel.ChangeCoachTeam(CoachTeamCombo.SelectedIndex);                
                InitCoachList();                                             
            }
        }

        #endregion

       
        #region Coach Navigate Filter Functions

        private void leftButton_Click(object sender, EventArgs e)
        {
            LoadCoachInfo(model.CoachModel.GetPreviousCoachRecord());
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            LoadCoachInfo(model.CoachModel.GetNextCoachRecord());
        }

        #endregion

        private void coachAge_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Age = (int)coachAge.Value;
            }
        }
        
        private void coachPlayoffWins_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.PlayoffWins = (int)coachPlayoffWins.Value;
            }
        }

        private void coachPlayoffLoses_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.PlayoffLosses = (int)coachPlayoffLoses.Value;
            }
        }

        private void coachSuperbowlWins_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.SuperBowlWins = (int)coachSuperbowlWins.Value;
            }
        }

        private void coachSuperBowlLoses_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.SuperBowlLoses = (int)coachSuperBowlLoses.Value;
            }
        }

        private void coachWinningSeasons_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.WinningSeasons = (int)coachWinningSeasons.Value;
            }
        }

        private void coachCareerWins_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CareerWins = (int)coachCareerWins.Value;
            }
        }

        private void coachCareerLoses_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CareerLosses = (int)coachCareerLoses.Value;
            }
        }

        private void coachCareerTies_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CareerTies = (int)coachCareerTies.Value;
            }
        }

        private void coachEthics_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Ethics = (int)coachEthics.Value;
            }
        }

        private void coachMotivation_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Motivation = (int)coachMotivation.Value;
            }
        }

        private void coachKnowledge_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Knowledge = (int)coachKnowledge.Value;
            }
        }

        private void coachChemistry_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Chemistry = (int)coachChemistry.Value;
            }
        }

       

        private void filterTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterTeamComboBox.SelectedIndex == 0)
                    model.CoachModel.FilterCoachTeam = -1;
                else if (filterTeamComboBox.Text == "Free Agents")
                    model.CoachModel.FilterCoachTeam = 1009;
                else if (Manager.stream_model != null)
                {
                    if (filterTeamComboBox.SelectedIndex == 1)
                        model.CoachModel.FilterCoachTeam = -2;
                    else model.CoachModel.FilterCoachTeam = filterTeamComboBox.SelectedIndex - 2;
                }
                else
                {
                    TeamRecord team = (TeamRecord)filterTeamComboBox.SelectedItem;
                    model.CoachModel.FilterCoachTeam = team.TeamId;                    
                }
                
                isInitialising = true;
                InitCoachList();
                isInitialising = false;
            }
        }      

        private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterPositionComboBox.SelectedIndex == 0)
                    model.CoachModel.FilterCoachPosition = -1;
                else model.CoachModel.FilterCoachPosition = filterPositionComboBox.SelectedIndex - 1;

                isInitialising = true;
                InitCoachList();
                isInitialising = false;
            }
        }
        
        private void threeFourButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.DefenseType = 5;
            }
        }

        private void fourThreeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.DefenseType = 95;
            }
        }

        private void coachPassOff_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.OffensiveStrategy = (int)coachPassOff.Value;
                coachRunOff.Value = (int)(100 - coachPassOff.Value);
                SetCoachTendency(model.CoachModel.CurrentCoachRecord);
            }
        }

        private void coachPassDef_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveStrategy = (int)coachPassDef.Value;
                coachRunDef.Value = (int)(100 - coachPassDef.Value);
            }
        }

        private void rb2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.RBCarryDist = (int)(100 - rb2.Value);
                rb1.Value = (int)(100 - rb2.Value);
            }
        }

        private void coachOffAggression_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.OffensiveAggression = (int)coachOffAggression.Value;
                SetCoachTendency(model.CoachModel.CurrentCoachRecord);
            }
        }

        private void coachDefAggression_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveAggression = (int)coachDefAggression.Value;
            }
        }
        private void coachOffensivePlaybook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.OffensivePlaybook = model.TeamModel.GetOFFPlaybook(coachOffensivePlaybook.Text);
            }
        }

        private void coachDefensivePlaybook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.DefensivePlaybook = model.TeamModel.GetDEFPlaybook(coachDefensivePlaybook.Text);
            }
        }

        private void cbSkinColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.SkinColor = ((GenericRecord)cbSkinColor.SelectedItem).Id;
                //  Anything greater than 2 is from 04/05 and is a medium tone, so set this to medium (1) for 06/07/08 values.
                if (model.MadVersion >= MaddenFileVersion.Ver2006 && model.CoachModel.CurrentCoachRecord.SkinColor > 2)
                    model.CoachModel.CurrentCoachRecord.SkinColor = 1;
            }
        }

        private void coachpic_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Coachpic = (int)coachpic.Value;
                DisplayCoachPort();
            }
        }

        private void coachyearsleft_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.ContractLength = (int)coachyearsleft.Value;
            }

        }

        private void coachQB_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachQB = (int)coachQB.Value;
            }

        }

        private void coachRB_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachRB = (int)coachRB.Value;
            }

        }

        private void coachWR_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachWR = (int)coachWR.Value;
            }
        }

        private void coachOL_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachOL = (int)coachOL.Value;
            }

        }

        private void coachDL_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachDL = (int)coachDL.Value;
            }

        }

        private void coachLB_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachLB = (int)coachLB.Value;
            }
        }

        private void coachDB_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachDB = (int)coachDB.Value;
            }

        }

        private void coachKS_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachKS = (int)coachKS.Value;
            }

        }

        private void coachPS_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.CoachPS = (int)coachPS.Value;
            }

        }
        
        private void priorityTypeSlider_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                int numPositions = Enum.GetNames(typeof(PlayerDraftedPositions)).Length;
                int index;
                for (index = 0; index < numPositions; index++)
                {
                    if (sender == priorityTypeSliders[index])
                    {
                        break;
                    }
                }

                SortedList<int, CoachPrioritySliderRecord> priorities = model.CoachModel.GetCurrentCoachSliders();
                if (priorities.Count == numPositions)
                {
                    priorities.Values[index].PriorityType = (int)priorityTypeSliders[index].Value;
                    priorityDescriptionLabels[index].Text = DecodePriorityType((PlayerDraftedPositions)index, (int)priorityTypeSliders[index].Value);
                }
            }
        }

        private void prioritySlider_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                int numPositions = Enum.GetNames(typeof(PlayerDraftedPositions)).Length;
                int index;
                for (index = 0; index < numPositions; index++)
                {
                    if (sender == prioritySliders[index])
                    {
                        break;
                    }
                }

                SortedList<int, CoachPrioritySliderRecord> priorities = model.CoachModel.GetCurrentCoachSliders();
                if (priorities.Count == numPositions)
                {
                    priorities.Values[index].Priority = (int)prioritySliders[index].Value;
                }
            }
        }
        
        private void WearsGlassesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            model.CoachModel.CurrentCoachRecord.CoachGlasses = WearsGlassesCheckbox.Checked;
        }

        private void CoachFaceID_UpDown_ValueChanged(object sender, EventArgs e)
        {
            if(!isInitialising)
                model.CoachModel.CurrentCoachRecord.FaceId = (int)CoachFaceID_UpDown.Value;
        }

        private void CoachHeadID_UpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.HeadHair = (int)CoachHeadID_UpDown.Value;
        }
        
        private void CoachOff_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.Offense = (int)CoachOff.Value;
        }

        private void CoachSafetyUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.CoachS = (int)CoachSafetyUpDown.Value;
        }

        private void CoachDef_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.DefenseRating = (int)CoachDef.Value;
        }

        private void Approval_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.ApprovalRating = (int)Approval.Value;
        }
       
        private void ImportCoachPic_Button_Click(object sender, EventArgs e)
        {
            isInitialising = true;
            string custom = Manager.CoachPortDAT.grfx.GetLoadFile();
            if (custom == "")
                return;
            Manager.CoachPortDAT.grfx = new CustomBitmap(custom, Color.White);
            Manager.CoachPortDAT.ParentTerf.Data.DataFiles[model.CoachModel.CurrentCoachRecord.Coachpic + 1].mmap_data.ImportGraphic(Manager.CoachPortDAT.grfx.fixed_dds);
            Manager.CoachPortDAT.changed = true;
            isInitialising = false;

            DisplayCoachPort();     
        }

        private void ExportCoachPic_Button_Click(object sender, EventArgs e)
        {
            string savefilename = "";
            SaveFileDialog portsavedialog = new SaveFileDialog();
            portsavedialog.Title = "Save Player Portrait";
            portsavedialog.Filter = "BMP Image | *.BMP";
            portsavedialog.CheckPathExists = true;

            if (portsavedialog.ShowDialog() == DialogResult.OK)
                savefilename = portsavedialog.FileName;
            if (savefilename == "")
                return;

            Image image = Manager.CoachPortDAT.ParentTerf.Data.DataFiles[model.CoachModel.CurrentCoachRecord.Coachpic + 1].mmap_data.GetPortraitDisplay();
            image.Save(savefilename, ImageFormat.Bmp);            
        }

        private void coachSalary_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Salary = (int)(coachSalary.Value * 100);
            }
        }

        private void FormerPlayer_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.WasPlayer = FormerPlayer_Checkbox.Checked;
            }
        }        

        private void BodySize_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.BodySize = (int)BodySize.Value;
            }
        }

        private void Delete_Coach_Button_Click(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.SetDeleteFlag(true);
                isInitialising = true;
                InitCoachList();
                isInitialising = false;
            }
        }

        private void coachSeasonWins_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.SeasonWins = (int)coachSeasonWins.Value;
        }

        private void PlayoffsMade_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.PlayoffsMade = (int)PlayoffsMade.Value;
        }

        private void coachSeasonLosses_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.SeasonLosses = (int)coachSeasonLosses.Value;
        }

        private void coachSeasonTies_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.SeasonTies = (int)coachSeasonTies.Value;
        }

        private void coachAsset_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.Asset = coachAsset.Text;
        }

        private void DeleteCoach_Button_Click(object sender, EventArgs e)
        {

        }

        private void coachFirstName_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.FirstName = coachFirstName.Text;
        }

        private void coachLastName_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.CoachModel.CurrentCoachRecord.LastName = coachLastName.Text;
        }

        private void coachPreviousTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
    	
}
