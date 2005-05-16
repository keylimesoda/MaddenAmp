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
using System.Text;

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class TeamEditingModel
	{
		/** Collection of team names hashed by teamid */
		private Dictionary<int, string> teamNameList = null;
		/** View to the TeamRecords indexed by Teamid */
		private Dictionary<int, TeamRecord> teamRecords = null;
		/** View to the OwnedTeam records */
		private List<OwnerRecord> teamOwnerRecords = null;
		/** Reference to our EditorModel */
		private EditorModel model = null;

		public TeamEditingModel(EditorModel model)
		{
			this.model = model;
		}

		public TeamRecord GetTeamRecord(int teamId)
		{
			if (teamRecords == null)
			{
				teamRecords = new Dictionary<int, TeamRecord>();
				foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
				{
					TeamRecord tr = (TeamRecord)record;
					teamRecords.Add(tr.TeamId, tr);
				}
			}

			if (teamRecords.ContainsKey(teamId))
			{
				return teamRecords[teamId];
			}
			
			return null;
			
		}

		public ICollection<string> GetTeamNames()
		{
			if (teamNameList == null)
			{
				teamNameList = new Dictionary<int, string>();
				
				foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
				{
					TeamRecord teamRecord = (TeamRecord)record;
					teamNameList.Add(teamRecord.TeamId, teamRecord.Name);
				}
			}

			return teamNameList.Values;
		}

		public string GetTeamNameFromTeamId(int teamid)
		{
			if (teamNameList.ContainsKey(teamid))
				return teamNameList[teamid];
			else
				return EditorModel.UNKNOWN_TEAM_NAME;
		}

		public ICollection<OwnerRecord> GetTeamRecordsInOwnerTable()
		{
			if (teamOwnerRecords == null)
			{
				teamOwnerRecords = new List<OwnerRecord>();

				foreach (TableRecordModel record in model.TableModels[EditorModel.OWNER_TABLE].GetRecords())
				{
					teamOwnerRecords.Add((OwnerRecord)record);
				}
			}

			return teamOwnerRecords;
		}

		public int GetTeamIdFromTeamName(string teamName)
		{
			if (teamNameList.ContainsValue(teamName))
			{
				//Theres got to be a better way to do this
				int i = 0;
				foreach (string team in teamNameList.Values)
				{
					if (team.Equals(teamName))
					{
						break;
					}
					i++;
				}

				//Now if we iterate through teamName list Key list to 'i' then
				//we have the Team id
				int j = 0;
				int id = 0;
				foreach (int key in teamNameList.Keys)
				{
					if (j == i)
					{
						id = key;
						break;
					}
					j++;
				}

				return id;
			}
			else
			{
				throw new ApplicationException("Error getting TeamID for team name " + teamName);
			}
		}
	}
}
