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
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class TeamEditControl : UserControl, IEditorForm
    {
        private const string UNIFORM_OBJ_COL = "clmUniformObj";
        private EditorModel model = null;
        private bool isInitialising = false;
        private TeamRecord lastLoadedRecord = null;
        public List<UniformRecord> teamuniforms = new List<UniformRecord>();
        public List<DraftPickRecord> picks = new List<DraftPickRecord>();
        public List<string> currentteams = new List<string>();
        public int currentteamrow = 0;
        public int uniformid_low = 0;
        public int uniformid_high = 0;

        public TeamEditControl()
        {
            isInitialising = true;

            InitializeComponent();

            isInitialising = false;
        }

        #region IEditorForm Members

        public MaddenEditor.Core.EditorModel Model
        {
            set { this.model = value; }
        }

        public void InitialiseUI()
        {
            isInitialising = true;

            StadiumLabel.Visible = false;
            teamStadiumSelect.Visible = false;
            if (model.MadVersion >= MaddenFileVersion.Ver2019)
            {
                TeamStadiumID.Enabled = false;
                StadiumLabel.Visible = true;
                teamStadiumSelect.Visible = true;
                foreach (GenericRecord rec in model.TeamModel.StadiumList)                
                    teamStadiumSelect.Items.Add(rec);
                
                EndPlay.Visible = true;
                teamEndPlay.Visible = true;
                foreach (GenericRecord rec in model.TeamModel.EndPlayList)
                    teamEndPlay.Items.Add(rec);

                CustomArt_Textbox.Enabled = false;
                Team_Relocated_Checkbox.Enabled = false;
            }
            

            //Fill in the combo boxes
            foreach (GenericRecord rec in model.TeamModel.LeagueList)
            {
                leagueCombo.Items.Add(rec);
                filterTeamLeagueCombo.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.TeamModel.DivisionList)
            {
                divisionCombo.Items.Add(rec);
                filterTeamDivisionCombo.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.TeamModel.ConferenceList)
            {
                conferenceCombo.Items.Add(rec);
                filterTeamConferenceCombo.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.TeamModel.CityList)
            {
                cityCombo.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.TeamModel.OffensivePlaybookList)
            {
                teamOffensivePlaybookCombo.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.TeamModel.DefensivePlaybookList)
            {
                teamDefensivePlaybookCombo.Items.Add(rec);
            }

            foreach (TeamRecord team in model.TeamModel.GetTeams())
            {
                cbTeamRival1.Items.Add(team);
                cbTeamRival2.Items.Add(team);
                cbTeamRival3.Items.Add(team);
            }
            cbTeamRival1.SelectedIndex = -1;
            cbTeamRival2.SelectedIndex = -1;
            cbTeamRival3.SelectedIndex = -1;

            leagueCombo.SelectedIndex = -1;
            divisionCombo.SelectedIndex = -1;
            conferenceCombo.SelectedIndex = -1;
            filterTeamConferenceCombo.SelectedIndex = -1;
            filterTeamDivisionCombo.SelectedIndex = -1;
            filterTeamLeagueCombo.SelectedIndex = -1;
            cityCombo.SelectedIndex = -1;
            teamDefensivePlaybookCombo.SelectedIndex = -1;
            teamOffensivePlaybookCombo.SelectedIndex = -1;

            //Madden 2007 Doesn't have shoe colors anymore
            if (model.MadVersion >= MaddenFileVersion.Ver2007)
            {
                gbShoeColor.Visible = false;
            }
            else
            {
                gbShoeColor.Visible = true;
            }

            #region Franchise Options
            if (model.FileType == MaddenFileType.Franchise)
            {
                foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                {
                    // Only add specific teams to the combo box, not AFC and NFC
                    if (record.GetIntField("TGID") >= 0 && record.GetIntField("TGID") <= 31)
                    {
                        Owned1.Items.Add(record);
                        Owned2.Items.Add(record);
                        Owned3.Items.Add(record);
                        Owned4.Items.Add(record);
                        Owned5.Items.Add(record);
                        Owned6.Items.Add(record);
                        Owned7.Items.Add(record);
                        Owned8.Items.Add(record);
                        Owned9.Items.Add(record);
                        Owned10.Items.Add(record);
                        Owned11.Items.Add(record);
                        Owned12.Items.Add(record);
                        Owned13.Items.Add(record);
                        Owned14.Items.Add(record);
                        Owned15.Items.Add(record);
                        Owned16.Items.Add(record);
                    }
                }

                for (int round = 1; round < 9; round++)
                {
                    string name = "Round " + round.ToString();
                    if (round == 8)
                        name = "NA";
                    Round1.Items.Add(name);
                    Round2.Items.Add(name);
                    Round3.Items.Add(name);
                    Round4.Items.Add(name);
                    Round5.Items.Add(name);
                    Round6.Items.Add(name);
                    Round7.Items.Add(name);
                    Round8.Items.Add(name);
                    Round9.Items.Add(name);
                    Round10.Items.Add(name);
                    Round11.Items.Add(name);
                    Round12.Items.Add(name);
                    Round13.Items.Add(name);
                    Round14.Items.Add(name);
                    Round15.Items.Add(name);
                    Round16.Items.Add(name);
                }

                for (int c = 1; c < 17; c++)
                {
                    string name = "ToFrom" + c.ToString();
                    ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                    com.Items.Add("To");
                    com.Items.Add("From");
                }
            }
            #endregion

            //Load a team
            if (model.FileType != MaddenFileType.DBTeam)
                InitTeamList();
            else LoadTeamInfo(model.TeamModel.CurrentTeamRecord);

            isInitialising = false;
        }

        public void CleanUI()
        {
            //Clear the combo boxes
            filterTeamLeagueCombo.Items.Clear();
            filterTeamDivisionCombo.Items.Clear();
            filterTeamConferenceCombo.Items.Clear();
            leagueCombo.Items.Clear();
            divisionCombo.Items.Clear();
            conferenceCombo.Items.Clear();
            cityCombo.Items.Clear();
            teamDefensivePlaybookCombo.Items.Clear();
            teamOffensivePlaybookCombo.Items.Clear();
        }




        #endregion

        private void LoadTeamInfo(TeamRecord record)
        {

            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (record.TeamId == 127)
                return;

            bool holder = isInitialising;
            isInitialising = true;

            Add_Uniforms_Button.Enabled = false;

            try
            {
                int pc = 0;
                int ir = 0;
                foreach (InjuryRecord rec in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    if (rec.TeamId == record.TeamId)
                        ir--;
                }
                foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    if (rec.TeamId == record.TeamId)
                        pc++;
                }

                
                teamPlayerCount.Value = pc + ir;

                nameTextBox.Text = record.Name;
                longNameTextBox.Text = record.LongName;
                shortTeamName.Text = record.TeamShortName;

                teamQBRating.Value = record.QBRating;
                teamRBRating.Value = record.RBRating;
                teamWRRating.Value = record.WRRating;
                teamOLRating.Value = record.OLRating;

                teamDLRating.Value = record.DLRating;
                teamLBRating.Value = record.LBRating;
                teamDBRating.Value = record.DBRating;

                teamSpecialTeamsRating.Value = record.SpecialTeamsRating;
                teamOffensiveRating.Value = record.OffensiveRating;
                teamDefensiveRating.Value = record.DefensiveRating;
                teamOverallRating.Value = record.OverallRating;

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                {
                    rbWhite.Checked = (record.ShoeColor == 0);
                    rbBlack.Checked = (record.ShoeColor == 1);
                    rbWhite.Enabled = true;
                    rbBlack.Enabled = true;
                }
                else
                {
                    rbWhite.Enabled = false;
                    rbBlack.Enabled = false;
                }

                //Team colours
                pnlPrimary.BackColor = record.PrimaryColor;
                pnlSecondary.BackColor = record.SecondaryColor;
                
                TeamStadiumID.Value = record.StadiumID;

                foreach (Object obj in divisionCombo.Items)
                {
                    if (((GenericRecord)obj).Id == record.DivisionId)
                    {
                        divisionCombo.SelectedItem = obj;
                        break;
                    }
                }
                foreach (Object obj in leagueCombo.Items)
                {
                    if (((GenericRecord)obj).Id == record.LeagueId)
                    {
                        leagueCombo.SelectedItem = obj;
                        break;
                    }
                }
                foreach (object obj in conferenceCombo.Items)
                {
                    if (((GenericRecord)obj).Id == record.ConferenceId)
                    {
                        conferenceCombo.SelectedItem = obj;
                        break;
                    }
                }

                cityCombo.SelectedIndex = -1;
                cityCombo.Text = model.TeamModel.GetCity(record.CityId);

                teamOffensivePlaybookCombo.SelectedIndex = -1;
                teamOffensivePlaybookCombo.Text = model.TeamModel.GetOFFPlaybook(record.OffensivePlaybook);
                teamDefensivePlaybookCombo.SelectedIndex = -1;
                teamDefensivePlaybookCombo.Text = model.TeamModel.GetDEFPlaybook(record.DefensivePlaybook);

                if (model.MadVersion < MaddenFileVersion.Ver2019 || model.FileType == MaddenFileType.DBTeam)
                {
                    Uniforms_Panel.Visible = true;
                    InitTeamUniforms(record);
                }
                else Uniforms_Panel.Visible = false;

                cbTeamRival1.SelectedIndex = -1;
                cbTeamRival2.SelectedIndex = -1;
                cbTeamRival3.SelectedIndex = -1;
                cbTeamRival1.Enabled = false;
                cbTeamRival2.Enabled = false;
                cbTeamRival3.Enabled = false;
                teamReputation.Enabled = false;
                nickNameTextBox.Enabled = false;

                if (model.MadVersion >= MaddenFileVersion.Ver2005)
                {
                    if (model.TeamModel.CurrentTeamRecord.TeamId != 1009 && model.TeamModel.CurrentTeamRecord.TeamId != 1010 && model.TeamModel.CurrentTeamRecord.TeamId != 1011)
                    {
                        cbTeamRival1.Enabled = true;
                        cbTeamRival2.Enabled = true;
                        cbTeamRival3.Enabled = true;
                    }

                    teamReputation.Enabled = true;
                    teamReputation.Value = record.Reputation;
                    nickNameTextBox.Enabled = true;
                    nickNameTextBox.Text = record.NickName;
                    if (model.TeamModel.CurrentTeamRecord.TeamRival1 != TeamEditingModel.NO_TEAM_ID)
                    {
                        cbTeamRival1.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival1);
                    }

                    if (model.TeamModel.CurrentTeamRecord.TeamRival2 != TeamEditingModel.NO_TEAM_ID)
                    {
                        cbTeamRival2.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival2);
                    }

                    if (model.TeamModel.CurrentTeamRecord.TeamRival3 != TeamEditingModel.NO_TEAM_ID)
                    {
                        cbTeamRival3.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival3);
                    }
                }

                if (model.FileType == MaddenFileType.Franchise && model.MadVersion != MaddenFileVersion.Ver2019)
                {
                    DraftPicks_Panel.Visible = true;
                    SetDraftPicks();
                }
                else
                {
                    DraftPicks_Panel.Visible = false;
                }

                CustomArt_Textbox.Text = record.CustomArt;
                Team_Relocated_Checkbox.Checked = record.TeamRelocated;
                teamType.Value = record.TeamType;


                #region 2019
                if (model.MadVersion >= MaddenFileVersion.Ver2019)
                {
                    teamDBName.Enabled = true;
                    teamDBName.Text = record.TeamDB;

                    teamStadiumSelect.SelectedIndex = -1;
                    string desc = model.TeamModel.GetStadium(record.StadiumID);
                    teamStadiumSelect.Text = desc;
                    teamEndPlay.SelectedIndex = -1;
                    string desc2 = model.TeamModel.GetEndPlay(record.EndPlay);
                    teamEndPlay.Text = desc2;
                }
                else
                {
                    teamDBName.Text = "NA";
                    teamDBName.Enabled = false;
                    teamStadiumSelect.Enabled = false;
                    teamStadiumSelect.Text = "";
                }

                #endregion

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Occurred loading this Team:\r\n" + e.ToString(), "Exception Loading Team", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isInitialising = true;
                LoadTeamInfo(lastLoadedRecord);
                isInitialising = false;
                return;
            }
            finally
            {
                //isInitialising = false;
            }

            if (holder)
                isInitialising = true;
            else isInitialising = false;

            lastLoadedRecord = record;
        }
        

        private void SetDraftPicks()
        {
            foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
            {
                if (dpr.Deleted)
                    continue;
                if (dpr.PickNumber == model.DraftState.PickNumber)
                {
                    if (model.DraftState.TeamID != dpr.CurrentTeamId)
                    {
                        model.DraftState.TeamID = dpr.CurrentTeamId;
                    }
                    break;
                }
            }
            
            bool holder = isInitialising;
            isInitialising = true;
            // Reset the comboboxes
            for (int q = 1; q < 17; q++)
            {
                string name = "Round" + q.ToString();
                ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                com.SelectedIndex = 7;
                com.Enabled = false;
                name = "Owned" + q.ToString();
                com = this.Controls.Find(name, true).First() as ComboBox;
                com.SelectedIndex = -1;
            }
            for (int c = 1; c < 1; c++)
            {
                string name = "ToFrom" + c.ToString();
                ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                com.SelectedIndex = -1;
            }
            // create list of draft picks and add each pick for the current team
            picks.Clear();

            foreach (TableRecordModel dp in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
            {
                DraftPickRecord rec = (DraftPickRecord)dp;
                if (rec.OriginalTeamId == model.TeamModel.CurrentTeamRecord.TeamId || rec.CurrentTeamId == model.TeamModel.CurrentTeamRecord.TeamId)
                    picks.Add(rec);
            }
            
            //  sort picks from low to high
            picks.Sort((x, y) => x.PickNumber.CompareTo(y.PickNumber));

            for (int p = 0; p < 16; p++)
            {
                string name = "Round" + (p + 1).ToString();
                ComboBox com = this.Controls.Find(name, true).First() as ComboBox;
                if (p >= picks.Count)
                    com.Enabled = false;
                else
                {
                    com.Enabled = true;
                    for (int r = 1; r < 8; r++)
                    {
                        if (picks[p].PickNumber >= 32 * (r - 1) && picks[0].PickNumber < 32 * r)
                            com.SelectedIndex = r - 1;
                    }
                }

                name = "ToFrom" + (p + 1).ToString();
                com = this.Controls.Find(name, true).First() as ComboBox;
                if (p >= picks.Count)
                    com.Enabled = false;
                else
                {
                    com.Enabled = false;
                    if (model.TeamModel.CurrentTeamRecord.TeamId != picks[p].CurrentTeamId)
                        com.SelectedIndex = 0;
                    else if (model.TeamModel.CurrentTeamRecord.TeamId != picks[p].OriginalTeamId)
                        com.SelectedIndex = 1;
                    else com.SelectedIndex = -1;
                }

                name = "Owned" + (p + 1).ToString();
                com = this.Controls.Find(name, true).First() as ComboBox;

                if (p >= picks.Count)
                    com.Enabled = false;
                else
                {
                    com.Enabled = true;

                    if (picks[p].OriginalTeamId != model.TeamModel.CurrentTeamRecord.TeamId)
                    {
                        // Madden has titans=31 and vikings=30, id#s switched
                        if (picks[p].OriginalTeamId == 30)
                            com.SelectedIndex = 31;
                        else if (picks[p].OriginalTeamId == 31)
                            com.SelectedIndex = 30;
                        else com.SelectedIndex = picks[p].OriginalTeamId;
                    }
                    else
                    {
                        if (picks[p].CurrentTeamId == 30)
                            com.SelectedIndex = 31;
                        else if (picks[p].CurrentTeamId == 31)
                            com.SelectedIndex = 30;
                        else com.SelectedIndex = picks[p].CurrentTeamId;
                    }
                }
            }

            if (holder)
                isInitialising = true;
            else isInitialising = false;
        }

        private void InitTeamList()
        {
            model.TeamModel.GetTeamList();

            TeamGridView.Rows.Clear();
            TeamGridView.Refresh();
            TeamGridView.MultiSelect = false;
            TeamGridView.RowHeadersVisible = false;
            TeamGridView.AutoGenerateColumns = false;
            TeamGridView.AllowUserToAddRows = false;
            TeamGridView.ColumnCount = 2;
            TeamGridView.Columns[0].Name = "Rec";
            TeamGridView.Columns[0].Width = 35;
            TeamGridView.Columns[1].Name = "Team";
            TeamGridView.Columns[1].Width = 100;
            
            foreach (KeyValuePair<int, string> tn in model.TeamModel.TeamNames)
            {
                object[] o = { (int)tn.Key, (string)tn.Value };
                TeamGridView.Rows.Add(o);
            }

            if (model.TeamModel.TeamNames.Count > 0)
            {
                isInitialising = true;
                TeamGridView.Rows[0].Selected = true;
                currentteamrow = 0;
                model.TeamModel.CurrentTeamRecord = (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord((int)TeamGridView.Rows[0].Cells[0].Value);
                LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
                isInitialising = false;
            }

            else MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }


        #region Form Control Actions


        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.CoachModel.CurrentCoachRecord.Name = nameTextBox.Text;
            }
        }        

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.Name = nameTextBox.Text;
            }
        }

        private void longNameTextBox_Leave(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.LongName = longNameTextBox.Text;
            }
        }

        private void shortTeamName_Leave(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.ShortName = shortTeamName.Text;
            }
        }

        private void nickNameTextBox_Leave(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.NickName = nickNameTextBox.Text;
            }
        }

        private void conferenceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.ConferenceId = (((GenericRecord)conferenceCombo.SelectedItem).Id);
            }
        }

        private void divisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.DivisionId = (((GenericRecord)divisionCombo.SelectedItem).Id);
            }
        }

        private void leagueCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.LeagueId = (((GenericRecord)leagueCombo.SelectedItem).Id);
            }
        }

        private void cityCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.CityId = model.TeamModel.GetCity(cityCombo.Text);
            }
        }

        private void teamQBRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.QBRating = (int)teamQBRating.Value;
            }
        }

        private void teamRBRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.RBRating = (int)teamRBRating.Value;
            }
        }

        private void teamWRRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.WRRating = (int)teamWRRating.Value;
            }
        }

        private void teamOLRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.OLRating = (int)teamOLRating.Value;
            }
        }

        private void teamDLRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.DLRating = (int)teamDLRating.Value;
            }
        }

        private void teamLBRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.LBRating = (int)teamLBRating.Value;
            }
        }

        private void teamDBRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.DBRating = (int)teamDBRating.Value;
            }
        }

        private void teamOffensiveRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.OffensiveRating = (int)teamOffensiveRating.Value;
            }
        }

        private void teamSpecialTeamsRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.SpecialTeamsRating = (int)teamSpecialTeamsRating.Value;
            }
        }

        private void teamDefensiveRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.DefensiveRating = (int)teamDefensiveRating.Value;
            }
        }

        private void teamOverallRating_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.OverallRating = (int)teamOverallRating.Value;
            }
        }

        private void teamOffensivePlaybookCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.OffensivePlaybook = model.TeamModel.GetOFFPlaybook(teamOffensivePlaybookCombo.Text);
            }
        }

        private void teamDefensivePlaybookCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.DefensivePlaybook = model.TeamModel.GetDEFPlaybook(teamDefensivePlaybookCombo.Text);
            }
        }

        private void teamReputation_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.Reputation = (int)teamReputation.Value;
            }
        }

        private void rbWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.ShoeColor = (rbWhite.Checked == true ? 0 : 1);
            }
        }

        private void rbBlack_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.ShoeColor = (rbBlack.Checked == true ? 1 : 0);
            }
        }

        private void btnPrimary_Click(object sender, EventArgs e)
        {
            ColorChooser chooser = new ColorChooser(model.TeamModel.CurrentTeamRecord.PrimaryColor);

            DialogResult result = chooser.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                //Change primary color
                model.TeamModel.CurrentTeamRecord.PrimaryColor = chooser.ChosenColor;
                //Update the panel
                pnlPrimary.BackColor = chooser.ChosenColor;
            }

            chooser.Dispose();

            chooser = null;
        }

        private void btnSecondary_Click(object sender, EventArgs e)
        {
            ColorChooser chooser = new ColorChooser(model.TeamModel.CurrentTeamRecord.SecondaryColor);

            DialogResult result = chooser.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                //Change secondary color
                model.TeamModel.CurrentTeamRecord.SecondaryColor = chooser.ChosenColor;
                //Update the panel
                pnlSecondary.BackColor = chooser.ChosenColor;
            }

            chooser.Dispose();

            chooser = null;
        }

        private void cbTeamRival1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.TeamRival1 = ((TeamRecord)cbTeamRival1.SelectedItem).TeamId;
            }
        }

        private void cbTeamRival2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.TeamRival2 = ((TeamRecord)cbTeamRival2.SelectedItem).TeamId;
            }
        }

        private void cbTeamRival3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.CurrentTeamRecord.TeamRival3 = ((TeamRecord)cbTeamRival3.SelectedItem).TeamId;
            }
        }

        #endregion

        private void shortTeamName_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.TeamModel.CurrentTeamRecord.TeamShortName = shortTeamName.Text;
        }

        #region Changing Picks
        private void Owned1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned1.SelectedItem;

                if (rec.TeamId != picks[0].CurrentTeamId)
                {
                    picks[0].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[0].RecNo)
                            dpr.CurrentTeamId = picks[0].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned2.SelectedItem;

                if (rec.TeamId != picks[1].CurrentTeamId)
                {
                    picks[1].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[1].RecNo)
                            dpr.CurrentTeamId = picks[1].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        // fix the rest of these

        private void Owned3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned3.SelectedItem;

                if (rec.TeamId != picks[2].CurrentTeamId)
                {
                    picks[2].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[2].RecNo)
                            dpr.CurrentTeamId = picks[2].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned4.SelectedItem;

                if (rec.TeamId != picks[3].CurrentTeamId)
                {
                    picks[3].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[3].RecNo)
                            dpr.CurrentTeamId = picks[3].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned5.SelectedItem;

                if (rec.TeamId != picks[4].CurrentTeamId)
                {
                    picks[4].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[4].RecNo)
                            dpr.CurrentTeamId = picks[4].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned6.SelectedItem;

                if (rec.TeamId != picks[5].CurrentTeamId)
                {
                    picks[5].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[5].RecNo)
                            dpr.CurrentTeamId = picks[5].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned7.SelectedItem;

                if (rec.TeamId != picks[6].CurrentTeamId)
                {
                    picks[6].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[6].RecNo)
                            dpr.CurrentTeamId = picks[6].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned8.SelectedItem;

                if (rec.TeamId != picks[7].CurrentTeamId)
                {
                    picks[7].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[7].RecNo)
                            dpr.CurrentTeamId = picks[7].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned9.SelectedItem;

                if (rec.TeamId != picks[8].CurrentTeamId)
                {
                    picks[8].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[8].RecNo)
                            dpr.CurrentTeamId = picks[8].CurrentTeamId;
                    }
                }

                SetDraftPicks();
            }
        }

        private void Owned10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned10.SelectedItem;

                if (rec.TeamId != picks[9].CurrentTeamId)
                {
                    picks[9].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[9].RecNo)
                            dpr.CurrentTeamId = picks[9].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        #endregion

        public bool SetUniformIDs(int Uniform_num, int id_num)
        {
            if (id_num < uniformid_low || id_num > uniformid_high)
            {
                MessageBox.Show("Requested Uniform ID # does not belong to this team\r\n", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (id_num == (int)Uniform0.Value || id_num == (int)Uniform1.Value)
            {
                MessageBox.Show("Cannot change default Home/Away IDs\r\n", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }            

            int holder = teamuniforms[Uniform_num].UniformId;
            teamuniforms[Uniform_num].UniformId = id_num;

            for (int c = 0; c < teamuniforms.Count; c++)
            {
                if (c == Uniform_num)
                    continue;
                else if (teamuniforms[c].UniformId == id_num)
                {
                    teamuniforms[c].UniformId = holder;
                    break;
                }
            }

            return true;
        }

        public void InitTeamUniforms(TeamRecord record)
        {
            teamuniforms.Clear();            

            foreach (UniformRecord uni in model.TableModels[EditorModel.UNIFORM_TABLE].GetRecords())
            {
                if (uni.Deleted)
                    continue;
                UniformRecord thisrec = (UniformRecord)uni;
                if (thisrec.TeamId == record.TeamId)
                    teamuniforms.Add(thisrec);
            }

            if (teamuniforms.Count > 0)
            {
                teamuniforms.Sort((x, y) => x.UniformId.CompareTo(y.UniformId));
                uniformid_low = teamuniforms[0].UniformId;
                uniformid_high = teamuniforms[teamuniforms.Count - 1].UniformId;

                // sort these by record number
                teamuniforms.Sort((x, y) => x.RecNo.CompareTo(y.RecNo));
            }

            string name = "Uniform";
            for (int c = 0; c < 16; c++)
            {
                string findname = name + c;
                NumericUpDown updown = this.Controls.Find(findname, true).First() as NumericUpDown;
                if (c > teamuniforms.Count - 1)
                {
                    updown.Minimum = -1;
                    updown.Enabled = false;
                    updown.Value = -1;
                }
                else
                {
                    if (c == 0 || c == 1)
                        updown.Enabled = false;
                    else updown.Enabled = true;
                    updown.Minimum = uniformid_low;
                    updown.Maximum = uniformid_high;
                    updown.Value = teamuniforms[c].UniformId;
                }
            }
        }

        #region Uniform ID Changes
        private void Uniform0_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(0, (int)Uniform0.Value);
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(1, (int)Uniform1.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {                
                SetUniformIDs(2, (int)Uniform2.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform3_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(3, (int)Uniform3.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform4_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(4, (int)Uniform4.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform5_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(5, (int)Uniform5.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform6_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(6, (int)Uniform6.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform7_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(7, (int)Uniform7.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform8_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(8, (int)Uniform8.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform9_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(9, (int)Uniform9.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform10_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(10, (int)Uniform10.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform11_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(11, (int)Uniform11.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform12_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(12, (int)Uniform12.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform13_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(13, (int)Uniform13.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform14_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(14, (int)Uniform14.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        private void Uniform15_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                SetUniformIDs(15, (int)Uniform15.Value); 
                InitTeamUniforms(model.TeamModel.CurrentTeamRecord);
            }
        }

        #endregion

        private void TeamGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (!isInitialising)
            {
                
                DataGridViewRow row = TeamGridView.Rows[e.RowIndex];
                int r = (int)row.Cells[0].Value;
                if (r == currentteamrow)
                    return;
                else
                {
                    isInitialising = true;

                    TeamGridView.Rows[currentteamrow].Selected = false;
                    model.TeamModel.CurrentTeamRecord = (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord(r);
                    LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
                    TeamGridView.Rows[e.RowIndex].Selected = true;
                    currentteamrow = e.RowIndex;

                    isInitialising = false;
                }
               
            }
        }

        private void filterTeamConferenceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterTeamConferenceCombo.SelectedIndex == 0)
                    model.TeamModel.RemoveConferenceFilter();
                else model.TeamModel.SetConferenceFilter(filterTeamConferenceCombo.SelectedIndex - 1);
                isInitialising = true;
                InitTeamList();
                isInitialising = false;
            }
        }

        private void filterTeamDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterTeamDivisionCombo.SelectedIndex == 0)
                    model.TeamModel.RemoveDivisionFilter();
                else model.TeamModel.SetDivisionFilter(filterTeamDivisionCombo.SelectedIndex - 1);
                isInitialising = true;
                InitTeamList();
                isInitialising = false;
            }
        }

        private void filterTeamLeagueCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterTeamLeagueCombo.SelectedIndex == 0)
                    model.TeamModel.RemoveLeagueFilter();
                else model.TeamModel.SetLeagueFilter(filterTeamLeagueCombo.SelectedIndex - 1);
                isInitialising = true;
                InitTeamList();
                isInitialising = false;
            }
        }
                      
        private void Owned11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned11.SelectedItem;

                if (rec.TeamId != picks[11].CurrentTeamId)
                {
                    picks[10].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[10].RecNo)
                            dpr.CurrentTeamId = picks[10].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned12.SelectedItem;

                if (rec.TeamId != picks[12].CurrentTeamId)
                {
                    picks[11].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[11].RecNo)
                            dpr.CurrentTeamId = picks[11].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned13.SelectedItem;

                if (rec.TeamId != picks[12].CurrentTeamId)
                {
                    picks[12].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[12].RecNo)
                            dpr.CurrentTeamId = picks[12].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned14.SelectedItem;

                if (rec.TeamId != picks[13].CurrentTeamId)
                {
                    picks[13].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[13].RecNo)
                            dpr.CurrentTeamId = picks[13].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned15.SelectedItem;

                if (rec.TeamId != picks[14].CurrentTeamId)
                {
                    picks[14].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[14].RecNo)
                            dpr.CurrentTeamId = picks[14].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void Owned16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                TeamRecord rec = (TeamRecord)Owned16.SelectedItem;

                if (rec.TeamId != picks[15].CurrentTeamId)
                {
                    picks[15].CurrentTeamId = rec.TeamId;

                    foreach (DraftPickRecord dpr in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                    {
                        if (dpr.RecNo == picks[15].RecNo)
                            dpr.CurrentTeamId = picks[15].CurrentTeamId;
                    }
                }
                SetDraftPicks();
            }
        }

        private void CustomArt_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.TeamModel.CurrentTeamRecord.CustomArt = CustomArt_Textbox.Text;
        }

        private void Team_Relocated_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.TeamModel.CurrentTeamRecord.TeamRelocated && !Team_Relocated_Checkbox.Checked)
                    Add_Uniforms_Button.Enabled = true;
                else Add_Uniforms_Button.Enabled = false;
                    
                model.TeamModel.CurrentTeamRecord.TeamRelocated = Team_Relocated_Checkbox.Checked;
            }
        }

        private void Add_Uniforms_Button_Click(object sender, EventArgs e)
        {
                        
        }

        private void teamDBName_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.TeamModel.CurrentTeamRecord.TeamDB = teamDBName.Text;
        }

        private void teamStadiumSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.MadVersion == MaddenFileVersion.Ver2019)
                    model.TeamModel.CurrentTeamRecord.StadiumID = model.TeamModel.GetStadium(teamStadiumSelect.Text);
                else model.TeamModel.CurrentTeamRecord.StadiumID = teamStadiumSelect.SelectedIndex;
            }
        }

        private void teamEndPlay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.TeamModel.CurrentTeamRecord.EndPlay = model.TeamModel.GetEndPlay(teamEndPlay.Text);
        }

        private void teamPlayerCount_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void teamType_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.TeamModel.CurrentTeamRecord.TeamType = (int)teamType.Value;
        }
        
    }
}
	

