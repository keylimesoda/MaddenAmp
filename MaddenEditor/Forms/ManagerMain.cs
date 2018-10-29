/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Forms;
using MaddenEditor.Core.Manager;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;

namespace MaddenEditor.Forms
{
    public partial class ManagerMain : Form
    {
        // OCIS coaches previous teams/positions
        // TEAH team history wins/losses season
        // TSSE team season stats
        // TMSR team season records
        // OSCE coach expected salary


        private MGMT _manager;
        public bool isInitializing = false;

        public Coach currentcoach = null;
        public Team currentteam = null;
        public int currentowner = -1;
        public int currentcoachesteam = -1;
        public int scoutpos = 0;

        public List<RestrictedFreeAgentPlayers> rfaplayers = new List<RestrictedFreeAgentPlayers>();
        public List<TeamRecord> teams = new List<TeamRecord>();
        public Dictionary<int, List<DraftPickRecord>> picks = new Dictionary<int, List<DraftPickRecord>>();        
        public Dictionary<int, List<int>> CoachesDesiredTeams = new Dictionary<int, List<int>>();
        public Dictionary<int, List<CoachHistory>> CoachResume = new Dictionary<int, List<CoachHistory>>();
        public List<Coach> AvailableCoaches = new List<Coach>();
        public Dictionary<int, List<int>> CoachSalaries = new Dictionary<int, List<int>>();

        #region Get/Set
         
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        
        #endregion


        public ManagerMain()
        {
            InitializeComponent();            
        }

        public void Init()
        {
            isInitializing = true;

            manager.Initialize();

            #region Manager Tab
            FranchiseState_Groupbox.Visible = true;
            FranchiseWeek.Value = manager.s_week;
            FranchiseWeekDay.SelectedIndex = manager.s_day;
            
            #endregion


            #region Owner Info
            ClearOwnerInfo();

            #endregion

            #region Coach Page
            CoachContractPosition_Combo.Items.Clear();
            AvailCoachCombo.Items.Clear();
            CoachContractPosition_Combo.Items.Add("HC");
            AvailCoachCombo.Items.Add("HC");
            CoachContractPosition_Combo.Items.Add("OC");
            AvailCoachCombo.Items.Add("OC");
            CoachContractPosition_Combo.Items.Add("DC");
            AvailCoachCombo.Items.Add("DC");
            CoachContractPosition_Combo.Items.Add("ST");
            AvailCoachCombo.Items.Add("ST");
            CoachContractPosition_Combo.SelectedIndex = 0;
            AvailCoachCombo.SelectedIndex = 0;
            CoachInfoRatings_Panel.Visible = false;
            CoachContractHC_Panel.Visible = false;
            FireCoach_Button.Enabled = false;
            TeamStatsFilter.SelectedIndex = 0;
            CoachContractPanel.Visible = false;

            #endregion           
            
            CBAYearComboBox.Items.Clear();
            for (int t = 2004; t < 2019; t++)
                CBAYearComboBox.Items.Add(t);
            CBAYearComboBox.SelectedIndex = CBAYearComboBox.Items.IndexOf(manager.model.CurrentYear);            
           
            foreach (TeamRecord tr in manager.model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                if (tr.TeamId == 1010 || tr.TeamId == 1011) // NFC AFC probowl teams
                    continue;
                OwnerTeamComboBox.Items.Add(tr);
                CurrentCoachesTeam_Combo.Items.Add(tr);
                foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    DraftPickRecord dpr = (DraftPickRecord)rec;
                    if (tr.TeamId == dpr.CurrentTeamId)
                    {
                        List<DraftPickRecord> p = new List<DraftPickRecord>();
                        if (picks.ContainsKey(tr.TeamId))
                            p = picks[tr.TeamId];
                        p.Add(dpr);
                        picks[tr.TeamId] = p;                            
                    }
                }
            }

            for (int p = 0; p < 21; p++)
            {
                string pos = Enum.GetName(typeof(MaddenPositions), p);
                ScoutPOSComboBox.Items.Add(pos);
                EvalPosCombo.Items.Add(pos);
            }
            
            ScoutPOSComboBox.SelectedIndex = 0;
            EvalPosCombo.SelectedIndex = 0;

            if (!manager.LoadOwners())
            {
                foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    TeamRecord team = (TeamRecord)rec;
                    if (team.TeamId == 1009 || team.TeamId == 1010 || team.TeamId == 1011)
                        continue;
                    Coach own = new Coach();
                    own.NAME = team.Name + " Owner";
                    own.TEAM_ID = team.TeamId;

                    foreach (TableRecordModel record in manager.model.TableModels[EditorModel.OWNER_TABLE].GetRecords())
                    {
                        if (record.Deleted)
                            continue;
                        OwnerRecord o = (OwnerRecord)record;
                        if (o.TeamId != own.TEAM_ID)
                            continue;

                        own.SetOwner(o);
                    }

                    manager.Owners.Add(own);
                }
            }

            if (manager.Owners.Count > 0)
                currentowner = 0;
            
