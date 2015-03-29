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
	
    
    public partial class UserControlledTeamForm : Form, IEditorForm
	{
		private EditorModel model = null;
        public DataGridView teamview = new DataGridView();
        public List<string> userteams = new List<string>();

		public UserControlledTeamForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		
		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
            InitTeamView();
            checkBox1.Checked = true;
            checkBox2.Checked = true;
		}

		public void CleanUI()
		{
            teamview.Rows.Clear();
		}

		#endregion

        public void InitTeamView()
        {
            if (teamview.Rows.Count == 0)
            {
                teamview = new DataGridView();
                teamview.Bounds = new Rectangle(new Point(10, 100), new Size(848, 760));
            }
            else
            {
                teamview.Rows.Clear();
                userteams.Clear();
            }
            
            teamview.MultiSelect = false;

            teamview.ColumnCount = 8;
            teamview.Columns[0].Name = "Team";
            teamview.Columns[1].Name = "Coach Control";
            teamview.Columns[2].Name = "Draft Player";
            teamview.Columns[3].Name = "Sign Picks";
            teamview.Columns[4].Name = "Sign Free Agents";
            teamview.Columns[5].Name = "Fill Rosters";
            teamview.Columns[6].Name = "Re-Sign Players";
            teamview.Columns[7].Name = "Reorder Depth Charts";

            foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
            {
                //  coach controlled options are in the coach table, overrides anything set in the owner table

                string teamname = team.TeamName;
                string coachcontrol = "CPU";    
                string draftplayer = "CPU";
                string signpicks = "CPU";
                string signfreeagents = "CPU";
                string fillrosters = "CPU";
                string resignplayers = "CPU";
                string reorderdepth = "CPU";
                
                foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
				{
					CoachRecord crec = (CoachRecord)record;
					if (team.TeamId == crec.TeamId && crec.Position == 0) // Position 0 is Head coach
					{
                        if (crec.HumanControlled == true)
                            coachcontrol = "USER";
                        if (crec.DraftPlayer == false)
                            draftplayer = "USER";
                        if (crec.SignDraftPicks == false)
                            signpicks = "USER";
                        if (crec.SignFreeAgents == false)
                            signfreeagents = "USER";
                        if (crec.FillRosters == false)
                            fillrosters = "USER";
                        if (crec.ResignPlayers == false)
                            resignplayers = "USER";
                        if (crec.ManageDepth == false)
                            reorderdepth = "USER";

                        //  If this coach is all user controlled, add it to our list of user teams                        
                        if (crec.HumanControlled && !crec.DraftPlayer && !crec.SignDraftPicks && !crec.SignFreeAgents && !crec.FillRosters && !crec.ResignPlayers && !crec.ManageDepth)
                            userteams.Add(team.TeamName);
					}
				}                

                string[] entry = { teamname, coachcontrol, draftplayer, signpicks, signfreeagents, fillrosters, resignplayers, reorderdepth};
                teamview.Rows.Add(entry);
            }

            teamview.CellClick += teamview_CellClick;
            this.Controls.Add(teamview);            
        }

        private void teamview_CellClick(object sender, DataGridViewCellEventArgs e)
        {  
            if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "USER" && (string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "CPU")
                return;
            else if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "CPU";
            else
                teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "USER";

            foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
            {
                if (e.RowIndex == team.TeamId)
                {
                    if (e.ColumnIndex == 1)  // coach control
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                        {
                            team.UserControlled = true;
                        }
                        else team.UserControlled = false;
                    }
                    else if (e.ColumnIndex == 2)  // draft player
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.DraftPlayers = false;
                        else team.DraftPlayers = true;
                    }
                    else if (e.ColumnIndex == 3)  // sign picks
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.SignDraftPicks = false;
                        else team.SignDraftPicks = true;
                    }
                    else if (e.ColumnIndex == 4)  // sign free agents
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.SignFreeAgents = false;
                        else team.SignFreeAgents = true;
                    }
                    else if (e.ColumnIndex == 5)  // fill rosters
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.FillRosters = false;
                        else team.FillRosters = true;
                    }
                    else if (e.ColumnIndex == 6)  // re-sign players
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.ResignPlayers = false;
                        else team.ResignPlayers = true;
                    }
                    else                         // reorder depth charts
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            team.ReorderDepthCharts = false;
                        else team.ReorderDepthCharts = true;
                    }
                }
            }

            foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                CoachRecord crec = (CoachRecord)record;
                if (e.RowIndex == crec.TeamId && crec.Position == 0) // Position 0 is Head coach
                {
                    if (e.ColumnIndex == 1)  // coach control
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.HumanControlled = true;
                        else crec.HumanControlled = false;
                    }
                    else if (e.ColumnIndex == 2)  // draft player
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.DraftPlayer = false;
                        else crec.DraftPlayer = true;
                    }
                    else if (e.ColumnIndex == 3)  // sign picks
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.SignDraftPicks = false;
                        else crec.SignDraftPicks = true;
                    }
                    else if (e.ColumnIndex == 4)  // sign free agents
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.SignFreeAgents = false;
                        else crec.SignFreeAgents = true;
                    }
                    else if (e.ColumnIndex == 5)  // fill rosters
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.FillRosters = false;
                        else crec.FillRosters = true;
                    }
                    else if (e.ColumnIndex == 6)  // re-sign players
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.ResignPlayers = false;
                        else crec.ResignPlayers = true;
                    }
                    else                         // reorder depth charts
                    {
                        if ((string)teamview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "USER")
                            crec.ManageDepth = false;
                        else crec.ManageDepth = true;
                    }
                }
            }        


        }

        private void RecommendedButton_Click(object sender, EventArgs e)
        {
            foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
            {
                //  Need to check the list of original user controlled teams and leave all options for the as user controlled.

                team.UserControlled = true;
                team.DraftPlayers = false;
                team.SignDraftPicks = true;
                if (userteams.Contains(team.TeamName))
                    team.SignDraftPicks = false;
                team.SignFreeAgents = true;
                if (userteams.Contains(team.TeamName))
                    team.SignFreeAgents = false;
                team.FillRosters = true;
                if (userteams.Contains(team.TeamName))
                    team.FillRosters = false;
                team.ResignPlayers = true;
                if (userteams.Contains(team.TeamName))
                    team.ResignPlayers = false;
                team.ReorderDepthCharts = true;
                if (userteams.Contains(team.TeamName))
                    team.ReorderDepthCharts = false;

                foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    CoachRecord crec = (CoachRecord)record;
                    if (team.TeamId == crec.TeamId && crec.Position == 0) // Position 0 is Head coach
                    {
                        crec.HumanControlled = true;
                        crec.DraftPlayer = false;
                        crec.SignDraftPicks = true;
                        if (userteams.Contains(team.TeamName))
                            crec.SignDraftPicks = false;
                        crec.SignFreeAgents = true;
                        if (userteams.Contains(team.TeamName))
                            crec.SignFreeAgents = false;
                        crec.FillRosters = true;
                        if (userteams.Contains(team.TeamName))
                            crec.FillRosters = false;
                        crec.ResignPlayers = true;
                        if (userteams.Contains(team.TeamName))
                            crec.ResignPlayers = false;
                        crec.ManageDepth = true;
                        if (userteams.Contains(team.TeamName))
                            crec.ManageDepth = false;
                    }
                }
            }

            foreach (string name in userteams)
            {
                foreach (TableRecordModel rec in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
                {
                    try
                    {
                        //  We only want to set a game to human controlled if it is on our original list of user controlled teams.
                        if (((ScheduleRecord)rec).AwayTeam.Name == name || ((ScheduleRecord)rec).HomeTeam.Name == name)
                        {
                            //  Play the game if it is a home game
                            if (((ScheduleRecord)rec).HomeTeam.Name == name)
                                ((ScheduleRecord)rec).HumanControlled = true;
                            //  Play the game if we selected to play division games and both teams are from same division
                            else if (((ScheduleRecord)rec).HomeTeam.DivisionId == ((ScheduleRecord)rec).AwayTeam.DivisionId && checkBox2.Checked == true)
                                ((ScheduleRecord)rec).HumanControlled = true;
                            //  Play the game if we have not selected to sim away games
                            else if (((ScheduleRecord)rec).AwayTeam.Name == name && checkBox1.Checked == false)
                                ((ScheduleRecord)rec).HumanControlled = true;
                            //  Otherwise sim the game
                            else ((ScheduleRecord)rec).HumanControlled = false;
                        }

                        //  home/away team is a match for our desired team(s), so sim this game
                        else ((ScheduleRecord)rec).HumanControlled = false;
                    }
                    catch (NullReferenceException err)
                    {
                        err = err;
                        //A null reference exception happens when its trying to find teams that don't
                        //exist on the schedule, its ok
                        Trace.WriteLine("Team id not found when setting user controlled teams. This is ok");
                    }
                }
            }

            InitTeamView();
        }
    }
        

        
}
