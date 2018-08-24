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
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	
    
    public partial class UserControlledTeamForm : UserControl, IEditorForm
	{
		private EditorModel model = null;
        public DataGridView teamview = new DataGridView();
        public List<string> userteams = new List<string>();
        private bool changed = false;
        private bool isInitializing = false;
        private bool playall = true;
        public List<List<bool>> PlayGamesList = new List<List<bool>>();

		public UserControlledTeamForm()
		{			
			InitializeComponent();
		}

		
		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
            set { model = value; }
		}

		public void InitialiseUI()
		{
            isInitializing = true;

            InitTeamView();
            if (model.FranchiseStage.CurrentStage < 7)
            {
                PlayALLGames_Checkbox.Checked = false;
                PlayAwayGames_Checkbox.Checked = false;
                PlayHomeGames_Checkbox.Checked = false;
                PlayDIVGames_Checkbox.Checked = false;
                PlayALLGames_Checkbox.Visible = false;
                PlayAwayGames_Checkbox.Visible = false;
                PlayHomeGames_Checkbox.Visible = false;
                PlayDIVGames_Checkbox.Visible = false;
            }
            else
            {
                PlayALLGames_Checkbox.Visible = true;
                PlayAwayGames_Checkbox.Visible = true;
                PlayHomeGames_Checkbox.Visible = true;
                PlayDIVGames_Checkbox.Visible = true;
                PlayALLGames_Checkbox.Checked = playall;
                PlayAwayGames_Checkbox.Checked = false;
                PlayHomeGames_Checkbox.Checked = false;
                PlayDIVGames_Checkbox.Checked = false;
            }

            isInitializing = false;
		}

		public void CleanUI()
		{
            teamview.Rows.Clear();
		}

		#endregion

        public void InitTeamView()
        {
            if (teamview != null)
                teamview.Dispose();

            teamview = new DataGridView();
            teamview.Bounds = new Rectangle(new Point(1, 1), new Size(658, 760));
            teamview.MultiSelect = false;
            teamview.RowHeadersVisible = false;            
            teamview.AutoGenerateColumns = false;
            teamview.ScrollBars = ScrollBars.Vertical;
            teamview.Dock = DockStyle.Fill;
            teamview.AllowUserToAddRows = false;
            teamview.ColumnCount = 10;

            teamview.Columns[0].Name = "Team";
            teamview.Columns[0].Width = 100;
            teamview.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[1].Name = "Owner";
            teamview.Columns[1].Width = 50;
            teamview.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[2].Name = "Coach";
            teamview.Columns[2].Width = 50;
            teamview.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[3].Name = "Draft";
            teamview.Columns[3].Width = 50;
            teamview.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[4].Name = "Sign Picks";
            teamview.Columns[4].Width = 70;
            teamview.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[5].Name = "Sign FA";
            teamview.Columns[5].Width = 60;
            teamview.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[6].Name = "Fill Rosters";
            teamview.Columns[6].Width = 70;
            teamview.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[7].Name = "Re-Sign";
            teamview.Columns[7].Width = 60;
            teamview.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[8].Name = "Reorder";
            teamview.Columns[8].Width = 60;
            teamview.Columns[8].SortMode = DataGridViewColumnSortMode.NotSortable;
            teamview.Columns[9].Name = "Play";
            teamview.Columns[9].Width = 50;
            teamview.Columns[9].SortMode = DataGridViewColumnSortMode.NotSortable;

            InitTeamPlayGames();
            
            foreach (TableRecordModel rec in model.TableModels[EditorModel.OWNER_TABLE].GetRecords())
            {
                OwnerRecord owner = (OwnerRecord)rec;
                //  coach controlled options are in the coach table, overrides anything set in the owner table
                if (owner.TeamId > 31)
                    continue;
                string teamname = owner.TeamName;
                string ownedby = "CPU";
                if (owner.UserControlled)
                    ownedby = "USER";
                string coachcontrol = "CPU";
                string draftplayer = "CPU";
                string signpicks = "CPU";
                string signfreeagents = "CPU";
                string fillrosters = "CPU";
                string resignplayers = "CPU";
                string reorderdepth = "CPU";
                string playgames = "NO";

                foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    CoachRecord crec = (CoachRecord)record;
                    if (owner.TeamId == crec.TeamId && crec.Position == 0) // Position 0 is Head coach
                    {
                        if (crec.UserControlled == true)
                        {
                            coachcontrol = "USER";
                            playgames = "YES";
                        }
                        if (crec.CPUDraftPlayer == false)
                            draftplayer = "USER";
                        if (crec.CPUSignDraftPicks == false)
                            signpicks = "USER";
                        if (crec.CPUSignFreeAgents == false)
                            signfreeagents = "USER";
                        if (crec.CPUFillRosters == false)
                            fillrosters = "USER";
                        if (crec.CPUResignPlayers == false)
                            resignplayers = "USER";
                        if (crec.CPUManageDepth == false)
                            reorderdepth = "USER";

                        if (PlayGamesList[owner.TeamId].Contains(true))
                            playgames = "YES";
                        if (!PlayGamesList[owner.TeamId].Contains(true))
                            playgames = "NO";
                    }
                }

                string[] entry = { teamname, ownedby, coachcontrol, draftplayer, signpicks, signfreeagents, fillrosters, resignplayers, reorderdepth, playgames };
                teamview.Rows.Add(entry);
            }

            teamview.CellClick += teamview_CellClick;            
            panel2.Controls.Add(teamview);
        }

        public void InitTeamPlayGames()
        {
            PlayGamesList.Clear();
            for (int t = 0; t < 32; t++)
                PlayGamesList.Add(new List<bool>());
            
            foreach (TableRecordModel sch in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                ScheduleRecord sr = (ScheduleRecord)sch;
                if (sr.WeekType != 25 && sr.WeekType != 0)  // regular and pre season
                    continue;
                else if (sr.HumanControlled)
                {
                    PlayGamesList[sr.HomeTeam.TeamId].Add(true);
                    PlayGamesList[sr.AwayTeam.TeamId].Add(true);
                }
            } 
        }


        public void ApplyChanges()
        {
            // set owner table for user/cpu controlled teams
            for (int o = 0; o < teamview.RowCount; o++)
            {
                TableRecordModel t = model.TableModels[EditorModel.OWNER_TABLE].GetRecord(o);
                OwnerRecord owner = (OwnerRecord)t;
                if ((string)teamview.Rows[o].Cells[1].Value == "USER")
                    owner.UserControlled = true;
                else owner.UserControlled = false;

                foreach (TableRecordModel trm in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    CoachRecord crec = (CoachRecord)trm;
                    if (owner.TeamId == crec.TeamId && crec.Position == 0)      // Position 0 is Head coach
                    {
                        if ((string)teamview.Rows[o].Cells[2].Value == "USER")
                        {
                            crec.UserControlled = true;
                            crec.CPUControlled = false;                         // not sure what this does, but it needs to be set as user controlled
                        }
                        else
                        {
                            crec.UserControlled = false;
                            crec.CPUControlled = true;                          // again this needs to be set
                        }

                        if ((string)teamview.Rows[o].Cells[3].Value == "CPU")
                        {
                            crec.CPUDraftPlayer = true;
                            owner.DraftPlayers = true;
                        }
                        else
                        {
                            crec.CPUDraftPlayer = false;
                            owner.DraftPlayers = false;
                        }

                        if ((string)teamview.Rows[o].Cells[4].Value == "CPU")
                        {
                            crec.CPUSignDraftPicks = true;
                            owner.SignDraftPicks = true;
                        }
                        else
                        {
                            crec.CPUSignDraftPicks = false;
                            owner.SignDraftPicks = false;
                        }

                        if ((string)teamview.Rows[o].Cells[5].Value == "CPU")
                        {
                            crec.CPUSignFreeAgents = true;
                            owner.SignFreeAgents = true;
                        }
                        else
                        {
                            crec.CPUSignFreeAgents = false;
                            owner.SignFreeAgents = false;
                        }

                        if ((string)teamview.Rows[o].Cells[6].Value == "CPU")
                        {
                            crec.CPUFillRosters = true;
                            owner.FillRosters = true;
                        }
                        else
                        {
                            crec.CPUFillRosters = false;
                            owner.FillRosters = false;
                        }

                        if ((string)teamview.Rows[o].Cells[7].Value == "CPU")
                        {
                            crec.CPUResignPlayers = true;
                            owner.ResignPlayers = true;
                        }
                        else
                        {
                            crec.CPUResignPlayers = false;
                            owner.ResignPlayers = false;
                        }

                        if ((string)teamview.Rows[o].Cells[8].Value == "CPU")
                        {
                            crec.CPUManageDepth = true;
                            owner.ReorderDepthCharts = true;
                        }
                        else
                        {
                            crec.CPUManageDepth = false;
                            owner.ReorderDepthCharts = false;
                        }

                        if ((string)teamview.Rows[o].Cells[9].Value == "YES" && (string)teamview.Rows[o].Cells[1].Value == "USER")
                        {
                            if (model.FranchiseStage.CurrentStage < 7)  // No schedule exists while in training camp
                                return;

                            // Fix Scheduled Games
                            foreach (TableRecordModel sch in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
                            {                                
                                ScheduleRecord sr = (ScheduleRecord)sch;
                                if (sr.WeekType != 25 && sr.WeekType != 0)  // regular and pre season
                                    continue;
                                if (owner.TeamId == sr.AwayTeam.TeamId || owner.TeamId == sr.HomeTeam.TeamId)
                                {
                                    TeamRecord team = model.TeamModel.GetTeamRecord(owner.TeamId);

                                    if (PlayALLGames_Checkbox.Checked)
                                        sr.HumanControlled = true;
                                    else if (PlayAwayGames_Checkbox.Checked && sr.AwayTeam.TeamId == owner.TeamId)
                                        sr.HumanControlled = true;
                                    else if (PlayHomeGames_Checkbox.Checked && sr.HomeTeam.TeamId == owner.TeamId)
                                        sr.HumanControlled = true;
                                    else if (PlayDIVGames_Checkbox.Checked)
                                    {
                                        if (team.TeamId != sr.HomeTeam.TeamId && team.DivisionId == sr.HomeTeam.TeamId)
                                            sr.HumanControlled = true;
                                        else if (team.TeamId != sr.AwayTeam.TeamId && team.DivisionId == sr.AwayTeam.DivisionId)
                                            sr.HumanControlled = true;
                                        else sr.HumanControlled = false;
                                    }
                                    else sr.HumanControlled = false;
                                }
                                else sr.HumanControlled = false;

                            }
                        }

                        break;
                    }
                } 
            }
        }

        private void teamview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // if row = -1 then we want to change all teams to the opposite value
            // if column = 0 or then change all options for that team to opposite value

            if (e.ColumnIndex == 0 && e.RowIndex == -1)
                return;
            else
            {
                changed = true;
                ApplyChanges_Button.BackColor = Color.PaleGreen;
            }
            
            if (e.ColumnIndex == 0)
            {
                for (int c = 1; c < teamview.Columns.Count; c++)
                {
                    if (c==9)
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[c].Value == "NO" && (string)teamview.Rows[e.RowIndex].Cells[1].Value != "CPU")
                            teamview.Rows[e.RowIndex].Cells[c].Value = "YES";
                        else teamview.Rows[e.RowIndex].Cells[c].Value = "NO";
                    }
                    else if ((string)teamview.Rows[e.RowIndex].Cells[c].Value == "USER")
                        teamview.Rows[e.RowIndex].Cells[c].Value = "CPU";
                    else teamview.Rows[e.RowIndex].Cells[c].Value = "USER";
                }                
            }
            else if (e.RowIndex == -1)
            {
                for (int t = 0; t < teamview.RowCount; t++)
                {
                    if (e.ColumnIndex == 9)
                    {
                        if ((string)teamview.Rows[t].Cells[1].Value == "USER")
                        {
                            if ((string)teamview.Rows[t].Cells[e.ColumnIndex].Value == "NO")
                                teamview.Rows[t].Cells[e.ColumnIndex].Value = "YES";
                            else teamview.Rows[t].Cells[e.ColumnIndex].Value = "NO";
                        }
                        else teamview.Rows[t].Cells[e.ColumnIndex].Value = "NO";
                    }
                    else if ((string)teamview.Rows[t].Cells[e.ColumnIndex].Value == "USER")
                        teamview.Rows[t].Cells[e.ColumnIndex].Value = "CPU";
                    else teamview.Rows[t].Cells[e.ColumnIndex].Value = "USER";
                }                
            }
            else
            {
                if (e.ColumnIndex == 9)
                {
                    if ((string)teamview.Rows[e.RowIndex].Cells[1].Value == "USER")
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "NO")
                            teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "YES";
                        else teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "NO";
                    }
                    else teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "NO";
                }

                else if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "USER" && (string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "CPU")
                    return;
                else if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                    teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "CPU";
                else
                    teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "USER";
            }

        }
        
        private void PlayALLGames_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                playall = PlayALLGames_Checkbox.Checked;

                if (playall)
                {
                    PlayALLGames_Checkbox.Checked = true;
                    PlayAwayGames_Checkbox.Checked = true;
                    PlayHomeGames_Checkbox.Checked = true;
                    PlayDIVGames_Checkbox.Checked = true;
                }
            }
        }

        private void ApplyChanges_Button_Click(object sender, EventArgs e)
        {            
            ApplyChanges();  
            ApplyChanges_Button.BackColor = Color.Gainsboro;
        }
    }
        

        
}