            manager.InitTeams();
            manager.InitCoaches();
            InitCoachSalaries();
            InitAvailableCoaches();
            InitAvailCoachView();
            InitOwnerView();
            InitTeamStatsView(0);
            CurrentCoachesTeam_Combo.SelectedIndex = 0;
            isInitializing = false;
        }


        #region Coach Page Functions

        public void InitCoachSalaries()
        {
            CoachSalaries.Clear();
            for (int c = 0; c < 4; c++ )
                CoachSalaries.Add(c, new List<int>());

            foreach (KeyValuePair<int, Team> t in manager.Teams)
            {
                foreach (KeyValuePair<int, Coach> c in t.Value.Coaches)
                {
                    CoachSalaries[c.Value.POSITION].Add(c.Value.SALARY);
                }
            }

            foreach (KeyValuePair<int, List<int>> pos_sal in CoachSalaries)
            {
                pos_sal.Value.Sort((x, y) => y.CompareTo(x));
            }
        }
        
        public void ClearCoachInfo()
        {
            CoachInfoName.Text = "";
            CoachContractPosition_Combo.Items.Clear();
            CoachInfoContractLength.Value = 0;
            CoachInfoContractSalary.Value = 0;
            CoachInfoOC_Checkbox.Checked = false;
            CoachInfoDC_Checkbox.Checked = false;
            CoachInfoGM_Checkbox.Checked = false;
            CoachInfoDraftPlayers.Checked = false;
            CoachInfoCoaches_Checkbox.Checked = false;
            CoachInfoSignPicks_Checkbox.Checked = false;
            CoachInfoSignFA_Checkbox.Checked = false;
            CoachInfoResign_Checkbox.Checked = false;
            CoachPriQB.Value = 50;
            

            CoachInfoEthics.Value = 50;
            CoachInfoMotivation.Value = 50;
            CoachInfoKnowledge.Value = 50;
            CoachInfoChemistry.Value = 50;
            CoachInfoDefense.Value = 50;
            CoachInfoS.Value = 50;
            CoachInfoLB.Value = 50;
            CoachInfoDB.Value = 50;
            CoachInfoDL.Value = 50;
            CoachInfoP.Value = 50;
            CoachInfoDefense.Value = 50;
            CoachInfoQB.Value = 50;
            CoachInfoRB.Value = 50;
            CoachInfoWR.Value = 50;
            CoachInfoOL.Value = 50;
            CoachInfoK.Value = 50;

            CoachContractPanel.Visible = false;
            CoachContractHC_Panel.Visible = false;
            CoachInfoRatings_Panel.Visible = false;
            CoachInfoComments_Textbox.Clear();
            CoachInterview_Button.Enabled = true;
            CoachContract_Button.Enabled = true;
        }

        public void LoadCoachInfo()
        {
            ClearCoachInfo();
            if (currentcoach == null) 
                return;
            
            CoachInfoName.Text = currentcoach.NAME;
            CoachInfoContractLength.Value = currentcoach.CONTRACT_LENGTH;
            CoachInfoContractSalary.Value = (decimal)currentcoach.SALARY/100;
            

            if (currentcoach.InPlayoffs)
            {
                CoachInfoComments_Textbox.Text = "Preparing our team to play";
                CoachContract_Button.Enabled = false;
                CoachInterview_Button.Enabled = false;

                if (currentcoach.CanBeInterviewed)
                {
                    CoachInterview_Button.Enabled = true;
                    CoachInfoComments_Textbox.Text += ", but I may be willing to interview this week.";
                }
            }
            else
            {
                
                // coach comments
            }

            // No team selected, just return
            if (CurrentCoachesTeam_Combo.SelectedIndex < 0)
                return;

            // Get owner's eval for coach ratings
            TeamRecord tm = (TeamRecord)CurrentCoachesTeam_Combo.SelectedItem;            
            
            int teamid = tm.TeamId;

            if (manager.Teams[teamid].CoachesInterviewed.Contains(currentcoach.COACH_ID))
            {
                CoachInfoEthics.Value = 50;
                CoachInfoMotivation.Value = 50;
                CoachInfoKnowledge.Value = 50;
                CoachInfoChemistry.Value = 50;
                CoachInfoDefense.Value = 50;
                CoachInfoOffense.Value = 50;
                CoachInfoS.Value = 50;
                CoachInfoLB.Value = 50;
                CoachInfoDB.Value = 50;
                CoachInfoDL.Value = 50;
                CoachInfoP.Value = 50;                
                CoachInfoQB.Value = 50;
                CoachInfoRB.Value = 50;
                CoachInfoWR.Value = 50;
                CoachInfoOL.Value = 50;
                CoachInfoK.Value = 50;
                CoachInfoRatings_Panel.Visible = true;
            }            
        }
        
        public void InitAvailableCoaches()
        {
            int pos = AvailCoachCombo.SelectedIndex;
            AvailableCoaches.Clear();
            // unemployed coaches
            foreach (Coach c in manager.Coaches)
            {
                if (AvailableCoaches.Contains(c))
                    continue;
                else if (c.CONTRACT_LENGTH == 0)                    // No current contract
                    AvailableCoaches.Add(c);
                else if (pos == 0 && c.POSITION != 0)               // looking for HC and position is not head coach 
                    AvailableCoaches.Add(c);
                else if (pos == 1 && c.POSITION == 3 || pos == 2 && c.POSITION == 3)   // looking for OC/DC and position is ST
                    AvailableCoaches.Add(c);
            }

            bool stop = true;
        }

        public void InitCurrentCoachView()
        {
            if (currentcoachesteam < 0)
                return;

            if (CurrentCoachesView != null)
                CurrentCoachesView.Dispose();
            CurrentCoachesView = new DataGridView();
            CurrentCoachesView.BackgroundColor = Color.Silver;
            CurrentCoachesView.Bounds = new Rectangle(new Point(5, 25), new Size(352, 115));
            CurrentCoachesView.MultiSelect = false;
            CurrentCoachesView.AutoGenerateColumns = false;
            CurrentCoachesView.AllowUserToAddRows = false;
            CurrentCoachesView.RowHeadersVisible = false;
            CurrentCoachesView.ColumnCount = 8;

            CurrentCoachesView.Columns[0].Name = "ID";
            CurrentCoachesView.Columns[0].Width = 40;
            CurrentCoachesView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            CurrentCoachesView.Columns[1].Name = "Pos";
            CurrentCoachesView.Columns[1].Width = 30;
            CurrentCoachesView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[2].Name = "Name";
            CurrentCoachesView.Columns[2].Width = 80;
            CurrentCoachesView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[3].Name = "Con";
            CurrentCoachesView.Columns[3].Width = 30;
            CurrentCoachesView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            CurrentCoachesView.Columns[4].Name = "Sal";
            CurrentCoachesView.Columns[4].Width = 40;
            CurrentCoachesView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[5].Name = "Avg";
            CurrentCoachesView.Columns[5].Width = 40;
            CurrentCoachesView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[6].Name = "T10";
            CurrentCoachesView.Columns[6].Width = 40;
            CurrentCoachesView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[7].Name = "Top";
            CurrentCoachesView.Columns[7].Width = 40;
            CurrentCoachesView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Team team = manager.Teams[currentcoachesteam];


            foreach (KeyValuePair<int, Coach> coach in team.Coaches)
            {
                string pos = "";
                if (coach.Value.POSITION == 0)
                    pos = "HC";
                else if (coach.Value.POSITION == 1)
                    pos = "OC";
                else if (coach.Value.POSITION == 2)
                    pos = "DC";
                else pos = "ST";

                int total = 0;
                int top10 = 0;

                for (int i = 0; i < CoachSalaries[coach.Value.POSITION].Count; i++)
                {
                    total += CoachSalaries[coach.Value.POSITION][i];
                    if (i < 10)
                        top10 += CoachSalaries[coach.Value.POSITION][i];
                }

                string avgsal = String.Format("{0:0.00}", (double)total / (100 * CoachSalaries[coach.Value.POSITION].Count));
                string t10 = String.Format("{0:0.00}", (double)top10 / (100 * 10));
                string sal = String.Format("{0:0.00}", (double)coach.Value.SALARY / 100);
                string top = String.Format("{0:0.00}", (double)CoachSalaries[coach.Value.POSITION][0] / 100);
                object[] entry = { coach.Value.COACH_ID, pos, coach.Value.NAME, coach.Value.CONTRACT_LENGTH, sal, avgsal, t10, top };
                CurrentCoachesView.Rows.Add(entry);
            }
            CurrentCoachesView.Sort(CurrentCoachesView.Columns[1], ListSortDirection.Ascending);
            CurrentCoachesView.CellClick += CurrentCoachesView_CellClick;
            CoachSignPage.Controls.Add(CurrentCoachesView);
            CurrentCoachesView.ClearSelection();
            FireCoach_Button.Enabled = false;
        }
         
        public void InitAvailCoachView()
        {
            if (AvailCoachView != null)
                AvailCoachView.Dispose();
            AvailCoachView = new DataGridView();
            AvailCoachView.BackgroundColor = Color.Silver;
            AvailCoachView.Bounds = new Rectangle(new Point(5, 308), new Size(352, 177));
            AvailCoachView.MultiSelect = false;
            AvailCoachView.AutoGenerateColumns = false;
            AvailCoachView.AllowUserToAddRows = false;
            AvailCoachView.RowHeadersVisible = false;
            AvailCoachView.ColumnCount = 9;

            AvailCoachView.Columns[0].Name = "ID";
            AvailCoachView.Columns[0].Width = 30;
            AvailCoachView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AvailCoachView.Columns[1].Name = "Name";
            AvailCoachView.Columns[1].Width = 75;
            AvailCoachView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AvailCoachView.Columns[2].Name = "Team";
            AvailCoachView.Columns[2].Width = 40;
            AvailCoachView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            AvailCoachView.Columns[3].Name = "HY";
            AvailCoachView.Columns[3].Width = 30;
            AvailCoachView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AvailCoachView.Columns[4].Name = "HW";
            AvailCoachView.Columns[4].Width = 30;
            AvailCoachView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AvailCoachView.Columns[5].Name = "HL";
            AvailCoachView.Columns[5].Width = 30;
            AvailCoachView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AvailCoachView.Columns[6].Name = "OCY";
            AvailCoachView.Columns[6].Width = 30;
            AvailCoachView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AvailCoachView.Columns[7].Name = "DCY";
            AvailCoachView.Columns[7].Width = 30;
            AvailCoachView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AvailCoachView.Columns[8].Name = "STY";
            AvailCoachView.Columns[8].Width = 30;
            AvailCoachView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Coach c in AvailableCoaches)
            {
                int hcy = 0;
                int hcw = 0;
                int hcl = 0;
                int ocy = 0;
                int dcy = 0;
                int sty=0;

                foreach (KeyValuePair<int, CoachHistory> ch in c.History)
                {
                    if (ch.Value.CoachPosition == 0)
                    {
                        hcy++;
                        foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.TEAM_WIN_LOSS_RECORD].GetRecords())
                        {
                            if (rec.Deleted)
                                continue;
                            TeamWinLossRecord t = (TeamWinLossRecord)rec;                            
                            if (ch.Value.TeamID == t.TeamId && ch.Value.Season == t.Season)
                            {
                                hcw += t.Wins;
                                hcl += t.Losses;
                            }
                        }
                    }
                    else if (ch.Value.CoachPosition == 1)
                        ocy++;
                    else if (ch.Value.CoachPosition == 2)
                        dcy++;
                    else sty++;                                       
                }
                string sal = String.Format("{0:0.00}", (double)c.SALARY / 100);
                string teamname = "FA";
                if (c.TEAM_ID != 1009)
                    teamname = manager.Teams[c.TEAM_ID].team.TeamShortName;
                object[] entry = { c.COACH_ID, c.NAME, teamname,hcy, hcw, hcl,ocy,dcy,sty };
                AvailCoachView.Rows.Add(entry);
            }
            AvailCoachView.CellClick += AvailCoachView_CellClick;
            CoachSignPage.Controls.Add(AvailCoachView);
        }
                
        
        private void CurrentCoachesView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            FireCoach_Button.Enabled = false;
            CurrentCoachesView.ClearSelection();
            CurrentCoachesView.Rows[e.RowIndex].Selected = true;
            int cid = (int)CurrentCoachesView.Rows[e.RowIndex].Cells[0].Value;
            foreach (KeyValuePair<int,Coach> tc in manager.Teams[CurrentCoachesTeam_Combo.SelectedIndex].Coaches)
            {
                if (tc.Value.COACH_ID != cid)
                    continue;
                if (tc.Value.CONTRACT_LENGTH > 0)
                    FireCoach_Button.Enabled = true;
            }    
        }

        private void AvailCoachView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int ac = (int)AvailCoachView.Rows[e.RowIndex].Cells[0].Value;
            foreach (Coach c in AvailableCoaches)
            {
                if (ac == c.COACH_ID)
                {
                    currentcoach = c;
                    isInitializing = true;
                    AvailCoachView.ClearSelection();                    // Clear previous selected rows
                    AvailCoachView.Rows[e.RowIndex].Selected = true;    // Select current row                    
                    LoadCoachInfo();
                    
                    isInitializing = false;
                    break;
                }
            }
            
            
            
        }

        private void AvailCoachCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing && AvailCoachCombo.SelectedIndex >=0)
            {
                isInitializing = true;
                InitAvailableCoaches();
                InitAvailCoachView();
                isInitializing = false;
            }
        }

        public void InitTeamStatsView(int filter)
        {
            if (TeamStatsView != null)
                TeamStatsView.Dispose();
            TeamStatsView = new DataGridView();
            TeamStatsView.BackgroundColor = Color.Silver;
            TeamStatsView.Bounds = new Rectangle(new Point(5, 515), new Size(703, 135));
            TeamStatsView.MultiSelect = false;
            TeamStatsView.AutoGenerateColumns = false;
            TeamStatsView.AllowUserToAddRows = false;
            TeamStatsView.RowHeadersVisible = false;
            TeamStatsView.ColumnCount = 12;

            TeamStatsView.Columns[0].Name = "ID";
            TeamStatsView.Columns[0].Width = 30;
            TeamStatsView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamStatsView.Columns[1].Name = "Team";
            TeamStatsView.Columns[1].Width = 70;
            TeamStatsView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TeamStatsView.Columns[2].Name = "TOFF";
            TeamStatsView.Columns[2].Width = 40;
            TeamStatsView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[3].Name = "Pass";
            TeamStatsView.Columns[3].Width = 40;
            TeamStatsView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[4].Name = "Ptd";
            TeamStatsView.Columns[4].Width = 40;
            TeamStatsView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[5].Name = "Pyd";
            TeamStatsView.Columns[5].Width = 40;
            TeamStatsView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[6].Name = "Rush";
            TeamStatsView.Columns[6].Width = 40;
            TeamStatsView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[7].Name = "Rtd";
            TeamStatsView.Columns[7].Width = 40;
            TeamStatsView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[8].Name = "Ryd";
            TeamStatsView.Columns[8].Width = 40;
            TeamStatsView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[9].Name = "TDEF";
            TeamStatsView.Columns[9].Width = 40;
            TeamStatsView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[10].Name = "DPas";
            TeamStatsView.Columns[10].Width = 40;
            TeamStatsView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TeamStatsView.Columns[11].Name = "DRus";
            TeamStatsView.Columns[11].Width = 40;
            TeamStatsView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;




            foreach (KeyValuePair<int, Team> team in manager.Teams)
            {
                if (filter == 1 && team.Value.team.ConferenceId != currentteam.team.ConferenceId)
                    continue;
                if (filter == 2 && team.Value.team.DivisionId != currentteam.team.DivisionId)
                    continue;
                
                object[] entry = {team.Value.team.TeamId, team.Value.team.Name,team.Value.SeasonStats[manager.model.CurrentYear].OffenseYards, team.Value.SeasonStats[manager.model.CurrentYear].OffensePassAtt,
                                 team.Value.SeasonStats[manager.model.CurrentYear].OffensePassTDs, team.Value.SeasonStats[manager.model.CurrentYear].OffensePassYards,
                                team.Value.SeasonStats[manager.model.CurrentYear].RushingAttempts, team.Value.SeasonStats[manager.model.CurrentYear].RushingTDs, 
                                team.Value.SeasonStats[manager.model.CurrentYear].RushingYards, team.Value.SeasonStats[manager.model.CurrentYear].DefensePassYds +team.Value.SeasonStats[manager.model.CurrentYear].DefenseRushYds,
                                team.Value.SeasonStats[manager.model.CurrentYear].DefensePassYds,team.Value.SeasonStats[manager.model.CurrentYear].DefenseRushYds  };
                TeamStatsView.Rows.Add(entry);
            }

            CoachSignPage.Controls.Add(TeamStatsView);
        }

        public void InitCoachHistory()
        {
            CoachResume.Clear();

            if (manager.stream_model != null)
            {
                foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    CoachCollection coach = (CoachCollection)rec;
                    List<CoachHistory> resume = GetCoachResume(coach.CoachId);
                    if (!CoachResume.ContainsKey(coach.CoachId))
                        CoachResume.Add(coach.CoachId, resume);
                }
            }

            else
            {
                foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    CoachRecord coach = (CoachRecord)rec;
                    List<CoachHistory> resume = GetCoachResume(coach.CoachId);
                    if (!CoachResume.ContainsKey(coach.CoachId))
                        CoachResume.Add(coach.CoachId, resume);
                }
            }

        }

        public List<CoachHistory> GetCoachResume(int coachid)
        {
            List<CoachHistory> resume = new List<CoachHistory>();
            foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.COACHING_HISTORY_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                CoachHistory hist = (CoachHistory)rec;
                if (hist.CoachID == coachid)
                    resume.Add(hist);

            }
            return resume;
        }

        
        #endregion


        #region Owner/GM Page Functions

        public void ClearOwnerInfo()
        {
            OwnerNameTextBox.Text = "";
            OwnerTeamComboBox.SelectedIndex = -1;
            OwnerEgo.Value = 50;
            OwnerPatience.Value = 50;
            OwnerKnowledge.Value = 50;
            OwnerSpending.Value = 50;
            OwnerLoyalty.Value = 50;
            OwnerRisk.Value = 50;
            OwnerEthics.Value = 50;
            OwnerCPUControl.Checked = true;
            OwnerGM.Checked = true;
            OwnerCoaches.Checked = true;
            OwnerDraft.Checked = true;
            OwnerSignPicks.Checked = true;
            OwnerSignFA.Checked = true;
            OwnerResign.Checked = true;            
        }
        
        public void InitOwnerView()
        {
            if (OwnerView != null)
                OwnerView.Dispose();
            OwnerView = new DataGridView();
            OwnerView.BackgroundColor = Color.Silver;
            OwnerView.Bounds = new Rectangle(new Point(5, 5), new Size(250,420));
            OwnerView.MultiSelect = false;
            OwnerView.AutoGenerateColumns = false;
            OwnerView.AllowUserToAddRows = false;
            OwnerView.RowHeadersVisible = false;
            OwnerView.ColumnCount = 3;
            OwnerView.Columns[0].Name = "#";
            OwnerView.Columns[0].Width = 20;
            OwnerView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OwnerView.Columns[1].Name = "Name";
            OwnerView.Columns[1].Width = 105;
            OwnerView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            OwnerView.Columns[2].Name = "Team";
            OwnerView.Columns[2].Width = 120;
            OwnerView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            int id = 0;
            foreach (Coach own in manager.Owners)
            {
                object[] entry = { id, own.NAME, manager.model.TeamModel.GetTeamNameFromTeamId(own.TEAM_ID) };
                OwnerView.Rows.Add(entry);
                id++;
            }

            OwnerView.CellClick += OwnerView_CellClick;
            OwnerCoachEditPage.Controls.Add(OwnerView);
            if (manager.Owners.Count == 0)
                OwnerInfo_Panel.Enabled = false;
            else
            {
                OwnerInfo_Panel.Enabled = true;
                OwnerView.Rows[currentowner].Selected = true;
                LoadOwner(currentowner);
            }
        }

        private void OwnerView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            OwnerView.Rows[currentowner].Selected = false;
            currentowner = (int)OwnerView.Rows[e.RowIndex].Cells[0].Value;
            OwnerView.Rows[currentowner].Selected = true;
            LoadOwner(currentowner);
        }

        public void LoadOwner(int ownerid)
        {
            if (ownerid < 0)
                return;

            isInitializing = true;

            OwnerNameTextBox.Text = manager.Owners[currentowner].NAME;
            OwnerTeamComboBox.SelectedItem = manager.model.TeamModel.GetTeamRecord(manager.Owners[currentowner].TEAM_ID);
            OwnerEgo.Value = manager.Owners[currentowner].EGO;
            OwnerPatience.Value = manager.Owners[currentowner].PATIENCE;
            OwnerKnowledge.Value = manager.Owners[currentowner].KNOWLEDGE;
            OwnerSpending.Value = manager.Owners[currentowner].SPENDING;
            OwnerLoyalty.Value = manager.Owners[currentowner].LOYALTY;
            OwnerRisk.Value = manager.Owners[currentowner].RISK;
            OwnerEthics.Value = manager.Owners[currentowner].ETHICS;
            OwnerCPUControl.Checked = manager.Owners[currentowner].CPU_CONTROLLED;
            OwnerGM.Checked = manager.Owners[currentowner].HC_GM;
            OwnerCoaches.Checked = manager.Owners[currentowner].CPU_SIGN_COACHES;
            OwnerDraft.Checked = manager.Owners[currentowner].CPU_DRAFT_PLAYER;
            OwnerSignPicks.Checked = manager.Owners[currentowner].CPU_SIGN_DRAFT_PICKS;
            OwnerSignFA.Checked = manager.Owners[currentowner].CPU_SIGN_FREE_AGENTS;
            OwnerResign.Checked = manager.Owners[currentowner].CPU_RESIGN_PLAYERS;

            isInitializing = false;
        }

        #endregion

        
        #region RFA Page Functions

        public void InitRFAView()
        {
            if (CurrentCoachesView != null)
                CurrentCoachesView.Dispose();
            CurrentCoachesView = new DataGridView();
            CurrentCoachesView.BackgroundColor = Color.Silver;
            CurrentCoachesView.Bounds = new Rectangle(new Point(5, 25), new Size(700, 250));
            CurrentCoachesView.MultiSelect = false;
            CurrentCoachesView.AutoGenerateColumns = false;
            CurrentCoachesView.AllowUserToAddRows = false;
            CurrentCoachesView.RowHeadersVisible = false;
            CurrentCoachesView.ColumnCount = 9;

            CurrentCoachesView.Columns[0].Name = "First";
            CurrentCoachesView.Columns[0].Width = 60;
            CurrentCoachesView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[1].Name = "Last";
            CurrentCoachesView.Columns[1].Width = 120;
            CurrentCoachesView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[2].Name = "Pos";
            CurrentCoachesView.Columns[2].Width = 40;
            CurrentCoachesView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[3].Name = "OVR";
            CurrentCoachesView.Columns[3].Width = 40;
            CurrentCoachesView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CurrentCoachesView.Columns[4].Name = "Team";
            CurrentCoachesView.Columns[4].Width = 80;
            CurrentCoachesView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[5].Name = "Con";
            CurrentCoachesView.Columns[5].Width = 60;
            CurrentCoachesView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[6].Name = "Salary";
            CurrentCoachesView.Columns[6].Width = 80;
            CurrentCoachesView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[7].Name = "Comp1";
            CurrentCoachesView.Columns[7].Width = 60;
            CurrentCoachesView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            CurrentCoachesView.Columns[8].Name = "Comp2";
            CurrentCoachesView.Columns[8].Width = 60;
            CurrentCoachesView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            foreach (RestrictedFreeAgentPlayers rfa in rfaplayers)
            {
                PlayerRecord player = manager.model.PlayerModel.GetPlayerByPlayerId(rfa.PlayerId);
                string pos = Enum.GetNames(typeof(MaddenPositions))[player.PositionId].ToString();
                string comp1 = manager.model.GetDraftPickName(rfa.CompPick1);
                string comp2 = manager.model.GetDraftPickName(rfa.CompPick2);
                decimal salary = Math.Round((decimal)rfa.OfferedTotalSalary / 100, 4);
                object[] entry = {player.FirstName, player.LastName, pos,rfa.PlayerOverall,manager.model.TeamModel.GetTeamNameFromTeamId(rfa.TeamID),rfa.OfferedContractLength,salary,
                                 comp1,comp2};

                CurrentCoachesView.Rows.Add(entry);
            }

            CurrentCoachesView.Sort(CurrentCoachesView.Columns[1], ListSortDirection.Ascending);
            RFAPage.Controls.Add(CurrentCoachesView);
        }

        public void InitRFAPlayers()
        {
            rfaplayers.Clear();
            foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.RFA_PLAYERS].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                RestrictedFreeAgentPlayers rfa = (RestrictedFreeAgentPlayers)rec;
                rfaplayers.Add(rfa);
            }
        }
                
        public void InitScoutingView()
        {
            if (ScoutingView != null)
                ScoutingView.Dispose();
            ScoutingView = new DataGridView();
            ScoutingView.BackgroundColor = Color.Silver;
            ScoutingView.Bounds = new Rectangle(new Point(5, 320), new Size(700, 300));
            ScoutingView.MultiSelect = false;
            ScoutingView.AutoGenerateColumns = false;
            ScoutingView.AllowUserToAddRows = false;
            ScoutingView.RowHeadersVisible = false;
            ScoutingView.ColumnCount = 9;
            ScoutingView.Columns[0].Name = "ID";
            ScoutingView.Columns[0].Width = 50;
            ScoutingView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[1].Name = "Team";
            ScoutingView.Columns[1].Width = 50;
            ScoutingView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[2].Name = "OVR";
            ScoutingView.Columns[2].Width = 120;
            ScoutingView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[3].Name = "Owner";
            ScoutingView.Columns[3].Width = 60;
            ScoutingView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[4].Name = "Coach";
            ScoutingView.Columns[4].Width = 60;
            ScoutingView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[5].Name = "HC";
            ScoutingView.Columns[5].Width = 60;
            ScoutingView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[6].Name = "OC";
            ScoutingView.Columns[6].Width = 60;
            ScoutingView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[7].Name = "DC";
            ScoutingView.Columns[7].Width = 60;
            ScoutingView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ScoutingView.Columns[8].Name = "ST";
            ScoutingView.Columns[8].Width = 60;
            ScoutingView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            foreach (TableRecordModel rec in manager.model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                TeamRecord tr = (TeamRecord)rec;
                if (tr.TeamId == 1010 || tr.TeamId == 1011 || tr.TeamId == 1009)
                    continue;
                else
                {
                    Coach own = new Coach();
                    foreach (Coach o in manager.Owners)
                    {
                        if (o.TEAM_ID == tr.TeamId)
                        {
                            own = o;
                            break;
                        }
                    }
                    
                    double ow = 0;
                    double hc = 0;
                    double oc = 0;
                    double dc = 0;
                    double st = 0;
                    double owner_mult = 1;

                    if (own.NAME != "")
                    {
                        ow = 0;
                        if (own.EGO == 100)
                            owner_mult = 0;
                        else owner_mult = (double)(100 - own.EGO) / 100;
                    }

                    // Sometimes teams will not have any coaches, expiring contracts etc...
                    // in that case the scouting will be empty and we will use 0
                    if (manager.Scouting.ContainsKey(tr.TeamId))
                    {
                        List<Coach> teamcoaches = manager.Scouting[tr.TeamId];
                        foreach (Coach c in teamcoaches)
                        {
                            if (c.POSITION == 0)
                                hc = c.GetPositionProgRating(scoutpos);
                            else if (c.POSITION == 1)
                                oc = c.GetPositionProgRating(scoutpos);
                            else if (c.POSITION == 2)
                                dc = c.GetPositionProgRating(scoutpos);
                            else st = c.GetPositionProgRating(scoutpos);
                        }
                    }

                    double scout = hc + oc + dc + st;
                    object[] entry = {tr.TeamId, tr.ShortName, (double)(scout*owner_mult) + ow ,ow,scout, hc,oc,dc,st };
                    ScoutingView.Rows.Add(entry);
                }
            }

            RFAPage.Controls.Add(ScoutingView);

        }

        public void InitEvalView(int pos)
        {
            if (PlayerEvalView != null)
                PlayerEvalView.Dispose();
            PlayerEvalView = new DataGridView();
            PlayerEvalView.BackgroundColor = Color.Silver;
            PlayerEvalView.Bounds = new Rectangle(new Point(5, 25), new Size(700, 250));
            PlayerEvalView.MultiSelect = false;
            PlayerEvalView.AutoGenerateColumns = false;
            PlayerEvalView.AllowUserToAddRows = false;
            PlayerEvalView.RowHeadersVisible = false;
            PlayerEvalView.ColumnCount = 9;

            PlayerEvalView.Columns[0].Name = "Team";
            PlayerEvalView.Columns[0].Width = 60;
            PlayerEvalView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            PlayerEvalView.Columns[1].Name = "Player";
            PlayerEvalView.Columns[1].Width = 120;
            PlayerEvalView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            PlayerEvalView.Columns[2].Name = "Pos";
            PlayerEvalView.Columns[2].Width = 40;
            PlayerEvalView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerEvalView.Columns[3].Name = "Eval";
            PlayerEvalView.Columns[3].Width = 40;
            PlayerEvalView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        #endregion


        #region Controls

        #region Owners Add/Create/Import
        private void CreateOwnerButton_Click(object sender, EventArgs e)
        {
            Coach own = new Coach();
            own.NAME = OwnerNameTextBox.Text;

            if (OwnerTeamComboBox.SelectedItem == null)
                own.TEAM_ID = 1009;
            else
            {
                TeamRecord rec = (TeamRecord)OwnerTeamComboBox.SelectedItem;
                own.TEAM_ID = rec.TeamId;
            }
            own.EGO = (int)OwnerEgo.Value;
            own.PATIENCE = (int)OwnerPatience.Value;
            own.KNOWLEDGE = (int)OwnerKnowledge.Value;
            own.SPENDING = (int)OwnerSpending.Value;
            own.LOYALTY = (int)OwnerLoyalty.Value;
            own.RISK = (int)OwnerRisk.Value;
            own.ETHICS = (int)OwnerEthics.Value;
            own.CPU_CONTROLLED = OwnerCPUControl.Checked;
            own.CPU_SIGN_COACHES = OwnerCoaches.Checked;
            own.CPU_DRAFT_PLAYER = OwnerDraft.Checked;
            own.CPU_SIGN_DRAFT_PICKS = OwnerSignPicks.Checked;
            own.CPU_SIGN_FREE_AGENTS = OwnerSignFA.Checked;
            own.CPU_RESIGN_PLAYERS = OwnerResign.Checked;

            manager.Owners.Add(own);

            isInitializing = true;
            currentowner = manager.Owners.Count - 1;
            InitOwnerView();
            LoadOwner(currentowner);
            isInitializing = false;
        }

        private void ExportOwnersButton_Click(object sender, EventArgs e)
        {
            if (manager.Owners.Count < 1)
                return;

            string ownersfile = Application.StartupPath + @"\Owners.AMP";
            BinaryWriter binwriter = new BinaryWriter(File.Open(ownersfile, FileMode.Create));
            binwriter.Write((UInt16)manager.Owners.Count);
            foreach (Coach own in manager.Owners)
                own.Write(binwriter);
            binwriter.Close();
        }

        private void ImportOwnersButton_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Title = "Load Madden DAT";
            dialog.InitialDirectory = @"%USERPROFILE%\My Documents\";
            dialog.Filter = "Madden Amp Owners File (*.AMP)|*.AMP";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
                filename = dialog.FileName;            
            
            if (!File.Exists(filename))
            {
                MessageBox.Show("Cannot Find Owners File.", "File Does Not Exist", MessageBoxButtons.OK);
                return;
            }

            BinaryReader binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            try
            {
                int count = binreader.ReadUInt16();
                for (int c = 0; c < count; c++)
                {
                    Coach own = new Coach();
                    own.Read(binreader);                    
                    if (!manager.CheckOwners(own))
                        manager.Owners.Add(own);
                }
            }
            catch (EndOfStreamException err)
            {
                MessageBox.Show(err.GetType().Name, "Corrupted Owners File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                binreader.Close();
            }

            isInitializing = true;
            InitOwnerView();
            if (manager.Owners.Count > 0)
                currentowner = 0;
            else currentowner = -1;
            LoadOwner(currentowner);
            isInitializing = false;
        }
        
        #endregion

        #region Owner Info / Ratings

        private void OwnerTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                TeamRecord tr = (TeamRecord)OwnerTeamComboBox.SelectedItem;
                manager.Owners[currentowner].TEAM_ID = tr.TeamId;

                isInitializing = true;
                InitOwnerView();
                isInitializing = false;
            }
        }

        private void OwnerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.Owners[currentowner].NAME = OwnerNameTextBox.Text;
                isInitializing = true;
                InitOwnerView();
                isInitializing = false;
            }
        }

        private void OwnerEthics_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].ETHICS = (int)OwnerEthics.Value;
        }

        private void OwnerKnowledge_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].KNOWLEDGE = (int)OwnerKnowledge.Value;
        }

        private void OwnerEgo_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].EGO = (int)OwnerEgo.Value;
        }

        private void OwnerSpending_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].SPENDING = (int)OwnerSpending.Value;
        }

        private void OwnerLoyalty_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].LOYALTY = (int)OwnerLoyalty.Value;
        }

        private void OwnerRisk_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].RISK = (int)OwnerRisk.Value;
        }

        private void OwnerPatience_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].PATIENCE = (int)OwnerPatience.Value;
        }
        private void OwnerCPUControl_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_CONTROLLED = OwnerCPUControl.Checked;
        }
        private void OwnerGM_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].HC_GM = OwnerGM.Checked;
        }

        private void OwnerCoaches_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_SIGN_COACHES = OwnerCoaches.Checked;
        }

        private void OwnerDraft_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_DRAFT_PLAYER = OwnerDraft.Checked;
        }

        private void OwnerSignPicks_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_SIGN_DRAFT_PICKS = OwnerSignPicks.Checked;
        }

        private void OwnerSignFA_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_SIGN_FREE_AGENTS = OwnerSignFA.Checked;
        }

        private void OwnerResign_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                manager.Owners[currentowner].CPU_RESIGN_PLAYERS = OwnerResign.Checked;
        }
        
        #endregion

        
        
        
        private void ScoutPOSComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                isInitializing = true;
                scoutpos = ScoutPOSComboBox.SelectedIndex;
                InitScoutingView();
                isInitializing = false;
            }
        }

        private void CurrentCoachesTeam_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentteam = manager.Teams[CurrentCoachesTeam_Combo.SelectedIndex];
                currentcoachesteam = CurrentCoachesTeam_Combo.SelectedIndex;
                isInitializing = true;
                InitCurrentCoachView();
                isInitializing = false;
            }
        }

        private void CoachInfoPosition_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CoachInterview_Button_Click(object sender, EventArgs e)
        {
            manager.EvaluateCoach(currentcoach, currentteam);
        }

        private void InitOffseason_Button_Click(object sender, EventArgs e)
        {

        }

        private void CoachEvalButton_Click(object sender, EventArgs e)
        {

        }

        private void TeamStatsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing && TeamStatsFilter.SelectedIndex >= 0)
                InitTeamStatsView(TeamStatsFilter.SelectedIndex);
        }
        
        #endregion

        private void CoachSignPage_Click(object sender, EventArgs e)
        {

        }

        private void CoachContract_Button_Click(object sender, EventArgs e)
        {
           

        }

        public void InitCoachContracts()
        {

        }

        private void numericUpDown19_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown27_ValueChanged(object sender, EventArgs e)
        {

        }
        

        



    }
}
